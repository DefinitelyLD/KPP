using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class MessageModel
    {
        public ChatModel Chat { get; set; }
        public UserModel User { get; set; }
        public ICollection<MessageImageModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
