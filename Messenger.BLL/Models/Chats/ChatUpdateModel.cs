using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Chats
{
    public class ChatUpdateModel
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public IFormFile File { get; set; }
    }
}
