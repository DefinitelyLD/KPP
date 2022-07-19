using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Users
{
    public class UserUpdateModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IFormFile File { get; set; }
    }
}
