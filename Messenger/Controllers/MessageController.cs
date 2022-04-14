using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.Models;
using System.Collections.Generic;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageManager _messageManager;

        public MessageController (IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        public ActionResult<MessageModel> SendMessage(MessageModel msg)
        {
            return  _messageManager.ManagerSendMessage(msg);
        }

        public ActionResult<MessageModel> EditMessage(MessageModel msg)
        {
            return _messageManager.ManagerEditMessage(msg);
        }

        public ActionResult<bool> DeleteMessage(MessageModel msg)
        {
            return _messageManager.ManagerDeleteMessage(msg);
        }

        public ActionResult<MessageModel> GetMessage(MessageModel msg)
        {
            return _messageManager.ManagerGetMessage(msg);
        }

        public IEnumerable<MessageModel> GetAllMessages(MessageModel msg)
        {
            return _messageManager.ManagerGetAllMessages(msg);
        }
    }
}
