using Messenger.BLL.Models;

namespace Messenger.BLL.Users
{
    public class UserUpdateModel : BaseModel<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
