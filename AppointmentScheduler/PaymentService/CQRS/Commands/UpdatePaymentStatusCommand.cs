using CommonBase.Infrastructure.CQRS.BaseClasses;
using MediatR;

namespace PaymentService.CQRS.Commands
{
    
    public class UpdatePaymentStatusCommand : Command<Unit>
    {
        public Guid AppointmentId { get; set; }
        public string PaymentStatus { get; set; } // "Pending", "Paid", etc.
    }
}
