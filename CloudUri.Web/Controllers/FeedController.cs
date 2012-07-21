using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudUri.Web.Security;
using CloudUri.Web.ViewModels;

namespace CloudUri.Web.Controllers
{
    public class FeedController : SecureController
    {
        //
        // GET: /Feed/

        public ActionResult Index()
        {
            FeedViewModel feedViewModel = new FeedViewModel();
            return View(feedViewModel);
        }

        public ActionResult Index(string device)
        {
            FeedViewModel feedViewModel = new FeedViewModel();
            return View(feedViewModel);
        }
    }
}
