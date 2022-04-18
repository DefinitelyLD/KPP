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
        public ActionResult<UserAccountCreateModel> AddToChatroom(int userId, int chatId)
        {
            return _chatroomManager.AddToChatroom(userId, chatId);
        }

        [HttpDelete]
        public ActionResult<bool> LeaveFromChatroom(int userAccountId)
        {
            return _chatroomManager.LeaveFromChatroom(userAccountId);
        }

        [HttpDelete]
        public ActionResult<bool> KickUser(int userAccountId)
        {
            return _chatroomManager.KickUser(userAccountId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> BanUser(int userId, int chatId)
        {
            return _chatroomManager.BanUser(userId, chatId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnbanUser(int userId, int chatId)
        {
            return _chatroomManager.UnbanUser(userId, chatId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> SetAdmin(int userId, int chatId)
        {
            return _chatroomManager.SetAdmin(userId, chatId);
        }

        [HttpPost]
        public ActionResult<UserAccountUpdateModel> UnsetAdmin(int userId, int chatId)
        {
            return _chatroomManager.UnsetAdmin(userId, chatId);
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel)
        {
            return _chatroomManager.GetAllAdmins(chatModel);
        }
    }
}