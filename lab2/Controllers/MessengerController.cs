using E2EEMessenger.Models;
using E2EEMessenger.Services;
using Microsoft.AspNetCore.Mvc;

namespace E2EEMessenger.Controllers
{
    [ApiController]
    [Route("api")]
    public class MessengerController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessengerController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost("users")]
        public IActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var user = _service.CreateUser(dto);
                return Ok(user);
            }
            catch (ArgumentException ex) { return BadRequest(new { Error = ex.Message }); }
        }

        [HttpPost("conversations")]
        public IActionResult CreateConversation()
        {
            var conv = _service.CreateConversation();
            return Ok(conv);
        }

        [HttpPost("messages")]
        public IActionResult SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                var msg = _service.SendMessage(dto);
                return Ok(msg);
            }
            catch (ArgumentException ex) { return BadRequest(new { Error = ex.Message }); }
        }

        [HttpGet("conversations/{id}/messages")]
        public IActionResult GetMessages(string id)
        {
            try
            {
                var messages = _service.GetMessageHistory(id);
                return Ok(messages);
            }
            catch (ArgumentException ex) { return NotFound(new { Error = ex.Message }); }
        }
    }
}