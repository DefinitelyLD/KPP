using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public class Chat : BaseEntity<int>
    {
        [Required]
        public string Topic { get; set; }

        public string? Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<UserAccount> Users { get; set; }

        public virtual ChatImage Image { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsAdminsRoom { get; set; } = false;
    }
}
