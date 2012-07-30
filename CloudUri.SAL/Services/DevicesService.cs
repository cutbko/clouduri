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