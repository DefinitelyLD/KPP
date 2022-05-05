using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using System.Collections.Generic;
using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

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
            var userId = GetUserIdFromHttpContext();
            return await _messageManager.SendMessage(messageModel, userId);
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> EditMessage([FromQuery] MessageUpdateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();
            return await _messageManager.EditMessage(messageModel, userId);

        }

        [HttpDelete]
        public ActionResult<bool> DeleteMessage(int messageId)
        {
            var userId = GetUserIdFromHttpContext();
            return _messageManager.DeleteMessage(messageId, userId);
        }

        [HttpGet]
        public ActionResult<MessageViewModel> GetMessage(int messageId)
        {
            return _messageManager.GetMessage(messageId);
        }

        [HttpGet]
        public IEnumerable<MessageViewModel> GetMessagesFromChat(int chatId, DateTime? date = null)
        {
            var userId = GetUserIdFromHttpContext();
            return _messageManager.GetMessagesFromChat(chatId, userId, date);
        }

        private string GetUserIdFromHttpContext()
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();
            return httpContext.Value;
        }
    }
}
