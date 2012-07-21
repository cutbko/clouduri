using System;
using System.Web;
using System.Web.Hosting;
using CloudUri.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudUri.Web.Tests.Security
{
    /// <summary>
    /// Tests for the <see cref="CloudIdentity"/> class
    /// </summary>
    [TestClass]
    public class CloudIdentityTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullContstructorArgumentTest()
        {
            new CloudIdentity(null);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            new CloudIdentity(string.Empty);
            new CloudIdentity("name");
        }

        [TestMethod]
        public void IsAuthenticatedTest()
        {
            Assert.IsTrue(new CloudIdentity("name").IsAuthenticated);
        }

        [TestMethod]
        public void IsAuthenticatedFalseTest()
        {
            Assert.IsFalse(new CloudIdentity(string.Empty).IsAuthenticated);
        }

        [TestMethod]
        public void NameTest()
        {
            Assert.AreEqual("name",new CloudIdentity("name").Name);
        }
    }
}