using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Interface for roles repository
    /// </summary>
    public interface IRolesRepository : IRepository<Role>
    {
        /// <summary>
        /// Get roles for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>List of roles</returns>
        List<Role> GetRolesForUser(string name);
    }
}