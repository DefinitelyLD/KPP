using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.Models.UserAccounts;

namespace Messenger.BLL.Validators.UserAccounts
{
    public class UserAccountActionModelValidator : AbstractValidator<UserAccountActionModel>
    {
        public UserAccountActionModelValidator() 
        {
            RuleFor(x => x.user).NotNull();
            RuleFor(x => x.admin).NotNull();
        }
    }
}
