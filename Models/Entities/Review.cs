using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; } // Foreign key to Book

        [Required]
        public string ReviewerName { get; set; } = string.Empty;

        [Required]
        public int Rating { get; set; }

        [Range(1,5)]
        public string? Comment { get; set; }

        // Navigation property
        public Book Book { get; set; }
    }
}
