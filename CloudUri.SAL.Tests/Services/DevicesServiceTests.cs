using System;
using CloudUri.DAL;
using CloudUri.DAL.Entities;
using CloudUri.DAL.Repository;
using CloudUri.SAL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CloudUri.SAL.Tests.Services
{
    /// <summary>
    /// Test class for the <see cref="DevicesService"/> class
    /// </summary>
    [TestClass]
    public class DevicesServiceTests
    {
        private MockRepository _mockRepository = new MockRepository();
        private IDALContext _dalContextMock;
        private IDevicesRepository _devicesRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dalContextMock = _mockRepository.StrictMock<IDALContext>();
            _devicesRepository = _mockRepository.StrictMock<IDevicesRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNullCheck()
        {
            new DevicesService(null);
        }

        [TestMethod]
        public void ConstructorSuccessCheck()
        {

            new DevicesService(_dalContextMock);
        }


        [TestMethod]
        public void GetDeviceByIdSuccess()
        {
            int key = 5;
            Device device = new Device();
            _dalContextMock.Expect(x => x.DevicesRepository).Repeat.Once().Return(_devicesRepository);
            _devicesRepository.Expect(x => x.GetById(key)).Repeat.Once().Return(device);

            PerformTest(devicesService =>
                {
                    string error;
                    Device actualDevice = devicesService.GetDeviceById(key, out error);

                    Assert.IsNull(error);
                    Assert.AreEqual(device, actualDevice);
                });
        }

        [TestMethod]
        public void GetDeviceByIdErrored()
        {
            int key = 5;
            string expectedError = "Test error";
            _dalContextMock.Expect(x => x.DevicesRepository).Repeat.Once().Throw(new Exception(expectedError));
            
            PerformTest(devicesService =>
                {
                    string error;
                    Device device = devicesService.GetDeviceById(key, out error);
                    Assert.IsNull(device);
                    Assert.AreEqual(expectedError, error);
                });
        }

        public void PerformTest(Action<DevicesService> action)
        {
            _mockRepository.ReplayAll();
            DevicesService devicesService = new DevicesService(_dalContextMock);
            action.Invoke(devicesService);
            _mockRepository.VerifyAll();
        }
    }
}