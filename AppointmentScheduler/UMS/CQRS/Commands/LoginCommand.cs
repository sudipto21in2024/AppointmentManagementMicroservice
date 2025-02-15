using CommonBase.Infrastructure.CQRS.BaseClasses;
using CommonBase.Models;

namespace UMS.CQRS.Commands
{
    public class LoginCommand : Command<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
