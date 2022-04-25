using AutoMapper;
using Messenger.BLL.Models;
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

        public async Task<UserViewModel> RegisterUser(UserCreateModel model)
        {
            User user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, false);
            var userEntity = await _userManager.FindByNameAsync(model.UserName);
            var userModel = _mapper.Map<UserViewModel>(userEntity);
            userModel.Token = GenerateToken(userEntity);
            return userModel;
        }

        public async Task<UserViewModel> LoginUser(UserLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
                throw new Exception("Login error");
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
                throw new Exception("Failed generate token");
            return generatedToken;
        }
    }
}
