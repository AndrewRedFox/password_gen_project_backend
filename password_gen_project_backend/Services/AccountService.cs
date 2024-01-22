
using Microsoft.IdentityModel.Tokens;

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
            if (users.IsNullOrEmpty())
            {
                return "Non";
            }
            model = users[0];

            if (tokensController.validate(accessToken, model.login, model.accessToken))
                return getListOfPassword();
            else if(tokensController.validate(refreshToken, model.login, model.refreshToken))
            {
                CreateTokens createTokens = new CreateTokens();
                model.accessToken = getNewAccessToken(createTokens);
                model.refreshToken = getNewRefreshToken(createTokens);
                await db.updateList(model);

                return getListOfPassword();
            }
            return "Non";
        }

        public async Task<bool> updateInfo(string accessToken, string refreshToken, string login, string list)
        {
            var users = await db.getUserByLogin(login);
            TokensController tokensController = new TokensController();
            model = users[0]; 

            if (tokensController.validate(accessToken, model.login, model.accessToken))
            {
                model.listOfPassword = updateList(list);
                await db.updateList(model);
            }
            else if (tokensController.validate(refreshToken, model.login, model.refreshToken))
            {
                CreateTokens createTokens = new CreateTokens();
                model.accessToken = getNewAccessToken(createTokens);
                model.refreshToken = getNewRefreshToken(createTokens);
                model.listOfPassword = updateList(list);
                await db.updateList(model);
            }
            return true;
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
                list += "." + item.First + "/" + item.Second + "/" + item.Third;//stringBuilder use
            }
            return list;
        }

        private List<Pair<string,string, string>> updateList(string list)
        {
            List<Pair<string,string, string>> listOfPair = new List<Pair<string, string, string>>();
            var l = list.Split('.').ToList();
            l.RemoveAll(s => s == "");

            foreach(var pair in l)
            {
                Pair<string, string, string> p = new Pair<string, string, string>();
                string[] words = pair.Split(new char[] { '/' });
                p.First = words[0];
                p.Second = words[1];
                p.Third = words[2];
                listOfPair.Add(p);
            }
            return listOfPair;
        }
    }
}
