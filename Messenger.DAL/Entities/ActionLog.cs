using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Messenger.DAL.Entities
{
    public class ActionLog : BaseEntity<int>
    {
        public string ActionName { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
    }
}
