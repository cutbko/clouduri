using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public class AccountService : IAccountService
    {
        public User CreateUser(string username, string password, string email)
        {
            throw new System.NotImplementedException();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetRolesForUser(string username)
        {
            throw new System.NotImplementedException();
        }

        public void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }
    }
}