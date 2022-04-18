using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.ViewModels.User
{
    public class DeleteUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
