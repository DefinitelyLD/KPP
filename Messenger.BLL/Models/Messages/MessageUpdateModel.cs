using Messenger.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Messages
{
    public class MessageUpdateModel : BaseModel<int>
    {
        public string Text { get; set; }
        public IFormFile File { get; set; }
        public int ImageId { get; set; }
    }
}
