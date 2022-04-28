using Messenger.BLL.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.MessageImages
{
    public class MessageImageUpdateModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public MessageViewModel Message { get; set; }
    }
}
