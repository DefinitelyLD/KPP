using Messenger.BLL.Models;

namespace Messenger.BLL.Users
{
    public class UserChangePasswordModel : BaseModel<string>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
