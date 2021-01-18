using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Expenses.Application.Common.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Expenses.Application.Products.Queries.FilterProducts
{
    public class FilterProductsQuery : IRequest<ProductsVm>
    {
        public Guid Id { get; set; }
        public float MaxPrice { get; set; }
        public float MinPrice { get; set; }
        public DateTime CreatedAfter { get; set; }
        public DateTime CreatedBefore { get; set; }
    }

    public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, ProductsVm>
    {
        private readonly IMongoContext _context;
        private readonly IMapper _mapper;

        public FilterProductsQueryHandler(IMongoContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductsVm> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Eq(x => x.Id, request.Id)
                & builder.Gte(x => x.Price, request.MinPrice)
                & builder.Lte(x => x.Price, request.MaxPrice)
                & builder.Gte(x => x.Created, request.CreatedAfter)
                & builder.Lte(x => x.Created, request.CreatedBefore);
            var results = await _context.Products.Find(filter).ToListAsync();

            var products = _mapper.Map<List<Product>, List<ProductDto>>(results);

            return new ProductsVm {Products = products};
        }
    }
}