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
        public ChatViewModel CreateChatroom(ChatCreateModel chatModel);
        public ChatUpdateModel EditChatroom(ChatUpdateModel chatModel);
        public bool DeleteChatroom(int chatId);
        public ChatViewModel GetChatroom(int chatId);
        public IEnumerable<ChatViewModel> GetAllChatrooms();

        public UserAccountCreateModel AddToChatroom(string userId, int chatId);
        public bool LeaveFromChatroom(int userAccountId);
        public bool KickUser(UserAccountViewModel userAccountModel, string admin);
        public UserAccountUpdateModel BanUser(UserAccountViewModel userAccountModel, string admin);
        public UserAccountUpdateModel UnbanUser(UserAccountViewModel userAccountModel, string admin);
        public UserAccountUpdateModel SetAdmin(UserAccountViewModel userAccountModel, string admin);
        public UserAccountUpdateModel UnsetAdmin(UserAccountViewModel userAccountModel, string admin);
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId, string userName);
        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId, string userName);
        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId, string userName);


    }
}
