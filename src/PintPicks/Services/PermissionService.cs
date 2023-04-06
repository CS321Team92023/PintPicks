using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PintPicks.Services
{
    public class PermissionService
    {
        private readonly PromptService _promptService;
        public PermissionService(PromptService promptService) 
        {
            _promptService = promptService;
        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<TPermission>(string permissionName, string rationale = null) 
            where TPermission : BasePermission, new()
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                TPermission permission = new TPermission();
                var status = await permission.CheckStatusAsync();
                if (status == PermissionStatus.Granted)
                    return status;


                if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    // Prompt the user to turn on in settings
                    // On iOS once a permission has been denied it may not be requested again from the application
                    await _promptService.DisplayAlert("Permission Denied", $"Please go to the app settings and enable the {permissionName.ToLower()} permission for this app.", "Ok");
                    return status;
                }

                if (permission.ShouldShowRationale())
                {
                    var rationaleMessage = rationale ?? $"The app needs the {permissionName.ToLower()} permission to function.";
                    await _promptService.DisplayAlert($"Permission Needed", rationaleMessage, "Ok");
                }

                status = await permission.RequestAsync();

                return status;
            });
        }

    }
}
