
namespace password_gen_project_backend.Services
{
    public class AccountService
    {
        private AccessDB db = new AccessDB();
        private UserModel model = new UserModel();

        public string getListOfPassword()
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
