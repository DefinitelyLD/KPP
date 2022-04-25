using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Chats
{
    public class ChatUpdateModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string? Password { get; set; }
    }
}
