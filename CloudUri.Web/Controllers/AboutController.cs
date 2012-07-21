using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudUri.Web.Security;

namespace CloudUri.Web.Controllers
{
    public class AboutController : SecureController
    {
        //
        // GET: /About/
        public ActionResult Index()
        {
            return View();
        }

    }
}
