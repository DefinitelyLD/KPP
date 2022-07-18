using Messenger.BLL.Models.UserImages;

namespace Messenger.BLL.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public UserImageViewModel Image { get; set; }
    }
}
