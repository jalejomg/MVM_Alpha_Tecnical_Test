using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alpha.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet]
        [Route("api/messages/")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _messagesService.ListAsync());
        }

        [HttpGet]
        [Route("api/messages/{messageId:int}")]
        public async Task<IActionResult> GetById(int messageId)
        {
            try
            {
                return Ok(await _messagesService.GetByIdAsync(messageId));
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [Route("api/messages/")]
        public async Task<IActionResult> Create(MessageModel user)
        {
            try
            {
                return Ok(await _messagesService.CreateAsync(user));
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        [Route("api/messages/{userId:int}")]
        public async Task<IActionResult> Update(int userId, MessageModel user)
        {
            try
            {
                return Ok(await _messagesService.UpdateAsync(userId, user));
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Route("api/messages/{messageId:int}")]
        public async Task<IActionResult> Delete(int messageId)
        {
            try
            {
                await _messagesService.DeleteAsync(messageId);
                return Ok();
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        [Route("api/messages/{messageId:int}/send")]
        public async Task<IActionResult> Send(int messageId)
        {
            try
            {
                await _messagesService.SendAsync(messageId);
                return Ok();
            }
            catch (NotFoundCustomException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
