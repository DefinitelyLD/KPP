using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.UserAccounts;
using Messenger.BLL.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var admin = HttpContext.User.Identity.Name;
            return _chatroomManager.KickUser(userModel, admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser(UserAccountViewModel userModel)
        {
            var admin = HttpContext.User.Identity.Name;
            return _chatroomManager.BanUser(userModel, admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser(UserAccountViewModel userModel)
        {
            var admin = HttpContext.User.Identity.Name;
            return _chatroomManager.UnbanUser(userModel, admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin(UserAccountViewModel userModel)
        {
            var admin = HttpContext.User.Identity.Name;
            return _chatroomManager.SetAdmin(userModel, admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin(UserAccountViewModel userModel)
        {
            var admin = HttpContext.User.Identity.Name;
            return _chatroomManager.UnsetAdmin(userModel, admin);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId)
        {
            var userName = HttpContext.User.Identity.Name;
            return _chatroomManager.GetAllBannedUsers(chatId, userName);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId)
        {
            var userName = HttpContext.User.Identity.Name;
            return _chatroomManager.GetAllAdmins(chatId, userName);
        }

        [HttpGet]
        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId)
        {
            var userName = HttpContext.User.Identity.Name;
            return _chatroomManager.GetAllUsers(chatId, userName);
        }
    }
}