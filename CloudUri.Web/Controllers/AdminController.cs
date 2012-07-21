using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudUri.Web.Security;

namespace CloudUri.Web.Controllers
{
    public class AdminController : SecureController
    {
        //
        // GET: /Admin/
        [Authorize(Roles = "administrators")]
        public string Index()
        {
            return User.Identity.Name;
        }

    }
}
