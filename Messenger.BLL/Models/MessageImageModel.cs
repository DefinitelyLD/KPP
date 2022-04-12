using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class MessageImageModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public MessageModel Message { get; set; }

    }
}
