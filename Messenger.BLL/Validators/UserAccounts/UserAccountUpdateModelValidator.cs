using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
