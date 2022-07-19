using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public class UserImage : BaseEntity<int>
    {
        [Required]
        public string Path { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }
    }
}
