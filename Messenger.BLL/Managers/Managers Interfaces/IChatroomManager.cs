using Messenger.BLL.CreateModels;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;
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

        public UserAccountCreateModel AddToChatroom(int userId, int chatId);
        public bool LeaveFromChatroom(int userAccountId);
        public bool KickUser(int userAccountId);
        public UserAccountUpdateModel BanUser(int userId, int chatId);
        public UserAccountUpdateModel SetAdmin(int userId, int chatId);
        public UserAccountUpdateModel UnsetAdmin(int userId, int chatId);
        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel);

    }
}
