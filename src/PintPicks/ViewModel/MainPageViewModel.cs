using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using PintPicks.Api.Client.Abstractions;
using PintPicks.Api.Client.Clients;
using PintPicks.Api.Contract;
using PintPicks.View.Pages;
using PintPicks.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;

namespace PintPicks.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IPintPicksClientFactory _pintPicksClientFactory;
        private readonly PromptService _promptService;
        private readonly PermissionService _permissionService;
        private readonly MediaPickerService _mediaPickerService;
        private readonly IConfiguration _configuration;

        public MainPageViewModel(
            IPintPicksClientFactory pintPicksClientFactory,
            PromptService promptService,
            PermissionService permissionService,
            MediaPickerService mediaPickerService,
            IConfiguration configuration)
        {
            Title = "PintPicks";
            _pintPicksClientFactory = pintPicksClientFactory;
            _promptService = promptService;
            _permissionService = permissionService;
            _mediaPickerService = mediaPickerService;
            _configuration = configuration;
        }

        #region event commands
        /* Handles "Choose Picture" button being clicked
         */
        [RelayCommand]
        private async Task ChoosePicture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            var photo = await _mediaPickerService.PickAsync("Please select an image");
            if (photo == null)
                return;
            await UploadImageForAnalysis(photo);

            IsBusy = false;

        }

        /* Handles "Take Picture" button being clicked
         */
        [RelayCommand]
        private async Task TakePicture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            var photo = await _mediaPickerService.CapturePhotoAsync("Take Picture");
            if (photo == null)
                return;
            await UploadImageForAnalysis(photo);

            IsBusy = false;
        }
        #endregion

        #region private methods

        private async Task UploadImageForAnalysis(FileResult file)
        {
            if (file == null)
                throw new Exception("No valid file found for analysis");

            try
            {
                //get the stream and extension
                var stream = await file.OpenReadAsync();
                var fileExtension = Path.GetExtension(file.FileName);

                //get the pints

                var settings = _configuration.GetRequiredSection("Settings").Get<Settings>();
                var client = await _pintPicksClientFactory.CreateAsync<MenuClient>();
                var pints = await client.GetPintsFromMenuImage(stream, fileExtension, settings.Apikey);

                //navigate
                await NavigateToPintListPage(pints);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pints: {ex.Message}");
                await _promptService.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task NavigateToPintListPage(IEnumerable<Pint> pints)
        {
            ObservableCollection<Pint> observablePints = new(pints);
            var navigationParameter = new Dictionary<string, object>
            {
                { "Pints", observablePints }
            };

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PintListPage), navigationParameter);
            });
        }

        #endregion
    }
}
