using Messenger.BLL.Models;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IMessageManager
    {
        public MessageModel SendMessage(MessageModel msg);
        public MessageModel EditMessage(MessageModel msg);
        public bool DeleteMessage(int msgId);
        public MessageModel GetMessage(int msgId);
        public IEnumerable<MessageModel> GetAllMessages();
    }
}
