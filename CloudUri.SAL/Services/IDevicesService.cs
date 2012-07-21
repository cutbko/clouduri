using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IDevicesService
    {
        List<Device> GetDevicesByUsername(string userName);
        Device GetDeviceById(int key);
        bool UpdateDevice(Device device);
        int InsertDevice(Device device);
        bool DeleteDevice(int key);
        List<DeviceType> GetDeviceTypes();
    }
}