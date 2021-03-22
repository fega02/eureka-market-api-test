using AWSMarket.BL.Entities.Security;
using AWSMarketAPI.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSMarketAPI.Services
{
    public interface IUserService
    {
        public string GetDescripcion();
        Task<AuthenticateJWT> AuthAsync(string name, string lastName, string email, string ipAddress);
         
     
        Task<int> Save(User us);
        User FindUser(string userName, string lastName, string email);
    }
}