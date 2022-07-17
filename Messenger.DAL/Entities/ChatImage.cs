using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public class ChatImage : BaseEntity<int>
    {
        [Required]
        public string Path { get; set; }

        public virtual Chat Chat { get; set; }

        public int ChatId { get; set; } 
    }
}
