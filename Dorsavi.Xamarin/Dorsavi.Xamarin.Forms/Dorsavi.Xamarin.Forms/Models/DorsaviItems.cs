using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.Models
{
    internal class DorsaviItems
    {

        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
