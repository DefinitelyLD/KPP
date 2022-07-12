using Messenger.BLL.Models;

namespace Messenger.BLL.UserAccounts
{
    public class UserAccountUpdateModel : BaseModel<int>
    {
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
    }
}
