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
    /// Test class for the <see cref="RepositoryBase{TEntity}"/> class
    /// </summary>
    [TestClass]
    public class RepositoryBaseTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();
        private IDbWrapper _dbWrapperMock;

        /// <summary>
        /// Initializes every test
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _dbWrapperMock = _mockRepository.StrictMock<IDbWrapper>();
        }

        /// <summary>
        /// Tests the constructor
        /// </summary>
        [TestMethod]
        public void ConstructorArgumentCheck()
        {
            _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
        }

        /// <summary>
        /// Tests the constructor with null argument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNullCheck()
        {
            try
            {
                _mockRepository.PartialMock<RepositoryBase<IEntity>>((IDbWrapper)null);
            }
            catch (Exception exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }

                Assert.Fail("Exception must contain inner exception of type ArgumentNullException");
            }
        }

        /// <summary>
        /// Tests the insert method with null argument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertArgumentNullCheck()
        {
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            repositoryBase.Insert(null);
        }

        /// <summary>
        /// Tests the insert method with successful result expected
        /// </summary>
        [TestMethod]
        public void InsertSuccessTest()
        {
            // Prepare
            const int entityKey = int.MaxValue;
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForInserting).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForInserting(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPScalar<int>(spname, dbParams)).Repeat.Once().Return(entityKey);

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Insert(entityMock);
            Assert.AreEqual(entityKey, entityMock.Key, "After insert entity key must be set");
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests the insert method with failed result expected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void InsertFailedTest()
        {
            // Prepare
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForInserting).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForInserting(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPScalar<int>(spname, dbParams)).Repeat.Once().Throw(new DbException("Bang!!!"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Insert(entityMock);
        }

        /// <summary>
        /// Tests the update method with null argument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateArgumentNullCheck()
        {
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            repositoryBase.Update(null);
        }

        /// <summary>
        /// Tests the update method with successful result expected
        /// </summary>
        [TestMethod]
        public void UpdateSuccessTest()
        {
            // Prepare
            const string spName = "sp";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entity = _mockRepository.Stub<IEntity>();

            // expectations
            repositoryBase.Expect(i => i.SPForUpdating).Repeat.Once().Return(spName);
            repositoryBase.Expect(i => i.BuildParametersForUpdating(entity)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(i => i.ExecuteSPNonQuery(spName, dbParams)).Repeat.Once().Return(1);

            // tests
            _mockRepository.ReplayAll();
            repositoryBase.Update(entity);
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests the delete method with failed result expected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void UpdateFailedBecauseOfExceptionTest()
        {
            // Prepare
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForUpdating).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForUpdating(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPNonQuery(spname, dbParams)).Repeat.Once().Throw(new DbException("BANG!!!"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Update(entityMock);
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests the delete method with null argument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteArgumentNullCheck()
        {
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            repositoryBase.Delete(null);
        }

        /// <summary>
        /// Tests the delete method with successful result expected
        /// </summary>
        [TestMethod]
        public void DeleteSuccessTest()
        {
            // Prepare
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForDeleting).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForDeleting(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPNonQuery(spname, dbParams)).Repeat.Once().Return(1);

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Delete(entityMock);
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests the delete method with failed result expected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void DeleteFailedBecauseOfExceptionTest()
        {
            // Prepare
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForDeleting).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForDeleting(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPNonQuery(spname, dbParams)).Repeat.Once().Throw(new DbException("BANG!!!"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Delete(entityMock);
        }

        /// <summary>
        /// Tests the delete method with failed result expected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void DeleteFailedBecauseOfResultTest()
        {
            // Prepare
            const string spname = "SP";
            List<DbParam> dbParams = new List<DbParam>();
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForDeleting).Repeat.Once().Return(spname);
            repositoryBase.Expect(x => x.BuildParametersForDeleting(entityMock)).Repeat.Once().Return(dbParams);
            _dbWrapperMock.Expect(x => x.ExecuteSPNonQuery(spname, dbParams)).Repeat.Once().Return(0);

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Delete(entityMock);
        }

        /// <summary>
        /// Tests getall() method
        /// </summary>
        [TestMethod]
        public void GetRecordsTest()
        {
            // Prepare
            const int recordsCount = 10;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();
            IDataReader reader = _mockRepository.StrictMock<IDataReader>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetRecords).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam>(), out connection)).OutRef(connection).Repeat.Once().Return(reader);
            reader.Expect(x => x.Read()).Return(true).Repeat.Times(recordsCount);
            repositoryBase.Expect(x => x.ReadSingleEntity(reader)).Return(entityMock).Repeat.Times(recordsCount);
            reader.Expect(x => x.Read()).Return(false).Repeat.Once();
            reader.Expect(x => x.Dispose()).Repeat.Once();
            connection.Expect(x => x.Dispose()).Repeat.Once();
            
            // Test
            _mockRepository.ReplayAll();
            List<IEntity> entities = repositoryBase.GetRecords().ToList();
            Assert.AreEqual(recordsCount, entities.Count, "Count is wrong");
            Assert.IsTrue(entities.All(x => x == entityMock), "Hm... wrong entities?");
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests getall() method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void GetAllUnknownException()
        {
            // Prepare
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();
            IDataReader reader = _mockRepository.StrictMock<IDataReader>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetRecords).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam>(), out connection)).OutRef(connection).Repeat.Once().Return(reader);
            reader.Expect(x => x.Read()).Throw(new InvalidOperationException("Bang!!!"));
            reader.Expect(x => x.Dispose());
            connection.Expect(x => x.Dispose());

            // Test
            _mockRepository.ReplayAll();
            try
            {
                repositoryBase.GetRecords();
            }
            catch
            {
                _mockRepository.VerifyAll();
                throw;
            }
        }

        /// <summary>
        /// Test for getall() with exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void GetAllDbException()
        { 
            // Prepare
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetRecords).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam>(), out connection)).OutRef(connection).Repeat.Once().Throw(new DbException("Message"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.GetRecords();
        }

        /// <summary>
        /// Tests GetById() method
        /// </summary>
        [TestMethod]
        public void GetByIdTest()
        {
            // Prepare
            const int id = int.MaxValue;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IEntity entityMock = _mockRepository.Stub<IEntity>();
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();
            IDataReader reader = _mockRepository.StrictMock<IDataReader>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetById).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam> { new DbParam {Name = "@id", Value = id}}, out connection)).OutRef(connection).Repeat.Once().Return(reader);
            reader.Expect(x => x.Read()).Return(true).Repeat.Once();
            repositoryBase.Expect(x => x.ReadSingleEntity(reader)).Return(entityMock).Repeat.Once();
            reader.Expect(x => x.Dispose()).Repeat.Once();
            connection.Expect(x => x.Dispose()).Repeat.Once();

            // Test
            _mockRepository.ReplayAll();
            IEntity entity = repositoryBase.GetById(id);
            Assert.AreEqual(entityMock, entity, "Wrong entity returned");
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests GetById() method with no record returned
        /// </summary>
        [TestMethod]
        public void GetByIdNotFoundTest()
        {
            // Prepare
            const int id = int.MaxValue;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();
            IDataReader reader = _mockRepository.StrictMock<IDataReader>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetById).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam> { new DbParam { Name = "@id", Value = id } }, out connection)).OutRef(connection).Repeat.Once().Return(reader);
            reader.Expect(x => x.Read()).Return(false).Repeat.Once();
            reader.Expect(x => x.Dispose()).Repeat.Once();
            connection.Expect(x => x.Dispose()).Repeat.Once();

            // Test
            _mockRepository.ReplayAll();
            IEntity entity = repositoryBase.GetById(id);
            Assert.IsNull(entity, "Must be null returned");
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Test for GetById() with exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void GetByUnknownException()
        {
            // Prepare
            const int id = int.MaxValue;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();
            IDataReader reader = _mockRepository.StrictMock<IDataReader>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetById).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam> { new DbParam { Name = "@id", Value = id } }, out connection)).OutRef(connection).Repeat.Once().Return(reader);
            reader.Expect(x => x.Read()).Throw(new InvalidOperationException("Bang!!!"));
            reader.Expect(x => x.Dispose());
            connection.Expect(x => x.Dispose());

            // Test
            _mockRepository.ReplayAll();
            try
            {
                repositoryBase.GetById(id);
            }
            catch
            {
                _mockRepository.VerifyAll();
                throw;
            }
        }

        /// <summary>
        /// Test for GetById() with exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void GetByDbException()
        {
            // Prepare
            const int id = int.MaxValue;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);
            IDbConnection connection = _mockRepository.StrictMock<IDbConnection>();

            // Set up expectations
            repositoryBase.Expect(x => x.SPForGetById).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPReader(spname, new List<DbParam> { new DbParam { Name = "@id", Value = id } }, out connection)).OutRef(connection).Repeat.Once().Throw(new DbException("Bang!!!"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.GetById(id);
        }

        /// <summary>
        /// Tests the count method
        /// </summary>
        [TestMethod]
        public void TestCount()
        {
            // Prepare
            const int expectedCount = 10000;
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);

            // Set up expectations
            repositoryBase.Expect(x => x.SPForCount).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPScalar<int>(spname, new List<DbParam>())).Repeat.Once().Return(expectedCount);

            // Test
            _mockRepository.ReplayAll();
            Assert.AreEqual(expectedCount, repositoryBase.Count(), "Wrong number returned");
            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Tests the count method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DbException))]
        public void TestCountErrored()
        {
            // Prepare
            const string spname = "SP";
            RepositoryBase<IEntity> repositoryBase = _mockRepository.PartialMock<RepositoryBase<IEntity>>(_dbWrapperMock);

            // Set up expectations
            repositoryBase.Expect(x => x.SPForCount).Repeat.Once().Return(spname);
            _dbWrapperMock.Expect(x => x.ExecuteSPScalar<int>(spname, new List<DbParam>())).Repeat.Once().Throw(new DbException("Blah!"));

            // Test
            _mockRepository.ReplayAll();
            repositoryBase.Count();
        }
    }
}