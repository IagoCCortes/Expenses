using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;

namespace Expenses.Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IMongoEntityBase
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}