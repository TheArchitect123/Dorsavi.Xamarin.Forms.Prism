using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

using Prism;
using Prism.Ioc;

namespace Dorsavi.Xamarin.Forms
{
    using Dorsavi.Xamarin.Forms.ViewModels;
    using Dorsavi.Xamarin.Forms.Views;

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