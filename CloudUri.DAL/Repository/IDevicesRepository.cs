using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Interface of devices repository
    /// </summary>
    public interface IDevicesRepository : IRepository<Device>
    {
        /// <summary>
        /// Get devices for user
        /// </summary>
        /// <param name="username">User name</param>
        /// <returns>List of devices</returns>
        List<Device> GetDevicesForUser(string username);
    }
}