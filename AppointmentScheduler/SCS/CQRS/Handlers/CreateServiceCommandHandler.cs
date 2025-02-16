using CommonBase.Models;
using SCS.CQRS.Commands;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
   
    public class CreateServiceCommandHandler : CommandHandler<CreateServiceCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateServiceCommandHandler(ApplicationDbContext context, ILogger<CreateServiceCommandHandler> logger)
            : base(logger)
        {
            _context = context;
        }

        public override async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = new Service
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                DurationInMinutes = request.DurationInMinutes,
                CategoryId = request.CategoryId
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync(cancellationToken);

            return service.Id;
        }
    }
}
