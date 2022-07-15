using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public class Message : BaseEntity<int>
    {
        [Required]
        public virtual Chat Chat { get; set; }
        [Required]
        public int ChatId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual ICollection<MessageImage> Images { get; set; }
        public string Text { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
