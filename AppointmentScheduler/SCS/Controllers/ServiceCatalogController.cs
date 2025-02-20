using CommonBase.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SCS.CQRS.Commands;
using SCS.CQRS.Queries;

namespace SCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceCatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        //{
        //    var query = new GetAllServicesQuery();
        //    var services = await _mediator.Send(query);
        //    return Ok(services);
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices(
    int pageIndex = 0,
    int pageSize = 10,
    string? keyword = null,
    Guid? categoryId = null)
        {
            var query = new GetAllServicesQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Keyword = keyword,
                CategoryId = categoryId
            };
            var services = await _mediator.Send(query);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
            var query = new GetServiceByIdQuery { Id = id };
            var service = await _mediator.Send(query);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateService(CreateServiceCommand command)
        {
            var serviceId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetService), new { id = serviceId }, serviceId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, UpdateServiceCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var command = new DeleteServiceCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
