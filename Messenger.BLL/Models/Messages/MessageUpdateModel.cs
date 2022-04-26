using Messenger.BLL.MessageImages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Messages
{
    public class MessageUpdateModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<MessageImageViewModel> Images { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
