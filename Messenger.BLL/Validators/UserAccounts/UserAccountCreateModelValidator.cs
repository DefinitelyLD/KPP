using FluentValidation;
using Messenger.BLL.UserAccounts;

namespace Messenger.BLL.Validators.UserAccounts
{
    public class UserAccountCreateModelValidator : AbstractValidator<UserAccountCreateModel>
    {
        public UserAccountCreateModelValidator() 
        {
            RuleFor(x => x.ChatId).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
