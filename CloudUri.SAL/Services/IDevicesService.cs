using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IDevicesService
    {
        List<DeviceType> GetDeviceTypesForUser(string name);
    }
}