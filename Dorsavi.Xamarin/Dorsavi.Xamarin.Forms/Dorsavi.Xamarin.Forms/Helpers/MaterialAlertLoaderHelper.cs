using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Dorsavi.Xamarin.Forms.Helpers
{

    internal class MaterialAlertLoaderHelper
    {
        public static IMaterialModalPage LoaderDialogue;
        public static async Task OpenLoaderWithMessageAsync(string title)
        {
            if (DeviceApiHelpers.isUWP())
                await MainThread.InvokeOnMainThreadAsync(async () => UserDialogs.Instance.ShowLoading(title));
            else
                await MainThread.InvokeOnMainThreadAsync(async () => LoaderDialogue = await MaterialDialog.Instance.LoadingDialogAsync("", title));
        }

        public static async Task DismissLoaderAsync()
        {
            if (DeviceApiHelpers.isUWP())
                await MainThread.InvokeOnMainThreadAsync(async () => UserDialogs.Instance.HideLoading());
            else
                await MainThread.InvokeOnMainThreadAsync(async () => LoaderDialogue.DismissAsync());
        }
    }
}
