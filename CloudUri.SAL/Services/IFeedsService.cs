using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IFeedsService
    {
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
        List<Message> GetMessagesForUser(string username, string sendingDevice, string receivingDevice, int itemsPerPage, int page, out int pagesTotal);
    }
}