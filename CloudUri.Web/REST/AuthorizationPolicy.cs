using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using CloudUri.Common.Logging;
using CloudUri.SAL.Services;
using CloudUri.Web.Security;

namespace CloudUri.Web.REST
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        private string _id = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets a string that identifies this authorization component. 
        /// </summary>
        /// <returns>
        /// A string that identifies this authorization component.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public string Id { get { return _id; } }

        /// <summary>
        /// Evaluates whether a user meets the requirements for this authorization policy.
        /// </summary>
        /// <returns>
        /// false if the <see cref="M:System.IdentityModel.Policy.IAuthorizationPolicy.Evaluate(System.IdentityModel.Policy.EvaluationContext,System.Object@)"/> method for this authorization policy must be called if additional claims are added by other authorization policies to <paramref name="evaluationContext"/>; otherwise, true to state no additional evaluation is required by this authorization policy. 
        /// </returns>
        /// <param name="evaluationContext">An <see cref="T:System.IdentityModel.Policy.EvaluationContext"/> that contains the claim set that the authorization policy evaluates.</param><param name="state">A <see cref="T:System.Object"/>, passed by reference that represents the custom state for this authorization policy. </param><filterpriority>2</filterpriority>
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            IIdentity identity = HttpContext.Current.User.Identity;
            string errorMessage;
            List<string> rolesForUser = DependencyResolver.Current.GetService<IAccountService>().GetRolesForUser(identity.Name, out errorMessage);
            if(errorMessage==null)
            {
                evaluationContext.Properties["Principal"] = new CloudPrincipal(rolesForUser, new CloudIdentity(identity.Name));
                return true;
            }
            
            Logger.Log.Error(errorMessage);
            return false;
        }

        /// <summary>
        /// Gets a claim set that represents the issuer of the authorization policy. 
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.IdentityModel.Claims.ClaimSet"/> that represents the issuer of the authorization policy.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public ClaimSet Issuer { get { return ClaimSet.System; } }
    }
}