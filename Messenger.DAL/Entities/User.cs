using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Messenger.DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string AvatarUrl { get; set; }

        public IEnumerable<User> FriendsTo { get; set; }
        public IEnumerable<User> FriendsFrom { get; set; }

        public IEnumerable<User> BlockedUsersTo { get; set; }
        public IEnumerable<User> BlockedUsersFrom { get; set; }

        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<ChatUser> ChatUsers { get; set; }
    }
}