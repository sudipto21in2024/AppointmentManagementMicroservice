using CommonBase.Infrastructure.CQRS.BaseClasses;

namespace UMS.CQRS.Commands
{
    public class CreateUserCommand : Command<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
