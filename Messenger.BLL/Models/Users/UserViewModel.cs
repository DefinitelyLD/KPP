using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public virtual ICollection<UserViewModel> FriendsTo { get; set; }
        public virtual ICollection<UserViewModel> FriendsFrom { get; set; }

        public virtual ICollection<UserViewModel> BlockedUsersTo { get; set; }
        public virtual ICollection<UserViewModel> BlockedUsersFrom { get; set; }
    }
}
