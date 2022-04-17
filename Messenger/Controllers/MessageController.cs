using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.CreateModels;
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

        public ActionResult<MessageCreateModel> SendMessage(MessageCreateModel msg)
        {
            return  _messageManager.SendMessage(msg);
        }

        public ActionResult<MessageCreateModel> EditMessage(MessageCreateModel msg)
        {
            return _messageManager.EditMessage(msg);
        }

        public ActionResult<bool> DeleteMessage(int msgId)
        {
            return _messageManager.DeleteMessage(msgId);
        }

        public ActionResult<MessageCreateModel> GetMessage(int msgId)
        {
            return _messageManager.GetMessage(msgId);
        }

        public IEnumerable<MessageCreateModel> GetAllMessages()
        {
            return _messageManager.GetAllMessages();
        }
    }
}
