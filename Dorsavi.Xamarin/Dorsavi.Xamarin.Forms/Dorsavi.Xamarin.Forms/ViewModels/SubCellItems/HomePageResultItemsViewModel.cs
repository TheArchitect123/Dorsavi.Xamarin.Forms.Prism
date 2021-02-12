using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.ViewModels.SubCellItems
{
    public class HomePageResultItemsViewModel : BindableBase
    {
        private string _NameOfPerson;
        public string NameOfPerson
        {
            get => this._NameOfPerson;
            set => this.SetProperty(ref this._NameOfPerson, value);
        }

        private string _GenderName;
        public string GenderName
        {
            get => this._GenderName;
            set => this.SetProperty(ref this._GenderName, value);
        }

        //Collection of Tree Items
    }
}
