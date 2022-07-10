using Messenger.BLL.Chats;
using Messenger.BLL.UserAccounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IChatroomManager
    {
        public Task<ChatViewModel> CreateChatroom(ChatCreateModel chatModel);
        public Task<ChatUpdateModel> EditChatroom(ChatUpdateModel chatModel, string adminId);
        public Task<bool> DeleteChatroom(int chatId, string userId);
        public ChatViewModel GetChatroom(int chatId, string userId);
        public IEnumerable<ChatViewModel> GetAllChatrooms(string userId);
        public Task<UserAccountCreateModel> AddToChatroom(string userId, int chatId, string currentUserId);
        public Task<bool> LeaveFromChatroom(int chatId, string userId);
        public Task<bool> KickUser(int userAccountId, string admin);
        public Task<UserAccountUpdateModel> BanUser(int userAccountId, string adminId);
        public Task<UserAccountUpdateModel> UnbanUser(int userAccountId, string adminId);
        public Task<UserAccountUpdateModel> SetAdmin(int userAccountId, string adminId);
        public Task<UserAccountUpdateModel> UnsetAdmin(int userAccountId, string adminId);
        public UserAccountViewModel GetOwner(int chatId, string userId);
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId, string userId);
        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId, string userId);
        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId, string userId);
    }
}
