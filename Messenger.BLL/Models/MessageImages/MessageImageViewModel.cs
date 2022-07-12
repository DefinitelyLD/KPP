using Messenger.BLL.Messages;

namespace Messenger.BLL.MessageImages
{
    public class MessageImageViewModel
    {
        public int Id { get; set; }    
        public string Path { get; set; }
        public MessageViewModel Message { get; set; }
    }
}
