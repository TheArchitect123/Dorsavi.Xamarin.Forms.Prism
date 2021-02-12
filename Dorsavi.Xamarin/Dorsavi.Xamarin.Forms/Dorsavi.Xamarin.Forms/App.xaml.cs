using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Dorsavi.Xamarin.Forms
{
    using Dorsavi.Xamarin.Forms.ViewModels;
    using Dorsavi.Xamarin.Forms.Views;
    using Prism;
    using Prism.Ioc;

    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<DorsaviDefaultPage, DorsaviDefaultPageViewModel>();
            containerRegistry.RegisterForNavigation<DorsaviHomePage, DorsaviHomePageViewModel>();
            containerRegistry.RegisterForNavigation<DorsaviSettingsPage, DorsaviSettingsPageViewModel>();
        }
    }
}