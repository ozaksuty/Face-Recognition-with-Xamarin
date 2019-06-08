using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FaceSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfigurationPopupPage : PopupPage
	{
		public ConfigurationPopupPage ()
		{
			InitializeComponent ();
            BackgroundColor = Color.FromRgba(0, 0, 0, .6);
        }
    }
}