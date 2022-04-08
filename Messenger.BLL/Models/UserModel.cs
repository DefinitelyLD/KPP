using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> ContactsIds { get; set; }

        public IEnumerable<string> BlockedUsersIds { get; set; }

        public IEnumerable<string> ChatsIds { get; set; }
    }
}
