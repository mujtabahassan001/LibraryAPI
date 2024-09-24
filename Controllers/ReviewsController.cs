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
     public async Task<ActionResult<Review>> PostReview(int bookId, Review review)
     {
         // Check if the book exists
         var book = await _context.Books.FindAsync(bookId);
         if (book == null)
         {
             return NotFound(new { message = "Book not found" });
         }

         // Associate the review with the book via BookId
         review.BookId = bookId;

         // Add and save the review
         _context.Reviews.Add(review);
         await _context.SaveChangesAsync();

         // Return only the review data, without the book details
         return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, new
         {
             review.Id,
             review.BookId,
             review.ReviewerName,
             review.Rating,
             review.Comment
         });
     }
     // DELETE: api/reviews/{id}
     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteReview(int id)
     {
         var review = await _context.Reviews.FindAsync(id);
         if (review == null)
         {
             return NotFound();
         }

         _context.Reviews.Remove(review);
         await _context.SaveChangesAsync();
         return NoContent();
     }

     // Helper method to get a specific review (used in CreatedAtAction)
     [HttpGet("{id}")]
     public async Task<ActionResult<Review>> GetReviewById(int id)
     {
         var review = await _context.Reviews.FindAsync(id);
         if (review == null)
         {
             return NotFound();
         }

         return Ok(new
         {
             review.Id,
             review.BookId,
             review.ReviewerName,
             review.Rating,
             review.Comment
         });
     }
 }
}
