using Autofac;
using FaceSample.Services;
using Rg.Plugins.Popup.Pages;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace FaceSample.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator),
                default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        public static void RegisterDependencies(bool useMockServices)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Namespace.Contains("ViewModels"));

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            if (useMockServices)
            {
                UseMockService = true;
            }
            else
            {
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                       .AssignableTo<IBaseService>()
                       .AsImplementedInterfaces()
                       .SingleInstance();

                UseMockService = false;
            }

            if (_container != null)
                _container.Dispose();

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
                return;

            var viewType = view.GetType();
            var viewName = "";

            if (view is PopupPage)
                viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            else
                viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
                return;
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}