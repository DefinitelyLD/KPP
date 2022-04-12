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
        public void ManagerSendMessage(Message msg, int сhatId);
        public void ManagerEditMessage(Message msg, int сhatId);
        public void ManagerDeleteMessage(Message msg, int сhatId);
        public void ManagerGetMessage(Message msg, int сhatId);
    }
}
