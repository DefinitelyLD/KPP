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
using Microsoft.AspNetCore.Routing;

namespace Messenger.BLL.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUsersRepository _usersRepository;
        public AccountManager (UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService, IUsersRepository usersRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _usersRepository = usersRepository;
        }

        public async Task<UserViewModel> RegisterUser(UserCreateModel model)
        {
            User user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString());

            var userEntity = _usersRepository.GetAll().Where(x => x.UserName == model.UserName).SingleOrDefault();
            var userModel = _mapper.Map<UserViewModel>(userEntity);

            return userModel;
        }

        public async Task<bool> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded;
        }

        public async Task<UserViewModel> LoginUser(UserLoginModel model)
        {
            var userEntity = _usersRepository.GetAll().Where(x => x.UserName == model.UserName).SingleOrDefault();
            
            if (!await _userManager.IsEmailConfirmedAsync(userEntity))
                throw new BadRequestException("Email is not confirmed");
            if (userEntity == null)
                throw new BadRequestException("Login error");
            if (!await _userManager.CheckPasswordAsync(userEntity, model.Password))
                throw new BadRequestException("Incorrect Password");

            var userModel = _mapper.Map<UserViewModel>(userEntity);
            userModel.Token = GenerateToken(userEntity);

            return userModel;
        }

        public async Task<bool> ChangeUserPassword(UserChangePasswordModel model, string userId)
        {
            if (model.Id != userId)
                throw new BadRequestException("Wtf man this account ID is not yours.");

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                throw new KeyNotFoundException();
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return result.Succeeded;
        }

        private string GenerateToken(User user)
        {
            var generatedToken = _tokenService.BuildToken(user);
            if (generatedToken == null)
                throw new BadRequestException("Failed generate token");

            return generatedToken;
        }

        public UserViewModel AddFriend(string userId, string friendId)
        {
            if (userId == friendId)
                throw new BadRequestException("You cannot add yourself to your friends list :(");

            var userEntity = _usersRepository.GetById(userId);
            var friendEntity = _usersRepository.GetById(friendId);

            if (userEntity.FriendsFrom.Where(x => x.Id == friendId).SingleOrDefault() != null)
                throw new BadRequestException("You are friends already.");

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == friendId).SingleOrDefault() != null ||
                userEntity.BlockedUsersTo.Where(x => x.Id == friendId).SingleOrDefault() != null)
                throw new BadRequestException("You cannot add this user to your friends list");

            userEntity.FriendsFrom.Add(friendEntity);
            friendEntity.FriendsTo.Add(userEntity);

            _usersRepository.Update(friendEntity);
            var userUpdatedEntity = _usersRepository.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public UserViewModel DeleteFriend(string userId, string friendId)
        {
            var userEntity = _usersRepository.GetById(userId);
            var friendEntity = _usersRepository.GetById(friendId);

            if (userEntity.FriendsFrom.Where(x => x.Id == friendId).SingleOrDefault() == null)
                throw new BadRequestException("You are not friends.");

            userEntity.FriendsFrom.Remove(friendEntity);
            friendEntity.FriendsTo.Remove(userEntity);

            _usersRepository.Update(friendEntity);
            var userUpdatedEntity = _usersRepository.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public IEnumerable<UserViewModel> GetAllFriends(string userId)
        {
            var userEntity = _usersRepository.GetById(userId);
            var friendsModels = _mapper.Map<IEnumerable<UserViewModel>>(userEntity.FriendsFrom);

            return friendsModels;
        }

        public UserViewModel BlockUser(string userId, string blockedUserId)
        {
            if (userId == blockedUserId)
                throw new BadRequestException("You cannot block yourself");

            var userEntity = _usersRepository.GetById(userId);
            var blockedUserEntity = _usersRepository.GetById(blockedUserId);

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == blockedUserId).SingleOrDefault() != null)
                throw new BadRequestException("This user is already blocked.");

            userEntity.BlockedUsersFrom.Add(blockedUserEntity);
            blockedUserEntity.BlockedUsersTo.Add(userEntity);

            _usersRepository.Update(blockedUserEntity);
            var userUpdatedEntity = _usersRepository.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public UserViewModel UnblockUser(string userId, string blockedUserId)
        {
            var userEntity = _usersRepository.GetById(userId);
            var blockedUserEntity = _usersRepository.GetById(blockedUserId);

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == blockedUserId).SingleOrDefault() == null)
                throw new BadRequestException("This user is not blocked.");

            userEntity.BlockedUsersFrom.Remove(blockedUserEntity);
            blockedUserEntity.BlockedUsersTo.Remove(userEntity);

            _usersRepository.Update(blockedUserEntity);
            var userUpdatedEntity = _usersRepository.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public IEnumerable<UserViewModel> GetAllBlockedUsers(string userId)
        {
            var userEntity = _usersRepository.GetById(userId);
            var blockedUsersModels = _mapper.Map<IEnumerable<UserViewModel>>(userEntity.BlockedUsersFrom);

            return blockedUsersModels;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var userEntityList = _usersRepository.GetAll().ToList();
            var userViewModelList = _mapper.Map<IEnumerable<UserViewModel>>(userEntityList);

            return userViewModelList;
        }

        public UserViewModel GetUser(string id)
        {
            var userEntity = _usersRepository.GetById(id);

            return _mapper.Map<UserViewModel>(userEntity);
        }

        public UserViewModel GetUserByUserName(string userName)
        {
            var userEntity = _usersRepository.GetAll().Where(x => x.UserName == userName).SingleOrDefault();
            if (userEntity == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<UserViewModel>(userEntity);
        }

        public UserViewModel UpdateUser(UserUpdateModel userModel, string userId)
        {
            if (userModel.Id != userId)
                throw new BadRequestException("Wtf man this account ID is not yours.");

            var userEntity = _usersRepository.GetById(userModel.Id);
            userEntity.UserName = userModel.UserName;
            userEntity.Email = userModel.Email;
            var result = _usersRepository.Update(userEntity);

            return _mapper.Map<UserViewModel>(result);
        }
    }
}
