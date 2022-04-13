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
        public Message ManagerSendMessage(MessageModel msg);
        public void ManagerEditMessage(MessageModel msg);
        public void ManagerDeleteMessage(MessageModel msg);
        public void ManagerGetMessage(MessageModel msg);
    }
}
