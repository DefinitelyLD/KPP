using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.UserAccounts;

namespace Messenger.BLL.Validators.UserAccounts
{
    public class UserAccountViewModelValidator : AbstractValidator<UserAccountViewModel>
    {
        public UserAccountViewModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Chat).NotNull();
            RuleFor(x => x.User).NotNull();
        }
    }
}
