using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public class Message : BaseEntity
    {
        [Required]
        public Chat Chat { get; set; }
        [Required]
        public User User { get; set; }
        public IEnumerable<MessageImage> Images { get; set; }
        public string Text { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
