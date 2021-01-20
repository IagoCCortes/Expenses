using Application.Common.Interfaces;
using Domain.Entities;
using Expenses.Application.Common.Interfaces;
using Expenses.Application.Common.Interfaces.Repositories;

namespace Expenses.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context)
        {
        }
    }
}