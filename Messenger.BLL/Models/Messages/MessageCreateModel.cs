using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.BLL.Chats;
using Messenger.BLL.User;
using Messenger.BLL.MessageImage;

namespace Messenger.BLL.Messages
{
    public class MessageCreateModel
    {
        public ChatViewModel Chat { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<MessageImageViewModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
