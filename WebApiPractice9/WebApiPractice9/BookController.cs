using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice9.Models;

namespace WebApiPractice9
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        static private List<Models.Book> books = new List<Models.Book>
        {
            new Models.Book { Id = 1, Title = "Book One", Author = "Author A", Price = 9.99M },
            new Models.Book { Id = 2, Title = "Book Two", Author = "Author B", Price = 14.99M },
            new Models.Book { Id = 3, Title = "Book Three", Author = "Author C", Price = 19.99M }
        };

        [HttpGet]
        public ActionResult<List<Models.Book>> GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<List<Models.Book>> GetBookById( int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);

        }
        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if(newBook == null)
            {
                return BadRequest();
            }

            books.Add(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

    }
}
