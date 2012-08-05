using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.Common.Logging;
using CloudUri.DAL;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly IDALContext _dalContext;

        public DevicesService(IDALContext dalContext)
        {
            if (dalContext == null)
            {
                throw new ArgumentNullException("dalContext");
            }

            _dalContext = dalContext;
        }

        public List<Device> GetDevicesByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public Device GetDeviceById(int key)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public int InsertDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDevice(int key)
        {
            throw new NotImplementedException();
        }

        public List<DeviceType> GetDeviceTypes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets device by Key
        /// </summary>
        /// <param name="key">Device key</param>
        /// <param name="error"></param>
        /// <returns>Device</returns>
        public Device GetDeviceById(int key, out string error)
        {
            error = string.Empty;
            try
            {
                return _dalContext.DevicesRepository.GetById(key);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during reading device by Key : {0}, error: {1}", key, ex);
                error = ex.Message;
                return new Device();
            }
        }

        /// <summary>
        /// Updates device
        /// </summary>
        /// <param name="device">Device to update</param>
        /// <param name="error">Error message</param>
        public void UpdateDevice(Device device, out string error)
        {
            error = string.Empty;
            try
            {
                _dalContext.DevicesRepository.Update(device);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during updating device with Name : {0}, error: {1}", device.Name, ex);
                error = ex.Message;
            }
        }

        /// <summary>
        /// Inserts device
        /// </summary>
        /// <param name="device">Device to insert</param>
        /// <param name="error">Error message</param>
        public void InsertDevice(Device device, out string error)
        {
            error = string.Empty;
            try
            {
                _dalContext.DevicesRepository.Insert(device);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during inserting device with Name : {0}, error: {1}", device.Name, ex);
                error = ex.Message;
            }
        }

        /// <summary>
        /// Deletes devices
        /// </summary>
        /// <param name="device">Devise to delete</param>
        /// <param name="error">Error message</param>
        public void DeleteDevice(Device device, out string error)
        {
            error = string.Empty;
            try
            {
                _dalContext.DevicesRepository.Delete(device);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during deleting device with Name : {0}, error: {1}", device.Name, ex);
                error = ex.Message;
            }
        }

        /// <summary>
        /// Gets all available DeviceTypes
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns>List of all DeviceTypes</returns>
        public IEnumerable<DeviceType> GetDeviceTypes(out string error)
        {
            error = string.Empty;
            try
            {
                return _dalContext.DeviceTypesRepository.GetRecords();
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during reading DeviceTypes. Error: {0}", ex);
                error = ex.Message;
                return new List<DeviceType>();
            }
        }

        /// <summary>
        /// Gets devices for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="error">Error message</param>
        /// <returns>List of devices</returns>
        public List<Device> GetDevicesForUser(string name, out string error)
        {
            error = string.Empty;
            try
            {
                List<Device> devices = _dalContext.DevicesRepository.GetDevicesForUser(name);
                var deviceTypes = (List<DeviceType>) _dalContext.DeviceTypesRepository.GetRecords();
                foreach (var device in devices)
                {
                    Device device1 = device;
                    device.Type = deviceTypes.First(i => i.Key == device1.TypeId);
                }
                return devices;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during reading devices for user : {0}, error: {1}", name, ex);
                error = ex.Message;
                return new List<Device>();
            }
        }
    }
}