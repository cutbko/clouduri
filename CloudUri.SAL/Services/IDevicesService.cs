using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IDevicesService
    {
        /// <summary>
        /// Gets device by Key
        /// </summary>
        /// <param name="key">Device key</param>
        /// <param name="error"></param>
        /// <returns>Device</returns>
        Device GetDeviceById(int key, out string error);

        /// <summary>
        /// Updates device
        /// </summary>
        /// <param name="device">Device to update</param>
        /// <param name="error">Error message</param>
        void UpdateDevice(Device device, out string error);

        /// <summary>
        /// Inserts device
        /// </summary>
        /// <param name="device">Device to insert</param>
        /// <param name="error">Error message</param>
        void InsertDevice(Device device, out string error);

        /// <summary>
        /// Deletes devices
        /// </summary>
        /// <param name="device">Devise to delete</param>
        /// <param name="error">Error message</param>
        void DeleteDevice(Device device, out string error);

        /// <summary>
        /// Gets all available DeviceTypes
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns>List of all DeviceTypes</returns>
        IEnumerable<DeviceType> GetDeviceTypes(out string error);

        /// <summary>
        /// Gets devices for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="error">Error message</param>
        /// <returns>List of devices</returns>
        List<Device> GetDevicesForUser(string name, out string error);
    }
}