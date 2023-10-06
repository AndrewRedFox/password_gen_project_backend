using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using password_gen_project_backend.DataAcess;
using password_gen_project_backend.Helpers;

namespace password_gen_project_backend.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestToken()
        {
            //var token = AuthService.GenerateJWT();
            //Console.WriteLine(token);
        }

        [TestMethod]
        public void TestHashedPassword()
        {
            var password = "123abc";
            var hash = "dd130a849d7b29e5541b05d2f7f86a4acd4f1ec598c1c9438783f56bc4f0ff80";
            Assert.AreEqual(HashPasswordHelper.HashPassword(password), hash);
        }

        [TestMethod]
        public void TestAccessDB()
        {
           /* AccessDB db = new AccessDB();
            db.createUser(new UserModel() { login = "loginTest", password = "passwordTest"});
            var result = db.GetAllUsers();
            var excepted = "loginTest";
            string a = "";
            foreach (var user in result)
            {
                a=user.login;
            }
            Assert.AreEqual(a, excepted);*/

        }

    }
}
