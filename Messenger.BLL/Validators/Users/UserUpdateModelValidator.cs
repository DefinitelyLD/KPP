using FluentValidation;
using Messenger.BLL.Users;
using Messenger.BLL.Validators.Files;

namespace Messenger.BLL.Validators.Users
{
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.File).SetValidator(new ImageValidator());
        }
    }
}
