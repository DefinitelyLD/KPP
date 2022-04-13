using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.Models;

namespace Messenger.WEB.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageManager _messageManager;

        public MessageController (IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        public void SendMessage(MessageModel msg, int chatId)
        {
            _messageManager.ManagerSendMessage(msg, chatId);
        }

        public void EditMessage(MessageModel msg, int chatId)
        {
            _messageManager.ManagerEditMessage(msg, chatId);
        }

        public void DeleteMessage(MessageModel msg, int chatId)
        {
            _messageManager.ManagerDeleteMessage(msg, chatId);
        }

        public void GetMessage(MessageModel msg, int chatId)
        {
            _messageManager.ManagerGetMessage(msg, chatId);
        }
    }
}
