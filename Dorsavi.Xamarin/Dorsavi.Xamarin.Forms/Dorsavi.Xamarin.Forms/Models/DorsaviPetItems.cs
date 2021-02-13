using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Dorsavi.Xamarin.Forms.Models
{
    public class DorsaviPetItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        //Configure the Foreign Key Relationships (Required for Sqlite Extensions Package to work)
        [ForeignKey(typeof(DorsaviItems))]
        public int DorsaviItemId { get; set; }

        [ManyToOne]
        public DorsaviItems ParentItem { get; set; }
    }
}
