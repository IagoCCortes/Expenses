using System;
using Expenses.Application.Common.Mappings;
using Expenses.Domain.Entities;

namespace Expenses.Application.Products.Queries.FilterProducts
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}