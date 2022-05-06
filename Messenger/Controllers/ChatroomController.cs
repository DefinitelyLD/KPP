using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.UserAccounts;
using Messenger.BLL.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatroomController : Controller
    {
        private readonly IChatroomManager _chatroomManager;

        public ChatroomController(IChatroomManager chatroomManager)
        {
            _chatroomManager = chatroomManager;
        }

        [HttpPost]
        public ActionResult<ChatViewModel> CreateChatroom([FromBody] ChatCreateModel chat)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _chatroomManager.CreateChatroom(chat, userId);
        }

        [HttpPost]
        public ActionResult<ChatUpdateModel> EditChatroom([FromBody] ChatUpdateModel chat)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.EditChatroom(chat, adminId);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteChatroom([FromBody] int chatId)
        {
            return _chatroomManager.DeleteChatroom(chatId);
        }

        [HttpPost]
        public ActionResult<ChatViewModel> GetChatroom([FromBody] int chatId)
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
        public ActionResult<UserAccountCreateModel> AddToChatroom([FromBody] string userId, int chatId)
        {
            return _chatroomManager.AddToChatroom(userId, chatId);
        }

        [HttpDelete]
        public ActionResult<bool> LeaveFromChatroom([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return _chatroomManager.LeaveFromChatroom(chatId, userId);
        }

        [HttpDelete]
        public ActionResult<bool> KickUser([FromBody] UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.KickUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser([FromBody] UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.BanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser([FromBody] UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.UnbanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin([FromBody] UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.SetAdmin(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin([FromBody] UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext();
            return _chatroomManager.UnsetAdmin(userModel, adminId);
        }

        [HttpPost]
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return _chatroomManager.GetAllBannedUsers(chatId, userId);
        }

        [HttpPost]
        public IEnumerable<UserAccountViewModel> GetAllAdmins([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return _chatroomManager.GetAllAdmins(chatId, userId);
        }

        [HttpPost]
        public IEnumerable<UserAccountViewModel> GetAllUsers([FromBody] int chatId)
        {
            var userId = GetUserIdFromHttpContext();
            return _chatroomManager.GetAllUsers(chatId, userId);
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