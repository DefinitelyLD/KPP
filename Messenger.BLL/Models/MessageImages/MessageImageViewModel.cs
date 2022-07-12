using Messenger.BLL.Messages;
using Messenger.BLL.Models;

namespace Messenger.BLL.MessageImages
{
    public class MessageImageViewModel : BaseModel<int>
    {
        public string Path { get; set; }
        public MessageViewModel Message { get; set; }
    }
}
