using Agency.ViewModels.Account;
using FluentValidation;

namespace Agency.Validations.Account
{
    public class LoginValidation:AbstractValidator<LoginVM>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(50);
        }
    }
}
