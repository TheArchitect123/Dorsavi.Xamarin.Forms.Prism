using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using XF.Material.Forms.UI.Dialogs;

namespace Dorsavi.Xamarin.Forms.Helpers
{
    internal class MaterialAlertDialogueHelper
    {
        public static async void OpenAlertDialogueWithMessage(string title, string message)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await MaterialDialog.Instance.AlertAsync(message, title));
        }

        public static async void OpenAlertDialogueWithMessage(string message, IAppInfo appInfo)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await MaterialDialog.Instance.AlertAsync(message, appInfo.Name));
        }
    }
}
