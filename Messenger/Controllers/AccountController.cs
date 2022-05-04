using Messenger.BLL.Managers;
using Messenger.BLL.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
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
        public async Task<bool> ChangePassword(UserChangePasswordModel model)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return await _accountManager.ChangeUserPassword(model, userId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return _accountManager.GetAllUsers();
        }

        [HttpPost]
        public UserViewModel GetUser(string id)
        {
            return _accountManager.GetUser(id);
        }

        [HttpPost]
        public UserViewModel GetUserByUserName(string userName)
        {
            return _accountManager.GetUserByUserName(userName);
        }

        [HttpPost]
        public UserViewModel AddFriend(string friendId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _accountManager.AddFriend(userId, friendId);
        }

        [HttpPost]
        public UserViewModel DeleteFriend(string friendId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _accountManager.DeleteFriend(userId, friendId);
        }

        [HttpPost]
        public UserViewModel BlockUser(string blockedUserId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _accountManager.BlockUser(userId, blockedUserId);
        }

        [HttpPost]
        public UserViewModel UnblockUser(string blockedUserId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _accountManager.UnblockUser(userId, blockedUserId);
        }

        [HttpPost]
        public UserViewModel UpdateUser(UserUpdateModel userModel)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _accountManager.UpdateUser(userModel, userId);
        }
    }
}