using MediatR;
using SCS.CQRS.Commands;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : CommandHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<UpdateCategoryCommandHandler> logger)
            : base(logger)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);

            if (existingCategory == null)
            {
                _logger.LogWarning($"Category with ID: {request.Id} not found.");
                return Unit.Value;
            }

            existingCategory.Name = request.Name;
            existingCategory.Description = request.Description;

            await _categoryRepository.UpdateAsync(existingCategory);
            return Unit.Value;
        }
    }
}
