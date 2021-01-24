using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Domain.Entities;

namespace Expenses.Infrastructure.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context, "Products")
        {
        }
    }
}