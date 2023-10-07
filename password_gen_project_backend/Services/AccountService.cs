
namespace password_gen_project_backend.Services
{
    public class AccountService
    {
        private AccessDB db = new AccessDB();
        private UserModel model = new UserModel();

        public async Task<string> getInfo(string accessToken, string refreshToken, string login)
        {
            var users = await db.getUserByLogin(login);
            TokensController tokensController = new TokensController();
            foreach (var user in users)
            {
                model = user;
            }

            if(tokensController.validate(accessToken, model.login, model.accessToken))
                return getListOfPassword();
            else if(tokensController.validate(refreshToken, model.login, model.refreshToken))
            {
                CreateTokens createTokens = new CreateTokens();
                /*string newAccessToken = createTokens.createAccessToken(model);
                string newRefreshToken = createTokens.createRefreshToken(model);*/
                model.accessToken = getNewAccessToken(createTokens);
                model.refreshToken = getNewRefreshToken(createTokens);
                await db.updateList(model);

                return getListOfPassword();
            }
            return "Non";
        }

        public string getNewAccessToken(CreateTokens createTokens)
        {
            return createTokens.createAccessToken(model);
        }

        public string getNewRefreshToken(CreateTokens createTokens)
        {
            return createTokens.createRefreshToken(model);
        }

        public async Task<string> getNewAccessToken()
        {
            return model.accessToken;
        }

        public async Task<string> getNewRefreshToken()
        {
            return model.refreshToken;
        }

        private string getListOfPassword()
        {
            string list = "";
            foreach (var item in model.listOfPassword)
            {
                list += "." + item.First + "/" + item.Second;//stringBuilder use
            }
            return list;
        }
    }
}
