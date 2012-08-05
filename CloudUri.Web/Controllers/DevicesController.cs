using System.Collections.Generic;
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

            string error;
            List<Device> devices = _deviceService.GetDevicesForUser(name,out error);
            ViewBag.Result = error;
            return View(devices);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var errors = new List<string>();
            string error;

            Device dev = _deviceService.GetDeviceById(id,out error);
            if(!string.IsNullOrEmpty(error))
                errors.Add(error);

            var devicesViewModel = new DevicesViewModel(dev, _deviceService.GetDeviceTypes(out error));
            if (!string.IsNullOrEmpty(error))
                errors.Add(error);

            return View(devicesViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DevicesViewModel deviceViewModel)
        {
            string error;
            _deviceService.UpdateDevice(deviceViewModel.CurrentDevice,out error);

            if (string.IsNullOrEmpty(error))
            {
                ViewBag.Result = "Success";
                return RedirectToAction("Index");
            }
            ViewBag.Result = error;
            return View(deviceViewModel);
        }

        public ActionResult Delete(int id)
        {
            var errors = new List<string>();
            string error;

            Device device = _deviceService.GetDeviceById(id,out error);
            if(!string.IsNullOrEmpty(error))
                errors.Add(error);

            _deviceService.DeleteDevice(device,out error);
            if (!string.IsNullOrEmpty(error))
                errors.Add(error);

            ViewBag.Result = errors.Count == 0 
                ? "Success" 
                : errors.ToString();
            return RedirectToAction("Index");
        }
    }
}
