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
        public ICollection<User> FriendsTo { get; set; }
        public ICollection<User> FriendsFrom { get; set; }

        public ICollection<User> BlockedUsersTo { get; set; }
        public ICollection<User> BlockedUsersFrom { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<UserAccount> Chats { get; set; }
    }
}