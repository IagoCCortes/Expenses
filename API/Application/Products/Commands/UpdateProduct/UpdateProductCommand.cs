using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Application.Common.Exceptions;
using Expenses.Application.Common.Interfaces;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Domain.Entities;
using MediatR;

namespace Expenses.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal? Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _uor;

        public UpdateProductCommandHandler(IProductRepository repository, IUnitOfWork uor)
        {
            _uor = uor;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);

            if (entity == null) throw new NotFoundException(nameof(Product), request.Id);

            if (request.Name != null) entity.UpdateName(request.Name);
            entity.UpdatePrice(request.Currency, request.Price);

            _repository.Update(entity);

            return await _uor.Commit();
        }
    }
}