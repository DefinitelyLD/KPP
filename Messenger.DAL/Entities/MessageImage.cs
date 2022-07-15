using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public class MessageImage : BaseEntity<int>
    {
        [Required]
        public string Path { get; set; }
        public virtual Message Message { get; set; }
        public int MessageId { get; set; } 
    }
}
