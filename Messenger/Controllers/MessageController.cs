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
            return  _messageManager.SendMessage(msg);
        }

        public ActionResult<MessageModel> EditMessage(MessageModel msg)
        {
            return _messageManager.EditMessage(msg);
        }

        public ActionResult<bool> DeleteMessage(MessageModel msg)
        {
            return _messageManager.DeleteMessage(msg);
        }

        public ActionResult<MessageModel> GetMessage(int msgId)
        {
            return _messageManager.GetMessage(msgId);
        }

        public IEnumerable<MessageModel> GetAllMessages(MessageModel msg)
        {
            return _messageManager.GetAllMessages(msg);
        }
    }
}
