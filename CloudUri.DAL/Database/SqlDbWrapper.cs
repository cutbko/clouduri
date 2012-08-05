using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CloudUri.Common.Logging;

namespace CloudUri.DAL.Database
{
    /// <summary>
    /// Incapsulates all the calls to SQL server db
    /// </summary>
    public class SqlDbWrapper : IDbWrapper
    {
        private const string InvalidOperationDuringConnectionOpenMessage = "Cannot open a connection without specifying a data source or server or connection is already open.";
        private const string SqlError = "A connection-level error occurred while opening the connection";
        private const string PasswordExpired = "The specified password has expired or must be reset";

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["CloudUriDb"].ConnectionString;

        private static SqlConnection NewConnection
        {
            get
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                try
                {
                    connection.Open();
                }
                catch (InvalidOperationException exception)
                {
                    Logger.Log.Error(InvalidOperationDuringConnectionOpenMessage, exception);
                    throw new DbException(InvalidOperationDuringConnectionOpenMessage, exception);
                }
                catch (SqlException exception)
                {
                    string message = SqlError;

                    if (exception.Number == 18487 || exception.Number == 18488)
                    {
                        message = PasswordExpired;
                    }

                    Logger.Log.Error(message, exception);
                    throw new DbException(message, exception);
                }
                catch (Exception exception)
                {
                    Logger.Log.Error("Unknown exception during connection.Open", exception);
                    throw new DbException("Unknown exception during connection.Open", exception);
                }

                return connection;
            }
        }

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="connection">Db connection (must be disposed after IDataReader)</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Gotten data reader</returns>
        public IDataReader ExecuteSPReader(string storedProcedureName, IList<DbParam> parameters, out IDbConnection connection, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default)
        {
            SqlDataReader result;

            SqlConnection sqlConnection = NewConnection;
            var sqlCommand = CreateCommand(storedProcedureName, parameters, commandTimeout, sqlConnection);
            
            try
            {
                connection = sqlConnection;
                result = sqlCommand.ExecuteReader(commandBehavior);
            }
            catch (Exception exception)
            {
                DbException dbException = new DbException("Exception while executing sqlCommand.ExecuteSPReader()", exception, storedProcedureName, parameters);
                Logger.Log.Info(dbException);

                throw dbException;
            }

            return result;
        }

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
        public IDataReader ExecuteSPReader(string storedProcedureName, IList<DbParam> parameters, out IDbConnection connection, out IList<SqlParameter> outputParams, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default)
        {
            SqlDataReader result;

            SqlConnection sqlConnection = NewConnection;
            var sqlCommand = CreateCommand(storedProcedureName, parameters, commandTimeout, sqlConnection);

            try
            {
                connection = sqlConnection;
                result = sqlCommand.ExecuteReader(commandBehavior);
                outputParams = new List<SqlParameter>();
                for (int index = 0; index < sqlCommand.Parameters.Count; index++)
                {
                    var parameter = sqlCommand.Parameters[index];

                    if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                    {
                        outputParams.Add(parameter);
                    }
                }
            }
            catch (Exception exception)
            {
                DbException dbException = new DbException("Exception while executing sqlCommand.ExecuteSPReader()", exception, storedProcedureName, parameters);
                Logger.Log.Info(dbException);

                throw dbException;
            }

            return result;
        }

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Object returned from the stored procedure</returns>
        public object ExecuteSPScalar(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default)
        {
            object result;
            
            SqlConnection sqlConnection = NewConnection;
            var sqlCommand = CreateCommand(storedProcedureName, parameters, commandTimeout, sqlConnection);

            try
            {
                result = sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                DbException dbException = new DbException("Exception while executing sqlCommand.ExecuteScalar()", exception, storedProcedureName, parameters);
                Logger.Log.Info(dbException);

                throw dbException;
            }

            GetReturnedParameters(parameters, sqlCommand);

            return result;
        }


        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>Generic object returned from the stored procedure</returns>
        /// <typeparam name="TObjectType">Type of returning object</typeparam>
        public TObjectType ExecuteSPScalar<TObjectType>(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default)
        {
            object dbResult = ExecuteSPScalar(storedProcedureName, parameters, commandTimeout, commandBehavior);

            if (dbResult is TObjectType)
            {
                return (TObjectType) dbResult;
            }

            DbException dbException = new DbException(string.Format("ExecuteSPScalar returned result with value: {0} of the non-expected type ({1}). Expected type is: {2}", dbResult, dbResult.GetType(), typeof (TObjectType)), null, storedProcedureName, parameters);
            Logger.Log.Error(dbException);

            throw dbException;
        }

        /// <summary>
        /// Executes a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">A list of parameters</param>
        /// <param name="commandTimeout">Command timeout</param>
        /// <param name="commandBehavior">Command behaviour</param>
        /// <returns>The number of rows affected</returns>
        public int ExecuteSPNonQuery(string storedProcedureName, IList<DbParam> parameters, int commandTimeout = 0, CommandBehavior commandBehavior = CommandBehavior.Default)
        {
            int result;

            SqlConnection sqlConnection = NewConnection;
            var sqlCommand = CreateCommand(storedProcedureName, parameters, commandTimeout, sqlConnection);

            try
            {
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                DbException dbException = new DbException("Exception while executing sqlCommand.ExecuteSPNonQuery()", exception, storedProcedureName, parameters);
                Logger.Log.Info(dbException);

                throw dbException;
            }

            GetReturnedParameters(parameters, sqlCommand);

            return result;
        }

        private static SqlCommand CreateCommand(string storedProcedureName, IEnumerable<DbParam> parameters, int commandTimeout, SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = storedProcedureName;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = commandTimeout;

            foreach (DbParam parameter in parameters)
            {
                sqlCommand.Parameters.Add(new SqlParameter(parameter.Name, parameter.Type, parameter.Size) { Direction = parameter.Direction, Value = parameter.Value });
            }

            return sqlCommand;
        }

        private static void GetReturnedParameters(IList<DbParam> parameters, SqlCommand sqlCommand)
        {
            for (int index = 0; index < sqlCommand.Parameters.Count; index++)
            {
                var parameter = sqlCommand.Parameters[index];

                if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                {
                    parameters[index].Value = parameter.Value;
                }
            }
        }
    }
}