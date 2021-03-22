using AWSMarketAPI.Security;
using AWSMarketAPI.Utils;

namespace AWSMarketAPI.Models
{
    public class AuthenticateResponse : ResponseBase
    {
        public AuthenticateJWT Authenticate { get; set; }

    }
}
