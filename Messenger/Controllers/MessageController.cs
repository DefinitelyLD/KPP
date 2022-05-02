using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using System.Collections.Generic;
using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : Controller
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> SendMessage([FromQuery] MessageCreateModel messageModel)
        {
            return await _messageManager.SendMessage(messageModel);
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> EditMessage([FromQuery] MessageUpdateModel messageModel)
        {
            var result = await _messageManager.EditMessage(messageModel);
            return result;
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
