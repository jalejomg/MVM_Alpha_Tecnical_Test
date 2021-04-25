using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Security.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
    }
}
