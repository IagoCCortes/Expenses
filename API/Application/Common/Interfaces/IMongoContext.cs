using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Expenses.Application.Common.Interfaces
{
    public interface IMongoContext
    {
         IMongoCollection<Product> Products { get; }
         Task<IClientSessionHandle> StartSectionAsync();
    }
}