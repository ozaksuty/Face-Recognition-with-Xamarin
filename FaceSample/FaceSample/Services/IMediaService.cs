using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace FaceSample.Services
{
    public interface IMediaService : IBaseService
    {
        bool IsSupported { get; }
        bool IsCameraAvailable { get; }
        bool IsTakePhotoSupported { get; }
        bool IsPickPhotoSupported { get; }
        bool IsTakeVideoSupported { get; }
        bool IsPickVideoSupported { get; }
        Task<bool> Init();
        Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null);
        Task<MediaFile> PickVideoAsync();
        Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options);
        Task<MediaFile> TakeVideoAsync(StoreVideoOptions options);
    }
}