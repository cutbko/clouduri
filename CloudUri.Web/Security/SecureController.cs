using System.Web.Mvc;

namespace CloudUri.Web.Security
{
    /// <summary>
    /// Controller that supports authorization through <see cref="SimpleSessionPersister"/>
    /// </summary>
    public class SecureController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                filterContext.HttpContext.User = new CloudPrincipal(SimpleSessionPersister.Roles, new CloudIdentity(SimpleSessionPersister.Username));    
            }

            base.OnAuthorization(filterContext);
        }
    }
}