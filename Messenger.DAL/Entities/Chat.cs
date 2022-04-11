using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public class Chat : BaseEntity
    {
        [Required]
        public string Topic { get; set; }
        public string? Password { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<ChatUser> ChatUsers { get; set; }
    }
}
