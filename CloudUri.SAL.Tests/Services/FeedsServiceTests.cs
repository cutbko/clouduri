using System;
using System.Collections.Generic;
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
    public class FeedsServiceTests
    {
        private readonly MockRepository _mockRepository = new MockRepository();
        private IDALContext _dalContextMock;
        private IMessagesRepository _messagesRepositoryMock;
        private IDevicesRepository _devicesRepositoryMock;
        private const string UserName = "Belka";
        private const int Page = 1;
        private const int ItemsPerPage = 100;
        private const int TotalPages = 25;
        private const string SendingDevice = "Assa";
        private const string ReceivingDevice = "Vassa";
        private readonly List<Message> _expectedMessages = new List<Message>
            {
                new Message
                    {
                        FromId = 1,
                        ToId = 2,
                        Key = 1,
                        CreatedOn = DateTime.Now,
                        MessageText = "Hello"
                    },
                    
                new Message
                    {
                        FromId = 1,
                        ToId = 2,
                        Key = 2,
                        CreatedOn = DateTime.Now,
                        MessageText = "Hello"
                    }
            };

        private readonly List<Device> _expectedDevices = new List<Device>
            {
                new Device
                    {
                        Key = 1,
                        Name = "d1"
                    },
                new Device
                    {
                        Key = 2,
                        Name = "d2"
                    },
            };
            
        [TestInitialize]
        public void Initialize()
        {
            _dalContextMock = _mockRepository.StrictMock<IDALContext>();
            _messagesRepositoryMock = _mockRepository.StrictMock<IMessagesRepository>();
            _devicesRepositoryMock = _mockRepository.StrictMock<IDevicesRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorArgumentNullTest()
        {
            new FeedsService(null);
        }

        [TestMethod]
        public void ConstructorSuccessTest()
        {
            PerformTest(x=> {});
        }

        [TestMethod]
        public void GetMessagesForUserSuccess()
        {
            AddExpectationToMessageRepository();
            AddExpectationToDevicesRepository();

            int totalPages;
            _messagesRepositoryMock.Expect(x => x.GetMessagesForUser(UserName, SendingDevice, ReceivingDevice, ItemsPerPage, Page, out totalPages)).OutRef(TotalPages).Return(_expectedMessages);
            _devicesRepositoryMock.Expect(x => x.GetDevicesForUser(UserName)).Return(_expectedDevices);

            PerformTest(x=>
                {
                    List<Message> messages = x.GetMessagesForUser(UserName, SendingDevice, ReceivingDevice, ItemsPerPage, Page, out totalPages);

                    Assert.AreEqual(_expectedMessages.Count,  messages.Count);
                    Assert.AreEqual(TotalPages, totalPages);

                    foreach (Message message in messages)
                    {
                        Assert.IsTrue(_expectedMessages.Contains(message));
                        Assert.IsTrue(_expectedDevices.Contains(message.SenderDevice));
                        Assert.IsTrue(message.ToId == null && message.RecieverDevice == null || _expectedDevices.Contains(message.RecieverDevice));
                    }
                });
        }

        [TestMethod]
        public void GetMessagesForDALErrorDevices()
        {
            AddExpectationToDevicesRepository();

            _devicesRepositoryMock.Expect(x => x.GetDevicesForUser(UserName)).Throw(new DbException("Assa"));

            PerformTest(x =>
            {
                int totalPages;
                List<Message> messages = x.GetMessagesForUser(UserName, SendingDevice, ReceivingDevice, ItemsPerPage, Page, out totalPages);

                Assert.AreEqual(0, messages.Count);
            });
        }

        [TestMethod]
        public void GetMessagesForDALErrorMessages()
        {
            AddExpectationToMessageRepository();
            AddExpectationToDevicesRepository();
            int totalPages;

            _devicesRepositoryMock.Expect(x => x.GetDevicesForUser(UserName)).Return(_expectedDevices);
            _messagesRepositoryMock.Expect(x => x.GetMessagesForUser(UserName, SendingDevice, ReceivingDevice, ItemsPerPage, Page, out totalPages)).OutRef(TotalPages).Return(_expectedMessages).Throw(new DbException("Assa"));

            PerformTest(x =>
            {
                List<Message> messages = x.GetMessagesForUser(UserName, SendingDevice, ReceivingDevice, ItemsPerPage, Page, out totalPages);

                Assert.AreEqual(0, messages.Count);
                Assert.AreEqual(0, totalPages);
            });
        }


        private void AddExpectationToMessageRepository(int times = 1)
        {
            _dalContextMock.Expect(x => x.MessagesRepository).Return(_messagesRepositoryMock).Repeat.Times(times);
        }

        private void AddExpectationToDevicesRepository(int times = 1)
        {
            _dalContextMock.Expect(x => x.DevicesRepository).Return(_devicesRepositoryMock).Repeat.Times(times);
        }

        private void PerformTest(Action<FeedsService> action)
        {
            FeedsService feedsService = new FeedsService(_dalContextMock);
            _mockRepository.ReplayAll();
            action.Invoke(feedsService);
            _mockRepository.VerifyAll();
        }
    }
}