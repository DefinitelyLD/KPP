using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IAccountManager
    {
        public Task<UserViewModel> RegisterUser(UserCreateModel model);
        public Task<UserViewModel> LoginUser(UserLoginModel model);
        public Task LogoutUser();
        public Task<bool> DeleteUser(string id);
        public Task<bool> ChangeUserPassword(UserChangePasswordModel model, string userId);
        public IEnumerable<UserViewModel> GetAllUsers();
        public UserViewModel GetUser(string id);
        public UserViewModel GetUserByUserName(string userName);
        public UserViewModel AddFriend(string userId, string friendId);
        public UserViewModel DeleteFriend(string userId, string friendId);
        public UserViewModel BlockUser(string userId, string blockedUserId);
        public UserViewModel UnblockUser(string userId, string blockedUserId);
        public UserViewModel UpdateUser(UserUpdateModel userModel, string userId);
    }
}
