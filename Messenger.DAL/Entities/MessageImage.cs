using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public class MessageImage : BaseEntity
    {
        [Required]
        public string Path { get; set; }
        public Message Message { get; set; }
    }
}
