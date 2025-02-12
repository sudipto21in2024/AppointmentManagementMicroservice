
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.CQRS.Commands;
using UMS.CQRS.Queries;

namespace UMS.Controllers
{
    [ApiController]
    [Route("[User]")]
    public class UserController : Controller
    {
        //private readonly IMediator _mediator;

        //public UserController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(CreateUserCommand command)
        //{
        //    var userId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUser(Guid id)
        //{
        //    var getUserByIdQuery = new GetUserByIdQuery { UserId = id };
        //    var user = await _mediator.Send(getUserByIdQuery);
        //    return Ok(user);
        //}

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var getUserByIdQuery = new GetUserByIdQuery{ UserId = id };
            var user = await _mediator.Send(getUserByIdQuery);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand command)
        {
            command.UserId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleteUserCommand = new DeleteUserCommand { UserId = id };
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }
    }
}
