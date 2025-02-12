using MediatR;
using System;

namespace CommonBase.Infrastructure.CQRS.BaseClasses
{
    public abstract class Query<TResponse> : IRequest<TResponse> where TResponse : notnull
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}