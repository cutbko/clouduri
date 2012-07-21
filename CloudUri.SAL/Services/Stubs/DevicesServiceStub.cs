using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services.Stubs
{
    public class DevicesServiceStub:IDevicesService
    {
        public DevicesServiceStub()
        {
            _deviceTypes = new List<DeviceType>
                               {
                                   new DeviceType
                                       {
                                           Key = 1,
                                           Name = "wp7",
                                           DownloadUrl = @"http://"
                                       },
                                   new DeviceType
                                       {
                                           Key = 2,
                                           Name = "android",
                                           DownloadUrl = @"http://"
                                       },
                                   new DeviceType
                                       {
                                           Key = 3,
                                           Name = "ios",
                                           DownloadUrl = @"http://"
                                       },
                               };

            _devices = new List<Device>
                           {
                               new Device
                                   {
                                       Key = 1,
                                       Name = "Dev1",
                                       OwnerId = 1,
                                       TypeId = 1,
                                       Type = _deviceTypes[0]
                                   },
                               new Device
                                   {
                                       Key = 2,
                                       Name = "Dev2",
                                       OwnerId = 1,
                                       TypeId = 1,
                                       Type = _deviceTypes[1]
                                   },
                               new Device
                                   {
                                       Key = 3,
                                       Name = "Dev3",
                                       OwnerId = 1,
                                       TypeId = 1,
                                       Type = _deviceTypes[2]
                                   }
                           };
        }

        private readonly List<DeviceType> _deviceTypes;
        private readonly List<Device> _devices;

        public List<Device> GetDevicesByUsername(string userName)
        {
            return _devices;
        }

        public Device GetDeviceById(int key)
        {
            return _devices.Where(i => i.Key == key).FirstOrDefault();
        }

        public bool UpdateDevice(Device device)
        {
            Device dev = _devices.Where(i => i.Key == device.Key).FirstOrDefault();
            if (dev == null)
                return false;
            dev.Name = device.Name;
            dev.TypeId = device.TypeId;
            return true;
        }

        public int InsertDevice(Device device)
        {
            int newKey = _devices.Max(i => i.Key) + 1;
            device.Key = newKey;
            _devices.Add(device);
            return newKey;
        }

        public bool DeleteDevice(int key)
        {
            Device device = _devices.Where(i => i.Key == key).FirstOrDefault();
            if (device == null)
                return false;
            return _devices.Remove(device);
        }

        public List<DeviceType> GetDeviceTypes()
        {
            return _deviceTypes;
        }
    }
}