using JohPlaxLibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using JohPlaxLibraryAPI.Models;

namespace JohPlaxLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BookController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var listOfBooks = await _booksService.GetBooksAsync();
                return listOfBooks.Any() ? Ok(listOfBooks) : NotFound(new {message = $"No employees found"}); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500,new { message =  "An error occured while processing your request" });
            }
        }

        [HttpGet("{id:guid:length(24):required}")]
        public async Task<ActionResult<Book>> GetBookById( string id)
        {
            try
            {
                var existingBook = await _booksService.GetBookByIdAsync(id);

                return existingBook is null ? NotFound($"Book with the id {id} could not be found") : Ok(existingBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString);
                return StatusCode(500, new { message = $"Book with the id {id} could not be found" });
            }
            
        }

        [HttpGet("Genre/{genre:alpha:minlength(4):required}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByGenre(string genre)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByGenreAsync(genre);
                return existingBooks.Any() ? Ok(existingBooks) : NotFound($"Books of {genre} genre could not be found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("Author/{author:alpha:minlength(4):required}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(string author)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByAuthorAsync(author);
                return existingBooks.Any() ? Ok(existingBooks) : NotFound(new { message = $"Books of author {author} could not be found" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("Author/{author:alpha:minlength(3):required}/Genre/{genre:alpha:minlength(3):required}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthorAndGenre(string author, string genre)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByAuthorAndGenreAsync(author, genre);
                return existingBooks.Any() ? Ok(existingBooks) : NotFound(new { message = $"Books of author {author} and {genre} genre could be found" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            try
            {
                // validate the data in the book object first
                if(book is null || 
                    string.IsNullOrEmpty(book.Author) || 
                    string.IsNullOrEmpty(book.Genre) ||
                    string.IsNullOrEmpty(book.BookTitle))
                {
                    return BadRequest(new { message = "Invalid book data object" });
                }

                var createdBook = await _booksService.CreateBookAsync(book);

                return createdBook is null ? throw new Exception("Failed to create book") :
                    CreatedAtAction(nameof(GetBookByIdAsync), new { id = createdBook.Id }, createdBook);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPut("{id:guid:length(24):requried}")]
        public async Task<ActionResult> UpdateBookById(string id,[FromBody] Book updatedBook)
        {
            try
            {
                // validate the updatedBook id
                if(string.IsNullOrEmpty(id) || id != updatedBook.Id || updatedBook is null)
                {
                    return BadRequest(new { message = "Invalid book data" });
                }

                var queryBook = await _booksService.GetBookByIdAsync(id);

                if (queryBook is null)
                {
                    return NotFound($"Book with the id {id} could not be found");
                }

                queryBook.Author = updatedBook.Author;
                queryBook.Genre = updatedBook.Genre;
                queryBook.PublishedDate = updatedBook.PublishedDate;
                queryBook.Price = updatedBook.Price;

                await _booksService.UpdateBookByIdAsync(id, queryBook);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
            
        }

        [HttpDelete("{id:guid:length(24):required}")]
        public async Task<ActionResult> DeleteBookById(string id)
        {
            try
            {
                var existingBook = await _booksService.GetBookByIdAsync(id);

                if (existingBook is null)
                {
                    return NotFound($"Book with the id {id} could not be found");
                }

                await _booksService.DeleteBookByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
           
        }

        [HttpDelete("Genre/{genre:alpha:minlength(4):required}")]
        public async Task<ActionResult> DeleteBookByGenre(string genre)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByGenreAsync(genre);

                if (!existingBooks.Any())
                {
                    return NotFound($"Books of this genre: {genre} could not be found");
                }

                await _booksService.DeleteBooksByGenreAsync(genre);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { messsage = "Internal server error" });
            }
            
        }

        [HttpDelete("Author/{author:alpha:minlength(3):required}")]
        public async Task<ActionResult> DeleteBookByGenre(string author)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByAuthorAsync(author);

                if (!existingBooks.Any())
                {
                    return NotFound($"Books of this author: {author} could not be found");
                }

                await _booksService.DeleteBooksByAuthorAsync(author);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
            
        }

        [HttpDelete("Author/{author:alpha:minlength(3):required}/Genre/{genre:alpha:minlength(4):required}")]
        public async Task<ActionResult> DeleteBookByAuthorAndGenre(string author,string genre)
        {
            try
            {
                var existingBooks = await _booksService.GetBooksByAuthorAndGenreAsync(author, genre);

                if (!existingBooks.Any())
                {
                    return NotFound($"Books of this author: {author} and {genre} genre could not be found");
                }

                await _booksService.DeleteBooksByAuthorAndGenreAsync(author, genre);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, new { message = "Internal server error" });
            }
            
        }
    }
}
