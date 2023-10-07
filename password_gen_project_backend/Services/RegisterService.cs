using password_gen_project_backend.Entity;
using password_gen_project_backend.Helpers;

namespace password_gen_project_backend.Services
{
    public class RegisterService
    {
        private AccessDB db = new AccessDB();
        private static int id = 0;
        public async Task<bool> register(RegisterModel registerModel)
        {
            AccessDB db = new AccessDB();
            var loginExists = await db.getUserByLogin(registerModel.login);

            if (loginExists.Count != 0)
                return false;
            if(registerModel.password != registerModel.passwordConfirm)
                return false;
            
            await db.createUser(new UserModel(id, registerModel.login, HashPasswordHelper.HashPassword(registerModel.password)) { });
            id += 1;
            return true;
        }

    
    }
}
