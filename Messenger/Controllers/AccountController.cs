﻿using Messenger.BLL;
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
using System.Security.Claims;
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
        [HttpPatch]
        public async Task<ActionResult<bool>> ConfirmEmail(string userId, string code)
        {
            var result = await _accountManager.ConfirmEmail(userId, code);
            
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] UserLoginModel model)
        {
            var result = await _accountManager.LoginUser(model);
            
            return result;
        }

        [HttpPatch]
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

        [HttpPost]
        public UserViewModel AddFriend([FromBody] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.AddFriend(userId, friendId);
        }

        [HttpDelete]
        public UserViewModel DeleteFriend([FromBody] string friendId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.DeleteFriend(userId, friendId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllFriends([FromQuery] string userId)
        {
            return _accountManager.GetAllFriends(userId);
        }

        [HttpPost]
        public UserViewModel BlockUser([FromBody] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.BlockUser(userId, blockedUserId);
        }

        [HttpDelete]
        public UserViewModel UnblockUser([FromBody] string blockedUserId)
        {
            var userId = GetUserIdFromHttpContext();
            
            return _accountManager.UnblockUser(userId, blockedUserId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllBlockedUsers([FromQuery] string userId)
        {
            return _accountManager.GetAllBlockedUsers(userId);
        }

        [HttpPut]
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
