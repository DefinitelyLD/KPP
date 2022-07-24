using Messenger.BLL.ChatImages;
using Messenger.BLL.Messages;
using Messenger.BLL.UserAccounts;
using System.Collections.Generic;

namespace Messenger.BLL.Chats
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public ChatImageViewModel Image {get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

        public ICollection<UserAccountViewModel> Users { get; set; }

        public bool IsAdminsRoom { get; set; }
    }
}