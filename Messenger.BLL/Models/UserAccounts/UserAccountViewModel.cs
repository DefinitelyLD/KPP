using Messenger.BLL.Models;
using Messenger.BLL.Users;

namespace Messenger.BLL.UserAccounts
{
    public class UserAccountViewModel : BaseModel<int>
    {
        public UserViewModel User { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
