using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintPicks.Services
{
    public class MediaPickerService
    {
        private readonly PromptService _promptService;
        private readonly PermissionService _permissionService;
        public MediaPickerService(PromptService promptService,
            PermissionService permissionService)
        {
            _promptService = promptService;
            _permissionService = permissionService;
        }

        public async Task<FileResult> CapturePhotoAsync(string title)
        {
            var readStoragePermission = await _permissionService.CheckAndRequestPermissionAsync<Permissions.StorageRead>(
                "storage",
                "The app needs the read storage permission to read image information from the camera");
            if (readStoragePermission != PermissionStatus.Granted)
            {
                await _promptService.DisplayAlert("Error", "The read storage permission has been denied", "Ok");
                return null;
            }

            var writeStoragePermission = await _permissionService.CheckAndRequestPermissionAsync<Permissions.StorageWrite>(
                "storage",
                "The app needs the write storage permission to write image information from the camera");
            if (writeStoragePermission != PermissionStatus.Granted)
            {
                await _promptService.DisplayAlert("Error", "The write storage permission has been denied", "Ok");
            }

            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    //Media Picker code
                    var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = title,
                    });
                    return photo;
                }
                catch (Exception)
                {
                    await _promptService.DisplayAlert("Error", "There was an error using the camera, please try selecting an image instead", "Exit");
                    return null;
                }
            });
        }

        public async Task<FileResult> PickAsync(string title, string mediaType = "image/*", DevicePlatform? platform = null)
        {

            var readStoragePermission = await _permissionService.CheckAndRequestPermissionAsync<Permissions.StorageRead>(
                "storage",
                "The app needs the read storage permission to read meadia information from storage"
                );

            if (readStoragePermission != PermissionStatus.Granted)
            {
                await _promptService.DisplayAlert("Error", "The read storage permission has been denied", "Ok");
                return null;
            }

            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    var file = await FilePicker.PickAsync(new PickOptions
                    {
                        FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { platform ?? DevicePlatform.Android, new[] { mediaType } },
                        }),
                        PickerTitle = title,
                    });
                    return file;
                }
                catch (Exception)
                {
                    await _promptService.DisplayAlert("Error", "There was an error selecting your media.", "Exit");
                    return null;
                }
            });
        }

    }
}
