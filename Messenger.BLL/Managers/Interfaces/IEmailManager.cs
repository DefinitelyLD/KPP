using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers.Interfaces
{
    interface IEmailManager
    {
        public Task SendEmailAsync(string email, string subject, string message);

        public string RegistrationMessageTemplate(string userName, string url);
    }
}
