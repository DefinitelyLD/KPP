using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class ChatModel
    {
        public string Topic { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<MessageModel> Messages { get; set; }
        public virtual ICollection<UserAccountModel> Users { get; set; }
    }
}
