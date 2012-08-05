using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.Common.Logging;
using CloudUri.DAL;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public class AccountService : IAccountService
    {
        public const string UserNotFoundMessage = "User name or password is incorrect";
        public const string ErrorDuringReadingUserMessage = "An error occured during retreiving user from DB";
        public const string UserAlreadyExistsMessage = "User already exists in DB";
        public const string ErrorDuringUserCreation = "An error occured during user creation";
        public const string UserNameIsAlreadyTaken = "Oops... sorry somebody is already uses this name";
        public const string EmailIsAlreadyTaken = "Oops... sorry somebody is already registered with this email. Maybe it's you?";

        public const string UserDefaultRole = "users";

        private readonly IDALContext _dalContext;

        public AccountService(IDALContext dalContext)
        {
            if (dalContext == null)
            {
                throw new ArgumentNullException("dalContext");
            }

            _dalContext = dalContext;
        }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">User password</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Returns null if something wrong, returns instance of user with roles if provided credentials are correct</returns>
        public User ValidateUser(string userName, string password, out string errorMessage)
        {
            try
            {
                User user = _dalContext.UsersRepository.GetUserByNameAndPassword(userName, password);
                if (user == null)
                {
                    errorMessage = UserNotFoundMessage;
                    return null;
                }

                foreach (Role role in _dalContext.RolesRepository.GetRolesForUser(userName))
                {
                    user.Roles.Add(role);
                }

                errorMessage = null;
                return user;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ErrorDuringReadingUserMessage, ex);
                errorMessage = ErrorDuringReadingUserMessage;
                return null;
            }
        }

        /// <summary>
        /// Gets the user by name
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>User by name</returns>
        public User GetUserByName(string name, out string errorMessage)
        {
            try
            {
                User user = _dalContext.UsersRepository.GetUserByName(name);
                if (user == null)
                {
                    errorMessage = UserNotFoundMessage;
                    return null;
                }

                foreach (Role role in _dalContext.RolesRepository.GetRolesForUser(name))
                {
                    user.Roles.Add(role);
                }

                errorMessage = null;
                return user;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ErrorDuringReadingUserMessage, ex);
                errorMessage = ErrorDuringReadingUserMessage;
                return null;
            }
        }

        /// <summary>
        /// Gets roles for user
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>
        /// List of roles
        /// </returns>
        public List<string> GetRolesForUser(string name, out string errorMessage)
        {
            try
            {
                List<string> roles = _dalContext.RolesRepository.GetRolesForUser(name).Select(x=>x.Name).ToList();
                errorMessage = roles.Count > 0  ? null : UserNotFoundMessage;

                return roles;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ErrorDuringReadingUserMessage, ex);
                errorMessage = ErrorDuringReadingUserMessage;
                return null;
            }
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
            User user = new User
                {
                    Username = userName,
                    Email = email,
                    Password = password
                };
            user.Roles.Add(new Role
                {
                    Name = UserDefaultRole
                });

            try
            {
                if (!_dalContext.UsersRepository.CheckIfUsernameAvailable(userName))
                {
                    errorMessage = UserNameIsAlreadyTaken;
                    return null;
                }
                if (!_dalContext.UsersRepository.CheckIfEmailAvailable(email))
                {
                    errorMessage = EmailIsAlreadyTaken;
                    return null;
                }
                User createdUser = _dalContext.UsersRepository.CreateUser(user);
                errorMessage = null;

                return createdUser;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ErrorDuringUserCreation, ex);
                errorMessage = ErrorDuringUserCreation;
                return null;
            }
        }
    }
}