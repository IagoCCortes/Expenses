using System;
using System.Threading.Tasks;
using Expenses.Application.Products.Commands.CreateProduct;
using Expenses.Application.Products.Commands.DeleteProduct;
using Expenses.Application.Products.Commands.UpdateProduct;
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
        public async Task<ActionResult<bool>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(UpdateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteProductCommand { Id = id }); 
        }
    }
}