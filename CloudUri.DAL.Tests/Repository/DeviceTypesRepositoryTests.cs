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
    ///This is a test class for DeviceTypesRepositoryTest and is intended
    ///to contain all <see cref="DeviceTypesRepository"/> Unit Tests
    ///</summary>
    [TestClass]
    public class DeviceTypesRepositoryTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private readonly DeviceType _deviceType = new DeviceType
        {
            Key = int.MaxValue,
            Name = "WP",
            DownloadUrl = "http://marketplace.com"
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
                           new DbParam { Name = "@Name", Value = _deviceType.Name },
                           new DbParam { Name = "@DownloadUrl", Value = _deviceType.DownloadUrl }
                       };


            DeviceTypesRepository deviceTypesRepository = new DeviceTypesRepository(_dbWrapper);
            List<DbParam> actualParametersForInserting = deviceTypesRepository.BuildParametersForInserting(_deviceType).ToList();

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
                           new DbParam {Name = "@Id", Value = _deviceType.Key },
                           new DbParam { Name = "@Name", Value = _deviceType.Name },
                           new DbParam { Name = "@DownloadUrl", Value = _deviceType.DownloadUrl }
                       };


            DeviceTypesRepository deviceTypesRepository = new DeviceTypesRepository(_dbWrapper);
            List<DbParam> actualParametersForUpdating = deviceTypesRepository.BuildParametersForUpdating(_deviceType).ToList();

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
            _dataReader.Expect(x => x.GetInt32(0)).Return(_deviceType.Key).Repeat.Once();

            _mockRepository.ReplayAll();

            DeviceTypesRepository deviceTypesRepository = new DeviceTypesRepository(_dbWrapper);
            DeviceType actual = deviceTypesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_deviceType.Key, actual.Key);
            Assert.IsNull(actual.Name);
            Assert.IsNull(actual.DownloadUrl);

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
            _dataReader.Expect(x => x.GetInt32(0)).Return(_deviceType.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetString(1)).Return(_deviceType.Name).Repeat.Once();
            _dataReader.Expect(x => x.GetString(2)).Return(_deviceType.DownloadUrl).Repeat.Once();

            _mockRepository.ReplayAll();

            DeviceTypesRepository deviceTypesRepository = new DeviceTypesRepository(_dbWrapper);
            DeviceType actual = deviceTypesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_deviceType.Key, actual.Key);
            Assert.AreEqual(_deviceType.Name, actual.Name);
            Assert.AreEqual(_deviceType.DownloadUrl, actual.DownloadUrl);

            _mockRepository.VerifyAll();
        }
    }
}
