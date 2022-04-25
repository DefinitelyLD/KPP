using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.Users;

namespace Messenger.BLL.Validators.Users
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(20);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
