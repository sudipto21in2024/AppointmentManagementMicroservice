using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SharedLibrary.CQRS.Handlers
{
    public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : Query<TResponse>
    {
        protected readonly ILogger<QueryHandler<TQuery, TResponse>> _logger;

        protected QueryHandler(ILogger<QueryHandler<TQuery, TResponse>> logger)
        {
            _logger = logger;
        }

        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }
}