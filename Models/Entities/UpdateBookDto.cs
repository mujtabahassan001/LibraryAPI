using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Entities
{
    public class UpdateBookDto
    {
        
        public string Title { get; set; } = string.Empty;

        
        public string Author { get; set; } = string.Empty;

        [Range(1, 9999)]
        public int PublishedYear { get; set; }
    }
}
