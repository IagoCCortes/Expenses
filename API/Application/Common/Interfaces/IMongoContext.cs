using System;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Domain.Common;
using Expenses.Domain.Entities;
using MongoDB.Driver.Linq;

namespace Expenses.Application.Common.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        IMongoQueryable<Product> Products { get; }
        Task Insert<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        Task Update<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        Task Delete<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        void AddInsertToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        void AddUpdateToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        void AddDeleteToTransaction<TEntity>(IMongoEntity obj) where TEntity : IMongoEntity;
        Task<int> SaveChanges();
    }
}