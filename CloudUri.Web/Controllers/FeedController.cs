using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudUri.Web.Security;

namespace CloudUri.Web.Controllers
{
    public class FeedController : SecureController
    {
        //
        // GET: /Feed/

        public ActionResult Index()
        {
            return View();
        }

    }
}
