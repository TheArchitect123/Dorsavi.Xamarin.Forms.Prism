using Dorsavi.Xamarin.Forms.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.ViewModels.Collections
{
    public class DorsaviListItemViewModel : BindableBase
    {
        private string _Name;
        public string Name
        {
            get { return this._Name; }
            set { this.SetProperty(ref this._Name, value); }
        }

        private string _Gender;
        public string Gender
        {
            get { return this._Gender; }
            set { this.SetProperty(ref this._Gender, value); }
        }

        private string _Age;
        public string Age
        {
            get { return this._Age; }
            set { this.SetProperty(ref this._Age, value); }
        }

        public List<DorsaviPetItems> PetItems { get; set; }
    }
}
