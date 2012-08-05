using System.Collections.Generic;
using System.Data;
using System.Linq;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;
using CloudUri.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CloudUri.DAL.Tests.Repository
{
    /// <summary>
    ///This is a test class for DeviceTypesRepositoryTest and is intended
    ///to contain all <see cref="DevicesRepository"/> Unit Tests
    ///</summary>
    [TestClass]
    public class DevicesRepositoryTest
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private readonly Device _device = new Device
        {
            Key = int.MaxValue,
            Name = "WP Eugene",
            OwnerId = 100,
            TypeId = 200
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
                           new DbParam { Name = "@Name", Value = _device.Name, Type = SqlDbType.NVarChar},
                           new DbParam { Name = "@TypeId", Value = _device.TypeId, Type = SqlDbType.Int},
                           new DbParam { Name = "@OwnerId", Value = _device.OwnerId, Type = SqlDbType.Int}
                       };


            DevicesRepository devicesRepository = new DevicesRepository(_dbWrapper);
            List<DbParam> actualParametersForInserting = devicesRepository.BuildParametersForInserting(_device).ToList();

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
                           new DbParam { Name = "@Id", Value = _device.Key, Type = SqlDbType.Int},
                           new DbParam { Name = "@Name", Value = _device.Name, Type = SqlDbType.NVarChar },
                           new DbParam { Name = "@TypeId", Value = _device.TypeId, Type = SqlDbType.Int },
                           new DbParam { Name = "@OwnerId", Value = _device.OwnerId, Type = SqlDbType.Int }
                       };


            DevicesRepository devicesRepository = new DevicesRepository(_dbWrapper);
            List<DbParam> actualParametersForUpdating = devicesRepository.BuildParametersForUpdating(_device).ToList();

            CollectionAssert.AreEqual(expectedParametersForUpdating, actualParametersForUpdating);
        }

        /// <summary>
        ///A test for ReadSingleEntity all null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_device.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(2)).Return(_device.TypeId).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(3)).Return(_device.OwnerId).Repeat.Once();

            _mockRepository.ReplayAll();

            DevicesRepository devicesRepository = new DevicesRepository(_dbWrapper);
            Device actual = devicesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_device.Key, actual.Key);
            Assert.IsNull(actual.Name);
            Assert.AreEqual(_device.TypeId, actual.TypeId);
            Assert.AreEqual(_device.OwnerId, actual.OwnerId);

            _mockRepository.VerifyAll();
        }

        /// <summary>
        ///A test for ReadSingleEntity all not null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNotNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_device.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetString(1)).Return(_device.Name).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(2)).Return(_device.TypeId).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(3)).Return(_device.OwnerId).Repeat.Once();

            _mockRepository.ReplayAll();

            DevicesRepository devicesRepository = new DevicesRepository(_dbWrapper);
            Device actual = devicesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_device.Key, actual.Key);
            Assert.AreEqual(_device.Name, actual.Name);
            Assert.AreEqual(_device.TypeId, actual.TypeId);
            Assert.AreEqual(_device.OwnerId, actual.OwnerId);

            _mockRepository.VerifyAll();
        }
    }
}