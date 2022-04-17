using Messenger.BLL.CreateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<MessageCreateModel> Messages { get; set; }
        public virtual ICollection<UserAccountCreateModel> Users { get; set; }
    }
}
