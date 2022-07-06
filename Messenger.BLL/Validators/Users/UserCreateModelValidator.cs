using FluentValidation;
using Messenger.BLL.Users;

namespace Messenger.BLL.Validators.Users
{
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(20);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
