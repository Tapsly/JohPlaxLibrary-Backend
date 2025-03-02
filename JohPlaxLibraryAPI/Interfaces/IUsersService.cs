using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IUsersService
    {
        Task<User?> CreateUserAsync(User user);
        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<DeleteResult> DeleteUserByIdAsync(string id);
        Task<ReplaceOneResult> UpdateUserByIdAsync(string id, User user);
        
    }
}
