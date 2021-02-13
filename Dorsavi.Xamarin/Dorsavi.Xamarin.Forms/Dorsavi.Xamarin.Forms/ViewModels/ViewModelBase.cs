using Dorsavi.Xamarin.Forms.Prism.Extensions;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;

namespace Dorsavi.Xamarin.Forms.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        protected INavigationService NavigationService { get; private set; }

        private string _PageTitle;
        public string PageTitle
        {
            get { return this._PageTitle; }
            set { this.SetProperty(ref this._PageTitle, value); }
        }

        private string _AuthorName;
        public string AuthorName
        {
            get { return this._AuthorName; }
            set { this.SetProperty(ref this._AuthorName, value); }
        }

        public ICommand IPopPage => new RelayExtension(PopPage, () => true);
        private async void PopPage()
        {
            await this.NavigationService.GoBackAsync();
        }

        public ViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            this.AuthorName = "Dan Gerchcovich";
        }
    }
}
