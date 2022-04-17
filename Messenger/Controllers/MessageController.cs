using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using Messenger.BLL.CreateModels;
using System.Collections.Generic;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;

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

        public ActionResult<MessageCreateModel> SendMessage(MessageCreateModel messageModel)
        {
            return  _messageManager.SendMessage(messageModel);
        }

        public ActionResult<MessageUpdateModel> EditMessage(MessageUpdateModel messageModel)
        {
            return _messageManager.EditMessage(messageModel);
        }

        public ActionResult<bool> DeleteMessage(int messageId)
        {
            return _messageManager.DeleteMessage(messageId);
        }

        public ActionResult<MessageViewModel> GetMessage(int messageId)
        {
            return _messageManager.GetMessage(messageId);
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            return _messageManager.GetAllMessages();
        }
    }
}
