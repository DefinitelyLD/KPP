using Messenger.BLL.Managers;
using Messenger.BLL.CreateModels;
using Messenger.BLL.UpdateModels;
using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.ViewModels;
using System.Collections.Generic;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatroomController : Controller
    {
        private readonly IChatroomManager _chatroomManager;

        public ChatroomController(IChatroomManager chatroomManager)
        {
            _chatroomManager = chatroomManager;
        }

        public ActionResult<ChatCreateModel> CreateChatroom(ChatCreateModel chat)
        {
            return _chatroomManager.CreateChatroom(chat);
        }

        public ActionResult<ChatUpdateModel> EditChatroom(ChatUpdateModel chat)
        {
            return _chatroomManager.EditChatroom(chat);
        }

        public ActionResult<bool> DeleteChatroom(int chatId)
        {
            return _chatroomManager.DeleteChatroom(chatId);
        }

        public ActionResult<ChatViewModel> GetChatroom(int chatId)
        {
            return _chatroomManager.GetChatroom(chatId);
        }

        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            return _chatroomManager.GetAllChatrooms();
        }

        public ActionResult<UserAccountCreateModel> AddToChatroom(int userId, int chatId)
        {
            return _chatroomManager.AddToChatroom(userId, chatId);
        }

        public ActionResult<bool> LeaveFromChatroom(int userAccountId)
        {
            return _chatroomManager.LeaveFromChatroom(userAccountId);
        }

        public ActionResult<bool> KickUser(int userAccountId)
        {
            return _chatroomManager.KickUser(userAccountId);
        }

        public ActionResult<UserAccountUpdateModel> BanUser(int userId, int chatId)
        {
            return _chatroomManager.BanUser(userId, chatId);
        }

        public ActionResult<UserAccountUpdateModel> SetAdmin(int userId, int chatId)
        {
            return _chatroomManager.SetAdmin(userId, chatId);
        }

        public ActionResult<UserAccountUpdateModel> UnsetAdmin(int userId, int chatId)
        {
            return _chatroomManager.UnsetAdmin(userId, chatId);
        }

        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel)
        {
            return _chatroomManager.GetAllAdmins(chatModel);
        }
    }
}