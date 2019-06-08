using Acr.UserDialogs;
using FaceSample.Helpers;
using FaceSample.Models.Request;
using FaceSample.Models.Response;
using FaceSample.Provider;
using FaceSample.ViewModels.Base;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FaceSample.ViewModels
{
    public class FaceDetectPageViewModel : ViewModelBase
    {
        private readonly IServiceManager _serviceManager;

        private List<FaceDetectResponse> Response;

        private IList<string> _selectedAttributes;
        public IList<string> SelectedAttributes
        {
            get
            {
                if (_selectedAttributes == null)
                    _selectedAttributes = new ObservableCollection<string>();
                return _selectedAttributes;
            }
            set
            {
                _selectedAttributes = value;
            }
        }

        private FaceDetectRequest _faceDetectRequest;
        public FaceDetectRequest FaceDetectRequest
        {
            get
            {
                if (_faceDetectRequest == null)
                    _faceDetectRequest = new FaceDetectRequest();
                return _faceDetectRequest;
            }
            set
            {
                _faceDetectRequest = value;
            }
        }
        
        private string _imgSource;
        public string ImgSource
        {
            get
            {
                return _imgSource;
            }
            set
            {
                _imgSource = value;
                RaisePropertyChanged(() => ImgSource);
            }
        }

        public ICommand TakePictureCommand => new Command(async () => await TakePicture());
        public ICommand PickPictureCommand => new Command(async () => await PickPicture());
        public ICommand ConfigurationCommand => new Command(async () => await Configuration());
        public ICommand AttributeDetailCommand => new Command((o) => AttributeDetail(o));

        void AttributeDetail(object o)
        {
            if (o != null && o is string attr)
            {
                string value = "";
                try
                {
                    attr = attr.First().ToString().ToLower() + attr.Substring(1);
                    value = Response[0].faceAttributes.GetType().
                    GetProperty(attr).GetValue(Response[0].faceAttributes, null).ToString();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                }
                DialogService.ShowAlertAsync(value, attr, "Ok");
            }
        }

        async Task Configuration()
        {
            await NavigationService.NavigateToPopupAsync<ConfigurationPopupPageViewModel>();
        }

        private async Task Process(MediaFile photo)
        {
            if (photo != null)
            {
                IsBusy = true;
                FaceDetectUrlHelper faceDetectUrlHelper = new FaceDetectUrlHelper
                {
                    Request = FaceDetectRequest
                };
                Response = await _serviceManager.PostStream<List<FaceDetectResponse>>(
                     photo.GetStream(), faceDetectUrlHelper.GenerateUrl());

                if (Response != null)
                {
                    ImgSource = photo.Path;
                    SelectedAttributes.Clear();
                    foreach (string item in FaceDetectRequest.FaceAttributes)
                    {
                        SelectedAttributes.Add(item);
                    }
                }
                else
                    ShowErrorDialog();
                IsBusy = false;
            }
            else
            {
                DialogService.ConfirmAlert(new ConfirmConfig
                {
                    Title = "Error",
                    CancelText = "Cancel",
                    OkText = "Try Again",
                    Message = "No Photo",
                    OnAction = async (o) =>
                    {
                        if (o)
                            await TakePicture();
                    }
                });
            }
        }
        async Task TakePicture()
        {
            if (FaceDetectRequest == null)
            {
                await NavigationService.NavigateToPopupAsync<ConfigurationPopupPageViewModel>();
                return;
            }

            await MediaService.Init();
            var photo = await MediaService.TakePhotoAsync(new StoreCameraMediaOptions
            {
                AllowCropping = true,
                CompressionQuality = 100,
                DefaultCamera = CameraDevice.Front,
                MaxWidthHeight = 300,
                PhotoSize = PhotoSize.MaxWidthHeight,
                Name = "takephoto.png"
            });

            await Process(photo);
        }

        async Task PickPicture()
        {
            if (FaceDetectRequest == null)
            {
                await NavigationService.NavigateToPopupAsync<ConfigurationPopupPageViewModel>();
                return;
            }

            await MediaService.Init();
            var photo = await MediaService.PickPhotoAsync(new PickMediaOptions
            {
                MaxWidthHeight = 300,
                PhotoSize = PhotoSize.MaxWidthHeight
            });

            await Process(photo);
        }

        void ShowErrorDialog()
        {
            DialogService.ConfirmAlert(new ConfirmConfig
            {
                Title = "Error",
                CancelText = "Cancel",
                OkText = "Try Again",
                Message = "No Photo",
                OnAction = async (o) =>
                {
                    if (o)
                        await TakePicture();
                }
            });
        }

        public FaceDetectPageViewModel(IServiceManager serviceManager)
        {
            SelectedAttributes.Clear();
            FaceDetectRequest = null;
            _serviceManager = serviceManager;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}