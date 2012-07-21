using System;
using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Device types repository
    /// </summary>
    public class DeviceTypesRepository : RepositoryBase<DeviceType>, IDeviceTypesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTypesRepository"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public DeviceTypesRepository(IDbWrapper dbWrapper) 
            : base(dbWrapper)
        {
        }

        /// <summary>
        /// Gets the stored procedure for inserting data to the table
        /// </summary>
        protected internal override string SPForInserting
        {
            get { return StoredProcedureNames.DeviceTypeInsert; }
        }

        /// <summary>
        /// Build parameters list for inserting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForInserting(DeviceType entity)
        {
            return new List<DbParam>
                       {
                           new DbParam { Name = "@Name", Value = entity.Name },
                           new DbParam { Name = "@DownloadUrl", Value = entity.DownloadUrl }
                       };
        }

        /// <summary>
        /// Gets the stored procedure for deleting data from the table
        /// </summary>
        protected internal override string SPForDeleting
        {
            get { return StoredProcedureNames.DeviceTypeDelete; }
        }

        /// <summary>
        /// Gets the stored procedure for updating data from the table
        /// </summary>
        protected internal override string SPForUpdating
        {
            get { return StoredProcedureNames.DeviceTypeUpdate; }
        }

        /// <summary>
        /// Build parameters list for updating
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForUpdating(DeviceType entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Id", Value = entity.Key },
                           new DbParam { Name = "@Name", Value = entity.Name },
                           new DbParam { Name = "@DownloadUrl", Value = entity.DownloadUrl }
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting all the data from the table
        /// </summary>
        protected internal override string SPForGetRecords
        {
            get { return StoredProcedureNames.DeviceTypeGetAll; }
        }

        /// <summary>
        /// Reads entity from data reader
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        /// <returns>Built entity</returns>
        protected internal override DeviceType ReadSingleEntity(IDataReader dataReader)
        {
            int key = dataReader.GetInt32(0);
            string name = dataReader.GetNullableString(1);
            string downoadUri = dataReader.GetNullableString(2);
            return new DeviceType { Key = key, Name = name, DownloadUrl = downoadUri };
        }

        /// <summary>
        /// Gets the stored procedure for getting element by ID
        /// </summary>
        protected internal override string SPForGetById
        {
            get { return StoredProcedureNames.DeviceTypeGetById; }
        }

        /// <summary>
        /// Stored procedure that returns number of elements in the table
        /// </summary>
        protected internal override string SPForCount
        {
            get { return StoredProcedureNames.DeviceTypeCount; }
        }
    }
}