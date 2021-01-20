using System;
using System.Collections.Generic;
using System.Linq;
// using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Expenses.Application.Common.Interfaces.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Expenses.Infrastructure.Persistence
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IMongoEntityBase
    {
        private readonly IMongoContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public IQueryable<TEntity> DbSet { get; set; }

        protected BaseRepository(IMongoContext context)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(typeof(TEntity).Name);
            DbSet = _collection.AsQueryable();
        }

        public virtual void Add(TEntity obj)
        {
            _context.AddCommand(async () => await _collection.InsertOneAsync(obj));
        }

        public virtual void Update(TEntity obj)
        {
            _context.AddCommand(async () =>
            {
                await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj);
            });
        }

        public virtual void Remove(Guid id)
        {
            _context.AddCommand(() => _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await _collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}