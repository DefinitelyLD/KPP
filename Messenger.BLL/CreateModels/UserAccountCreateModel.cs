using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.CreateModels
{
    public class UserAccountCreateModel
    {
        public ChatCreateModel Chat { get; set; }
        public UserCreateModel User { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
