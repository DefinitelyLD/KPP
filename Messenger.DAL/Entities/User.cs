using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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