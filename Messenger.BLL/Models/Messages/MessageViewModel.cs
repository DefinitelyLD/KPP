using Messenger.BLL.MessageImages;
using Messenger.BLL.Models;
using Messenger.BLL.Users;
using System;
using System.Collections.Generic;

namespace Messenger.BLL.Messages
{
    public class MessageViewModel : BaseModel<int>
    {
        public UserViewModel User { get; set; }
        public ICollection<MessageImageViewModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
