using Messenger.BLL.CreateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.ViewModels
{
    public class UserAccountViewModel
    {
        public int Id { get; set; }
        public ChatCreateModel Chat { get; set; }
        public UserCreateModel User { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
