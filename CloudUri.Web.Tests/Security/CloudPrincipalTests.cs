using System;
using System.Collections.Generic;
using CloudUri.DAL.Entities;
using CloudUri.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudUri.Web.Tests.Security
{
    /// <summary>
    /// Tests for the <see cref="CloudPrincipal"/> class
    /// </summary>
    [TestClass]
    public class CloudPrincipalTests
    {
        private readonly CloudIdentity _cloudIdentity = new CloudIdentity("name");
        private readonly List<string> _roles = new List<string> {"role1"};

        [TestMethod]
        public void ConstructorTests()
        {
            try
            {
                new CloudPrincipal(null, _cloudIdentity);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                
            }

            try
            {
                new CloudPrincipal(_roles, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

            }

            new CloudPrincipal(_roles, _cloudIdentity);
        }

        [TestMethod]
        public void IsInRoleTest()
        {
            CloudPrincipal cloudPrincipal = new CloudPrincipal(_roles, _cloudIdentity);
            Assert.IsTrue(cloudPrincipal.IsInRole(_roles[0]));
        }

        [TestMethod]
        public void IsInRoleFalseTest()
        {
            CloudPrincipal cloudPrincipal = new CloudPrincipal(_roles, _cloudIdentity);
            Assert.IsFalse(cloudPrincipal.IsInRole("role2"));
        }

        [TestMethod]
        public void IdentityTest()
        {
            CloudPrincipal cloudPrincipal = new CloudPrincipal(_roles, _cloudIdentity);
            Assert.AreSame(_cloudIdentity, cloudPrincipal.Identity);
        }
    }
}