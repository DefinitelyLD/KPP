using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.Managers.Interfaces;
using Messenger.BLL.Token;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Messenger.DAL.UoW;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManager _imageManager;
        private readonly IActionLogManager _logger;
        public AccountManager (UserManager<User> userManager, 
            IMapper mapper, 
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IImageManager imageManager,
            IActionLogManager actionLogManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _imageManager = imageManager;
            _logger = actionLogManager;
        }

        public async Task<UserViewModel> RegisterUser(UserCreateModel model)
        {
            var filePath = "/DefaultUserImage/image.png";

            User user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Registration Error");
            }
                
            await _userManager.AddToRoleAsync(user, "User");

            var userEntity = _unitOfWork.Users.GetAll().Where(x => x.UserName == model.UserName).SingleOrDefault();
            var userViewModel = _mapper.Map<UserViewModel>(userEntity);

            UserImage imageEntity = new()
            {
                Path = filePath,
                UserId = userViewModel.Id
            };

            await _unitOfWork.UserImages.CreateAsync(imageEntity);
            await _logger.CreateLog("Registered", userEntity.Id);

            return userViewModel;
        }

        public async Task<bool> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            await _logger.CreateLog("Confirmed Email", user.Id);

            return result.Succeeded;
        }

        public async Task<UserViewModel> LoginUser(UserLoginModel model)
        {
            var userEntity = _unitOfWork.Users.GetAll().Where(x => x.UserName == model.UserName).SingleOrDefault();

            if (userEntity == null)
                throw new BadRequestException("This username is not registered");
            if (!await _userManager.IsEmailConfirmedAsync(userEntity))
                throw new BadRequestException("Email is not confirmed");
            if (!await _userManager.CheckPasswordAsync(userEntity, model.Password))
                throw new BadRequestException("Incorrect Password");

            var userModel = _mapper.Map<UserViewModel>(userEntity);
            userModel.Token = await GenerateToken(userEntity);

            return userModel;
        }

        public async Task<bool> ChangeUserPassword(UserChangePasswordModel model, string userId)
        {
            if (model.Id != userId)
            {
                throw new BadRequestException("man this account ID is not yours.");
            } 

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }
                
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            await _logger.CreateLog("Changed Password", user.Id);

            return result.Succeeded;
        }

        private async Task<string> GenerateToken(User user)
        {
            var generatedToken = await _tokenService.BuildTokenAsync(user);
            if (generatedToken == null)
                throw new BadRequestException("Failed generate token");

            return generatedToken;
        }

        public UserViewModel AddFriend(string userId, string friendId)
        {
            if (userId == friendId)
                throw new BadRequestException("You cannot add yourself to your friends list :(");

            var userEntity = _unitOfWork.Users.GetById(userId);
            var friendEntity = _unitOfWork.Users.GetById(friendId);

            if (userEntity.FriendsFrom.Where(x => x.Id == friendId).SingleOrDefault() != null)
                throw new BadRequestException("You are friends already.");

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == friendId).SingleOrDefault() != null ||
                userEntity.BlockedUsersTo.Where(x => x.Id == friendId).SingleOrDefault() != null)
                throw new BadRequestException("You cannot add this user to your friends list");

            userEntity.FriendsFrom.Add(friendEntity);
            friendEntity.FriendsTo.Add(userEntity);

            _unitOfWork.Users.Update(friendEntity);
            var userUpdatedEntity = _unitOfWork.Users.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public UserViewModel DeleteFriend(string userId, string friendId)
        {
            var userEntity = _unitOfWork.Users.GetById(userId);
            var friendEntity = _unitOfWork.Users.GetById(friendId);

            if (userEntity.FriendsFrom.Where(x => x.Id == friendId).SingleOrDefault() == null)
                throw new BadRequestException("You are not friends.");

            userEntity.FriendsFrom.Remove(friendEntity);
            friendEntity.FriendsTo.Remove(userEntity);

            _unitOfWork.Users.Update(friendEntity);
            var userUpdatedEntity = _unitOfWork.Users.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public IEnumerable<UserViewModel> GetAllFriends(string userId)
        {
            var userEntity = _unitOfWork.Users.GetById(userId);
            var friendsModels = _mapper.Map<IEnumerable<UserViewModel>>(userEntity.FriendsFrom);

            return friendsModels;
        }

        public UserViewModel BlockUser(string userId, string blockedUserId)
        {
            if (userId == blockedUserId)
                throw new BadRequestException("You cannot block yourself");

            var userEntity = _unitOfWork.Users.GetById(userId);
            var blockedUserEntity = _unitOfWork.Users.GetById(blockedUserId);

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == blockedUserId).SingleOrDefault() != null)
                throw new BadRequestException("This user is already blocked.");

            userEntity.BlockedUsersFrom.Add(blockedUserEntity);
            blockedUserEntity.BlockedUsersTo.Add(userEntity);

            _unitOfWork.Users.Update(blockedUserEntity);
            var userUpdatedEntity = _unitOfWork.Users.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public UserViewModel UnblockUser(string userId, string blockedUserId)
        {
            var userEntity = _unitOfWork.Users.GetById(userId);
            var blockedUserEntity = _unitOfWork.Users.GetById(blockedUserId);

            if (userEntity.BlockedUsersFrom.Where(x => x.Id == blockedUserId).SingleOrDefault() == null)
                throw new BadRequestException("This user is not blocked.");

            userEntity.BlockedUsersFrom.Remove(blockedUserEntity);
            blockedUserEntity.BlockedUsersTo.Remove(userEntity);

            _unitOfWork.Users.Update(blockedUserEntity);
            var userUpdatedEntity = _unitOfWork.Users.Update(userEntity);

            return _mapper.Map<UserViewModel>(userUpdatedEntity);
        }

        public IEnumerable<UserViewModel> GetAllBlockedUsers(string userId)
        {
            var userEntity = _unitOfWork.Users.GetById(userId);
            var blockedUsersModels = _mapper.Map<IEnumerable<UserViewModel>>(userEntity.BlockedUsersFrom);

            return blockedUsersModels;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var userEntityList = _unitOfWork.Users.GetAll().ToList();
            var userViewModelList = _mapper.Map<IEnumerable<UserViewModel>>(userEntityList);

            return userViewModelList;
        }

        public UserViewModel GetUser(string id)
        {
            var userEntity = _unitOfWork.Users.GetById(id);

            return _mapper.Map<UserViewModel>(userEntity);
        }

        public UserViewModel GetUserByUserName(string userName)
        {
            var userEntity = _unitOfWork.Users.GetAll().Where(x => x.UserName == userName).SingleOrDefault();
            if (userEntity == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<UserViewModel>(userEntity);
        }

        public async Task<UserViewModel> UpdateUser(UserUpdateModel userModel, string userId)
        {
            if (userModel.Id != userId)
            {
                throw new BadRequestException("man this account ID is not yours.");
            }

            var userEntity = _unitOfWork.Users.GetById(userModel.Id);
            userEntity.UserName = userModel.UserName;
            userEntity.Email = userModel.Email;

            if (userModel.File != null)
            {
                var filePath = await _imageManager.UploadImage(userModel.File);
                var userImageEntity = _unitOfWork.UserImages.GetAll().Where(u => u.UserId == userModel.Id).SingleOrDefault();
                userImageEntity.Path = filePath;
                _unitOfWork.UserImages.Update(userImageEntity);
            }

            var result = _unitOfWork.Users.Update(userEntity);

            await _logger.CreateLog("Updated Profile", userEntity.Id);

            return _mapper.Map<UserViewModel>(result);
        }

        public async Task<bool> IsUserSuperAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return await _userManager.IsInRoleAsync(user, "Admin");
        }

        public UserViewModel GetCurrentUser(string userId)
        {
            var userIdEntity = _userManager.FindByIdAsync(userId).Result;
            var roles = _userManager.GetRolesAsync(userIdEntity).Result;
            var userEntity = _unitOfWork.Users.GetAll().Where(x => x.Id == userId).SingleOrDefault();
            if (userEntity == null)
                throw new KeyNotFoundException();

            return _mapper.Map<UserViewModel>(userEntity);
        }
    }
}
