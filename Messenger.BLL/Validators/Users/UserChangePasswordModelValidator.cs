using FluentValidation;
using Messenger.BLL.Users;

namespace Messenger.BLL.Validators.Users
{
    public class UserChangePasswordModelValidator : AbstractValidator<UserChangePasswordModel>
    {
        public UserChangePasswordModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(20);
        }
    }
}
