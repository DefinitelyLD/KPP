using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.Models;

namespace Messenger.WEB.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageManager _messageManager;
        public void SendMessage(Message msg, int chatId)
        {
            _messageManager.ManagerSendMessage(msg, chatId);
        }

        public void EditMessage(Message msg, int chatId)
        {
            _messageManager.ManagerEditMessage(msg, chatId);
        }

        public void DeleteMessage(Message msg, int chatId)
        {
            _messageManager.ManagerDeleteMessage(msg, chatId);
        }

        public void GetMessage(Message msg, int chatId)
        {
            _messageManager.ManagerGetMessage(msg, chatId);
        }
    }
}
