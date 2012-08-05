using System;
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
    ///This is a test class for MessagesRepositoryTest and is intended
    ///to contain all <see cref="MessagesRepository"/> Unit Tests
    ///</summary>
    [TestClass]
    public class MessagesRepositoryTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        private readonly Message _message = new Message
        {
            Key = int.MaxValue,
            MessageText = "Hello!!!",
            FromId = 100,
            ToId = 200,
            CreatedOn = DateTime.Now
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
                           new DbParam {Name = "@MessageText", Value = _message.MessageText},
                           new DbParam {Name = "@FromId", Value = _message.FromId},
                           new DbParam {Name = "@ToId", Value = _message.ToId}
                       };


            MessagesRepository repository = new MessagesRepository(_dbWrapper);
            List<DbParam> actualParametersForInserting = repository.BuildParametersForInserting(_message).ToList();

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
                           new DbParam {Name = "@Id", Value = _message.Key},
                           new DbParam {Name = "@MessageText", Value = _message.MessageText},
                           new DbParam {Name = "@FromId", Value = _message.FromId},
                           new DbParam {Name = "@ToId", Value = _message.ToId}
                       };


            MessagesRepository repository = new MessagesRepository(_dbWrapper);
            List<DbParam> actualParametersForUpdating = repository.BuildParametersForUpdating(_message).ToList();

            CollectionAssert.AreEqual(expectedParametersForUpdating, actualParametersForUpdating);
        }

        /// <summary>
        ///A test for ReadSingleEntity all null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.IsDBNull(3)).Repeat.Once().Return(true);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_message.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(2)).Return(_message.FromId).Repeat.Once();
            _dataReader.Expect(x => x.GetDateTime(4)).Return(_message.CreatedOn).Repeat.Once();

            _mockRepository.ReplayAll();

            MessagesRepository devicesRepository = new MessagesRepository(_dbWrapper);
            Message actual = devicesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_message.Key, actual.Key);
            Assert.IsNull(actual.MessageText);
            Assert.IsNull(actual.ToId);
            Assert.AreEqual(_message.FromId, actual.FromId);
            Assert.AreEqual(_message.Key, actual.Key);

            _mockRepository.VerifyAll();
        }

        /// <summary>
        ///A test for ReadSingleEntity all not null
        ///</summary>
        [TestMethod]
        public void ReadSingleEntityAllNotNullTest()
        {
            _dataReader.Expect(x => x.IsDBNull(1)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.IsDBNull(3)).Repeat.Once().Return(false);
            _dataReader.Expect(x => x.GetInt32(0)).Return(_message.Key).Repeat.Once();
            _dataReader.Expect(x => x.GetString(1)).Return(_message.MessageText).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(2)).Return(_message.FromId).Repeat.Once();
            _dataReader.Expect(x => x.GetInt32(3)).Return((int)_message.ToId).Repeat.Once();
            _dataReader.Expect(x => x.GetDateTime(4)).Return(_message.CreatedOn).Repeat.Once();

            _mockRepository.ReplayAll();

            MessagesRepository devicesRepository = new MessagesRepository(_dbWrapper);
            Message actual = devicesRepository.ReadSingleEntity(_dataReader);
            Assert.AreEqual(_message.Key, actual.Key);
            Assert.AreEqual(_message.MessageText, actual.MessageText);
            Assert.AreEqual(_message.FromId, actual.FromId);
            Assert.AreEqual(_message.ToId, actual.ToId);

            _mockRepository.VerifyAll();
        }
    }
}