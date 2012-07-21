using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace CloudUri.Web.Security
{
    /// <summary>
    /// Cloud principal
    /// </summary>
    public class CloudPrincipal : IPrincipal
    {
        private readonly List<string> _roles;

        private readonly CloudIdentity _cloudIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudPrincipal"/> class
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="cloudIdentity"></param>
        public CloudPrincipal(List<string> roles, CloudIdentity cloudIdentity)
        {
            if (cloudIdentity == null)
            {
                throw new ArgumentNullException("cloudIdentity");
            }

            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            _roles = roles;
            _cloudIdentity = cloudIdentity;
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity
        {
            get { return _cloudIdentity; }
        }
    }
}