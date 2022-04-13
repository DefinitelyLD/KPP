using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.Models;

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

        public ActionResult<Message> SendMessage(MessageModel msg)
        {
            return  _messageManager.ManagerSendMessage(msg);
        }

        public void EditMessage(MessageModel msg)
        {
            _messageManager.ManagerEditMessage(msg);
        }

        public void DeleteMessage(MessageModel msg)
        {
            _messageManager.ManagerDeleteMessage(msg);
        }

        public void GetMessage(MessageModel msg)
        {
            _messageManager.ManagerGetMessage(msg);
        }
    }
}
