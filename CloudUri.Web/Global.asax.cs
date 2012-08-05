using System;
using System.Collections;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CloudUri.Common.Logging;
using CloudUri.SAL.Services;
using CloudUri.Web.Security;

namespace CloudUri.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            FillRoutesForFeedsConroller(routes);

            //routes.MapRoute(
            //    "DevicesEdit",
            //    "Devices/Edit/{id}",
            //    new {controller = "Devices", action = "Edit", id = UrlParameter.Optional});

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "About", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        private static void FillRoutesForFeedsConroller(RouteCollection routes)
        {
            routes.MapRoute(
                "FeedDefault",
                "Feed",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedPages",
                "Feed/Page{page}",
                new { controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1 });

            routes.MapRoute(
                "FeedBySendingDevice",
                "Feed/From/{sendingDevice}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedBySendingDeviceWithPages",
                "Feed/From/{sendingDevice}/Page{page}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedByReceivingDevice",
                "Feed/To/{receivingDevice}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedByReceivingDeviceWithPages",
                "Feed/To/{receivingDevice}/Page{page}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedBySendingAndReceivingDevice",
                "Feed/From/{sendingDevice}/To/{receivingDevice}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});

            routes.MapRoute(
                "FeedBySendingAndReceivingDeviceWithNumbers",
                "Feed/From/{sendingDevice}/To/{receivingDevice}/Page{page}",
                new {controller = "Feed", action = "Index", sendingDevice = "All", receivingDevice = "All", page = 1});
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Logger.Log.Info("---------------------App started------------------");

            Bootstrapper.Initialise();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                return;
            }
            
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                //Extract the forms authentication cookie
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                SimpleSessionPersister.Username = authTicket.Name;
                string errorMessage;
                SimpleSessionPersister.Roles = DependencyResolver.Current.GetService<IAccountService>().GetRolesForUser(authTicket.Name, out errorMessage);
                if (errorMessage != null)
                {
                    Logger.Log.Error(errorMessage);
                }
            }
        }
    }
}