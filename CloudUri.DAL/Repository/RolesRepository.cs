using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Roles repository
    /// </summary>
    public class RolesRepository : RepositoryBase<Role>, IRolesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RolesRepository"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public RolesRepository(IDbWrapper dbWrapper)
            : base(dbWrapper)
        {
        }

        /// <summary>
        /// Gets the stored procedure for inserting data to the table
        /// </summary>
        protected internal override string SPForInserting
        {
            get { return StoredProcedureNames.RoleInsert; }
        }

        /// <summary>
        /// Build parameters list for inserting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForInserting(Role entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Name", Value = entity.Name},
                           new DbParam {Name = "@Description", Value = entity.Description},
                       };
        }

        /// <summary>
        /// Gets the stored procedure for deleting data from the table
        /// </summary>
        protected internal override string SPForDeleting
        {
            get { return StoredProcedureNames.RoleDelete; }
        }

        /// <summary>
        /// Gets the stored procedure for updating data from the table
        /// </summary>
        protected internal override string SPForUpdating
        {
            get { return StoredProcedureNames.RoleUpdate; }
        }

        /// <summary>
        /// Build parameters list for updating
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForUpdating(Role entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Key", Value = entity.Key},
                           new DbParam {Name = "@Name", Value = entity.Name},
                           new DbParam {Name = "@Description", Value = entity.Description},
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting all the data from the table
        /// </summary>
        protected internal override string SPForGetRecords
        {
            get { return StoredProcedureNames.RoleGetAll; }
        }

        /// <summary>
        /// Reads entity from data reader
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        /// <returns>Built entity</returns>
        protected internal override Role ReadSingleEntity(IDataReader dataReader)
        {
            int key = dataReader.GetInt32(0);
            string name = dataReader.GetNullableString(1);
            string desc = dataReader.GetNullableString(2);

            return new Role {Key = key, Description = desc, Name = name};
        }

        /// <summary>
        /// Gets the stored procedure for getting element by ID
        /// </summary>
        protected internal override string SPForGetById
        {
            get { return StoredProcedureNames.RoleGetById; }
        }

        /// <summary>
        /// Stored procedure that returns number of elements in the table
        /// </summary>
        protected internal override string SPForCount
        {
            get { return StoredProcedureNames.RoleCount; }
        }
    }
}