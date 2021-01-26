using FluentValidation;

namespace Expenses.Application.Products.Queries.FilterProducts
{
    public class FilterProductsQueryValidator : AbstractValidator<FilterProductsQuery>
    {
        public FilterProductsQueryValidator()
        {
            RuleFor(v => v.CreatedBy).MinimumLength(4).WithMessage("The minimun length for Created by is 4.");
        }
    }
}