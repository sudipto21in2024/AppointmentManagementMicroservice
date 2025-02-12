using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SharedLibrary.CQRS.Handlers
{
    public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {
        protected readonly ILogger<CommandHandler<TCommand, TResponse>> _logger;

        protected CommandHandler(ILogger<CommandHandler<TCommand, TResponse>> logger)
        {
            _logger = logger;
        }

        public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
    }
}
