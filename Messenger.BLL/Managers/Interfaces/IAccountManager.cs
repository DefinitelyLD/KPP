using Messenger.BLL.Models;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IAccountManager
    {
        public Task<UserViewModel> RegisterUser(UserCreateModel model, HttpContext httpContext, IUrlHelper url);
        public Task<bool> ConfirmUserEmail(string userId, string code);
        public Task<UserViewModel> LoginUser(UserLoginModel model);
        public Task LogoutUser();
        public Task<bool> DeleteUser(string id);
        public Task<bool> ChangeUserPassword(UserChangePasswordModel model);
    }
}
