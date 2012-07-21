using System;
using System.Security.Principal;

namespace CloudUri.Web.Security
{
    /// <summary>
    /// Identity that is used to identify the user
    /// </summary>
    public class CloudIdentity : IIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudIdentity"/> class
        /// </summary>
        /// <param name="userName"></param>
        public CloudIdentity(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            Name = userName;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }
    }
}