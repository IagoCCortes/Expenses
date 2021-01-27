using System.Threading;
using System.Threading.Tasks;
using Expenses.Application.Common.Interfaces;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Domain.Entities;
using MediatR;

namespace Expenses.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _uor;

        public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork uor)
        {
            _uor = uor;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product(request.Name, request.Currency, request.Price);

            _repository.Add(entity);

            return await _uor.Commit();
        }
    }
}