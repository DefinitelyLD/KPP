using Messenger.BLL.Chats;
using Messenger.BLL.Managers;
using Messenger.BLL.Models.UserAccounts;
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
        public ActionResult<bool> KickUser(UserAccountActionModel actionModel)
        {
            return _chatroomManager.KickUser(actionModel.user, actionModel.admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser(UserAccountActionModel actionModel)
        {
            return _chatroomManager.BanUser(actionModel.user, actionModel.admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser(UserAccountActionModel actionModel)
        {
            return _chatroomManager.UnbanUser(actionModel.user, actionModel.admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin(UserAccountActionModel actionModel)
        {
            return _chatroomManager.SetAdmin(actionModel.user, actionModel.admin);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin(UserAccountActionModel actionModel)
        {
            return _chatroomManager.UnsetAdmin(actionModel.user, actionModel.admin);
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