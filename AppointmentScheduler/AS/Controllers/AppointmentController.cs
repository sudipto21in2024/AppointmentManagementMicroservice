using AS.CQRS.Commands;
using AS.CQRS.Queries;
using CommonBase.Exception;
using CommonBase.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IMediator mediator, ILogger<AppointmentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments(
         int pageIndex = 0,
         int pageSize = 10,
         DateTime? startDate = null,
         DateTime? endDate = null,
         Guid? userId = null)
        {
            try
            {
                var query = new GetAllAppointmentsQuery
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    UserId = userId
                };
                var appointments = await _mediator.Send(query);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting appointments.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            try
            {
                var query = new GetAppointmentByIdQuery { Id = id };
                var appointment = await _mediator.Send(query);

                if (appointment == null)
                {
                    return NotFound(); // 404 Not Found
                }

                return Ok(appointment); // 200 OK
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting appointment with ID: {id}.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAppointment(CreateAppointmentCommand command)
        {
            try
            {
                var appointmentId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAppointment), new { id = appointmentId }, appointmentId);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid appointment request: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the appointment.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, UpdateAppointmentCommand command)
        {
            try
            {
                command.Id = id;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid appointment update request: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating appointment with ID: {id}.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(Guid id)
        {
            try
            {
                var command = new CancelAppointmentCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while canceling appointment with ID: {id}.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            try
            {
                var command = new ConfirmAppointmentCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 Not Found
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while confirming appointment with ID: {id}.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
