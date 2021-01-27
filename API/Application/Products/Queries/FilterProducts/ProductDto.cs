using System;
using AutoMapper;
using Expenses.Application.Common.Mappings;
using Expenses.Domain.Entities;

namespace Expenses.Application.Products.Queries.FilterProducts
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(pDto => pDto.Price, opt => opt.MapFrom(p => p.Price.ToString()));
        }
    }
}