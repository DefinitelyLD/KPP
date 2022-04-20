using Messenger.BLL.Messages;
using Messenger.BLL.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Chats
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string? Password { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
        public ICollection<UserAccountViewModel> Users { get; set; }
    }
}