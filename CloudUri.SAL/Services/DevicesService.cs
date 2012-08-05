using System;
using System.Collections.Generic;
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
        /// Gets devices for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>List of devices</returns>
        public List<Device> GetDevicesForUser(string name)
        {
            try
            {
                return _dalContext.DevicesRepository.GetDevicesForUser(name);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error during reading devices for user : {0}, error: {1}", name, ex);
                return new List<Device>();
            }
        }
    }
}