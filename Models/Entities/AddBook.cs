using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Entities
{
    public class AddBook
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Range(1, 9999)]
        public int PublishedYear { get; set; }

        public string? Genre { get; set; }
    }
}
