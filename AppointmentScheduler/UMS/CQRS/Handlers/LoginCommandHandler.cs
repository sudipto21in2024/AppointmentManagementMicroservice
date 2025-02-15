using CommonBase.Models;
using SharedLibrary.CQRS.Handlers;
using UMS.CQRS.Commands;
using UMS.Interfaces;

namespace UMS.CQRS.Handlers
{
    public class LoginCommandHandler : CommandHandler<LoginCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository, ILogger<LoginCommandHandler> logger)
            : base(logger)
        {
            _userRepository = userRepository;
        }

        public override async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // In a real-world scenario, you would hash and compare passwords securely
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || CommonBase.Auth.PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        
    }
}
