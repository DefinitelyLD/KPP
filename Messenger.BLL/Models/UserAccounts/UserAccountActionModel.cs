using Messenger.BLL.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Models.UserAccounts
{
    public class UserAccountActionModel
    {
        public UserAccountViewModel user { get; set; }
        public UserAccountViewModel admin { get; set; }
    }
}
