using Grpc.Core;
using password_gen_project_backend;
using password_gen_project_backend.Entity;

namespace password_gen_project_backend.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        UserModel _user;
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<RegisterReply> UserRegister(RegisterRequest request, ServerCallContext context)
        {
            RegisterService registerService = new RegisterService();
            bool registerSuccess = registerService.register(new RegisterModel(request.Login, request.Password, request.PasswordConfirm)).Result;

            if (registerSuccess)
            {
                return Task.FromResult(new RegisterReply
                {
                    ReplyCode = RegisterReply.Types.StatusCode.Ok,
                });
            }
            return Task.FromResult(new RegisterReply
            {
                ReplyCode = RegisterReply.Types.StatusCode.LoginAlreadyUse,
            });
        }

        public override Task<TokenReply> UserAuth(UserDataRequest request, ServerCallContext context)
        {
            AuthService authService = new AuthService();
            bool isAuth = authService.auth(request.Login, request.Password).Result;
            if (isAuth)
            {
                CreateTokens createTokens = new CreateTokens();
                string accessToken = createTokens.createAccessToken(authService.GetUserModel());
                string refreshToken = createTokens.createRefreshToken(authService.GetUserModel());
                authService.setAccessToken(accessToken);
                authService.setRefreshToken(refreshToken);
                return Task.FromResult(new TokenReply
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Login = request.Login,
                    ReplyCode = TokenReply.Types.StatusCode.Ok,
                });
            }
            return Task.FromResult(new TokenReply
            {
                AccessToken = null,
                RefreshToken = null,
                Login = request.Login,
                ReplyCode = TokenReply.Types.StatusCode.IncorrectPassword,
            });

        }
    }
}