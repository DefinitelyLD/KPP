using Messenger.BLL.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IAccountManager
    {
        public Task<UserViewModel> RegisterUser(UserCreateModel model);
        public Task<bool> ConfirmEmail(string userId, string code);
        public Task<UserViewModel> LoginUser(UserLoginModel model);
        public Task<bool> ChangeUserPassword(UserChangePasswordModel model, string userId);
        public IEnumerable<UserViewModel> GetAllUsers();
        public UserViewModel GetUser(string id);
        public UserViewModel GetUserByUserName(string userName);
        public UserViewModel AddFriend(string userId, string friendId);
        public UserViewModel DeleteFriend(string userId, string friendId);
        public IEnumerable<UserViewModel> GetAllFriends(string userId);
        public UserViewModel BlockUser(string userId, string blockedUserId);
        public UserViewModel UnblockUser(string userId, string blockedUserId);
        public IEnumerable<UserViewModel> GetAllBlockedUsers(string userId);
        public Task<UserViewModel> UpdateUser(UserUpdateModel userModel, string userId);
        public Task<bool> IsUserSuperAdmin(string userId);
        public UserViewModel GetCurrentUser(string userId);
    }
}
