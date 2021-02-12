using Xamarin.Forms;

namespace Dorsavi.Xamarin.Forms.Helpers
{
    internal static class DeviceApiHelpers
    {
        public static bool isUWP() => Device.RuntimePlatform == Device.UWP;
        public static bool isDroid() => Device.RuntimePlatform == Device.Android;
        public static bool isiOS() => Device.RuntimePlatform == Device.iOS;
    }
}
