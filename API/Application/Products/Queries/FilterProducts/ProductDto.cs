using System;
using Domain.Entities;
using Expenses.Application.Common.Mappings;

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