using CMS.Users.WebApi.Requests.Users;
using FluentValidation;

namespace CMS.Users.WebApi.Validators.UserController
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Enter Username")
                .MaximumLength(50).WithMessage("Username cannot be over 50 characters in length");
        }
    }
}
