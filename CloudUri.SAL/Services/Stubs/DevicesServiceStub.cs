using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services.Stubs
{
    public class DevicesServiceStub:IDevicesService
    {
        public List<DeviceType> GetDeviceTypesForUser(string name)
        {
            return new List<DeviceType>
                       {
                           new DeviceType
                               {
                                   Key = 1, 
                                   Name = "WindowsPhone7"
                               },
                            new DeviceType
                                {
                                    Key = 2,
                                    Name = "Android"
                                }
                       };
        }
    }
}