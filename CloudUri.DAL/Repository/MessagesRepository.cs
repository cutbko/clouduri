using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Extensions;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Messages repository
    /// </summary>
    public class MessagesRepository : RepositoryBase<Message>, IMessagesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesRepository"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public MessagesRepository(IDbWrapper dbWrapper) 
            : base(dbWrapper)
        {
        }

        /// <summary>
        /// Gets the stored procedure for inserting data to the table
        /// </summary>
        protected internal override string SPForInserting
        {
            get { return StoredProcedureNames.MessageInsert; }
        }

        /// <summary>
        /// Build parameters list for inserting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForInserting(Message entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@MessageText", Value = entity.MessageText},
                           new DbParam {Name = "@FromId", Value = entity.FromId},
                           new DbParam {Name = "@ToId", Value = entity.ToId}
                       };
        }

        /// <summary>
        /// Gets the stored procedure for deleting data from the table
        /// </summary>
        protected internal override string SPForDeleting
        {
            get { return StoredProcedureNames.MessageInsert; }
        }

        /// <summary>
        /// Gets the stored procedure for updating data from the table
        /// </summary>
        protected internal override string SPForUpdating
        {
            get { return StoredProcedureNames.MessageUpdate; }
        }

        /// <summary>
        /// Build parameters list for updating
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal override IList<DbParam> BuildParametersForUpdating(Message entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Id", Value = entity.Key},
                           new DbParam {Name = "@MessageText", Value = entity.MessageText},
                           new DbParam {Name = "@FromId", Value = entity.FromId},
                           new DbParam {Name = "@ToId", Value = entity.ToId}
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting all the data from the table
        /// </summary>
        protected internal override string SPForGetRecords
        {
            get { return StoredProcedureNames.MessageGetAll; }
        }

        /// <summary>
        /// Reads entity from data reader
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        /// <returns>Built entity</returns>
        protected internal override Message ReadSingleEntity(IDataReader dataReader)
        {
            int key = dataReader.GetInt32(0);
            string messageText = dataReader.GetNullableString(1);
            int fromId = dataReader.GetInt32(2);
            int? toId = dataReader.GetNullableInt(3);

            return new Message
                       {
                           Key = key,
                           MessageText = messageText,
                           FromId = fromId,
                           ToId = toId
                       };
        }

        /// <summary>
        /// Gets the stored procedure for getting element by ID
        /// </summary>
        protected internal override string SPForGetById
        {
            get { return StoredProcedureNames.MessageGetById; }
        }

        /// <summary>
        /// Stored procedure that returns number of elements in the table
        /// </summary>
        protected internal override string SPForCount
        {
            get { return StoredProcedureNames.MessageCount; }
        }
    }
}