using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Messenger.BLL.Token
{
    public class TokenAuthOptions
    {
        const string KEY = "ultramegasuperduper_secretkey";
        public const int LIFETIME = 7;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
