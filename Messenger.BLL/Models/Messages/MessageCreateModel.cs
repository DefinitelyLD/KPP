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
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public IFormFileCollection Files { get; set; }
        public string Text { get; set; }
    }
}
