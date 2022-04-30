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
            return _chatroomManager.CreateChatroom(chat);
        }

        [HttpPost]
        public ActionResult<ChatUpdateModel> EditChatroom(ChatUpdateModel chat)
        {
            return _chatroomManager.EditChatroom(chat);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteChatroom(int chatId)
        {
            return _chatroomManager.DeleteChatroom(chatId);
        }

        [HttpGet]
        public ActionResult<ChatViewModel> GetChatroom(int chatId)
        {
            return _chatroomManager.GetChatroom(chatId);
        }

        [HttpGet]
        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            return _chatroomManager.GetAllChatrooms();
        }

        [HttpPost]
        public ActionResult<UserAccountCreateModel> AddToChatroom(string userId, int chatId)
        {
            return _chatroomManager.AddToChatroom(userId, chatId);
        }

        [HttpDelete]
        public ActionResult<bool> LeaveFromChatroom(int userAccountId)
        {
            return _chatroomManager.LeaveFromChatroom(userAccountId);
        }

        [HttpDelete]
        public ActionResult<bool> KickUser(UserAccountViewModel userModel)
        {
            var adminId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _chatroomManager.KickUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser(UserAccountViewModel userModel)
        {
            if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new KeyNotFoundException();

            var adminId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _chatroomManager.BanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser(UserAccountViewModel userModel)
        {
            if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new KeyNotFoundException();

            var adminId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _chatroomManager.UnbanUser(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin(UserAccountViewModel userModel)
        {
            if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new KeyNotFoundException();

            var adminId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _chatroomManager.SetAdmin(userModel, adminId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin(UserAccountViewModel userModel)
        {
            if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new KeyNotFoundException();

            var adminId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _chatroomManager.UnsetAdmin(userModel, adminId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel)
        {
            return _chatroomManager.GetAllAdmins(chatModel);
        }
    }
}