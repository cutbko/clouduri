using System.Collections.Generic;
using System.Web.Mvc;
using CloudUri.DAL.Entities;

namespace CloudUri.Web.ViewModels
{
    public class DevicesViewModel
    {
        public DevicesViewModel()
        {
            
        }

        public DevicesViewModel(Device currentDevice, List<DeviceType> types)
        {
            CurrentDevice = currentDevice;
            DeviceTypes = new SelectList(types, "Key", "Name", currentDevice.TypeId);
        }

        public SelectList DeviceTypes { get; set; }

        public Device CurrentDevice { get; set; }
    }
}