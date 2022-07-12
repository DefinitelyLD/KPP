using Messenger.BLL.Users;
﻿using Messenger.BLL.Chats;

namespace Messenger.BLL.UserAccounts
{
    public class UserAccountViewModel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public UserViewModel User { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
