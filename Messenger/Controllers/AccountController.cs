using Messenger.BLL.Managers;
using Messenger.BLL.Models;
using Messenger.BLL.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Register(UserCreateModel model)
        {
            var result = await _accountManager.RegisterUser(model);
            HttpContext.Session.SetString("Token", result.Token);
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login(UserLoginModel model)
        {
            var result = await _accountManager.LoginUser(model);
            HttpContext.Session.SetString("Token", result.Token);
            return result;
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