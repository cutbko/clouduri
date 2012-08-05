using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    /// <summary>
    /// Account service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Returns null if something wrong, returns instance of user with roles if provided credentials are correct</returns>
        User ValidateUser(string userName, string password, out string errorMessage);

        /// <summary>
        /// Gets the user by name
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>User by name</returns>
        User GetUserByName(string name, out string errorMessage);

        /// <summary>
        /// Gets roles for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>
        /// List of roles
        /// </returns>
        List<string> GetRolesForUser(string name, out string errorMessage);

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Created user</returns>
        User CreateUser(string userName, string email, string password, out string errorMessage);
    }
}