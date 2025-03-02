using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IOrdersService
    {
        public Task<Order?> CreateOrderAsync(Order Order);
        public Task<List<Order>> GetOrdersAsync();
        public Task<Order?> GetOrderByIdAsync(string id);
        public Task<DeleteResult> DeleteOrderByIdAsync(string id);
        public Task<ReplaceOneResult> UpdateOrderByIdAsync(string id, Order Order);
    }
}
