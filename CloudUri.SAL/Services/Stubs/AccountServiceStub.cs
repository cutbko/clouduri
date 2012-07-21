using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services.Stubs
{
    /// <summary>
    /// Stub for account service
    /// </summary>
    public class AccountServiceStub : IAccountService
    {
        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <returns>Returns null if something wrong, returns instance of user with roles if provided credentials are correct</returns>
        public User ValidateUser(string userName, string password)
        {
            if (userName == "admin" && password == "P@ssw0rd")
            {
                User user = new User(1, "admin", "admin@clouduri.com", string.Empty, string.Empty);
                user.Roles.Add(new Role {Key = 1, Name = "administrators"});
                user.Roles.Add(new Role {Key = 1, Name = "users"});

                return user;
            }

            return null;
        }

        /// <summary>
        /// Gets the user by name
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>User by name</returns>
        public User GetUserByName(string name)
        {
            return new User(1, name, name + "@hotmail.com", string.Empty, string.Empty);
        }

        /// <summary>
        /// Gets roles for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>
        /// List of roles
        /// </returns>
        public List<string> GetRolesForUser(string name)
        {
            if (name == "admin")
            {
                return new List<string>
                           {
                               "administrators",
                               "users"
                           };
            }

            return null;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Created user</returns>
        public User CreateUser(string userName, string email, string password, out string errorMessage)
        {
            if (userName == "cutbko")
            {
                errorMessage = null;
                User user = new User {Key = 1, Email = email, Username = userName, Password = password};
                user.Roles.Add(new Role {Key = 1, Name = "users"});
                return user;
            }

            if (userName == "semen")
            {
                errorMessage = "User is already created";
                return null;
            }

            errorMessage = "Unknown error";
            return null;
        }
    }
}