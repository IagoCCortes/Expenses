using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Expenses.Application.Common.Interfaces;
using MediatR;

namespace Expenses.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IMongoContext _context;

        public CreateProductCommandHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = request.Name,
                Price = request.Price,
            };

            await _context.Products.InsertOneAsync(entity);

            return entity.Id;
        }
    }
}   