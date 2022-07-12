using Messenger.BLL.Models;

namespace Messenger.BLL.MessageImages
{
    public class MessageImageUpdateModel : BaseModel<int>
    {
        public string Path { get; set; }
    }
}
