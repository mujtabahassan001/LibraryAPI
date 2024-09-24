using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Range(1, 9999)]
        public int PublishedYear { get; set; }

        public string? Genre { get; set; }

        // Navigation property
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
