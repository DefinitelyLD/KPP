using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.Models;
using Messenger.BLL.UserAccounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatroomController : Controller
    {
        private readonly IChatroomManager _chatroomManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatroomController(IChatroomManager chatroomManager, IHttpContextAccessor httpContextAccessor)
        {
            _chatroomManager = chatroomManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CreateChatroom
        ///     {
        ///        "topic": "TestChat" ,
        ///        "userdId": "d073e154-282f-4ee1-8a08-1e9d6744e101"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<ChatViewModel>> CreateChatroom([FromBody] ChatCreateModel chat)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;

            return await _chatroomManager.CreateChatroom(chat, userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /EditChatroom
        ///     {
        ///        "id": 1,
        ///        "topic": "NewTestChat"
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<ChatUpdateModel>> EditChatroom([FromBody] ChatUpdateModel chat)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.EditChatroom(chat, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /DelteteChatroom
        ///     {
        ///        "chatId": 1,
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<bool>> SoftDeleteChatroom([FromBody] BaseModel<int> chatBaseModel)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.DeleteChatroom(chatBaseModel.Id, userId);
        }

        [HttpGet]
        public ActionResult<ChatViewModel> GetChatroom([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetChatroom(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetAllChatrooms(userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddToChatroom
        ///     {
        ///        "userId": "acd91b54-7696-4e03-bee0-49f0dde9ad0c"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<UserAccountCreateModel>> AddToChatroom([FromBody] BaseModel<string> userBaseModel, int chatId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var currentUserId = httpContext.Value;

            return await _chatroomManager.AddToChatroom(userBaseModel.Id, chatId, currentUserId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /LeaveFromChatroom
        ///     {
        ///        "chatId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<bool>> LeaveFromChatroom([FromBody] BaseModel<int> chatBaseModel)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.LeaveFromChatroom(chatBaseModel.Id, userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /KickUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<bool>> KickUser([FromBody] BaseModel<int> userAccountBaseModel)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.KickUser(userAccountBaseModel.Id, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /BanUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> BanUser([FromBody] BaseModel<int> userAccountBaseModel)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.BanUser(userAccountBaseModel.Id, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /UnbanUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> UnbanUser([FromBody] BaseModel<int> userAccountBaseModel)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.UnbanUser(userAccountBaseModel.Id, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /SetAdmin
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> SetAdmin([FromBody] BaseModel<int> userAccountBaseModel)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.SetAdmin(userAccountBaseModel.Id, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /UnsetAdmin
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> UnsetAdmin([FromBody] BaseModel<int> userAccountBaseModel)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.UnsetAdmin(userAccountBaseModel.Id, adminId);
        }

        [HttpGet]
        public UserAccountViewModel GetOwner([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetOwner(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetAllBannedUsers(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllAdmins([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetAllAdmins(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllUsers([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetAllUsers(chatId, userId);
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