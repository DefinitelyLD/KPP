using Messenger.BLL.Messages;
using Messenger.BLL.Models;
using Messenger.BLL.UserAccounts;
using System.Collections.Generic;

namespace Messenger.BLL.Chats
{
    public class ChatViewModel : BaseModel<int>
    {
        public string Topic { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
        public ICollection<UserAccountViewModel> Users { get; set; }
    }
}