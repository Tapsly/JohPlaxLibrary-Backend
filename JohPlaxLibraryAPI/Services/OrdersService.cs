using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JohPlaxLibraryAPI.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IMongoCollection<Order> _OrdersCollection;

        public OrdersService(IOptions<JohPlaxLibraryDatabaseSettings> dbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _OrdersCollection = database.GetCollection<Order>(dbSettings.Value.OrderCollectionName);
        }

        public async Task<Order?> CreateOrderAsync(Order order)
        {
            var existingOrder = await _OrdersCollection.Find(b => b.Id == order.Id).FirstOrDefaultAsync();
            if (existingOrder != null)
            {
                order.Id = ObjectId.GenerateNewId().ToString();
                await _OrdersCollection.InsertOneAsync(order);
            }

            return order;
        }
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _OrdersCollection.Find(b => true).ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(string id)
        {
            return await _OrdersCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DeleteResult> DeleteOrderByIdAsync(string id)
        {
            return await _OrdersCollection.DeleteOneAsync(b => b.Id == id);
        }
        public async Task<ReplaceOneResult> UpdateOrderByIdAsync(string id, Order order)
        {
            return await _OrdersCollection.ReplaceOneAsync(b => b.Id == id, order);
        }

    }
}
