using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace UMS.CQRS.Commands
{
    public class UpdateUserCommand : Command<Unit>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
