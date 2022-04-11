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

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<ChatUserProfile> ChatUserProfile { get; set; }
    }
}
