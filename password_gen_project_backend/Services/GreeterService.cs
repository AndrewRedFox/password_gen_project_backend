using Grpc.Core;
using password_gen_project_backend;
using password_gen_project_backend.Entity;

namespace password_gen_project_backend.Services
{
    public class GreeterService : Greeter.GreeterBase
    {       
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
                AccessToken = "",
                RefreshToken = "",
                Login = request.Login,
                ReplyCode = TokenReply.Types.StatusCode.IncorrectPassword,
            });
        }

        public override Task<InfoReply> UserGetInfo(InfoRequest request, ServerCallContext context)
        {
            AccountService accountService = new AccountService();
            string info = accountService.getInfo(request.AccessToken, request.RefreshToken, request.Login).Result;

            if (Equals(info, "Non"))
            {
                return Task.FromResult(new InfoReply
                {
                    List = info,
                    ReplyCode = InfoReply.Types.StatusCode.Unlogin,
                    AccessToken = "Non",
                    RefreshToken = "Non",
                });
            }

            return Task.FromResult(new InfoReply
            {
                List = info,
                ReplyCode = InfoReply.Types.StatusCode.Ok,
                AccessToken = accountService.getNewAccessToken().Result,
                RefreshToken = accountService.getNewRefreshToken().Result,
            });
        }

        public override Task<UpdateReply> UserUpdateInfo(UpdateRequest request, ServerCallContext context)
        {
            AccountService accountService = new AccountService();
            bool flag = accountService.updateInfo(request.AccessToken, request.RefreshToken, request.Login, request.List).Result;

            return Task.FromResult(new UpdateReply
            {
                AccessToken = accountService.getNewAccessToken().Result,
                RefreshToken = accountService.getNewRefreshToken().Result,
            });
        }
    }
}