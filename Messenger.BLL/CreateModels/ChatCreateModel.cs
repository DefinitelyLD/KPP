using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.CreateModels
{
    public class ChatCreateModel
    {
        public string Topic { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<MessageCreateModel> Messages { get; set; }
        public virtual ICollection<UserAccountCreateModel> Users { get; set; }
    }
}
