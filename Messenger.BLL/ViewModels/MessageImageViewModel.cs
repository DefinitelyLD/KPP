using Messenger.BLL.CreateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.ViewModels
{
    public class MessageImageViewModel
    {
        public int Id { get; set; }    
        public string Path { get; set; }
        public MessageCreateModel Message { get; set; }
    }
}
