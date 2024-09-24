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
    [Range(1, 5)]
    public int Rating { get; set; }
        
    // Comment can be null and has no validation
    public string? Comment { get; set; } 
    }
}
