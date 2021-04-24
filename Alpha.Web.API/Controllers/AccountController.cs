using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Security.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return Ok(model);
        }

        [HttpPost]
        [Route("api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return Ok();
        }
    }
}
