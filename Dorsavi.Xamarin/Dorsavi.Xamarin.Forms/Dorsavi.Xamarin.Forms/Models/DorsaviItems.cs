using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Dorsavi.Xamarin.Forms.Models
{
    internal class DorsaviItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
