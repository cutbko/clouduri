using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.Common.Logging;
using CloudUri.DAL;
using CloudUri.DAL.Entities;
using CloudUri.DAL.Repository;

namespace CloudUri.SAL.Services
{
    public class FeedsService : IFeedsService
    {
        private readonly IDALContext _dalContext;

        public FeedsService(IDALContext dalContext)
        {
            if (dalContext == null)
            {
                throw new ArgumentNullException("dalContext");
            }

            _dalContext = dalContext;
        }

        private static void ConnectDevicesToMessages(List<Device> devices, List<Message> messages)
        {
            messages.ForEach(x =>
                {
                    x.SenderDevice = devices.Single(d => d.Key == x.FromId);
                    if (x.ToId != null)
                    {
                        x.RecieverDevice = devices.Single(d => d.Key == x.ToId);
                    }
                });
        }

        /// <summary>
        /// Get messages for user
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="sendingDevice">Sending device name</param>
        /// <param name="receivingDevice">Receiving device name</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <param name="page">Page</param>
        /// <param name="pagesTotal">Total pages</param>
        /// <returns>Messages for user</returns>
        public List<Message> GetMessagesForUser(string username, string sendingDevice, string receivingDevice, int itemsPerPage, int page, out int pagesTotal)
        {
            try
            {
                List<Device> devices = _dalContext.DevicesRepository.GetDevicesForUser(username);
                List<Message> messages = _dalContext.MessagesRepository.GetMessagesForUser(username, sendingDevice, receivingDevice, itemsPerPage, page, out  pagesTotal);

                ConnectDevicesToMessages(devices, messages);

                return messages;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during reading messages for user:{0}, error: {1}", username, ex);
                pagesTotal = 0;
                return new List<Message>();
            }
        }
    }
}