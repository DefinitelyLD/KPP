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
        public MessageModel ManagerSendMessage(MessageModel msg);
        public MessageModel ManagerEditMessage(MessageModel msg);
        public bool ManagerDeleteMessage(MessageModel msg);
        public MessageModel ManagerGetMessage(MessageModel msg);
    }
}
