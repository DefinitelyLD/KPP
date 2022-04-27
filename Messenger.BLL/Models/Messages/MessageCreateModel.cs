using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.BLL.Chats;
using Messenger.BLL.Users;
using Messenger.BLL.MessageImages;
using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Messages
{
    public class MessageCreateModel
    {
        public ChatViewModel Chat { get; set; }
        public UserViewModel User { get; set; }
        public IFormFileCollection Files { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
