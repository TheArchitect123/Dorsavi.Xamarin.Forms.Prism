using Dorsavi.Xamarin.Forms.Constants;
using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Dorsavi.Xamarin.Forms.RemoteServer.Resources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Dorsavi.Xamarin.Forms.ViewModels
{
    public sealed class DorsaviSettingsPageViewModel : ViewModelBase
    {
        public DorsaviSettingsPageViewModel(INavigationService navigation) : base(navigation)
        {
            this.PageTitle = PageNames.SettingsPageName;
        }

        public ICommand IOpenAboutUs => new RelayExtension(OpenAboutUs, () => true);
        private async void OpenAboutUs()
        {
            await Browser.OpenAsync(CommonDorsaviResources.DorsaviAboutUsDirectory, BrowserLaunchMode.SystemPreferred);
        }
    }
}
