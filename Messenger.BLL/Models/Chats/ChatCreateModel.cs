using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Chats
{
    public class ChatCreateModel
    {
        public string Topic { get; set; }

        public IFormFile File { get; set; }

    }
}
