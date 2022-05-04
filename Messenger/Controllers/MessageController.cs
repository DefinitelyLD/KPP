using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using System.Collections.Generic;
using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;

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
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return await _messageManager.SendMessage(messageModel, userId);
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> EditMessage([FromQuery] MessageUpdateModel messageModel)
        {
            return await _messageManager.EditMessage(messageModel);
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
