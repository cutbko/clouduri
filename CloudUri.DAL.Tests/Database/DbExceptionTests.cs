using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using CloudUri.DAL.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudUri.DAL.Tests.Database
{
    /// <summary>
    /// Tests the <see cref="DbException"/> class
    /// </summary>
    [TestClass]
    public class DbExceptionTests
    {
        /// <summary>
        /// Tests the first constructor
        /// </summary>
        [TestMethod]
        public void Constructor1Test()
        {
            const string message = "Message";
            DbException dbException = new DbException(message);
            Assert.AreEqual(message, dbException.Message);
            Assert.IsNull(dbException.InnerException);
            Assert.IsNull(dbException.Params);
            Assert.IsNull(dbException.StoredProcedureName);
        }

        /// <summary>
        /// Tests the second constructor
        /// </summary>
        [TestMethod]
        public void Constructor2Test()
        {
            const string message = "Message";
            InvalidOperationException exception = new InvalidOperationException();
            DbException dbException = new DbException(message, exception);
            Assert.AreEqual(message, dbException.Message);
            Assert.AreEqual(exception, dbException.InnerException);
            Assert.IsNull(dbException.Params);
            Assert.IsNull(dbException.StoredProcedureName);
        }

        /// <summary>
        /// Tests the third constructor
        /// </summary>
        [TestMethod]
        public void Constructor3Test()
        {
            const string message = "Message";
            const string sp = "storedprocedure";
            InvalidOperationException exception = new InvalidOperationException();
            DbException dbException = new DbException(message, exception, sp);
            Assert.AreEqual(message, dbException.Message);
            Assert.AreEqual(exception, dbException.InnerException);
            Assert.IsNull(dbException.Params);
            Assert.AreEqual(sp, dbException.StoredProcedureName);
        }

        /// <summary>
        /// Tests the fourth constructor
        /// </summary>
        [TestMethod]
        public void Constructor4Test()
        {
            const string message = "Message";
            const string sp = "storedprocedure";
            InvalidOperationException exception = new InvalidOperationException();
            ICollection<DbParam>  @params = new Collection<DbParam>();
            DbException dbException = new DbException(message, exception, sp, @params);
            Assert.AreEqual(message, dbException.Message);
            Assert.AreEqual(exception, dbException.InnerException);
            Assert.AreEqual(@params, dbException.Params);
            Assert.AreEqual(sp, dbException.StoredProcedureName);
        }

        /// <summary>
        /// Tests the tostring() method
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            const string message = "Message";
            const string sp = "storedprocedure";
            InvalidOperationException exception = new InvalidOperationException();
            ICollection<DbParam> @params = new Collection<DbParam> { new DbParam { Direction = ParameterDirection.Output, Name = "@", Size = 4, Type = SqlDbType.Money, Value = 123 } };
            DbException dbException = new DbException(message, exception, sp, @params);
            const string expectedTostring = "Stored procedure: storedprocedure, params: Name: @, Value: 123, Type: Money, Direction: Output, Size: 4, exception: CloudUri.DAL.Database.DbException: Message ---> System.InvalidOperationException: Operation is not valid due to the current state of the object.\r\n   --- End of inner exception stack trace ---";

            Assert.AreEqual(expectedTostring, dbException.ToString());
        }

        /// <summary>
        /// Tests the tostring() method
        /// </summary>
        [TestMethod]
        public void ToStringWithNullParamsTest()
        {
            const string message = "Message";
            const string sp = "storedprocedure";
            InvalidOperationException exception = new InvalidOperationException();
            DbException dbException = new DbException(message, exception, sp);
            const string expectedTostring = "Stored procedure: storedprocedure, params: , exception: CloudUri.DAL.Database.DbException: Message ---> System.InvalidOperationException: Operation is not valid due to the current state of the object.\r\n   --- End of inner exception stack trace ---";

            Assert.AreEqual(expectedTostring, dbException.ToString());
        }
    }
}