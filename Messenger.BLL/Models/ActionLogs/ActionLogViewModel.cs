using Messenger.BLL.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models.ActionLogs
{
    public class ActionLogViewModel
    {
        public int Id { get; set; }

        public string ActionName { get; set; }

        public UserViewModel User { get; set; }

        public DateTime Time { get; set; }
    }
}
