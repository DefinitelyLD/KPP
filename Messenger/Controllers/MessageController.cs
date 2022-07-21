using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using System.Collections.Generic;
using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Messenger.WEB.Roles;

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

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /SendMessage
        ///     {
        ///        "chatId": 1,
        ///        "userId": "d073e154-282f-4ee1-8a08-1e9d6744e101",
        ///        files: [
        ///         "string"
        ///        ],
        ///        "text": "Hello, chat!"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> SendMessage([FromForm] MessageCreateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();
            return await _messageManager.SendMessage(messageModel, userId);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult<MessageViewModel>> SendAdminsMessage([FromForm] MessageCreateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();
            return await _messageManager.SendAdminsMessage(messageModel, userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /EditMessage
        ///     {
        ///        "id": 1,
        ///        "text": "Hi, chat!",
        ///        "file": "string",
        ///        "imageId": 0
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<MessageViewModel>> EditMessage([FromBody] MessageUpdateModel messageModel)
        {
            var userId = GetUserIdFromHttpContext();

            return await _messageManager.EditMessage(messageModel, userId);

        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /DeleteMessage
        ///     {
        ///        "messageId": 1,
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<bool>> SoftDeleteMessage([FromBody] int messageId)
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
