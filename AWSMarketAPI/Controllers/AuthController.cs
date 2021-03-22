using System;
using System.Threading.Tasks;
using AWSMarket.BL.Entities.Security;
using AWSMarketAPI.Controllers.Base;
using AWSMarketAPI.Models;
using AWSMarketAPI.Security;
using AWSMarketAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWSMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : StockBaseController
    {
        public IUserService userService;
        public AuthController(IUserService userServ, ILogger<AuthController> logger) : base(logger)
        {
            this.userService = userServ;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest model)
        {
            try
            {
                var response = new AuthenticateResponse();
                AuthenticateJWT auth = await userService.AuthAsync(model.Name, model.LastName, model.Email, ipAddress());
                response.Authenticate = auth;
                if (auth == null)
                    return BadRequest(new { message = "The Name, LastName or Email is wrong" });

                setTokenCookie(auth.RefreshToken);

                response.Authenticate = auth;
                response.Code = 200;
                response.Success = true;


                _logger.LogInformation("User is logued:" + response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occurred on " + ex);
                throw;
            }

        }


        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SinginAsync(SignupRequest request)
        {
            try
            {
                var response = new SignupResponse();
                var user = userService.FindUser(request.Name, request.LastName, request.Email);
                if (user != null)
                {
                    response.Code = 202;
                    response.Message = "User already exist.";
                    return Ok(response);
                }

                user = new User { Name = request.Name, LastName = request.LastName, Email = request.Email };
                await userService.Save(user);
                response.Success = true;
                response.Message = "The user has successfully registered.";
                response.Code = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occurred on " + ex);
                throw;
            }

        }


        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
