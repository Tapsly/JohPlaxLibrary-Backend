using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        public Task<Book?> CreateBookAsync(Book book);
        public Task<List<Book>> GetBooksAsync();
        public Task<Book?> GetBookByIdAsync(string id);
        public Task<DeleteResult> DeleteBookByIdAsync(string id);
        public Task<ReplaceOneResult> UpdateBookByIdAsync(string id, Book book);
    }
}
