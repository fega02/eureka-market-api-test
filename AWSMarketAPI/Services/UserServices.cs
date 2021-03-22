using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using AWSMarketAPI.Security;
using AWSMarket.BL.Entities.Security;
using AWSMarketAPI.Configs;

namespace AWSMarketAPI.Services
{
    public class UserServices : IUserService
    {
        private DataContext _context;
        private readonly AppSettings _appSettings;

        public UserServices(
            DataContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateJWT> AuthAsync(string name, string lastName, string email, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Name == name && x.LastName == lastName && x.Email == email);

            // return null if user not found
            if (user == null) return null;
            // user.RefreshTokens.Clear();
            _context.Update(user);
            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(ipAddress);
            //refreshToken.Usuario = user;
            // save refresh token

            user.RefreshTokens.Add(refreshToken);
            _context.Update(user);
            _context.SaveChanges();

            return new AuthenticateJWT(user, jwtToken, refreshToken.Token);
        }


        public async Task<int> Save(User us)
        {
            _context.Users.Add(us);
            return await _context.SaveChangesAsync();

        }

        public User FindUser(string userName, string lastName, string email)
        {
            return _context.Users.SingleOrDefault(u => u.Name == userName && u.LastName == lastName && u.Email == email);

        }



        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.SessionTimeoutMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public string GetDescripcion()
        {
            string var = "Mi descripcion";
            return var;
        }
    }
}
