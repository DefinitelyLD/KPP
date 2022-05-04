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
        public ActionResult<ChatViewModel> CreateChatroom(ChatCreateModel chat)
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();

            var userId = httpContext.Value;
            return _chatroomManager.CreateChatroom(chat, userId);
        }

        [HttpPost]
        public ActionResult<ChatUpdateModel> EditChatroom(ChatUpdateModel chat)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.EditChatroom(chat, adminId);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteChatroom(int chatId)
        {
            return _chatroomManager.DeleteChatroom(chatId);
        }

        [HttpGet]
        public ActionResult<ChatViewModel> GetChatroom(int chatId)
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.GetChatroom(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.GetAllChatrooms(userId);
        }

        [HttpPost]
        public ActionResult<UserAccountCreateModel> AddToChatroom(string userId, int chatId)
        {
            return _chatroomManager.AddToChatroom(userId, chatId);
        }

        [HttpDelete]
        public ActionResult<bool> LeaveFromChatroom(int chatId)
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.LeaveFromChatroom(chatId, userId);
        }

        [HttpDelete]
        public ActionResult<bool> KickUser(UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.KickUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser(UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.BanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser(UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.UnbanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin(UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.SetAdmin(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin(UserAccountViewModel userModel)
        {
            var adminId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.UnsetAdmin(userModel, adminId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId)
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.GetAllBannedUsers(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId)
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.GetAllAdmins(chatId, userId);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId)
        {
            var userId = GetUserIdFromHttpContext().Value;
            return _chatroomManager.GetAllUsers(chatId, userId);
        }

        private Claim GetUserIdFromHttpContext()
        {
            var httpContext = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (httpContext == null)
                throw new KeyNotFoundException();
            return httpContext;
        }
    }
}