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
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksAsync()
            => Ok(await _booksService.GetBooksAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> GetBookByIdAsync( string id)
        {
            var existingBook = await _booksService.GetBookByIdAsync(id);

            return existingBook is null ? NotFound($"Book with the id {id} could not be found") : Ok(existingBook);
        }

        [HtttpGet("Genre/{genre}")]
        public async Task<IActionResult<IEnumerable<Book>>> GetBooksByGenreAsync(string genre)
        {
            try
            {
                return Ok(await _booksService.GetBooksByGenreAsync(genre));
            }
            catch (global::System.Exception e)
            {

                throw e;
            }
        }

        [HtttpGet("Author/{author}")]
        public async Task<IActionResult<IEnumerable<Book>>> GetBooksByGenreAsync(string author)
        {
            try
            {
                return Ok(await _booksService.GetBooksByAuthorAsync());
            }
            catch (global::System.Exception e)
            {

                throw e;
            }
        }

        [HtttpGet("Author/{author}/Genre/{genre}")]
        public async Task<IActionResult<IEnumerable<Book>>> GetBooksByGenreAsync(string author, string genre)
        {
            try
            {
                return Ok(await _booksService.GetBooksByAuthorAndGenreAsync(author, genre));
            }
            catch (global::System.Exception e)
            {

                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBookAsync(Book book)
        {
            var createdBook = await _booksService.CreateBookAsync(book);

            return createdBook is null ? throw new Exception("Failed to create book"):
                CreatedAtAction(nameof(GetBookByIdAsync), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdateBookByIdAsync(string id, Book updatedBook)
        {
            var queryBook = await _booksService.GetBookByIdAsync(id);

            if(queryBook is null)
            {
                return NotFound($"Book with the id {id} could not be found");
            }

            await _booksService.UpdateBookByIdAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteBookByIdAsync(string id)
        {
            var existingBook = await _booksService.GetBookByIdAsync(id);

            if(existingBook is null)
            {
                return NotFound($"Book with the id {id} could not be found");
            }

            await _booksService.DeleteBookByIdAsync(id);
            return NoContent();
        }

        [HttpDelete("Genre/{genre}")]
        public async Task<ActionResult> DeleteBookByGenreAsync(string genre)
        {
            var existingBooks = await _booksService.GetBooksByGenreAsync(genre);

            if (!existingBooks.Any())
            {
                return NotFound($"Books of this genre: {genre} could not be found");
            }

            await _booksService.DeleteBooksByGenreAsync(genre);
            return NoContent();
        }

        [HttpDelete("Author/{author}")]
        public async Task<ActionResult> DeleteBookByGenreAsync(string author)
        {
            var existingBooks = await _booksService.GetBooksByAuthorAsync(author);

            if (!existingBooks.Any())
            {
                return NotFound($"Books of this author: {author} could not be found");
            }

            await _booksService.DeleteBooksByAuthorAsync(author);
            return NoContent();
        }

        [HttpDelete("Author/{author}/Genre/{genre}")]
        public async Task<ActionResult> DeleteBookByAuthorAndGenreAsync(string author,string genre)
        {
            var existingBooks = await _booksService.GetBooksByAuthorAndGenreAsync(author, genre);

            if (!existingBooks.Any())
            {
                return NotFound($"Books of this author: {author} and genre: {genre} could not be found");
            }

            await _booksService.DeleteBooksByAuthorAndGenreAsync(author, genre);
            return NoContent();
        }
    }
}
