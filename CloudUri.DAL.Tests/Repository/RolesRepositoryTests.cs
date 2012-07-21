using System.Linq;
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
    ///This is a test class for RolesRepository and is intended
    ///to contain all <see cref="RolesRepository"/> Unit Tests
    ///</summary>
    [TestClass]
    public class RolesRepositoryTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private readonly Role _role = new Role
        {
            Key = int.MaxValue,
            Name = "Admin",
            Description = "Mega user"
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
                           new DbParam { Name = "@Name", Value = _role.Name },
                           new DbParam { Name = "@Description", Value = _role.Description }
                       };


            RolesRepository rolesRepository = new RolesRepository(_dbWrapper);
            List<DbParam> actualParametersForInserting = rolesRepository.BuildParametersForInserting(_role).ToList();

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
                           new DbParam {Name = "@Key", Value = _role.Key },
                           new DbParam { Name = "@Name", Value = _role.Name },
                           new DbParam { Name = "@Description", Value = _role.Description }
                       };


            RolesRepository rolesRepository = new RolesRepository(_dbWrapper);
            List<DbParam> actualParametersForUpdating = rolesRepository.BuildParametersForUpdating(_role).ToList();

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
            _dataReader.Expect(x => x.GetInt32(0)).Return(_role.Key).Repeat.Once();

            _mockRepository.ReplayAll();

            RolesRepository rolesRepository = new RolesRepository(_dbWrapper);
            Role actual = rolesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_role.Key, actual.Key);
            Assert.IsNull(actual.Name);
            Assert.IsNull(actual.Description);

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
            _dataReader.Expect(x => x.GetInt32(0)).Return(_role.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetString(1)).Return(_role.Name).Repeat.Once();
            _dataReader.Expect(x => x.GetString(2)).Return(_role.Description).Repeat.Once();

            _mockRepository.ReplayAll();

            RolesRepository rolesRepository = new RolesRepository(_dbWrapper);
            Role actual = rolesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_role.Key, actual.Key);
            Assert.AreEqual(_role.Name, actual.Name);
            Assert.AreEqual(_role.Description, actual.Description);

            _mockRepository.VerifyAll();
        }
    }
}
