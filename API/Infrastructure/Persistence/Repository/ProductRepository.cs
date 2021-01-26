using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Application.Products.Queries.FilterProducts;
using Expenses.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Expenses.Infrastructure.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IMongoContext context) : base(context, "Products")
        {
            _collection = context.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> FilterProducts(FilterProductsQuery query)
        {
            var builder = Builders<Product>.Filter;
            FilterDefinition<Product> filter = builder.Empty;
            if (query.Id != null) filter &= builder.Eq(x => x.Id, query.Id);
            if (query.MinPrice != null) filter &= builder.Gte(x => x.Price, query.MinPrice);
            if (query.MaxPrice != null) filter &= builder.Lte(x => x.Price, query.MaxPrice);
            if (query.CreatedAfter != null) filter &= builder.Gte(x => x.Created, query.CreatedAfter);
            if (query.CreatedBefore != null) filter &= builder.Lte(x => x.Created, query.CreatedBefore);
            if (query.CreatedBy != null) filter &= builder.Regex("CreatedBy", new BsonRegularExpression(query.CreatedBy, "i"));
            var results = await _collection.Find(filter).ToListAsync();

            return results;
        }
    }
}
