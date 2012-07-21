using System.Collections.Generic;
using System.Web;

namespace CloudUri.Web.Security
{
    /// <summary>
    /// Class for storing and retreiving session data
    /// </summary>
    public class SimpleSessionPersister
    {
        public const string UsernameSessionVar = "username";

        public const string RolesSessionVar = "roles";

        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }

                object sessionVar = HttpContext.Current.Session[UsernameSessionVar];

                return sessionVar == null ? null : sessionVar as string;

            }

            set { HttpContext.Current.Session[UsernameSessionVar] = value; }
        }
        
        /// <summary>
        /// Gets or sets user roles
        /// </summary>
        public static List<string> Roles
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }

                object sessionVar = HttpContext.Current.Session[RolesSessionVar];

                return sessionVar == null ? null : sessionVar as List<string>;

            }

            set { HttpContext.Current.Session[RolesSessionVar] = value; }
        }
    }
}