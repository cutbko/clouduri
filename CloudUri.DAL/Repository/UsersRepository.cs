using System;
using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

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
                                     new DbParam {Name = "@Username", Value = entity.Username},
                                     new DbParam {Name = "@Email", Value = entity.Email},
                                     new DbParam {Name = "@PasswordHash", Value = entity.PasswordHash},
                                     new DbParam {Name = "@Salt", Value = entity.Salt}
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
    }
}