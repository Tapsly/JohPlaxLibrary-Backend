using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IUsersService
    {
        public Task<User?> CreateUserAsync(User user);

        public Task<List<User>> GetUsersAsync();

        public Task<List<User>> GetUsersByFirstNameAsync(string firstName);
        
        public Task<List<User>> GetUsersByLastNameAsync(string firstName);

        public Task<List<User>> GetUsersByFistNameAndLastName(string firstName, string lastName);

        public Task<List<User>> GetUsersByCityAsync(string city);

        public Task<List<User>> GetUsersByCountryAsync(string country);

        public Task<User?> GetUserByIdAsync(string id);

        public Task<User?> GetUserByEmailAddressAsync(string emailAddress);

        public Task<DeleteResult> DeleteUserByIdAsync(string id);

        public Task<DelelteResult> DeleteUsersByCountry(string country);

        public Task<DeleteResult> DeleteUsersByCity(string city);

        public Task<DeleteResult> DeleteUsersByFirstName(string firstName);
        
        public Task<DeleteResult> DeleteUsersByLastName(string lastName);

        public Task<DeleteResult> DeleteUsersByFirstNameAndLastName(string firstName , string lastName);

        public Task<ReplaceOneResult> UpdateUserByIdAsync(string id, User user);
        
    }
}
