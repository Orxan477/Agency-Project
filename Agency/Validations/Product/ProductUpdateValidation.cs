using Agency.ViewModels.Product;
using FluentValidation;

namespace Agency.Validations.Product
{
    public class ProductUpdateValidation:AbstractValidator<PortfolioUpdateVM>
    {
        public ProductUpdateValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Category).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Client).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Info).NotEmpty().NotNull().MaximumLength(255);
        }
    }
}
