using AutoMapper;
using Dorsavi.Xamarin.Forms.Constants;
using Dorsavi.Xamarin.Forms.Exceptions;
using Dorsavi.Xamarin.Forms.Helpers;
using Dorsavi.Xamarin.Forms.Models;
using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Dorsavi.Xamarin.Forms.RemoteServer.Models;
using Dorsavi.Xamarin.Forms.Services;
using Dorsavi.Xamarin.Forms.Services.HttpClients;
using Dorsavi.Xamarin.Forms.ViewModels.Collections;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLiteNetExtensions.Extensions;
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
    public sealed class DorsaviHomePageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private readonly IMapper mappingServiceImplementation;
        private readonly Database databaseServiceImplementation;
        public DorsaviHomePageViewModel(INavigationService navigation,
            IMapper mappingService, Database databaseService) : base(navigation)
        {
            this.mappingServiceImplementation = mappingService;
            this.databaseServiceImplementation = databaseService;

            this.PageTitle = PageNames.HomePageName;

            //Load Any Collections into Memory
            FetchedItems = new ObservableCollection<DorsaviListItemViewModel>();
        }

        //Collections
        private ObservableCollection<DorsaviListItemViewModel> _FetchedItems;
        public ObservableCollection<DorsaviListItemViewModel> FetchedItems
        {
            get
            {
                return _FetchedItems;
            }
            set => this.SetProperty(ref _FetchedItems, value);
        }

        //Load Items From the Remote Server
        private bool _IsCollectionRefreshing;
        public bool IsCollectionRefreshing
        {
            get
            {
                return _IsCollectionRefreshing;
            }
            set => this.SetProperty(ref _IsCollectionRefreshing, value);
        }

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
        public void OnAppearing()
        {
            FetchItemsFromRemoteServer();
        }

        public void OnDisappearing()
        {

        }
        #endregion

        private void fetchItemsFromLocalStorage(List<DorsaviItems> dorsaviItemsDto)
        {
            //Retrieve the Results from Local Storage
            var fetchedResultsFromStorage = this.databaseServiceImplementation.GetEntireCollectionOfData(dorsaviItemsDto
                .SelectMany(w => w.PetItems).Select(i => i.Id));

            if (fetchedResultsFromStorage != null && fetchedResultsFromStorage.Count != 0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    this.FetchedItems.Add(new DorsaviListItemViewModel()
                    {
                        Name = fetchedResultsFromStorage[0].Key.Name,
                        Gender = fetchedResultsFromStorage[0].Key.Gender
                    });

                    this.RaisePropertyChanged(); //Force Refresh the ViewModel
                });
            }
        }
        private async void FetchItemsFromRemoteServer()
        {
            IsCollectionRefreshing = true;
            await MaterialAlertLoaderHelper.OpenLoaderWithMessageAsync("Fetching Data From Azure");
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    var fetchedResults = await CommonHttpClientConsumer.RetrieveMelbourneResultsFromAzureRemoteResource();
                    if (fetchedResults != null && fetchedResults.Count != 0)
                    {
                        //Begin Processing the results
                        //SQLiteNetExtensions will deal with the relationships between both entities
                        var dorsaviItems = fetchedResults.ConvertAll(w =>
                        {
                            var mappedResult = this.mappingServiceImplementation.Map<DorsaviItemsDto, DorsaviItems>(w);
                            if (w.Pets != null)
                                mappedResult.PetItems = w.Pets.ConvertAll(i => this.mappingServiceImplementation.Map<DorsaviPetItemsDto, DorsaviPetItems>(i));
                            else
                                mappedResult.PetItems = new List<DorsaviPetItems>() { };

                            return mappedResult;
                        }).ToList();

                        this.databaseServiceImplementation.InsertItemsWithChildren(dorsaviItems); //Insert items into the database, with OneToMany Relationship between Users & their Pets

                        //Manage the Fetched Results via Local Storage
                        fetchItemsFromLocalStorage(dorsaviItems);
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
                catch (Exception ex)
                {
                    MaterialAlertDialogueHelper.OpenAlertDialogueWithMessage("Unknown Error has Occurred", ex.Message);
                }
                finally
                {
                    IsCollectionRefreshing = false;
                    await MaterialAlertLoaderHelper.DismissLoaderAsync();
                }
            }, TaskCreationOptions.PreferFairness);
        }
    }
}
