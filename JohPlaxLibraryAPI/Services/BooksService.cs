using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Services
{
    public class BooksService : IBooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksService(IOptions<JohPlaxLibraryDatabaseSettings> dbSettings,IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _booksCollection = database.GetCollection<Book>(dbSettings.Value.BookCollectionName);
        }

        public async Task<Book?> CreateBookAsync(Book book)
        {
            var existingBook = await _booksCollection.Find(b => b.Id == book.Id).FirstOrDefaultAsync();
            if(existingBook != null)
            {
                book.Id = ObjectId.GenerateNewId().ToString();
                await _booksCollection.InsertOneAsync(book);
            }

            return book;
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            return await _booksCollection.Find(b => true).ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(string id)
        {
            return await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DeleteResult> DeleteBookByIdAsync(string id)
        {
            return await _booksCollection.DeleteOneAsync(b => b.Id == id);
        }
        public async Task<ReplaceOneResult> UpdateBookByIdAsync(string id, Book book) 
        {
            return await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);
        }



    }
}
