using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Domain.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Expenses.Infrastructure.Persistence.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context, string collectionName)
        {
            Context = context;

            DbSet = Context.GetCollection<TEntity>(collectionName);
        }

        public virtual void Add(TEntity obj)
        {
            obj.Created = DateTime.Now;
            obj.CreatedBy = Context.CurrentUserService.UserId;
            Context.AddCommand(() => DbSet.InsertOneAsync(obj));
        }

        public virtual void Update(TEntity obj)
        {
            obj.LastModified = DateTime.Now;
            obj.LastModifiedBy = Context.CurrentUserService.UserId;
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj));
        }

        public virtual void Remove(Guid id)
        {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<List<TEntity>> GetByExpression(Expression<Func<TEntity, bool>> expression) => 
            await DbSet.AsQueryable().Where(expression).ToListAsync();

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}