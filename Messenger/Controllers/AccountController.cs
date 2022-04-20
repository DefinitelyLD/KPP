using Messenger.BLL.Managers;
using Messenger.BLL.Models;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> Register(UserRegisterModel model)
        {
            return await _accountManager.RegisterUser(model);
        }

        [HttpPost]
        public async Task<ActionResult<Microsoft.AspNetCore.Identity.SignInResult>> Login(UserLoginModel model)
        {
            return await _accountManager.LoginUser(model);
        }

        [HttpPost]
        public async Task Logout()
        {
            await _accountManager.LogoutUser();
        }

        [HttpPost]
        public async Task<bool> DeleteUser(string id)
        {
            return await _accountManager.DeleteUser(id);
        }

        [HttpPost]
        public async Task<bool> ChangePassword(UserChangePasswordModel model)
        {
            return await _accountManager.ChangeUserPassword(model);
        }
    }
}