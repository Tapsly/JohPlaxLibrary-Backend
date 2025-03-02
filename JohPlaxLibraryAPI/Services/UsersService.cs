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
            var existingUser = await _UsersCollection.Find(b => b.Id == User.Id).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                User.Id = ObjectId.GenerateNewId().ToString();
                await _UsersCollection.InsertOneAsync(User);
            }

            return User;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _UsersCollection.Find(b => true).ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _UsersCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DeleteResult> DeleteUserByIdAsync(string id)
        {
            return await _UsersCollection.DeleteOneAsync(b => b.Id == id);
        }
        public async Task<ReplaceOneResult> UpdateUserByIdAsync(string id, User User)
        {
            return await _UsersCollection.ReplaceOneAsync(b => b.Id == id, User);
        }

    }
}
