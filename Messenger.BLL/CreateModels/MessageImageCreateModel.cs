using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.CreateModels
{
    public class MessageImageCreateModel
    {
        public string Path { get; set; }
        public MessageCreateModel Message { get; set; }

    }
}
