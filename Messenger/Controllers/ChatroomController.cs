using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult<ChatModel> CreateChatroom(ChatModel chat)
        {
            return _chatroomManager.CreateChatroom(chat);
        }
    }
}