using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.DAL;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;
using CloudUri.DAL.Repository;
using CloudUri.SAL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CloudUri.SAL.Tests.Services
{
    [TestClass]
    public class AccountServiceTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private IDALContext _dalContext;
        private IUsersRepository _usersRepository;
        private IRolesRepository _rolesRepository;


        [TestInitialize]
        public void Initialise()
        {
            _dalContext = _mockRepository.StrictMock<IDALContext>();
            _usersRepository = _mockRepository.StrictMock<IUsersRepository>();
            _rolesRepository = _mockRepository.StrictMock<IRolesRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTestArgNull()
        {
            new AccountService(null);
        }

        [TestMethod]
        public void ConstructorTestSuccess()
        {
            new AccountService(_dalContext);
        }

        [TestMethod]
        public void ValidateUserSuccessTest()
        {
            const string Name = "user";
            const string Password = "pass";
            User user = new User();
            List<Role> roles = new List<Role>
                {
                    new Role
                        {
                            Key = 1,
                            Name = "Vassas",
                            Description = "Blah"
                        }
                };
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _dalContext.Expect(x => x.RolesRepository).Return(_rolesRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByNameAndPassword(Name, Password)).Return(user);
            _rolesRepository.Expect(x => x.GetRolesForUser(Name)).Return(roles).Repeat.Once();

            _mockRepository.ReplayAll();
            User actual = accountService.ValidateUser(Name, Password, out message);

            Assert.AreEqual(user, actual);
            CollectionAssert.AreEqual(roles, user.Roles.ToList());
            Assert.IsNull(message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ValidateUserNotFoundTest()
        {
            const string Name = "user";
            const string Password = "pass";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByNameAndPassword(Name, Password)).Return(null);
            _mockRepository.ReplayAll();

            User actual = accountService.ValidateUser(Name, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.UserNotFoundMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ValidateUserDALErrorTest()
        {
            const string Name = "user";
            const string Password = "pass";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByNameAndPassword(Name, Password)).Throw(new DbException("An error"));
            _mockRepository.ReplayAll();

            User actual = accountService.ValidateUser(Name, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringReadingUserMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetUserByNameSuccessTest()
        {
            const string Name = "user";
            User user = new User();
            List<Role> roles = new List<Role>
                {
                    new Role
                        {
                            Key = 1,
                            Name = "Vassas",
                            Description = "Blah"
                        }
                };
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _dalContext.Expect(x => x.RolesRepository).Return(_rolesRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByName(Name)).Return(user);
            _rolesRepository.Expect(x => x.GetRolesForUser(Name)).Return(roles).Repeat.Once();

            _mockRepository.ReplayAll();
            User actual = accountService.GetUserByName(Name, out message);

            Assert.AreEqual(user, actual);
            Assert.IsNull(message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetUserByNameNotFoundTest()
        {
            const string Name = "user";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByName(Name)).Return(null);
            _mockRepository.ReplayAll();

            User actual = accountService.GetUserByName(Name, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.UserNotFoundMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetUserByNameDALErrorTest()
        {
            const string Name = "user";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.GetUserByName(Name)).Throw(new DbException("An error"));
            _mockRepository.ReplayAll();

            User actual = accountService.GetUserByName(Name, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringReadingUserMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetRolesForUserSuccessTest()
        {
            const string Name = "user";
            List<Role> roles = new List<Role>
                {
                    new Role
                        {
                            Name = "r1"
                        },
                    new Role
                        {
                            Name = "r2"
                        },
                };
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.RolesRepository).Return(_rolesRepository).Repeat.Once();
            _rolesRepository.Expect(x => x.GetRolesForUser(Name)).Return(roles);

            _mockRepository.ReplayAll();
            List<string> actual = accountService.GetRolesForUser(Name, out message);

            CollectionAssert.AreEqual(roles.Select(x=>x.Name).ToArray(), actual);
            Assert.IsNull(message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
         public void GetRolesForUserNotFoundTest()
        {
            const string Name = "user";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.RolesRepository).Return(_rolesRepository).Repeat.Once();
            _rolesRepository.Expect(x => x.GetRolesForUser(Name)).Return(new List<Role>());
            _mockRepository.ReplayAll();

            List<string> actual = accountService.GetRolesForUser(Name, out message);

            Assert.AreEqual(0, actual.Count);
            Assert.AreEqual(AccountService.UserNotFoundMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetRolesForUserDALErrorTest()
        {
            const string Name = "user";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.RolesRepository).Return(_rolesRepository).Repeat.Once();
            _rolesRepository.Expect(x => x.GetRolesForUser(Name)).Throw(new DbException("An error"));
            _mockRepository.ReplayAll();

            List<string> actual = accountService.GetRolesForUser(Name, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringReadingUserMessage, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserSuccessTest()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;

            User userToCreate = new User
                {
                    Username = Name,
                    Email = Email,
                    Password = Password
                };
            userToCreate.Roles.Add(new Role {Name = AccountService.UserDefaultRole});

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.AtLeastOnce();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Return(true).Repeat.Once();
            _usersRepository.Expect(x => x.CheckIfEmailAvailable(Email)).Return(true).Repeat.Once();
            _usersRepository.Expect(x => x.CreateUser(userToCreate)).Return(userToCreate);

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(userToCreate, actual);
            Assert.IsNull(message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserUserExistsTest()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Return(false).Repeat.Once();

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.UserNameIsAlreadyTaken, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserEmailExistsTest()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Twice();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Return(true).Repeat.Once();
            _usersRepository.Expect(x => x.CheckIfEmailAvailable(Email)).Return(false).Repeat.Once();

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.EmailIsAlreadyTaken, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserDALError1Test()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;


            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.Once();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Throw(new DbException("Message"));

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringUserCreation, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserDALError2Test()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;

            User userToCreate = new User
            {
                Username = Name,
                Email = Email,
                Password = Password
            };
            userToCreate.Roles.Add(new Role { Name = AccountService.UserDefaultRole });

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.AtLeastOnce();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Return(true);
            _usersRepository.Expect(x => x.CheckIfEmailAvailable(Email)).Throw(new DbException("Message"));

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringUserCreation, message);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateUserDALError3Test()
        {
            const string Name = "user";
            const string Email = "cutbko@hotmail.com";
            const string Password = "P@ssw0rd";
            string message;

            User userToCreate = new User
            {
                Username = Name,
                Email = Email,
                Password = Password
            };
            userToCreate.Roles.Add(new Role { Name = AccountService.UserDefaultRole });

            AccountService accountService = new AccountService(_dalContext);
            _dalContext.Expect(x => x.UsersRepository).Return(_usersRepository).Repeat.AtLeastOnce();
            _usersRepository.Expect(x => x.CheckIfUsernameAvailable(Name)).Return(true);
            _usersRepository.Expect(x => x.CheckIfEmailAvailable(Email)).Return(true);
            _usersRepository.Expect(x => x.CreateUser(userToCreate)).Throw(new DbException("Error"));

            _mockRepository.ReplayAll();
            User actual = accountService.CreateUser(Name, Email, Password, out message);

            Assert.AreEqual(null, actual);
            Assert.AreEqual(AccountService.ErrorDuringUserCreation, message);
            _mockRepository.VerifyAll();
        }
    }
}