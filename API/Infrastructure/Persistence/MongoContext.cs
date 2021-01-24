using System;
using Expenses.Application.Common.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Expenses.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database;
        private MongoClient _client;
        private readonly List<Func<Task>> _commands;
        private IClientSessionHandle _session;
        private readonly ICurrentUserService _currentUserService;
        public MongoContext(IMongoDatabase database, MongoClient client, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _database = database;
            _client = client;
            _commands = new List<Func<Task>>();
        }

        public void AddCommand(Func<Task> func) => _commands.Add(func);

        public async Task<int> SaveChanges()
        {
            using (_session = await _client.StartSessionAsync())
            {
                _session.StartTransaction();

                var commandTasks = _commands.Select(command => command());

                await Task.WhenAll(commandTasks);

                await _session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}