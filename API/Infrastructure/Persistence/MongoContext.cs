using System;
using Expenses.Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Expenses.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;
        public MongoContext(IMongoDatabase database, MongoClient client)
        {
            _database = database;
            _client = client;
        }

        public IMongoCollection<Product> Products
        {
            get => _database.GetCollection<Product>("Products", new MongoCollectionSettings { AssignIdOnInsert = true });
        }

        public Task<IClientSessionHandle> StartSectionAsync() =>  _client.StartSessionAsync();

        public async Task<bool> TransactionAsync(IEnumerable<Func<Task>> operations)
        {
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                foreach (var operation in operations)
                {
                    await operation();
                }
                
                await session.CommitTransactionAsync();
                return true;
            }
            catch (Exception e)
            {
                await session.AbortTransactionAsync();
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}