using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;

namespace FaceSample.Services
{
    public class MediaService : IMediaService
    {
        private bool IsInitialize { get; set; }

        public bool IsSupported => CrossMedia.IsSupported;

        public bool IsCameraAvailable => CrossMedia.Current.IsCameraAvailable;

        public bool IsTakePhotoSupported => CrossMedia.Current.IsTakePhotoSupported;

        public bool IsPickPhotoSupported => CrossMedia.Current.IsPickPhotoSupported;

        public bool IsTakeVideoSupported => CrossMedia.Current.IsTakeVideoSupported;

        public bool IsPickVideoSupported => CrossMedia.Current.IsPickVideoSupported;

        public async Task<bool> Init()
        {
            IsInitialize = await CrossMedia.Current.Initialize();
            return IsInitialize;
        }

        public async Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null)
        {
            if (IsInitialize && IsSupported && IsPickPhotoSupported)
                return await CrossMedia.Current.PickPhotoAsync(options);

            throw new NotImplementedException("No Permission");
        }

        public async Task<MediaFile> PickVideoAsync()
        {
            if (IsInitialize && IsSupported && IsPickVideoSupported)
                return await CrossMedia.Current.PickVideoAsync();

            throw new NotImplementedException("No Permission");
        }

        public async Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options)
        {
            if (IsInitialize && IsSupported && IsTakePhotoSupported)
                return await CrossMedia.Current.TakePhotoAsync(options);

            throw new NotImplementedException("No Permission");
        }

        public async Task<MediaFile> TakeVideoAsync(StoreVideoOptions options)
        {
            if (IsInitialize && IsSupported && IsTakeVideoSupported)
                return await CrossMedia.Current.TakeVideoAsync(options);

            throw new NotImplementedException("No Permission");
        }
    }
}