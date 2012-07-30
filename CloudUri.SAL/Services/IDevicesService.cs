using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IDevicesService
    {
        /// <summary>
        /// Gets devices for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>List of devices</returns>
        List<Device> GetDevicesForUser(string name);
    }
}