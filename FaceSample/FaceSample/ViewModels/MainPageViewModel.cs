using FaceSample.Models.Common;
using FaceSample.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FaceSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IList<Models.Common.Menu> _menu;
        public IList<Models.Common.Menu> Menu
        {
            get
            {
                if (_menu == null)
                    _menu = new ObservableCollection<Models.Common.Menu>();
                return _menu;
            }
            set
            {
                _menu = value;
            }
        }
        public ICommand SelectAPIReferenceCommand => 
            new Command(async (o) => await SelectAPIreference(o));

        void BindMenuItems()
        {
            Menu.Clear();
            Menu.Add(new Models.Common.Menu
            {
               Name = "Detect",
               Description = "Detect human faces in an image, return face rectangles, and optionally with faceIds, landmarks, and attributes."
            });
        }

        public MainPageViewModel()
        {
            BindMenuItems();
        }

        async Task SelectAPIreference(object o)
        {
            if (o is Models.Common.Menu _m && _m != null)
                await NavigationService.NavigateToAsync<FaceDetectPageViewModel>();
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}