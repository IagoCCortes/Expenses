using System;
using System.Threading.Tasks;
using Expenses.Application.Common.Interfaces;
using MongoDB.Driver;

namespace Expenses.Infrastructure.Persistence
{
    public interface IMongoContext : IDisposable
    {
        ICurrentUserService CurrentUserService { get; }
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}