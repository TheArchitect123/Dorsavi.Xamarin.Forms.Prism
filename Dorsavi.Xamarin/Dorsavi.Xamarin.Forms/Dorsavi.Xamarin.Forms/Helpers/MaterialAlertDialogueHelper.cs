using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using XF.Material.Forms.UI.Dialogs;

namespace Dorsavi.Xamarin.Forms.Helpers
{
    internal class MaterialAlertDialogueHelper
    {
        public static async void OpenAlertDialogueWithMessage(string title, string message)
        {
            if (DeviceApiHelpers.isUWP())
                await MainThread.InvokeOnMainThreadAsync(async () => await UserDialogs.Instance.AlertAsync(message, title, "Ok"));
            else
                await MainThread.InvokeOnMainThreadAsync(async () => await MaterialDialog.Instance.AlertAsync(message, title));
        }

        public static async void OpenAlertDialogueWithMessage(string message, IAppInfo appInfo)
        {
            if (DeviceApiHelpers.isUWP())
                await MainThread.InvokeOnMainThreadAsync(async () => await UserDialogs.Instance.AlertAsync(message, okText: "Ok"));
            else
                await MainThread.InvokeOnMainThreadAsync(async () => await MaterialDialog.Instance.AlertAsync(message, appInfo.Name));
        }
    }
}
