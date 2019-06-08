using FaceSample.Services;
using System.Threading.Tasks;

namespace FaceSample.ViewModels.Base
{
    public class ViewModelBase : ExtendedBindableObject
    {
        protected readonly IMediaService MediaService;
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            MediaService = ViewModelLocator.Resolve<IMediaService>();
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;

                if (IsBusy)
                    DialogService.ShowLoading();
                else
                    DialogService.HideLoading();

                RaisePropertyChanged(() => IsBusy);
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}