using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        public Task<Book?> CreateBookAsync(Book book);

        public Task<List<Book>> GetBooksAsync();

        public Task<List<Book>> GetBooksByDateAsync(DateTime date);

        public Task<List<Book>> GetBooksByGenreAsync(string genre);

        public Task<List<Book>> GetBooksByAuthorAsync(string author);

        public Task<List<Book>> GetBooksByPriceAsync(decimal price);

        public Task<Book?> GetBookByIdAsync(string id);

        public Task<ReplaceOneResult> UpdateBookByIdAsync(string id, Book book);

        public Task<DeleteResult> DeleteBookByIdAsync(string id);

        public Task<DeleteResult> DeleteBooksByGenreAsync(string genre);

        public Task<DeleteResult> DeleteBooksByAuthorAsync(string author);

        public Task<DeleteResult> DeleteBooksByDateAsync(DateTime date);


    }
}
