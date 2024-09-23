using LibraryAPI.Data;
using LibraryAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(int bookId, Review review)
        {
            var book = await dbContext.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            review.BookId = bookId;
            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        // Helper method for fetching review
        private ActionResult<Review> GetReview(int id)
        {
            var review = dbContext.Reviews.Find(id);
            if (review is null)
            {
                return NotFound();
            }
            return review;
        }

    }
}
