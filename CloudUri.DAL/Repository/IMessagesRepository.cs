using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Interface of Messages repository
    /// </summary>
    public interface IMessagesRepository : IRepository<Message>
    {
        /// <summary>
        /// Get messages for user
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="sendingDevice">Sending device name. If null you will get all the messages.</param>
        /// <param name="receivingDevice">Receiving device name. If null you will get all the messages.</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <param name="page">Page</param>
        /// <param name="pagesTotal">Total pages</param>
        /// <returns>Messages for user</returns>
        List<Message> GetMessagesForUser(string username, string sendingDevice, string receivingDevice, int itemsPerPage, int page, out int pagesTotal);
    }
}