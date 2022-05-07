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
        public async Task<ActionResult<ChatViewModel>> CreateChatroom([FromForm] ChatCreateModel chat)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return await _chatroomManager.CreateChatroom(chat, userId);
        }

        [HttpPost]
        public async Task<ActionResult<ChatUpdateModel>> EditChatroom([FromForm] ChatUpdateModel chat)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.EditChatroom(chat, adminId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteChatroom([FromQuery] int chatId)
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
        public async Task<ActionResult<UserAccountCreateModel>> AddToChatroom(string userId, int chatId)
        {
            return await _chatroomManager.AddToChatroom(userId, chatId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> LeaveFromChatroom([FromQuery] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return await _chatroomManager.LeaveFromChatroom(chatId, userId);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> KickUser([FromQuery] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.KickUser(userAccountId, adminId);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccountUpdateModel>> BanUser([FromForm] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.BanUser(userAccountId, adminId);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccountUpdateModel>> UnbanUser([FromForm] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.UnbanUser(userAccountId, adminId);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccountUpdateModel>> SetAdmin([FromForm] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.SetAdmin(userAccountId, adminId);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccountUpdateModel>> UnsetAdmin([FromForm] int userAccountId)
        {
            var adminId = GetUserIdFromHttpContext();
            return await _chatroomManager.UnsetAdmin(userAccountId, adminId);
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