using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Users repository
    /// </summary>
    public interface IUsersRepository : IRepository<User>
    {
        /// <summary>
        /// Gets the user with roles from DB if username and password are correct
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="password">Password</param>
        /// <returns>Instance of user if provided credentials are correct, null if wrong</returns>
        User GetUserByNameAndPassword(string name, string password);

        /// <summary>
        /// Gets the user with roles from DB
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Instance of user if name is found, null if wrong</returns>
        User GetUserByName(string name);

        /// <summary>
        /// Creates user and adds roles to it
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Created user</returns>
        User CreateUser(User user);

        /// <summary>
        /// Checks if username available
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Availability</returns>
        bool CheckIfUsernameAvailable(string userName);

        /// <summary>
        /// Checks if email available
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Availability</returns>
        bool CheckIfEmailAvailable(string email);
    }
}