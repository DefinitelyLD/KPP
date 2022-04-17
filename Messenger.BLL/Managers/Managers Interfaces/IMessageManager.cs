using Messenger.BLL.Models;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IMessageManager
    {
        public MessageCreateModel SendMessage(MessageCreateModel msg);
        public MessageUpdateModel EditMessage(MessageUpdateModel msg);
        public bool DeleteMessage(int msgId);
        public MessageViewModel GetMessage(int msgId);
        public IEnumerable<MessageViewModel> GetAllMessages();
    }
}
