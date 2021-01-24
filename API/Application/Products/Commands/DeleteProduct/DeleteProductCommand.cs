using System;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Application.Common.Exceptions;
using Expenses.Application.Common.Interfaces;
using Expenses.Application.Common.Interfaces.Repository;
using Expenses.Domain.Entities;
using MediatR;

namespace Expenses.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _uor;

        public DeleteProductCommandHandler(IProductRepository repository, IUnitOfWork uor)
        {
            _uor = uor;
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);

            if (entity == null) throw new NotFoundException(nameof(Product), request.Id);

            _repository.Remove(request.Id);

            return await _uor.Commit();
        }
    }
}