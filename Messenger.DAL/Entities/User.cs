using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Messenger.DAL.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<User> FriendsTo { get; set; }
        public virtual ICollection<User> FriendsFrom { get; set; }

        public virtual ICollection<User> BlockedUsersTo { get; set; }
        public virtual ICollection<User> BlockedUsersFrom { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserAccount> Chats { get; set; }
    }
}