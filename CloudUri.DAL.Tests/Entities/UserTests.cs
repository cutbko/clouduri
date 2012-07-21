using System.Linq;
using CloudUri.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudUri.DAL.Tests.Entities
{
    /// <summary>
    /// Test class for the <see cref="User"/> class
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// Tests the User constructor
        /// </summary>
        [TestMethod]
        public void UserEntityDefaultCtorInitializeRolesTest()
        {
            var userEntity = new User();
            Assert.IsNotNull(userEntity.Roles);
            Assert.AreEqual(0, userEntity.Roles.Count);
        }

        /// <summary>
        /// Tests password setter
        /// </summary>
        [TestMethod]
        public void UserPasswordSetterSetsPassAndSaltTest()
        {
            const string password = "Password";
            
            var userEntity = new User();
            // After initializer PasswordHash and Salt must be null
            Assert.IsNull(userEntity.PasswordHash);
            Assert.IsNull(userEntity.Salt);

            // Password setter must calculates hash and salt
            userEntity.Password = password;
            Assert.IsNotNull(userEntity.PasswordHash);
            Assert.IsNotNull(userEntity.Salt);
        }
    }
}