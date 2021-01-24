using System;
using Expenses.Application.Common.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Linq;
using Expenses.Domain.Entities;
using Expenses.Domain.Common;

namespace Expenses.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database;
        private MongoClient _client;
        private readonly List<Func<Task>> _commands;
        private IClientSessionHandle _session;
        public IMongoQueryable<Product> Products { get; }
        private readonly ICurrentUserService _currentUserService;
        public MongoContext(IMongoDatabase database, MongoClient client, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _database = database;
            _client = client;
            _commands = new List<Func<Task>>();

            Products = _database.GetCollection<Product>("Products").AsQueryable();
        }

        public async Task Insert<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            if (obj.GetType().GetInterfaces().Contains(typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)obj).Created = DateTime.Now;
                ((IAuditableEntity)obj).CreatedBy = _currentUserService.UserId;
            }
            await _database.GetCollection<TEntity>(obj.TableName).InsertOneAsync((TEntity)obj);
        }

        public async Task Update<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            if (obj.GetType().GetInterfaces().Contains(typeof(IAuditableEntity)))
            {
                ((IAuditableEntity)obj).LastModified = DateTime.Now;
                ((IAuditableEntity)obj).LastModifiedBy = _currentUserService.UserId;
            }
            await _database.GetCollection<IMongoEntity>(obj.TableName)
                    .ReplaceOneAsync(Builders<IMongoEntity>.Filter.Eq("_id", obj.Id), obj);
        }

        public async Task Delete<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            await _database.GetCollection<IMongoEntity>(obj.TableName)
                    .DeleteOneAsync(Builders<IMongoEntity>.Filter.Eq("_id", obj.Id));
        }

        public void AddInsertToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            _commands.Add(async () => await _database.GetCollection<TEntity>(obj.TableName).InsertOneAsync((TEntity)obj));
        }

        public void AddUpdateToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            _commands.Add(
                async () => await _database.GetCollection<IMongoEntity>(obj.TableName)
                    .ReplaceOneAsync(Builders<IMongoEntity>.Filter.Eq("_id", obj.Id), obj)
            );
        }

        public void AddDeleteToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity
        {
            _commands.Add(
                async () => await _database.GetCollection<IMongoEntity>(obj.TableName)
                    .DeleteOneAsync(Builders<IMongoEntity>.Filter.Eq("_id", obj.Id))
            );
        }

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

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}