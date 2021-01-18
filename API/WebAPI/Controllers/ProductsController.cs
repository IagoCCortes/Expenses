using System;
using System.Threading.Tasks;
using Expenses.Application.Products.Commands.CreateProduct;
using Expenses.Application.Products.Queries.FilterProducts;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebAPI.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpPost("filter")]
        public async Task<ActionResult<ProductsVm>> GetProducts(FilterProductsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}