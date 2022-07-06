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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageController(IMessageManager messageManager, IHttpContextAccessor httpContextAccessor)
        {
            _messageManager = messageManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> SendMessage([FromBody] MessageCreateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();

            return await _messageManager.SendMessage(messageModel, userId);
        }

        [HttpPut]
        public async Task<ActionResult<MessageViewModel>> EditMessage([FromBody] MessageUpdateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();

            return await _messageManager.EditMessage(messageModel, userId);

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteMessage([FromBody] int messageId)
        {
            var userId = GetUserIdFromHttpContext();

            return await _messageManager.DeleteMessage(messageId, userId);
        }

        [HttpGet]
        public ActionResult<MessageViewModel> GetMessage([FromQuery] int messageId)
        {
            return _messageManager.GetMessage(messageId);
        }

        [HttpGet]
        public IEnumerable<MessageViewModel> GetMessagesFromChat([FromQuery] int chatId, DateTime? date = null)
        {
            var userId = GetUserIdFromHttpContext();

            return _messageManager.GetMessagesFromChat(chatId, userId, date);
        }

        private string GetUserIdFromHttpContext()
        {
            var httpContext = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            return httpContext.Value;
        }
    }
}
