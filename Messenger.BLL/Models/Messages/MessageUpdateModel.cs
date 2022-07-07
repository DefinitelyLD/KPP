using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Messages
{
    public class MessageUpdateModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IFormFile File { get; set; }
        public int ImageId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
