/*
 This file contains all the methods allowed for editing and manipulating 
 the Order collection in the connected database solution
 */

using JohPlaxLibraryAPI.Models;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Interfaces
{
    public interface IOrdersService
    {
        public Task<Order?> CreateOrderAsync(Order Order);

        public Task<List<Order>> GetOrdersAsync();

        public Task<List<Order>> GetOrdersByOrderDateAsync(DateTime date);

        public Task<List<Order>> GetOrdersByReturnDateAsync(DateTime date);

        public Task<List<Order>> GetOrdersByUserIdAsync(string userId);

        public Task<List<Order>> GetOrdersByBookIdAsync(string bookId);

        public Task<Order?> GetOrderByIdAsync(string id);

        public Task<Order?> GetOrderByUserIdAsync(string userId);

        public Task<Order?> GetOrderByBookIdAsync(string bookId);

        public Task<DeleteResult> DeleteOrderByIdAsync(string id);

        public Task<DeleteResult> DeleteOrdersByOrderDateAsync(DateTime date);

        public Task<DeleteResult> DeleteOrdersByUserIdAsync(string userId);

        public Task<DeleteResult> DeleteOrdersByBookIdAsync(string bookId);

        public Task<ReplaceOneResult> UpdateOrderByIdAsync(string id, Order Order);
    }
}
