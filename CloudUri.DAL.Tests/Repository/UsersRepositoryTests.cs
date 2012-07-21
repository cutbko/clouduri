using System.Linq;
using CloudUri.DAL.Helpers;
using CloudUri.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using Rhino.Mocks;

namespace CloudUri.DAL.Tests.Repository
{
    /// <summary>
    ///This is a test class for UsersRepository and is intended
    ///to contain all <see cref="UsersRepository"/> Unit Tests
    ///</summary>
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private readonly User _user = new User
        {
            Key = int.MaxValue,
            Email = "cutbko@hotmail.com",
            Password = "P@ssw0rd",
            Username = "cutbko"
        };

        private IDbWrapper _dbWrapper;
        private IDataReader _dataReader;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbWrapper = _mockRepository.Stub<IDbWrapper>();
            _dataReader = _mockRepository.StrictMock<IDataReader>();
        }


        /// <summary>
        ///A test for BuildParametersForInserting
        ///</summary>
        [TestMethod]
        public void BuildParametersForInsertingTest()
        {
            List<DbParam> expectedParametersForInserting = new List<DbParam>
                       {
                           new DbParam { Name = "@Username", Value = _user.Username },
                           new DbParam { Name = "@Email", Value = _user.Email },
                           new DbParam { Name = "@PasswordHash", Value = _user.PasswordHash },
                           new DbParam { Name = "@Salt", Value = _user.Salt }
                       };


            UsersRepository usersRepository = new UsersRepository(_dbWrapper);
            List<DbParam> actualParametersForInserting = usersRepository.BuildParametersForInserting(_user).ToList();

            CollectionAssert.AreEqual(expectedParametersForInserting, actualParametersForInserting);
        }

        /// <summary>
        ///A test for BuildParametersForUpdating
        ///</summary>
        [TestMethod]
        public void BuildParametersForUpdatingTest()
        {
            List<DbParam> expectedParametersForUpdating = new List<DbParam>
                       {
                           new DbParam { Name = "@Id", Value = _user.Key },
                           new DbParam { Name = "@Username", Value = _user.Username },
                           new DbParam { Name = "@Email", Value = _user.Email },
                           new DbParam { Name = "@PasswordHash", Value = _user.PasswordHash },
                           new DbParam { Name = "@Salt", Value = _user.Salt }
                       };


            UsersRepository usersRepository = new UsersRepository(_dbWrapper);
            List<DbParam> actualParametersForUpdating = usersRepository.BuildParametersForUpdating(_user).ToList();

            CollectionAssert.AreEqual(expectedParametersForUpdating, actualParametersForUpdating);
        }

        /// <summary>
        ///A test for ReadSingleEntity all null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.IsDBNull(2)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.IsDBNull(3)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.IsDBNull(4)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_user.Key).Repeat.Once();

            _mockRepository.ReplayAll();

            UsersRepository deviceTypesRepository = new UsersRepository(_dbWrapper);
            User actual = deviceTypesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_user.Key, actual.Key);
            Assert.IsNull(actual.Email);
            Assert.IsNull(actual.Salt);
            Assert.IsNull(actual.Username);
            Assert.IsNull(actual.PasswordHash);

            _mockRepository.VerifyAll();
        }

        /// <summary>
        ///A test for ReadSingleEntity all not null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNotNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.IsDBNull(2)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.IsDBNull(3)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.IsDBNull(4)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_user.Key).Repeat.Once();

            _dataReader.Expect(x => x.GetString(1)).Repeat.Once().Return(_user.Username);
            _dataReader.Expect(x => x.GetString(2)).Repeat.Once().Return(_user.Email);
            _dataReader.Expect(x => x.GetString(3)).Repeat.Once().Return(_user.PasswordHash);
            _dataReader.Expect(x => x.GetString(4)).Repeat.Once().Return(_user.Salt);

            _mockRepository.ReplayAll();

            UsersRepository deviceTypesRepository = new UsersRepository(_dbWrapper);
            User actual = deviceTypesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_user.Key, actual.Key);
            Assert.AreEqual(_user.Email, actual.Email);
            Assert.AreEqual(_user.Salt, actual.Salt);
            Assert.AreEqual(_user.Username, actual.Username);
            Assert.AreEqual(_user.PasswordHash, actual.PasswordHash);

            _mockRepository.VerifyAll();
        }
    }
}
