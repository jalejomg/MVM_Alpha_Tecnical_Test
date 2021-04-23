using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{    
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/users/")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.ListAsync());
        }

        [HttpGet]
        [Route("api/users/{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                return Ok(await _userService.GetByIdAsync(userId));
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [Route("api/users/")]
        public async Task<IActionResult> Create(UserModel user)
        {
            try
            {
                return Ok(await _userService.CreateAsync(user));
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
                return Ok(await _userService.UpdateAsync(userId, user));
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
                await _userService.DeleteAsync(userId);
                return Ok();
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
