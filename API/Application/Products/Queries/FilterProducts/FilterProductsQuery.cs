using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Expenses.Domain.Entities;
using Expenses.Application.Common.Interfaces.Repository;

namespace Expenses.Application.Products.Queries.FilterProducts
{
    public class FilterProductsQuery : IRequest<ProductsVm>
    {
        public Guid? Id { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string CreatedBy { get; set; }
    }

    public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, ProductsVm>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public FilterProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ProductsVm> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.FilterProducts(request);

            var products = _mapper.Map<List<Product>, List<ProductDto>>(results);

            return new ProductsVm { Products = products };
        }
    }
}