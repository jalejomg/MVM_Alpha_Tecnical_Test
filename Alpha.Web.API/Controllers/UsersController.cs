using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    /// <summary>
    /// This class contain all APIs endpoints about Users 
    /// </summary>
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IAspNetUsersService _aspNetUsersService;

        public UsersController(IAspNetUsersService aspNetUsersService)
        {
            _aspNetUsersService = aspNetUsersService;
        }

       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("api/users/")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _aspNetUsersService.ListAsync());
        }

        [HttpGet]
        [Route("api/users/{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                return Ok(await _aspNetUsersService.GetByIdAsync(userId));
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [Route("api/users/")]
        public async Task<IActionResult> Add(string password, UserModel user)
        {
            try
            {
                return Ok(await _aspNetUsersService.AddAsync(user, password));
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        [Route("api/users/{userId}")]
        public async Task<IActionResult> Update(string userId, UserModel user)
        {
            try
            {
                return Ok(await _aspNetUsersService.UpdateAsync(userId, user));
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Route("api/users/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _aspNetUsersService.DeleteAsync(userId);
                return Ok();
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
