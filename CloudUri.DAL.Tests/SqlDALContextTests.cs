using System;
using System.Threading.Tasks;
using CloudUri.DAL.Database;
using CloudUri.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CloudUri.DAL.Tests
{
    /// <summary>
    /// Tests the <see cref="SqlDALContext"/> class
    /// </summary>
    [TestClass]
    public class SqlDALContextTests
    {
        private readonly IDbWrapper _dbWrapper = MockRepository.GenerateStub<IDbWrapper>();

        /// <summary>
        /// Tests contstructor for null argument check
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNullTest()
        {
            new SqlDALContext(null);
        }

        /// <summary>
        /// Tests contstructor
        /// </summary>
        [TestMethod]
        public void ConstructorTest()
        {
            new SqlDALContext(_dbWrapper);
        }

        /// <summary>
        /// Tests messages repository
        /// </summary>
        [TestMethod]
        public void MessagesRepositoryTest()
        {
            RunTest(dal=>Assert.IsNotNull(dal.MessagesRepository));
        }

        /// <summary>
        /// Tests messages repository in multithreading scenario
        /// </summary>
        [TestMethod]
        public void MessagesRepositoryMultithreadingTest()
        {
            RunTest(dal =>
            {
                Task<IMessagesRepository> task1 = Task<IMessagesRepository>.Factory.StartNew(() => dal.MessagesRepository);
                Task<IMessagesRepository> task2 = Task<IMessagesRepository>.Factory.StartNew(() => dal.MessagesRepository);
                Task.WaitAll(task1, task2);
                Assert.AreSame(dal.MessagesRepository, task1.Result);
                Assert.AreSame(dal.MessagesRepository, task2.Result);
            });
        }

        /// <summary>
        /// Tests users repository
        /// </summary>
        [TestMethod]
        public void UsersRepositoryTest()
        {
            RunTest(dal => Assert.IsNotNull(dal.UsersRepository));
        }

        /// <summary>
        /// Tests users repository in multithreading scenario
        /// </summary>
        [TestMethod]
        public void UsersRepositoryMultithreadingTest()
        {
            RunTest(dal =>
            {
                Task<IUsersRepository> task1 = Task<IUsersRepository>.Factory.StartNew(() => dal.UsersRepository);
                Task<IUsersRepository> task2 = Task<IUsersRepository>.Factory.StartNew(() => dal.UsersRepository);
                Task.WaitAll(task1, task2);
                Assert.AreSame(dal.UsersRepository, task1.Result);
                Assert.AreSame(dal.UsersRepository, task2.Result);
            });
        }

        /// <summary>
        /// Tests Roles repository
        /// </summary>
        [TestMethod]
        public void RolesRepositoryTest()
        {
            RunTest(dal => Assert.IsNotNull(dal.RolesRepository));
        }

        /// <summary>
        /// Tests roles repository in multithreading scenario
        /// </summary>
        [TestMethod]
        public void RolesRepositoryMultithreadingTest()
        {
            RunTest(dal =>
            {
                Task<IRolesRepository> task1 = Task<IRolesRepository>.Factory.StartNew(() => dal.RolesRepository);
                Task<IRolesRepository> task2 = Task<IRolesRepository>.Factory.StartNew(() => dal.RolesRepository);
                Task.WaitAll(task1, task2);
                Assert.AreSame(dal.RolesRepository, task1.Result);
                Assert.AreSame(dal.RolesRepository, task2.Result);
            });
        }

        /// <summary>
        /// Tests Devices repository
        /// </summary>
        [TestMethod]
        public void DevicesRepositoryTest()
        {
            RunTest(dal => Assert.IsNotNull(dal.DevicesRepository));
        }

        /// <summary>
        /// Tests Devices repository in multithreading scenario
        /// </summary>
        [TestMethod]
        public void DevicesRepositoryMultithreadingTest()
        {
            RunTest(dal =>
            {
                Task<IDevicesRepository> task1 = Task<IDevicesRepository>.Factory.StartNew(() => dal.DevicesRepository);
                Task<IDevicesRepository> task2 = Task<IDevicesRepository>.Factory.StartNew(() => dal.DevicesRepository);
                Task.WaitAll(task1, task2);
                Assert.AreSame(dal.DevicesRepository, task1.Result);
                Assert.AreSame(dal.DevicesRepository, task2.Result);
            });
        }

        /// <summary>
        /// Tests DeviceTypes repository
        /// </summary>
        [TestMethod]
        public void DeviceTypesRepositoryTest()
        {
            RunTest(dal => Assert.IsNotNull(dal.DeviceTypesRepository));
        }

        /// <summary>
        /// Tests DeviceTypes repository in multithreading scenario
        /// </summary>
        [TestMethod]
        public void DeviceTypesRepositoryMultithreadingTest()
        {
            RunTest(dal =>
            {
                Task<IDeviceTypesRepository> task1 = Task<IDeviceTypesRepository>.Factory.StartNew(() => dal.DeviceTypesRepository);
                Task<IDeviceTypesRepository> task2 = Task<IDeviceTypesRepository>.Factory.StartNew(() => dal.DeviceTypesRepository);
                Task.WaitAll(task1, task2);
                Assert.AreSame(dal.DeviceTypesRepository, task1.Result);
                Assert.AreSame(dal.DeviceTypesRepository, task2.Result);
            });
        }

        private void RunTest(Action<SqlDALContext> testAction)
        {
            SqlDALContext sqlDALContext = new SqlDALContext(_dbWrapper);
            testAction.Invoke(sqlDALContext);
        }
    }
}