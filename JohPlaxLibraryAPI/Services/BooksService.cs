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

        public BooksService(IOptions<JohPlaxLibraryDatabaseSettings> dbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _booksCollection = database.GetCollection<Book>(dbSettings.Value.BookCollectionName);
        }

        public async Task<Book?> CreateBookAsync(Book book)
        {
            var existingBook = await _booksCollection.Find(b => b.Id == book.Id).FirstOrDefaultAsync();
            if (existingBook is null)
            {
                book.Id = ObjectId.GenerateNewId().ToString();
                await _booksCollection.InsertOneAsync(book);
                return book;

            }else
            {
                return existingBook;
            }

        }
        public async Task<List<Book>> GetBooksAsync()
        {
            var filter = Builders<Book>.Filter.Empty;
            return await _booksCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByDateAsync(DateTime date)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.PublishedDate == date);
            return await _booksCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByGenreAsync(string genre)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Genre == genre);
            return await _booksCollection.Find(filter).ToListAsync();
        }


        public async Task<List<Book>> GetBooksByAuthorAsync(string author)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Author == author);
            return await _booksCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Book>> GetBooksByPriceAsync(decimal price)
        {
            var filter = Builder<Book>.Filter.Eq(b => b.Price == price);
            return await _booksCollection.Find(filter).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(string id)
        {
            var filter = Builder<Book>.Filter.Eq(b => b.Id == id);
            return await _booksCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> DeleteBookByIdAsync(string id)
        {
            var filter = Builder<Book>.Filter.Eq(b => b.Id == id);
            return await _booksCollection.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> DeleteBooksByGenreAsync(string genre)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Genre == genre);    
            return await _booksCollection.DeleteManyAsync(filter);
        }

        public async Task<DeleteResult> DeleteBooksByDateAsync(DateTime date)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Date =< DateTime(date));
            return await _booksCollection.DeleteManyAsync(filter);
        }

        public async Task<DeleteResult> DeleteBooksByAuthorAsync(string author)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Author == author);
            return await _booksCollection.DeleteManyAsync(filter);
        }

        public async Task<ReplaceOneResult> UpdateBookByIdAsync(string id, Book book)
        {
            var filter = Builder<Book>.Filter.Eq(b => b.Id == id);
            return await _booksCollection.ReplaceOneAsync(filter, book);
        }

    }
}
