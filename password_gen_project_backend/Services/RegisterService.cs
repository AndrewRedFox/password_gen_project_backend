using password_gen_project_backend.Entity;
using password_gen_project_backend.Helpers;

namespace password_gen_project_backend.Services
{
    public class RegisterService
    {
        private AccessDB db = new AccessDB();
        public async Task<bool> register(RegisterModel registerModel)
        {
            AccessDB db = new AccessDB();
            //int id = await db.getIdDB();
            var loginExists = await db.getUserByLogin(registerModel.login);

            if (loginExists.Count != 0)
                return false;
            if(registerModel.password != registerModel.passwordConfirm)
                return false;
            
            await db.createUser(new UserModel(Id.get(), registerModel.login, HashPasswordHelper.HashPassword(registerModel.password)) { });
            return true;
        }

    
    }
}
