using Messenger.BLL.Models;

namespace Messenger.BLL.Users
{
    public class UserViewModel : BaseModel<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
