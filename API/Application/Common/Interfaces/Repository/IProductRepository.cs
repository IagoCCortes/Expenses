using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Application.Products.Queries.FilterProducts;
using Expenses.Domain.Entities;

namespace Expenses.Application.Common.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> FilterProducts(FilterProductsQuery query);
    }
}