using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace UMS.CQRS.Commands
{
    public class DeleteUserCommand : Command<Unit>
    {
        public Guid UserId { get; set; }
    }
}
