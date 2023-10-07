using password_gen_project_backend.Entity;
using password_gen_project_backend.Helpers;

namespace password_gen_project_backend.Services
{
    public class AuthService
    {
        private AccessDB db = new AccessDB();
        private UserModel model = new UserModel();
        public async Task<bool> auth(string login, string password)
        {
            var user = await db.getUserByLogin(login);
            if(isAuth(user, password))
            {
                foreach (var item in user)
                { 
                    model = item;
                    return true;
                }  
            }
            return false;
        }

        private bool isAuth(List<UserModel>? user, string password)
        {
            foreach (var item in user)
            {
                if (Equals(HashPasswordHelper.HashPassword(password), item.password))
                    return true;
            }
            return false;
        }

        public UserModel GetUserModel() { return model; }

        public async void setAccessToken(string accessToken)
        {
            model.accessToken = accessToken;
            await db.updateList(model);
        }

        public async void setRefreshToken(string refreshToken)
        {
            model.refreshToken = refreshToken;
            await db.updateList(model);
        }
    }
}
