using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudUri.DAL.Entities;
using CloudUri.SAL.Services;
using CloudUri.Web.Security;
using CloudUri.Web.ViewModels;

namespace CloudUri.Web.Controllers
{
    [Authorize]
    public class DevicesController : SecureController
    {
        private IDevicesService _deviceService;
        //
        // GET: /Devices/

        public DevicesController(IDevicesService devicesService)
        {
            _deviceService = devicesService;
        }

        public ActionResult Index()
        {
            var name = User.Identity.Name;
            List<Device> devices = _deviceService.GetDevicesByUsername(name);
            return View(devices);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Device dev = _deviceService.GetDeviceById(id);
            DevicesViewModel devicesViewModel = new DevicesViewModel
                                                    {
                                                        CurrentDevice = dev,
                                                        DeviceTypes = _deviceService.GetDeviceTypes()
                                                    };
            return View(devicesViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DevicesViewModel deviceViewModel)
        {
            bool result = _deviceService.UpdateDevice(deviceViewModel.CurrentDevice);

            if (result)
            {
                ViewBag.Result = "Success";
                return RedirectToAction("Index");
            }
            ViewBag.Result = "Fail";
            return View(deviceViewModel);
        }

        public ActionResult Delete(int id)
        {
            bool result = _deviceService.DeleteDevice(id);
            ViewBag.Result = result 
                ? "Success" 
                : "Fail";
            return RedirectToAction("Index");
        }
    }
}
