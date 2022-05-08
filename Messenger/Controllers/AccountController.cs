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
        public async Task<ActionResult<UserViewModel>> Register([FromBody] UserCreateModel model)
        {
            var result = await _accountManager.RegisterUser(model);
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] UserLoginModel model)
        {
            var result = await _accountManager.LoginUser(model);
            return result;
        }

        [HttpGet]
        public async Task Logout()
        {
            await _accountManager.LogoutUser();
        }

        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] UserChangePasswordModel model)
        {
            var userId = GetUserIdFromHttpContext();
            return await _accountManager.ChangeUserPassword(model, userId);
        }
        
        [HttpGet]
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return _accountManager.GetAllUsers();
        }
        
        [HttpPost]
        public UserViewModel GetUser([FromBody] string id)
        {
            return _accountManager.GetUser(id);
        }

        [HttpPost]
        public UserViewModel GetUserByUserName([FromBody] string userName)
        {
            return _accountManager.GetUserByUserName(userName);
        }

        [HttpPost]
        public UserViewModel AddFriend([FromBody] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            return _accountManager.AddFriend(userId, friendId);
        }

        [HttpPost]
        public UserViewModel DeleteFriend([FromBody] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            return _accountManager.DeleteFriend(userId, friendId);
        }

        [HttpPost]
        public IEnumerable<UserViewModel> GetAllFriends([FromBody] string userId)
        {
            return _accountManager.GetAllFriends(userId);
        }

        [HttpPost]
        public UserViewModel BlockUser([FromBody] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            return _accountManager.BlockUser(userId, blockedUserId);
        }

        [HttpPost]
        public UserViewModel UnblockUser([FromBody] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            return _accountManager.UnblockUser(userId, blockedUserId);
        }

        [HttpPost]
        public IEnumerable<UserViewModel> GetAllBlockedUsers([FromBody] string userId)
        {
            return _accountManager.GetAllBlockedUsers(userId);
        }

        [HttpPost]
        public UserViewModel UpdateUser([FromBody] UserUpdateModel userModel)
        {
            var userId = GetUserIdFromHttpContext();
            return _accountManager.UpdateUser(userModel, userId);
        }

        private string GetUserIdFromHttpContext()
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();
            return httpContext.Value;
        }
    }
}
