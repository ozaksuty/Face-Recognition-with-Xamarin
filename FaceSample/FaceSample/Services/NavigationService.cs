using FaceSample.Pages;
using FaceSample.ViewModels;
using FaceSample.ViewModels.Base;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FaceSample.Services
{
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 1].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public Task InitializeAsync()
        {
            return SetMainPageAsync<MainPageViewModel>(true, null);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToPopupAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToPopupAsync(typeof(TViewModel), null);
        }

        public Task NavigateToPopupAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToPopupAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            if (Xamarin.Forms.Application.Current.MainPage is CustomNavigationPage mainPage)
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            return Task.FromResult(true);
        }

        public Task RemovePopupAsync()
        {
            if (Xamarin.Forms.Application.Current.MainPage is CustomNavigationPage mainPage)
            {
                try
                {
                    mainPage.Navigation.PopPopupAsync();
                }
                catch (Exception)
                {
                }
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync(bool includeLastPage = false)
        {
            if (Xamarin.Forms.Application.Current.MainPage is CustomNavigationPage mainPage)
            {
                var count = includeLastPage ? 0 : 1;
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - count; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            NavigationPage.SetHasNavigationBar(page, false);

            if (Xamarin.Forms.Application.Current.MainPage is CustomNavigationPage navigationPage)
                await navigationPage.PushAsync(page);
            else
                Xamarin.Forms.Application.Current.MainPage = new CustomNavigationPage(page);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private async Task InternalNavigateToPopupAsync(Type viewModelType, object parameter)
        {
            PopupPage page = CreatePopupPage(viewModelType, parameter);

            if (Application.Current.MainPage is CustomNavigationPage navigationPage)
                await navigationPage.Navigation.PushPopupAsync(page);
            else
                Xamarin.Forms.Application.Current.MainPage = new CustomNavigationPage(page);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = $"{viewModelType.FullName.Split('.')[0]}.Pages.{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Type GetPopupPageTypeForViewModel(Type viewModelType)
        {
            var viewName = $"{viewModelType.FullName.Split('.')[0]}.Views.{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Type GetViewTypeForViewModel(Type viewModelType)
        {
            var viewName = $"{viewModelType.FullName.Split('.')[0]}.Drawers.{viewModelType.Name.Replace("ViewModel", string.Empty)}";
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
                throw new Exception($"Cannot locate page type for {viewModelType}");
            return Activator.CreateInstance(pageType) as Page;
        }

        private PopupPage CreatePopupPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPopupPageTypeForViewModel(viewModelType);
            if (pageType == null)
                throw new Exception($"Cannot locate popup page type for {viewModelType}");
            return Activator.CreateInstance(pageType) as PopupPage;
        }

        private ContentView CreateContentView(Type viewModelType)
        {
            Type viewType = GetViewTypeForViewModel(viewModelType);
            if (viewType == null)
                throw new Exception($"Cannot locate popup page type for {viewModelType}");
            return Activator.CreateInstance(viewType) as ContentView;
        }

        public async Task SetMainPageAsync<TViewModel>(bool wrapInNavigationPage, object parameter) where TViewModel : ViewModelBase
        {
            Page page = CreatePage(typeof(TViewModel), parameter);
            if (wrapInNavigationPage)
            {
                try
                {
                    NavigationPage.SetHasNavigationBar(page, false);
                    Application.Current.MainPage = new CustomNavigationPage(page);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Application.Current.MainPage = page;
            }
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        public async Task NavigatePopAsync()
        {
            if (Application.Current.MainPage is CustomNavigationPage navigationPage)
                await navigationPage.PopAsync();
        }
    }
}