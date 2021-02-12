using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

using Prism;
using Prism.Ioc;

namespace Dorsavi.Xamarin.Forms
{
    using Dorsavi.Xamarin.Forms.Constants;
    using Dorsavi.Xamarin.Forms.ViewModels;
    using Dorsavi.Xamarin.Forms.Views;
    using global::Xamarin.Essentials;
    using XF.Material.Forms.UI.Dialogs;

    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync(NavigationRoutes.DefaultRoute);
            ExternalSubscriptionsManager.AppSubscriptions();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Register ViewModels 
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<DorsaviDefaultPage, DorsaviDefaultPageViewModel>();
            containerRegistry.RegisterForNavigation<DorsaviHomePage, DorsaviHomePageViewModel>();
            containerRegistry.RegisterForNavigation<DorsaviSettingsPage, DorsaviSettingsPageViewModel>();

            //Register Internal Services 
            RegisterInternalServices.RegisterHttpClientServices();
            RegisterInternalServices.RegisterProfileMappingServices(ref containerRegistry);
            RegisterInternalServices.RegisterSqliteEncryption(ref containerRegistry);
        }

        private static class ExternalSubscriptionsManager
        {
            internal static void AppSubscriptions()
            {
                Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            }

            private static async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
            {
                await MaterialDialog.Instance.AlertAsync($"This app requires an internet connection in order to fetch items from the Azure Resource", "Lost Internet Connection");
            }
        }
    }
}