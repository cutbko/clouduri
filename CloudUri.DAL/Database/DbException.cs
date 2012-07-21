using System;
using System.Collections.Generic;

namespace CloudUri.DAL.Database
{
    /// <summary>
    /// Exception at database level
    /// </summary>
    public class DbException : Exception
    {
        /// <summary>
        /// Gets the executed stored procedure
        /// </summary>
        public string StoredProcedureName { get; private set; }

        /// <summary>
        /// Gets the params that were used for the SP
        /// </summary>
        public ICollection<DbParam> Params { get; private set; } 

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class
        /// </summary>
        /// <param name="message">Exception message</param>
        public DbException(string message)
            : this(message, null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DbException(string message, Exception innerException)
            : this(message, innerException, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        /// <param name="storedProcedure">Executed stored procedure</param>
        public DbException(string message, Exception innerException, string storedProcedure)
            : this(message, innerException, storedProcedure, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbException"/> class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        /// <param name="storedProcedure">Executed stored procedure</param>
        /// <param name="parameters">Stored procedure parameters</param>
        public DbException(string message, Exception innerException, string storedProcedure, ICollection<DbParam> parameters)
            : base(message, innerException)
        {
            StoredProcedureName = storedProcedure;
            Params = parameters;
        }

        /// <summary>
        /// Returns a string that represents the current object. 
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Stored procedure: {0}, params: {1}, exception: {2}", StoredProcedureName, Params != null ? string.Join(";" + Environment.NewLine, Params) : null, base.ToString());
        }
    }
}