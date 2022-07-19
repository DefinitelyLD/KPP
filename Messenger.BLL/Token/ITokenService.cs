using Messenger.DAL.Entities;
using System.Threading.Tasks;

namespace Messenger.BLL.Token
{
    public interface ITokenService
    {
        Task<string> BuildTokenAsync(User user);
    }
}
