using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Services;
using Alpha.Web.API.Security.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAspNetUsersService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(
            IAccountService accountService,
            IAspNetUsersService userService,
            IConfiguration configuration)
        {
            _accountService = accountService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _accountService.LoginAsync(model);
            if (result.Succeeded)
            {
                return Ok(model);
            }

            return BadRequest("Email or password incorrect.");
        }

        [HttpPost]
        [Route("api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("api/account/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            var user = await _userService.GetByIdAsync(model.Username);

            if (user != null)
            {
                var result = await _accountService.ValidatePasswordAsync(user, model.Password);

                if (result.Succeeded)
                {
                    Claim[] claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                    SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken token = new JwtSecurityToken(
                        _configuration["Tokens:Issuer"],
                        _configuration["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(99),
                        signingCredentials: credentials);
                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        user
                    };

                    return Ok(results);
                }
            }
            return BadRequest();
        }

    }
}
