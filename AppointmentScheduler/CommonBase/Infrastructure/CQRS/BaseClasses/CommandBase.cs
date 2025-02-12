using MediatR;
using System;

namespace CommonBase.Infrastructure.CQRS.BaseClasses
{
    public abstract class Command<TResponse> : IRequest<TResponse> where TResponse : notnull
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}
