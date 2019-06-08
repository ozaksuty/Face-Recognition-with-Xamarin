using FaceSample.Models.Common;
using FaceSample.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FaceSample.ViewModels
{
    public class ConfigurationPopupPageViewModel : ViewModelBase
    {
        private IList<FaceDetectAttributes> _faceDetectAttributes;
        public IList<FaceDetectAttributes> FaceDetectAttributes
        {
            get
            {
                if (_faceDetectAttributes == null)
                    _faceDetectAttributes = new ObservableCollection<FaceDetectAttributes>();
                return _faceDetectAttributes;
            }
            set
            {
                _faceDetectAttributes = value;
            }
        }

        public ICommand SaveCommand => new Command(async () => await Save());

        async Task Save()
        {
            var vm = NavigationService.PreviousPageViewModel as FaceDetectPageViewModel;
            vm.FaceDetectRequest = new Models.Request.FaceDetectRequest();
            foreach (var item in FaceDetectAttributes)
            {
                PropertyInfo prop = vm.FaceDetectRequest.FaceAttributes.GetType().
                    GetProperty(item.Attribute, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(vm.FaceDetectRequest.FaceAttributes, item.IsSelected, null);
            }
            await NavigationService.RemovePopupAsync();
        }

        void BindFaceDetectAttributes()
        {
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Age"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Gender"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "HeadPose"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Smile"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "FacialHair"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Glasses"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Emotion"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Hair"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Makeup"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Occlusion"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Accessories"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Blur"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Exposure"
            });
            FaceDetectAttributes.Add(new FaceDetectAttributes
            {
                Attribute = "Noise"
            });
        }

        public override Task InitializeAsync(object navigationData)
        {
            BindFaceDetectAttributes();
            return base.InitializeAsync(navigationData);
        }
    }
}