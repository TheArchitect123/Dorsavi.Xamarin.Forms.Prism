namespace Dorsavi.Xamarin.Forms.Models
{
    using global::SQLite;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using SQLiteNetExtensions.Attributes;

    internal class DorsaviItems
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DorsaviPetItems> Pets { get; set; } //One to Many Relationship with Pets
    }
}
