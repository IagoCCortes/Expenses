using System;
using System.Threading.Tasks;

namespace Expenses.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}