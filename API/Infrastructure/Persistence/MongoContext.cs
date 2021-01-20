using System;
using Expenses.Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Conventions;
using System.Linq;
using System.Threading;
using Application.Common.Interfaces;

namespace Expenses.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database;
        private MongoClient _client;
        private readonly List<Func<Task>> _commands;
        private IClientSessionHandle _session;
        public MongoContext(IMongoDatabase database, MongoClient client)
        {            
            _database = database;
            _client = client;
        }

        public async Task<int> SaveChanges()
        {
            using (_session = await _client.StartSessionAsync())
            {
                _session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await _session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}