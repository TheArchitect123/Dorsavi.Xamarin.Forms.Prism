using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Dorsavi.Xamarin.Forms.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Dorsavi.Xamarin.Forms.ViewModels
{
    public sealed class DorsaviDefaultPageViewModel : ViewModelBase
    {
        public DorsaviDefaultPageViewModel(INavigationService navigation) : base(navigation) { }

        //Navigation Logic
        public ICommand IBeginNavigationToHomePage => new RelayExtension(NavigateToHomePage, () => true);
        private async void NavigateToHomePage()
        {
            this.NavigationService.NavigateToHomePage();
        }
    }
}
