using Prism.Navigation;

namespace Dorsavi.Xamarin.Forms.Services
{
    using Dorsavi.Xamarin.Forms.Constants;

    public static class NavigationRoutingService
    {
        public static async void NavigateToHomePage(this INavigationService navigationService)
        {
            await navigationService.NavigateAsync(NavigationRoutes.HomePageRoute);
        }

        public static async void NavigateToSettingsPage(this INavigationService navigationService)
        {
            await navigationService.NavigateAsync(NavigationRoutes.SettingsPageRoute);
        }
    }
}
