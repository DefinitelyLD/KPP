using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Entities
{
    public class ChatUserProfile
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsUserBanned { get; set; }
        public bool IsUserAdmin { get; set; }
        public bool IsUserOwner { get; set; }
    }
}
