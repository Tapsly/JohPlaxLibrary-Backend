using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UsersService(IOptions<JohPlaxLibraryDatabaseSettings> dbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _UsersCollection = database.GetCollection<User>(dbSettings.Value.UserCollectionName);
        }

        public async Task<User?> CreateUserAsync(User User)
        {
            var existingUser = await _UsersCollection.Find(user => user.Id == User.Id).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                User.Id = ObjectId.GenerateNewId().ToString();
                await _UsersCollection.InsertOneAsync(User);
            }

            return User;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _UsersCollection.Find(user => true).ToListAsync();
        }

        public async Task<List<User>> GetUsersByFirsttNameAsync(string firstName)
        {
            return await _UsersCollection.Find(user => user.FirstName == firstName).ToListAsync();
        };


        public async Task<List<User>> GetUsersByLastNameAsync(string lasttName)
        {
            return await _UsersCollection.Find(user => user.LastName == lastName).ToListAsync();
        };

        public async Task<List<User>> GetUsersByFistNameAndLastName(string firstName, string lastName)
        {
            return await _UserCollection.Find(user => user.FirstName == firstName && user.LastName == lastName).ToListAsync();
        };

        public async Task<List<User>> GetUsersByCityAsync(string city)
        {
            return await _UsersCollection.Find(user => user.City == city).ToListAsync();
        };

        public async Task<List<User>> GetUsersByCountryAsync(string country)
        {
            return await _UsersCollection.Find(user => user.Country == country).ToListAsync();  
        };


        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _UsersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAddressAsync(string emailAddress)
        {
            return await _UsersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> DeleteUserByIdAsync(string id)
        {
            return await _UsersCollection.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<DeleteResult> DeleteUsersByCountry(string country)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Country, country);
            return await _UsersCollection.DeleteManyAsync(filter);
        };

        public Task<DeleteResult> DeleteUsersByCity(string city)
        {
            var filter = Builders<User>.Filter.Eq(user => user.City, city);
            return await _UsersCollection.DeleteManyAsync(filter);
        };

        public Task<DeleteResult> DeleteUsersByFirstName(string firstName)
         {
            var filter = Builders<User>.Filter.Eq(user => user.FirstName, firstName);
            return await _UsersCollection.DeleteManyAsync(filter);
        };
        public Task<DeleteResult> DeleteUsersByLastName(string lastName)
        {
            var filter = Builders<User>.Filter.Eq(user => user.LastName, lastName);
            return await _UsersCollection.DeleteManyAsync(filter);
        };

        public Task<DeleteResult> DeleteUsersByFirstNameAndLastName(string firstName, string lastName)
        {
            var filter = Builders<User>.Filter.Eq(user => user.FirstName, firstName && user.LastName, lastName);
            return await _UsersCollection.DeleteManyAsync(filter);
        };

        public async Task<ReplaceOneResult> UpdateUserByIdAsync(string id, User User)
        {
            return await _UsersCollection.ReplaceOneAsync(b => b.Id == id, User);
        }

    }
}
