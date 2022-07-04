using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.UserAccounts;
using Messenger.BLL.Users;
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

        [HttpPost]
        public async Task<ActionResult<ChatViewModel>> CreateChatroom([FromBody] ChatCreateModel chat)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return await _chatroomManager.CreateChatroom(chat, userId);
        }

        [HttpPatch]
        public async Task<ActionResult<ChatUpdateModel>> EditChatroom([FromBody] ChatUpdateModel chat)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.EditChatroom(chat, adminId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteChatroom([FromBody] int chatId)
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

        [HttpPost]
        public async Task<ActionResult<UserAccountCreateModel>> AddToChatroom([FromBody] string userId, int chatId)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var currentUserId = httpContext.Value;
            return await _chatroomManager.AddToChatroom(userId, chatId, currentUserId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> LeaveFromChatroom([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return await _chatroomManager.LeaveFromChatroom(chatId, userId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> KickUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.KickUser(userAccountId, adminId);
        }

        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> BanUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.BanUser(userAccountId, adminId);
        }

        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> UnbanUser([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.UnbanUser(userAccountId, adminId);
        }

        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> SetAdmin([FromBody] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.SetAdmin(userAccountId, adminId);
        }

        [HttpPatch]
        public async Task<ActionResult<UserAccountUpdateModel>> UnsetAdmin([FromBody] int userAccountId)
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