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
        public ChatCreateModel CreateChatroom(ChatCreateModel chat);
        public ChatUpdateModel EditChatroom(ChatUpdateModel chat);
        public bool DeleteChatroom(int chatId);
        public ChatViewModel GetChatroom(int chatId);
        public IEnumerable<ChatViewModel> GetAllChatrooms();

        public ChatUpdateModel AddToChatroom(int userId, int chatId);
        public ChatUpdateModel LeaveFromChatroom(int chatId);

        public bool KickUser(int userId, int chatId);
        public UserAccountUpdateModel BanUser(int userId, int chatId);

        public UserAccountUpdateModel SetAdmin(int userId, int chatId);
        public UserAccountUpdateModel UnsetAdmin(int userId, int chatId);
        public IEnumerable<UserViewModel> GetAllAdmins(int chatId);

    }
}
