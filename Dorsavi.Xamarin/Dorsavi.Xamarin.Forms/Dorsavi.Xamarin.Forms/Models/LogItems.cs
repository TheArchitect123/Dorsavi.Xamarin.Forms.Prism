using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Dorsavi.Xamarin.Forms.Models
{
    public class LogItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string StackTrace { get; set; }
    }
}
