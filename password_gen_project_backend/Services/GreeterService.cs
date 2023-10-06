using Grpc.Core;
using password_gen_project_backend;

namespace password_gen_project_backend.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        UserModel _user;
        /*private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }*/

        public override Task<RegisterReply> UserRegister(RegisterRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RegisterReply
            {
                
            });
        }

        /*public override Task<RegisterReply> UserRegister(RegisterRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RegisterReply
            {

            });
        }*/
    }
}