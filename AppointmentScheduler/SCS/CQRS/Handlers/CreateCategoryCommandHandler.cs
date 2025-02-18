using CommonBase.Models;
using SCS.CQRS.Commands;
using SCS.Data;
using SharedLibrary.CQRS.Handlers;

namespace SCS.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : CommandHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ILogger<CreateCategoryCommandHandler> logger)
            : base(logger)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            var categoryId = await _categoryRepository.CreateAsync(category);
            return categoryId;
        }
    }
}
