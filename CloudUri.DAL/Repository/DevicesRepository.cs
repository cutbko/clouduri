using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Devices repository
    /// </summary>
    public class DevicesRepository : RepositoryBase<Device>, IDevicesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesRepository"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public DevicesRepository(IDbWrapper dbWrapper) 
            : base(dbWrapper)
        {
        }

        /// <summary>
        /// Gets the stored procedure for inserting data to the table
        /// </summary>
        protected internal override string SPForInserting
        {
            get { return StoredProcedureNames.DeviceInsert; }
        }

        /// <summary>
        /// Build parameters list for inserting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForInserting(Device entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Name", Value = entity.Name, Type = SqlDbType.NVarChar},
                           new DbParam {Name = "@TypeId", Value = entity.TypeId, Type = SqlDbType.Int},
                           new DbParam {Name = "@OwnerId", Value = entity.OwnerId, Type = SqlDbType.Int}
                       };
        }

        /// <summary>
        /// Gets the stored procedure for deleting data from the table
        /// </summary>
        protected internal override string SPForDeleting
        {
            get { return StoredProcedureNames.DeviceDelete; }
        }

        /// <summary>
        /// Gets the stored procedure for updating data from the table
        /// </summary>
        protected internal override string SPForUpdating
        {
            get { return StoredProcedureNames.DeviceUpdate;; }
        }

        /// <summary>
        /// Build parameters list for updating
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForUpdating(Device entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Id", Value = entity.Key,Type = SqlDbType.Int},
                           new DbParam {Name = "@Name", Value = entity.Name,Type = SqlDbType.NVarChar},
                           new DbParam {Name = "@TypeId", Value = entity.TypeId,Type = SqlDbType.Int},
                           new DbParam {Name = "@OwnerId", Value = entity.OwnerId,Type = SqlDbType.Int}
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting all the data from the table
        /// </summary>
        protected internal override string SPForGetRecords
        {
            get { return StoredProcedureNames.DeviceGetAll; }
        }

        /// <summary>
        /// Reads entity from data reader
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        /// <returns>Built entity</returns>
        protected internal override Device ReadSingleEntity(IDataReader dataReader)
        {
            int key = dataReader.GetInt32(0);
            string name = dataReader.GetNullableString(1);
            int typeId = dataReader.GetInt32(2);
            int ownerId = dataReader.GetInt32(3);

            return new Device
                       {
                           Key = key,
                           Name = name,
                           OwnerId = ownerId,
                           TypeId = typeId
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting element by ID
        /// </summary>
        protected internal override string SPForGetById
        {
            get { return StoredProcedureNames.DeviceGetById; }
        }

        /// <summary>
        /// Stored procedure that returns number of elements in the table
        /// </summary>
        protected internal override string SPForCount
        {
            get { return StoredProcedureNames.DeviceCount; }
        }

        public List<Device> GetDevicesForUser(string userName)
        {
            List<Device> devices = new List<Device>();
            IDbConnection connection;
            IDataReader dataReader = DbWrapper.ExecuteSPReader(StoredProcedureNames.DeviceGetForUser, new List<DbParam> { new DbParam { Name = "@UserName", Type = SqlDbType.NVarChar, Value = userName } }, out connection);

            UtilizeConnectionAndReader(connection, dataReader, (c,d)=>
                {
                    while (dataReader.Read())
                    {
                        devices.Add(ReadSingleEntity(dataReader));
                    }
                });

            return devices;
        }
    }
}