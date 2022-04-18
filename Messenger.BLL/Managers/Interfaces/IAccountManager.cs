using Messenger.BLL.Models;
using Messenger.BLL.ViewModels.User;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IAccountManager
    {
        public Task<IdentityResult> RegisterUser(RegisterUserViewModel model);
        public Task<SignInResult> LoginUser(LoginUserViewModel model);
        public Task LogoutUser();
        public Task<bool> DeleteUser(string id);
        public Task<bool> ChangeUserPassword(ChangeUserPasswordViewModel model);
    }
}
