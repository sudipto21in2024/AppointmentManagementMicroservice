using CommonBase.Exception;
using MediatR;
using PaymentService.CQRS.Commands;
using PaymentService.Data.Repositories;
using SharedLibrary.CQRS.Handlers;

namespace PaymentService.CQRS.Handlers
{
    
    public class UpdatePaymentStatusCommandHandler : CommandHandler<UpdatePaymentStatusCommand, Unit>
    {
        private readonly IPaymentRepository _paymentRepository;
        // ... logger

        public UpdatePaymentStatusCommandHandler(IPaymentRepository paymentRepository, ILogger<UpdatePaymentStatusCommandHandler> logger)
            : base(logger)
        {
            _paymentRepository = paymentRepository;
        }

        public override async Task<Unit> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByAppointmentIdAsync(request.AppointmentId);
            if (payment == null)
            {
                throw new EntityNotFoundException($"Payment for appointment {request.AppointmentId} not found.");
            }

            payment.Status = request.PaymentStatus;
            await _paymentRepository.UpdateAsync(payment);

            // ... any other logic (e.g., sending notifications)

            return Unit.Value;
        }
    }
}
