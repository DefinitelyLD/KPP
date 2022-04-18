using AutoMapper;
using Messenger.BLL.Models;
using Messenger.BLL.ViewModels.User;
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
        private readonly IUsersRepository _usersRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AccountManager (UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUsersRepository usersRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserViewModel model)
        {
            User user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return result;
        }

        public async Task<SignInResult> LoginUser(LoginUserViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            return result;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                throw new Exception("User not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            return result.Succeeded;
        }
    }
}
