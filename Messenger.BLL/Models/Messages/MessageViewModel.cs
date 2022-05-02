using Messenger.BLL.Chats;
using Messenger.BLL.MessageImages;
using Messenger.BLL.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Messages
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public ChatViewModel Chat { get; set; }
        public UserViewModel User { get; set; }
        public IFormFileCollection Files { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
