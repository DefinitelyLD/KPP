using Messenger.BLL.Chats;
using Messenger.BLL.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Messenger.BLL.UserAccounts
{
    public class UserAccountCreateModel
    {
        public int ChatId { get; set; } 
        public ChatViewModel Chat { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
