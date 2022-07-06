using System.Threading.Tasks;

namespace Messenger.BLL.Managers.Interfaces
{
    public interface IEmailManager
    {
        public Task SendEmailAsync(string email, string subject, string message);

        public string RegistrationMessageTemplate(string userName, string url);
    }
}
