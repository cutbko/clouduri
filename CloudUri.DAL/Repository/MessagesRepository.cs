using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            DateTime createdOn = dataReader.GetDateTime(4);

            return new Message
                       {
                           Key = key,
                           MessageText = messageText,
                           FromId = fromId,
                           ToId = toId,
                           CreatedOn = createdOn
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

        /// <summary>
        /// Get messages for user
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="sendingDevice">Sending device name. If null you will get all the messages.</param>
        /// <param name="receivingDevice">Receiving device name. If null you will get all the messages.</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <param name="page">Page</param>
        /// <param name="pagesTotal">Total pages</param>
        /// <returns>Messages for user</returns>
        public List<Message> GetMessagesForUser(string username, string sendingDevice, string receivingDevice, int itemsPerPage, int page, out int pagesTotal)
        {
            List<Message> messages = new List<Message>();
            DbParam userName = new DbParam
                {
                    Name = "@UserName", Type = SqlDbType.NVarChar, Value = username
                };
            DbParam itemsPerPageParam = new DbParam
                {
                    Name = "@ItemsPerPage", Type = SqlDbType.Int, Value = itemsPerPage
                };

            List<DbParam> parameters = new List<DbParam>
                {
                    userName,
                    itemsPerPageParam,
                    new DbParam
                        {
                            Name = "@Page",
                            Type = SqlDbType.Int,
                            Value = page
                        },
                    new DbParam
                        {
                            Name = "@PagesTotal",
                            Direction = ParameterDirection.Output,
                            Type = SqlDbType.Int,
                            Size = 4
                        }
                };

            string storedProcedure;


            if (string.IsNullOrWhiteSpace(sendingDevice) && string.IsNullOrWhiteSpace(receivingDevice))
            {
                storedProcedure = StoredProcedureNames.MessagesForUser;
            }
            else if (!string.IsNullOrWhiteSpace(sendingDevice) && string.IsNullOrWhiteSpace(receivingDevice))
            {
                storedProcedure = StoredProcedureNames.MessagesForUserGetBySendingDevice;
                parameters.Add(new DbParam
                    {
                        Name = "@SendingDevice",
                        Type = SqlDbType.NVarChar,
                        Value = sendingDevice
                    });
            }
            else if (string.IsNullOrWhiteSpace(sendingDevice) && !string.IsNullOrWhiteSpace(receivingDevice))
            {
                storedProcedure = StoredProcedureNames.MessagesForUserGetByReceivingDevice;
                parameters.Add(new DbParam
                {
                    Name = "@ReceivingDevice",
                    Type = SqlDbType.NVarChar,
                    Value = receivingDevice
                });
            }
            else
            {
                storedProcedure = StoredProcedureNames.MessagesForUserGetByDevices;
                parameters.Add(new DbParam
                {
                    Name = "@SendingDevice",
                    Type = SqlDbType.NVarChar,
                    Value = sendingDevice
                });
                parameters.Add(new DbParam
                {
                    Name = "@ReceivingDevice",
                    Type = SqlDbType.NVarChar,
                    Value = receivingDevice
                });
            }


            IDbConnection connection;
            IList<SqlParameter> outputParams;
            IDataReader reader = DbWrapper.ExecuteSPReader(storedProcedure, parameters, out connection, out outputParams);

            UtilizeConnectionAndReader(connection, reader, (c,r) =>
                {
                    while (r.Read())
                    {
                        messages.Add(ReadSingleEntity(r));
                    }
                });

            pagesTotal = (int) outputParams.Single(x => x.ParameterName == "@PagesTotal").Value;
            
            return messages;
        }
    }
}