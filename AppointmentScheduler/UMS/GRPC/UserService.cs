using CommonBase.Models;
using Microsoft.AspNetCore.Http;
using UMS.Interfaces;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserService;
using User = UserService.User;


namespace UserManagementService.GRPC
{
    public class UserService : UserServiceRPC.UserServiceRPCBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public override async Task<User> GetUser(GetUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Received request to fetch user with ID: {request.Id}");
            if (!Guid.TryParse(request.Id, out var userId))
            {
                _logger.LogWarning($"Invalid GUID format for ID: {request.Id}");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid user ID format."));
            }

            var user = await _userRepository.GetByIdAsync(userId);
            //var user = await _userRepository.GetByIdAsync((Guid)request.Id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {request.Id} not found.");
                throw new RpcException(new Status(StatusCode.NotFound, "User not found."));
            }

            var grpcUser = new User
            {
                Id = user.Id.ToString(), // Convert Guid to string
                FirstName = user.FirstName, // Ensure correct mapping
                LastName = user.LastName,
                Email = user.Email
                // Add other mapped properties here if needed
            };

            _logger.LogInformation($"Successfully retrieved user: {grpcUser.Id}");

            return grpcUser;
        }
    }
}
