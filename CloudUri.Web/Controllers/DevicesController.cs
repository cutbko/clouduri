using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            DevicesViewModel model = new DevicesViewModel();
            var name = User.Identity.Name;
            return View(model);
        }

    }
}
