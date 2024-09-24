using LibraryAPI.Data;
using LibraryAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/books/{bookId}/reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(int bookId, [FromBody] Review review)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            review.BookId = bookId;
            
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Return only the review data with both bookId and review Id
            return CreatedAtAction(nameof(GetReviewById), new { bookId = bookId, id = review.Id }, review);
        }
        
        // DELETE: api/reviews/{id}
        // DELETE: api/books/{bookId}/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int bookId, int id)
        {
            // Find the review by id
            var review = await _context.Reviews.FindAsync(id);
            
            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            if (review.BookId != bookId)
            {
                return NotFound(new { message = "Review does not belong to this book." });
            }

            // Remove the review and save changes
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Helper method
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound(new { message = "Review not found." });
            }

            return Ok(review);
        }

    }
}
