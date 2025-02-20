using CommonBase.Data;
using CommonBase.Models;
using MediatR;
using SCS.CQRS.Commands;
using SCS.CQRS.Queries;
using SCS.Data;

namespace SCS.Services
{
    public class ServiceCatalogServiceProvider : IServiceCatalogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public ServiceCatalogServiceProvider(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            var query = new GetAllServicesQuery();
            return await _mediator.Send(query);
        }

        public async Task<Service?> GetServiceByIdAsync(Guid id)
        {
            var query = new GetServiceByIdQuery { Id = id };
            return await _mediator.Send(query);
        }

        public async Task<Guid> CreateServiceAsync(Service service)
        {
            var command = new CreateServiceCommand
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes
            };

            return await _mediator.Send(command);
        }

        public async Task UpdateServiceAsync(Service service)
        {
            var command = new UpdateServiceCommand
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes
            };

            await _mediator.Send(command);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            var command = new DeleteServiceCommand { Id = id };
            await _mediator.Send(command);
        }

        public async Task<bool> ServiceExistsAsync(Guid id)
        {
            var query = new GetServiceByIdQuery { Id = id };
            return await _mediator.Send(query) != null;
        }
    }
}
