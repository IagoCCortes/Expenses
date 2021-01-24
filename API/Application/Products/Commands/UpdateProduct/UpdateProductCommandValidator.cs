using FluentValidation;

namespace Expenses.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}