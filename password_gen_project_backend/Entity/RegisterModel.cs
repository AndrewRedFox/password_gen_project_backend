using System.ComponentModel.DataAnnotations;

namespace password_gen_project_backend.Entity
{
    public class RegisterModel
    {
        public string login { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
}
