using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.UserAccounts;
using Messenger.WEB.Roles;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ChatViewModel>> CreateChatroom([FromForm] ChatCreateModel chat)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.CreateChatroom(chat, userId);
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult<ChatViewModel>> CreateAdminsChatroom([FromForm] ChatCreateModel chat)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.CreateAdminsChatroom(chat, userId);
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
        public async Task<ActionResult<ChatUpdateModel>> EditChatroom([FromForm] ChatUpdateModel chat)
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
        [HttpPut]
        public async Task<ActionResult<bool>> SoftDeleteChatroom([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.DeleteChatroom(chatId, userId);
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
        public async Task<ActionResult<UserAccountCreateModel>> AddToChatroom([FromBody] ChatInviteModel model)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var currentUserId = httpContext.Value;

            return await _chatroomManager.AddToChatroom(model.UserId, model.ChatId, currentUserId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /LeaveFromChatroom
        ///     {
        ///        "chatId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<bool>> LeaveFromChatroom([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return await _chatroomManager.LeaveFromChatroom(chatId, userId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /KickUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<bool>> KickUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.KickUser(userAccountId, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /BanUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>

        [HttpPut]
        public async Task<ActionResult<UserAccountViewModel>> BanUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.BanUser(userAccountId, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /UnbanUser
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<UserAccountViewModel>> UnbanUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.UnbanUser(userAccountId, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /SetAdmin
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<UserAccountViewModel>> SetAdmin([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.SetAdmin(userAccountId, adminId);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /UnsetAdmin
        ///     {
        ///        "userAccountId": 1
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<UserAccountViewModel>> UnsetAdmin([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();

            return await _chatroomManager.UnsetAdmin(userAccountId, adminId);
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
        public UserAccountViewModel GetCurrentUserAccount([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();

            return _chatroomManager.GetCurrentUserAccount(chatId, userId);
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