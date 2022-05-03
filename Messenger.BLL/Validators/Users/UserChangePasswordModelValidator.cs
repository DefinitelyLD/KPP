using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
