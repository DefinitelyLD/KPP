using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string ChatId { get; set; }
        public UserModel User { get; set; }
        public string Text { get; set; }
        public string ImageId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
