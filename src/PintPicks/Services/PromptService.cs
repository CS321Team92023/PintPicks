
namespace PintPicks.Services
{
    /// <summary>
    /// service used to centralize all of the prompt messages of the app and ensure that they are all run on the main thread
    /// </summary>
    public class PromptService
    {
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
            });
        }
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, FlowDirection flowDirection, params string[] buttons)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, flowDirection, buttons);
            });
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            });
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            });
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel, FlowDirection flowDirection)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel, flowDirection);
            });
        }

        public async Task DisplayAlert(string title, string message, string cancel, FlowDirection flowDirection)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel, flowDirection);
            });
        }

        public async Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = "")
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
            });
        }
    }
}
