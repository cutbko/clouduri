using System.Collections.Generic;
using System.Data;
using System.Linq;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;
using CloudUri.DAL.Helpers;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// User repository
    /// </summary>
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersRepository"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public UsersRepository(IDbWrapper dbWrapper) 
            : base(dbWrapper)
        {
        }

        protected internal override User ReadSingleEntity(IDataReader dataReader)
        {
            var key = dataReader.GetInt32(0);
            var name = dataReader.GetNullableString(1);
            var mail = dataReader.GetNullableString(2);
            var hash = dataReader.GetNullableString(3);
            var salt = dataReader.GetNullableString(4);
            return new User(key, name, mail, hash, salt);
        }

        #region CRUD Parameters

        protected internal override IList<DbParam> BuildParametersForInserting(User entity)
        {
            return new List<DbParam>
                                 {
                                     new DbParam {Name = "@Username", Value = entity.Username, Type = SqlDbType.NVarChar},
                                     new DbParam {Name = "@Email", Value = entity.Email, Type = SqlDbType.NVarChar},
                                     new DbParam {Name = "@PasswordHash", Value = entity.PasswordHash, Type = SqlDbType.NVarChar},
                                     new DbParam {Name = "@Salt", Value = entity.Salt, Type = SqlDbType.NVarChar}
                                 };
        }

        protected internal override IList<DbParam> BuildParametersForUpdating(User entity)
        {
            return new List<DbParam>
                                 {
                                     new DbParam {Name = "@Id", Value = entity.Key},
                                     new DbParam {Name = "@Username", Value = entity.Username},
                                     new DbParam {Name = "@Email", Value = entity.Email},
                                     new DbParam {Name = "@PasswordHash", Value = entity.PasswordHash},
                                     new DbParam {Name = "@Salt", Value = entity.Salt}
                                 };
        }

        #endregion

        #region Stored Procedures

        protected internal override string SPForGetRecords
        {
            get { return StoredProcedureNames.UserGetRecords; }
        }

        protected internal override string SPForInserting
        {
            get { return StoredProcedureNames.UserInsert; }
        }

        protected internal override string SPForDeleting
        {
            get { return StoredProcedureNames.UserDelete; }
        }

        protected internal override string SPForUpdating
        {
            get { return StoredProcedureNames.UserUpdate; }
        }

        protected internal override string SPForGetById
        {
            get { return StoredProcedureNames.UserGetById; }
        }

        protected internal override string SPForCount
        {
            get { return StoredProcedureNames.UserCount; }
        }

        #endregion

        /// <summary>
        /// Gets the user with roles from DB if username and password are correct
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="password">Password</param>
        /// <returns>Instance of user if provided credentials are correct, null if wrong</returns>
        public User GetUserByNameAndPassword(string name, string password)
        {
            User user = GetUserByName(name);

            if(user != null &&  PasswordHelper.ComparePasswordAndHash(user.PasswordHash, password, user.Salt))
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// Gets the user with roles from DB
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Instance of user if name is found, null if wrong</returns>
        public User GetUserByName(string name)
        {
            User user = null;
            IDbConnection connection;
            IDataReader reader = DbWrapper.ExecuteSPReader(StoredProcedureNames.UserGetByName, new List<DbParam> { new DbParam { Name = "@UserName", Value = name, Type = SqlDbType.NVarChar } }, out connection);

            UtilizeConnectionAndReader(connection, reader, (c, r) =>
            {
                if (reader.Read())
                {
                    user = ReadSingleEntity(reader);
                }
            });

            return user;
        }

        public User CreateUser(User user)
        {
            Insert(user);
            AddRolesToUser(user.Key, user.Roles.Select(x => x.Name));

            return user;
        }

        /// <summary>
        /// Checks if username available
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Availability</returns>
        public bool CheckIfUsernameAvailable(string userName)
        {
            return DbWrapper.ExecuteSPScalar<int>(StoredProcedureNames.UserNameExists, new List<DbParam> { new DbParam { Name = "@UserName", Value = userName, Type = SqlDbType.NVarChar } }) == 0;
        }

        /// <summary>
        /// Checks if email available
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Availability</returns>
        public bool CheckIfEmailAvailable(string email)
        {
            return DbWrapper.ExecuteSPScalar<int>(StoredProcedureNames.UserEmailExists, new List<DbParam> { new DbParam { Name = "@Email", Value = email, Type = SqlDbType.NVarChar } }) == 0;
        }

        protected void AddRolesToUser(int key, IEnumerable<string> roles)
        {
            foreach (string role in roles)
            {
                DbWrapper.ExecuteSPNonQuery(StoredProcedureNames.AddRoleToUser,
                                            new List<DbParam>
                                                {
                                                    new DbParam {Name = "@UserId", Value = key, Type = SqlDbType.Int},
                                                    new DbParam {Name = "@RoleName", Value = role, Type = SqlDbType.NVarChar}
                                                });
            }
        }
    }
}