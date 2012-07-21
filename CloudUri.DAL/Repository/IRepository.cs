using System;
using System.Collections.Generic;
using CloudUri.DAL.Database;
using CloudUri.DAL.Entities;

namespace CloudUri.DAL.Repository
{
    /// <summary>
    /// Basic repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Inserts a record to the repository
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <exception cref="DbException">Throws DbException if error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        void Insert(TEntity entity);

        /// <summary>
        /// Deletes a record from the repository
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="DbException">Throws DbException if entity wasn't found or error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        void Delete(TEntity entity);

        /// <summary>
        /// Updates a record from the repository
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <exception cref="DbException">Throws DbException if entity wasn't found or error occured.</exception>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if entity is null</exception>
        void Update(TEntity entity);

        /// <summary>
        /// Returns all the items from repository
        /// </summary>
        /// <returns>A list of items</returns>
        IEnumerable<TEntity> GetRecords();

        /// <summary>
        /// Returns the single entity
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity with such id</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Calculates a number of entities in repository
        /// </summary>
        /// <returns>Number of entities</returns>
        int Count();
    }
}