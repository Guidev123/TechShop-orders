using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Domain.Repositories;

namespace TechShop.Infrasctructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Order>("orders");
        }

        public async Task CreateAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(c => c.Id == order.Id, order);
        }
    }
}
