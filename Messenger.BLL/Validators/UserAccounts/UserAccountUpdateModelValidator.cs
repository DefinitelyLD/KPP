using FluentValidation;
using Messenger.BLL.UserAccounts;

namespace Messenger.BLL.Validators.UserAccounts
{
    public class UserAccountUpdateModelValidator : AbstractValidator<UserAccountUpdateModel>
    {
        public UserAccountUpdateModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
