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
        public ChatCreateModel CreateChatroom(ChatCreateModel chatModel);
        public ChatUpdateModel EditChatroom(ChatUpdateModel chatModel);
        public bool DeleteChatroom(int chatId);
        public ChatViewModel GetChatroom(int chatId);
        public IEnumerable<ChatViewModel> GetAllChatrooms();

        public UserAccountCreateModel AddToChatroom(string userId, int chatId);
        public bool LeaveFromChatroom(int userAccountId);
        public bool KickUser(UserAccountViewModel userAccountModel,
                             UserAccountViewModel adminAccountModel);
        public UserAccountUpdateModel BanUser(UserAccountViewModel userAccountModel,
                                              UserAccountViewModel adminAccountModel);
        public UserAccountUpdateModel UnbanUser(UserAccountViewModel userAccountModel,
                                                UserAccountViewModel adminAccountModel);
        public IEnumerable<UserViewModel> GetAllBannedUsers(ChatViewModel chatModel);

        public UserAccountUpdateModel SetAdmin(UserAccountViewModel userAccountModel,
                                               UserAccountViewModel adminAccountModel);
        public UserAccountUpdateModel UnsetAdmin(UserAccountViewModel userAccountModel,
                                                 UserAccountViewModel adminAccountModel);
        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel);

    }
}
