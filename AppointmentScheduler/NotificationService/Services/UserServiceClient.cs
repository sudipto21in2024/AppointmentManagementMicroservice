using Grpc.Core;
using Microsoft.AspNetCore.Http;
using NotificationService.Interfaces;
using UserService;

namespace NotificationService.Services
{
    public class UserServiceClient : IUserService
    {
        private readonly UserServiceRPC.UserServiceRPCClient _client; // Updated client type

        public UserServiceClient(ChannelBase channel)
        {
            _client = new UserServiceRPC.UserServiceRPCClient(channel); // Updated client type
        }

        public async Task<CommonBase.Models.User> GetUserByIdAsync(Guid id)
        {
            var request = new GetUserRequest { Id = id.ToString() };
            try
            {
                var response = await _client.GetUserAsync(request);
                var user = new CommonBase.Models.User // Your domain User class
                {
                    Id = Guid.Parse(response.Id),
                    FirstName = response.FirstName, // Use PascalCase
                    LastName = response.LastName, // Use PascalCase
                    Email = response.Email,
                };
                return user;
            }
            catch (RpcException ex)
            {
                // ... handle exception
                throw;
            }
        }

       
    }

}
