using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.CreateModels;
using System.Collections.Generic;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : Controller
    {
        private readonly IMessageManager _messageManager;

        public MessageController (IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        [HttpPost]
        public ActionResult<MessageCreateModel> SendMessage(MessageCreateModel messageModel)
        {
            return  _messageManager.SendMessage(messageModel);
        }

        [HttpPost]
        public ActionResult<MessageUpdateModel> EditMessage(MessageUpdateModel messageModel)
        {
            return _messageManager.EditMessage(messageModel);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteMessage(int messageId)
        {
            return _messageManager.DeleteMessage(messageId);
        }

        [HttpGet]
        public ActionResult<MessageViewModel> GetMessage(int messageId)
        {
            return _messageManager.GetMessage(messageId);
        }

        [HttpGet]
        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            return _messageManager.GetAllMessages();
        }
    }
}
