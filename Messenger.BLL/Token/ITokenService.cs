using Messenger.DAL.Entities;

namespace Messenger.BLL.Token
{
    public interface ITokenService
    {
        string BuildToken(User user);
    }
}
