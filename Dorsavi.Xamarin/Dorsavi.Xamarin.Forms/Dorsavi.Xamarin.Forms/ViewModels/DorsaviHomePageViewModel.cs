using AutoMapper;
using Dorsavi.Xamarin.Forms.Constants;
using Dorsavi.Xamarin.Forms.Exceptions;
using Dorsavi.Xamarin.Forms.Helpers;
using Dorsavi.Xamarin.Forms.Models;
using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Dorsavi.Xamarin.Forms.RemoteServer.Models;
using Dorsavi.Xamarin.Forms.Services;
using Dorsavi.Xamarin.Forms.Services.HttpClients;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Dorsavi.Xamarin.Forms.ViewModels
{
    public sealed class DorsaviHomePageViewModel : ViewModelBase, IInitialize
    {
        private readonly IMapper mappingServiceImplementation;
        private readonly Database databaseServiceImplementation;
        public DorsaviHomePageViewModel(INavigationService navigation,
            IMapper mappingService, Database databaseService) : base(navigation)
        {
            this.mappingServiceImplementation = mappingService;
            this.databaseServiceImplementation = databaseService;

            this.PageTitle = PageNames.HomePageName;
        }

        //Collections
        private List<DorsaviItemsDto> _FetchedItems;
        public List<DorsaviItemsDto> FetchedItems
        {
            get
            {
                if (this.FetchedItems == null)
                    return (this._FetchedItems = new List<DorsaviItemsDto>());

                return _FetchedItems;
            }
        }

        //Load Items From the Remote Server
        public ICommand RefreshItemsFromRemoteServerCommand => new RelayExtension(LoadResultsFromRemoteServerViaRefresh, () => true);
        public async void LoadResultsFromRemoteServerViaRefresh()
        {
            FetchItemsFromRemoteServer();
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
            FetchItemsFromRemoteServer();
        }
        #endregion

        private async void FetchItemsFromRemoteServer()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    var fetchedResults = await CommonHttpClientConsumer.RetrieveMelbourneResultsFromAzureRemoteResource();
                    if (fetchedResults != null && fetchedResults.Count != 0)
                    {
                        //Clear the Data Cache from the local Storage
                        this.databaseServiceImplementation.ClearAllData();

                        //Begin Processing the results
                        var localMappedItems = fetchedResults.ConvertAll(w => this.mappingServiceImplementation.Map<DorsaviItemsDto, DorsaviItems>(w)).ToList();
                        var localPetItems = fetchedResults.SelectMany(w => w.Pets).ToList().ConvertAll(i => this.mappingServiceImplementation.Map<DorsaviPetItemsDto, DorsaviPetItems>(i)).ToList();

                        //Begin Processing & Storing the items into the Database
                        this.databaseServiceImplementation.InsertItems<DorsaviItems>(localMappedItems);
                        this.databaseServiceImplementation.InsertItems<DorsaviPetItems>(localPetItems);

                        MainThread.InvokeOnMainThreadAsync(async() =>
                        {

                        })
                    }
                }
                catch (FailedFetchViaInternetConnectionException connectivityException)
                {
                    MaterialAlertDialogueHelper.OpenAlertDialogueWithMessage("Lost Internet Connection", "Failed to connect to remote server. It seems that there is no internet connection");
                }
                catch (FailedFetchZeroCountResultException zeroCountException)
                {
                    MaterialAlertDialogueHelper.OpenAlertDialogueWithMessage("No Results Found", "Could not find any results from the remote server");
                }
                catch (FailedToFindResourceException failedToFindResourceException)
                {
                    MaterialAlertDialogueHelper.OpenAlertDialogueWithMessage("Could not Find Resource", "404, Could not find the queried results");
                }
            }, TaskCreationOptions.PreferFairness);
        }
    }
}
