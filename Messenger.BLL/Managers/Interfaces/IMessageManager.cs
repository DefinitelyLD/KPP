using Messenger.BLL.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IMessageManager
    {
        public MessageCreateModel SendMessage(MessageCreateModel messageModel);
        public MessageUpdateModel EditMessage(MessageUpdateModel messageModel);
        public bool DeleteMessage(int messageId);
        public MessageViewModel GetMessage(int messageId);
        public IEnumerable<MessageViewModel> GetAllMessages();
    }
}
