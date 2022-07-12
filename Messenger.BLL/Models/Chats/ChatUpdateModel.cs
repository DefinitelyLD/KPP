using Messenger.BLL.Models;

namespace Messenger.BLL.Chats
{
    public class ChatUpdateModel : BaseModel<int>
    {
        public string Topic { get; set; }
    }
}
