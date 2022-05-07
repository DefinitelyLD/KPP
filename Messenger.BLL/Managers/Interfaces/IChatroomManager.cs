using Messenger.BLL.Chats;
using Messenger.BLL.UserAccounts;
using Messenger.BLL.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IChatroomManager
    {
        public ChatViewModel CreateChatroom(ChatCreateModel chatModel, string userId);
        public ChatUpdateModel EditChatroom(ChatUpdateModel chatModel, string adminId);
        public bool DeleteChatroom(int chatId);
        public UserAccountCreateModel AddToChatroom(string userId, int chatId, string currentUserId);
        public ChatViewModel GetChatroom(int chatId, string userId);
        public IEnumerable<ChatViewModel> GetAllChatrooms(string userId);
        public bool LeaveFromChatroom(int chatId, string userId);
        public bool KickUser(UserAccountViewModel userAccountModel, string admin);
        public UserAccountUpdateModel BanUser(UserAccountViewModel userAccountModel, string adminId);
        public UserAccountUpdateModel UnbanUser(UserAccountViewModel userAccountModel, string adminId);
        public UserAccountUpdateModel SetAdmin(UserAccountViewModel userAccountModel, string adminId);
        public UserAccountUpdateModel UnsetAdmin(UserAccountViewModel userAccountModel, string adminId);
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId, string userName);
        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId, string userName);
        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId, string userName);
    }
}
