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

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "About", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

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
                SimpleSessionPersister.Roles = DependencyResolver.Current.GetService<IAccountService>().GetRolesForUser(authTicket.Name);
            }
        }
    }
}