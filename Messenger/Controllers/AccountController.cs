using Messenger.BLL;
using Messenger.BLL.Managers;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.BLL.Managers.Interfaces;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmailManager _emailManager;
        private readonly UserManager<User> _userManager;
        
        public AccountController(IAccountManager accountManager, IEmailManager emailManager, UserManager<User> userManager)
        {
            _accountManager = accountManager;
            _userManager = userManager;
            _emailManager = emailManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Register(UserCreateModel model)
        {
            var result = await _accountManager.RegisterUser(model);
            
            var user = await _userManager.FindByNameAsync(model.UserName);

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = emailToken },
                HttpContext.Request.Scheme);
            await _emailManager.SendEmailAsync(model.Email, "Confirm new account", _emailManager.RegistrationMessageTemplate(model.UserName, callbackUrl));

            HttpContext.Session.SetString("Token", result.Token);
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<bool>> ConfirmEmail(string userId, string code)
        {
            var result = await _accountManager.ConfirmEmail(userId, code);
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