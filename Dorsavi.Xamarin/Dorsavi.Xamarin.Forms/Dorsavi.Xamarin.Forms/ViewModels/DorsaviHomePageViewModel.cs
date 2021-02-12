using Dorsavi.Xamarin.Forms.Constants;
using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Dorsavi.Xamarin.Forms.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dorsavi.Xamarin.Forms.ViewModels
{
    public sealed class DorsaviHomePageViewModel : ViewModelBase, IInitialize
    {
        public DorsaviHomePageViewModel(INavigationService navigation) : base(navigation)
        {
            this.PageTitle = PageNames.HomePageName;
        }



        private async Task FetchItemsFromRemoteServer()
        {

        }

        //Load Items From the Remote Server
        public ICommand RefreshItemsFromRemoteServerCommand => new RelayExtension(, () => true);
        public async void LoadResultsFromRemoteServerViaRefresh()
        {
            await FetchItemsFromRemoteServer();
        }

        // Navigation Logic
        public ICommand IBeginNavigationToSettingsPage => new RelayExtension(NavigateToSettingsPage, () => true);
        private async void NavigateToSettingsPage()
        {
            this.NavigationService.NavigateToSettingsPage();
        }

        #region Lifecycle Management
        public async void Initialize(INavigationParameters parameters)
        {
            //On Initialization, make sure to fetch the results from the remote server
            await FetchItemsFromRemoteServer();
        }
        #endregion
    }
}
