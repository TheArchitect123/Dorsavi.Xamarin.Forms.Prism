using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Dorsavi.Xamarin.Forms.Helpers
{

    internal class MaterialAlertLoaderHelper
    {
        public static async void OpenLoaderWithMessage(string title, string message)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await MaterialDialog.Instance.LoadingDialogAsync(message, title));
        }
    }
}
