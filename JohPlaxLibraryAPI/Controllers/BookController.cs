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

            return existingBook is null ? NotFound() : Ok(existingBook);
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
                return NotFound();
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
                return NotFound();
            }

            await _booksService.DeleteBookByIdAsync(id);
            return NoContent();
        }
    }
}
