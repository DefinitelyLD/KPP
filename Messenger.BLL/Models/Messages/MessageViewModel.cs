using Messenger.BLL.MessageImage;
using Messenger.BLL.User;
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
        public UserViewModel User { get; set; }
        public ICollection<MessageImageViewModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
