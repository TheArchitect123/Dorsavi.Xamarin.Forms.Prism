using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Dorsavi.Xamarin.Forms.Models
{
    internal class DorsaviPetItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
