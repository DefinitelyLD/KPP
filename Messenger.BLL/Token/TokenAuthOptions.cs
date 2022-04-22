using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Messenger.BLL.Token
{
    public class TokenAuthOptions
    {
        public const string ISSUER = "Messenger";
        public const string AUDIENCE = "Messenger";
        const string KEY = "ultramegasuperduper_secretkey";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
