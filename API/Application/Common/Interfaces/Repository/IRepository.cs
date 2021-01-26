using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Domain.Common;

namespace Expenses.Application.Common.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetByExpression(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity obj);
        void Remove(Guid id);
    }
}