using System;
using System.Collections.Generic;
using System.Data;
using CloudUri.Common.Logging;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Imlements basic repository functions
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDbWrapper DbWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class
        /// </summary>
        /// <param name="dbWrapper">Instance of the DB wrapper</param>
        protected RepositoryBase(IDbWrapper dbWrapper)
        {
            if (dbWrapper == null)
            {
                throw new ArgumentNullException("dbWrapper");
            }

            DbWrapper = dbWrapper;
        }

        /// <summary>
        /// Inserts a record to the repository
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <exception cref="DbException">Throws DbException if error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Logger.Log.InfoFormat("Inserting new entity: {0}", entity);

            try
            {
                entity.Key = DbWrapper.ExecuteSPScalar<int>(SPForInserting, BuildParametersForInserting(entity));
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during inserting: {0}, exception: {1}", entity, exception);
                throw new DbException(string.Format("Error during inserting: {0}", entity), exception);
            }
        }

        /// <summary>
        /// Deletes a record from the repository
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="DbException">Throws DbException if entity wasn't found or error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Logger.Log.InfoFormat("Deleting the entity: {0}", entity);

            try
            {
                if(DbWrapper.ExecuteSPNonQuery(SPForDeleting, BuildParametersForDeleting(entity)) == 0)
                {
                    string errorMessage = string.Format("Delete failed. Entity wasn't found in db. Entity: {0}", entity);
                    Logger.Log.InfoFormat(errorMessage);
                    throw new DbException(errorMessage);
                }
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during deleting: {0}, exception: {1}", entity, exception);
                throw new DbException(string.Format("Error during deleting: {0} from DB", entity), exception);
            }
        }

        /// <summary>
        /// Updates a record from the repository
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <exception cref="DbException">Throws DbException if entity wasn't found or error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Logger.Log.InfoFormat("Updating the entity: {0}", entity);

            try
            {
                if (DbWrapper.ExecuteSPNonQuery(SPForUpdating, BuildParametersForUpdating(entity)) == 0)
                {
                    string errorMessage = string.Format("Update failed. Entity wasn't found in db. Entity: {0}", entity);
                    Logger.Log.InfoFormat(errorMessage);
                    throw new DbException(errorMessage);
                }
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during updating: {0}, exception: {1}", entity, exception);
                throw new DbException(string.Format("Error during updatind: {0} from DB", entity), exception);
            }
        }

        /// <summary>
        /// Returns all the items from repository
        /// </summary>
        /// <returns>A list of items</returns>
        public IEnumerable<TEntity> GetRecords()
        {
            Logger.Log.InfoFormat("Getting N records from db");

            try
            {
                var entities = new List<TEntity>();
                IDbConnection connection;
                IDataReader dataReader = DbWrapper.ExecuteSPReader(SPForGetRecords, new List<DbParam>(), out connection);

                UtilizeConnectionAndReader(connection, dataReader, (dbConnection, reader) =>
                                                                       {
                                                                           while (dataReader.Read())
                                                                           {
                                                                               entities.Add(ReadSingleEntity(dataReader));
                                                                           }
                                                                       });
                return entities;
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during reading all the entities from DB, exception: {0}", exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Log.ErrorFormat("Unknown error during reading all the entities from DB, exception: {0}", exception);
                throw new DbException("Unknown error during reading all the entities from DB", exception);
            }
        }

        /// <summary>
        /// Returns the single entity
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity with such id</returns>
        public TEntity GetById(int id)
        {
            Logger.Log.InfoFormat("Getting entity by id : {0}", id);

            try
            {
                TEntity result = null;
                IDbConnection connection;
                IDataReader dataReader = DbWrapper.ExecuteSPReader(SPForGetById, new List<DbParam> { new DbParam { Name = "@id", Value = id } }, out connection);

                UtilizeConnectionAndReader(connection, dataReader, (dbConnection, reader) =>
                                                                       {
                                                                           if (dataReader.Read())
                                                                           {
                                                                               result = ReadSingleEntity(dataReader);
                                                                           }

                                                                       });

                return result;
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during reading entity with id {0} from DB, exception: {1}", id, exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Log.ErrorFormat("Unknown during reading entity with id {0} from DB, exception: {1}", id, exception);
                throw new DbException(string.Format("Unknown during reading entity with id {0} from DB", id), exception);
            }
        }

        /// <summary>
        /// Calculates a number of entities in repository
        /// </summary>
        /// <returns>Number of entities</returns>
        public int Count()
        {
            Logger.Log.InfoFormat("Getting number of entities");

            try
            {
                return DbWrapper.ExecuteSPScalar<int>(SPForCount, new List<DbParam>());
            }
            catch (DbException exception)
            {
                Logger.Log.ErrorFormat("Error during counting the entities, exception: {0}", exception);
                throw;
            }
            catch (Exception exception)
            {
                Logger.Log.ErrorFormat("Unknown error during counting the entities, exception: {0}", exception);
                throw new DbException("Unknown error during counting the entities", exception);
            }
        }
        
        /// <summary>
        /// Gets the stored procedure for inserting data to the table
        /// </summary>
        protected internal abstract string SPForInserting { get; }

        /// <summary>
        /// Build parameters list for inserting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal abstract IList<DbParam> BuildParametersForInserting(TEntity entity);

        /// <summary>
        /// Gets the stored procedure for deleting data from the table
        /// </summary>
        protected internal abstract string SPForDeleting { get; }

        /// <summary>
        /// Build parameters list for deleting
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal virtual IList<DbParam> BuildParametersForDeleting(TEntity entity)
        {
            return new List<DbParam>
                       {
                           new DbParam {Name = "@Id", Value = entity.Key}
                       };
        }


        /// <summary>
        /// Gets the stored procedure for updating data from the table
        /// </summary>
        protected internal abstract string SPForUpdating { get; }

        /// <summary>
        /// Build parameters list for updating
        /// </summary>
        /// <param name="entity">Entity for building</param>
        /// <returns>List of db parameters</returns>
        protected internal abstract IList<DbParam> BuildParametersForUpdating(TEntity entity);

        /// <summary>
        /// Gets the stored procedure for getting all the data from the table
        /// </summary>
        protected internal abstract string SPForGetRecords { get; }

        /// <summary>
        /// Reads entity from data reader
        /// </summary>
        /// <param name="dataReader">Data reader</param>
        /// <returns>Built entity</returns>
        protected internal abstract TEntity ReadSingleEntity(IDataReader dataReader);

        /// <summary>
        /// Gets the stored procedure for getting element by ID
        /// </summary>
        protected internal abstract string SPForGetById { get; }

        /// <summary>
        /// Stored procedure that returns number of elements in the table
        /// </summary>
        protected internal abstract string SPForCount { get; }

        /// <summary>
        /// Performs the action and disposes reader and connection
        /// </summary>
        /// <param name="connection">Db connection</param>
        /// <param name="reader">Data reader</param>
        /// <param name="action">Action to perform</param>
        protected internal void UtilizeConnectionAndReader(IDbConnection connection, IDataReader reader, Action<IDbConnection, IDataReader> action)
        {
            using (connection)
            {
                using (reader)
                {
                    action(connection, reader);
                }
            }
        }
    }
}