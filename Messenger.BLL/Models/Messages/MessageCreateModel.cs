using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Messages
{
    public class MessageCreateModel
    {
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public IFormFileCollection Files { get; set; }
        public string Text { get; set; }
    }
}
