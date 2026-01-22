using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiPractice9.Data;
using WebApiPractice9.Models;

namespace WebApiPractice9
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //static private List<Models.Book> books = new List<Models.Book>
        //{
        //    new Models.Book { Id = 1, Title = "Book One", Author = "Author A", Price = 9.99M },
        //    new Models.Book { Id = 2, Title = "Book Two", Author = "Author B", Price = 14.99M },
        //    new Models.Book { Id = 3, Title = "Book Three", Author = "Author C", Price = 19.99M }
        //};

        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Models.Book>>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);

        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;
            //existingBook.Id = updatedBook.Id;

            await _context.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
