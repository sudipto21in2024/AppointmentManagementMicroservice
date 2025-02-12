using MediatR;

namespace UMS.Events
{
    public class UserRegisteredEvent : INotification
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
