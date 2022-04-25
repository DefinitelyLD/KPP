using Messenger.BLL.Messages;
using Messenger.BLL.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Chats
{
    public class ChatCreateModel
    {
        public string Topic { get; set; }
        public string? Password { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public IEnumerable<UserAccountViewModel> Users { get; set; }
    }
}
