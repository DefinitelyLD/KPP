using Messenger.BLL.Managers;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Messenger.BLL.Managers.Interfaces;
using Messenger.WEB.Roles;

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

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Register
        ///     {
        ///        "userName": "MainUser",
        ///        "email": "nazarkinstephan@gmail.com",
        ///        "password": "12345678"
        ///     }
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Register([FromBody] UserCreateModel model)
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

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<bool>> ConfirmEmail(string userId, string code)
        {
            var result = await _accountManager.ConfirmEmail(userId, code);
            
            return result;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///        "userName": "MainUser",
        ///        "password": "12345678"
        ///     }
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] UserLoginModel model)
        {
            var result = await _accountManager.LoginUser(model);
            
            return result;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /ChangePassword
        ///     {
        ///        "id": "d073e154-282f-4ee1-8a08-1e9d6744e101",
        ///        "oldPassword": "12345678",
        ///        "newPassword": "123456789"
        ///     }
        /// </remarks>
        [HttpPut]
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

        [HttpGet]
        public UserViewModel GetUser([FromQuery] string id)
        {
            return _accountManager.GetUser(id);
        }

        [HttpGet]
        public UserViewModel GetUserByUserName([FromQuery] string userName)
        {
            return _accountManager.GetUserByUserName(userName);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddFriend
        ///     {
        ///        "userId": "acd91b54-7696-4e03-bee0-49f0dde9ad0c"
        ///     }
        /// </remarks>
        [HttpPost]
        public UserViewModel AddFriend([FromBody] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.AddFriend(userId, friendId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /DeleteFriend
        ///     {
        ///        "userId": "acd91b54-7696-4e03-bee0-49f0dde9ad0c"
        ///     }
        /// </remarks>
        [HttpDelete]
        public UserViewModel DeleteFriend([FromQuery] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.DeleteFriend(userId, friendId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllFriends()
        {
            var userId = GetUserIdFromHttpContext();

            return _accountManager.GetAllFriends(userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /BlockUser
        ///     {
        ///        "userId": "acd91b54-7696-4e03-bee0-49f0dde9ad0c"
        ///     }
        /// </remarks>
        [HttpPost]
        public UserViewModel BlockUser([FromBody] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.BlockUser(userId, blockedUserId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /UnblockUser
        ///     {
        ///        "userId": "acd91b54-7696-4e03-bee0-49f0dde9ad0c"
        ///     }
        /// </remarks>
        [HttpDelete]
        public UserViewModel UnblockUser([FromQuery] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.UnblockUser(userId, blockedUserId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllBlockedUsers()
        {
            var userId = GetUserIdFromHttpContext();

            return _accountManager.GetAllBlockedUsers(userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /UpdateUser
        ///     {
        ///        "id": "d073e154-282f-4ee1-8a08-1e9d6744e101",
        ///        "userName": "MainUser2",
        ///        "email": "stephannazarkin@gmail.com"
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<UserViewModel>> UpdateUser([FromForm] UserUpdateModel userModel)
        {
            var userId = GetUserIdFromHttpContext();
            
            return await _accountManager.UpdateUser(userModel, userId);
        }

        [HttpGet]
        public UserViewModel GetCurrentUser()
        {
            var userId = GetUserIdFromHttpContext();

            return _accountManager.GetCurrentUser(userId);
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.Admin)]
        public string TestAdmin()
        {
            return "You have role Admin";
        }

        [HttpGet]
        public Task<bool> IsUserSuperAdmin()
        {
            var userId = GetUserIdFromHttpContext();

            return _accountManager.IsUserSuperAdmin(userId);
        }

        private string GetUserIdFromHttpContext()
        {
            var httpContext = User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();
            
            return httpContext.Value;
        }
    }
}
