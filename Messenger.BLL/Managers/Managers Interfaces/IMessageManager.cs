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
        public void ManagerSendMessage(MessageModel msg, int сhatId);
        public void ManagerEditMessage(MessageModel msg, int сhatId);
        public void ManagerDeleteMessage(MessageModel msg, int сhatId);
        public void ManagerGetMessage(MessageModel msg, int сhatId);
    }
}
