using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Token;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Messenger.BLL.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountManager (UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserViewModel> RegisterUser(UserCreateModel model, HttpContext httpContext, IUrlHelper url)
        {
            User user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                var callbackUrl = url.Action(
                        "ConfirmEmail", 
                        "Account",
                        new { userId = user.Id, code = emailToken },
                        protocol: httpContext.Request.Scheme);
                EmailManager emailService = new EmailManager();
                await emailService.SendEmailAsync(model.Email, "Confirm new account", emailService.RegistrationMessageTemplate(model.UserName, callbackUrl));
            }
                
            var userEntity = await _userManager.FindByNameAsync(model.UserName);
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            userModel.Token = GenerateToken(userEntity);
            return userModel;
        }

        public async Task<bool> ConfirmUserEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded;
        }

        public async Task<UserViewModel> LoginUser(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new Exception("Email is not confirmed");
            
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
                throw new BadRequestException("Login error");
            var userEntity = await _userManager.FindByNameAsync(model.UserName);
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            userModel.Token = GenerateToken(userEntity);
            return userModel;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException();
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangeUserPassword(UserChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                throw new KeyNotFoundException();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            return result.Succeeded;
        }

        private string GenerateToken(User user)
        {
            var generatedToken = _tokenService.BuildToken(user);
            if (generatedToken == null)
                throw new BadRequestException("Failed generate token");
            return generatedToken;
        }
    }
}
