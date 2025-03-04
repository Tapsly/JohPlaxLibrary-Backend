/*
 This file contains code that injects the database settings and the mongo client
 inherits and implements methods from the IOrder Service to edit and manipulate the connected 
 database solution
 */
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

        public async Task<List<Order>> GetOrdersByOrderDateAsync(DateTime date)
        {
            return await _OrdersCollection.Find(b => b.OrderedDate == date).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByReturnDateAsync(DateTime date)
        {
            return await _OrdersCollection.Find(b => b.ReturnDate == date).ToListAsync();   
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _OrdersCollection.Find(b => b.UserId == userId).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByBookIdAsync(string bookId)
        {
            return await _OrdersCollection.Find(b => b.BookId == bookId).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(string id)
        {
            return await _OrdersCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DeleteResult> DeleteOrderByIdAsync(string id)
        {
            return await _OrdersCollection.DeleteOneAsync(b => b.Id == id);
        }

        public async Task<DeleteResult> DeleteOrdersByOrderDateAsync(DateTime date)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.OrderedDate, date);
            return await _OrdersCollection.DeleteManyAsync(filter);
        }

        public async Task<DeleteResult> DeleteOrdersByUserIdAsync(string userId)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.UserId, userId);
            return await _OrdersCollection.DeleteManyAsync(filter);
        }

        public Task<DeleteResult> DeleteOrdersByBookIdAsync(string bookId)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.BookId, bookId);
            return await _OrdersCollection.DeleteManyAsync(filter);
        }

        public async Task<ReplaceOneResult> UpdateOrderByIdAsync(string id, Order order)
        {
            return await _OrdersCollection.ReplaceOneAsync(b => b.Id == id, order);
        }

    }
}
