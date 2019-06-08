using FaceSample.ViewModels.Base;
using System.Threading.Tasks;

namespace FaceSample.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigatePopAsync();
        Task NavigateToPopupAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToPopupAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task RemovePopupAsync();
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync(bool includeLastPage = false);
        Task SetMainPageAsync<TViewModel>(bool wrapInNavigationPage = true, object parameter = null) where TViewModel : ViewModelBase;
    }
}