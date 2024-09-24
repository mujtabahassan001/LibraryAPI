using LibraryAPI.Data;
using LibraryAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //Get all Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await dbContext.Books.Include(b => b.Reviews).ToListAsync();
        }

        //Get Book by ID
        [HttpGet]
        [Route("{id}")]
            public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await dbContext.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
            {
                return NotFound();
            }
            return book;
        }

        //Add a new Book
        [HttpPost]
        public IActionResult AddBook(AddBookDto addBookDto)
        {
            var BookEntity = new Book()
            {
                Title = addBookDto.Title,
                Author = addBookDto.Author,
                PublishedYear = addBookDto.PublishedYear,
                Genre = addBookDto.Genre,
            };

            dbContext.Books.Add(BookEntity);
            dbContext.SaveChanges();

            return Ok(BookEntity);
        }

        [HttpPut]
        [Route ("{id:int}")]

        public IActionResult UpdateBookById(int id, UpdateBookDto updateBookDto)
        {
            var books = dbContext.Books.Find(id);

            if(books is null)
            {
                return NotFound();
            }

            books.Author = updateBookDto.Author;
            books.PublishedYear = updateBookDto.PublishedYear;
            books.Title = updateBookDto.Title;

            dbContext.SaveChanges();
            return Ok(books);
        }

        [HttpDelete]
        [Route("{id:int}")]

        public IActionResult DeleteBook(int id)
        {
            var books = dbContext.Books.Find(id);

            if (books is null)
            {
                return NotFound();
            }

            dbContext.Books.Remove(books);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
