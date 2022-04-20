using Messenger.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.BLL.Users;

namespace Messenger.BLL.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public ICollection<MessageImageModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
