using Agency.ViewModels.Account;
using FluentValidation;

namespace Agency.Validations.Account
{
    public class RegisterValidation:AbstractValidator<RegisterVM>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().Equal(x=>x.Password).WithMessage("Not Equal Password");
        }
    }
}
