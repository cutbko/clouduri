using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CloudUri.DAL.Database
{
    /// <summary>
    /// Incapsulates all the calls to db
    /// </summary>
    public interface IDbWrapper
    {
        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="connection">Db connection (must be disposed after IDataReader)</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Gotten data reader</returns>
        IDataReader ExecuteSPReader(string storedProcedureName, IList<DbParam> parameters, out IDbConnection connection, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default);

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="connection">Db connection (must be disposed after IDataReader)</param>
        /// <param name="outputParams">Output parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Gotten data reader</returns>
        IDataReader ExecuteSPReader(string storedProcedureName, IList<DbParam> parameters, out IDbConnection connection, out IList<SqlParameter> outputParams, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default);

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Object returned from the stored procedure</returns>
        object ExecuteSPScalar(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default);

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Generic object returned from the stored procedure</returns>
        /// <typeparam name="TObjectType">Type of returning object</typeparam>
        TObjectType ExecuteSPScalar<TObjectType>(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default);

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>The number of rows affected</returns>
        int ExecuteSPNonQuery(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default);

    }
}