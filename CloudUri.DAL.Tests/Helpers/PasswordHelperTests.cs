using System;
using System.Collections.Generic;
using System.Linq;
using CloudUri.DAL.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudUri.DAL.Tests.Helpers
{
    /// <summary>
    /// Tests class for the <see cref="PasswordHelper"/>
    /// </summary>
    [TestClass]
    public class PasswordHelperTests
    {
        /// <summary>
        /// Tests the create salt method when it works in concurrence
        /// </summary>
        [TestMethod]
        public void CreateSaltGeneratesRandomSalts()
        {
            List<byte[]> salts = Enumerable.Range(0, 100).AsParallel().Select(x => PasswordHelper.CreateSalt(100)).ToList();

            for (int i = 0; i < salts.Count; i++)
            {
                for (int j = 0; j < salts.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    CollectionAssert.AreNotEqual(salts[i], salts[j]);
                }
            }
        }


        /// <summary>
        /// Tests CreateHashAndSalt with null arg
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateHashAndSaltArgumentNullCheck()
        {
            string salt;
            string hash;
            PasswordHelper.CreateHashAndSalt(null, out hash, out salt);
        }

        /// <summary>
        /// Tests CreateHashAndSalt with null arg
        /// </summary>
        [TestMethod]
        public void CreateHashAndSalt()
        {
            string salt;
            string hash;
            PasswordHelper.CreateHashAndSalt("Qwerty", out hash, out salt);

            Assert.IsNotNull(salt);
            Assert.AreEqual(PasswordHelper.SaltLength, salt.Length);
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
        }

        /// <summary>
        /// Test CreateSalt method with zero argument
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateSaltZeroArgumentTest()
        {
            PasswordHelper.CreateSalt(0);
        }

        /// <summary>
        /// Tests CreateSalt method
        /// </summary>
        [TestMethod]
        public void CreateSaltTest()
        {
            byte[] notExpectedArray = new byte[256];
            const int saltSize = 256;
            byte[] salt = PasswordHelper.CreateSalt(saltSize);

            Assert.AreEqual(256, salt.Length);

            CollectionAssert.AreNotEqual(notExpectedArray, salt);
        }

        /// <summary>
        /// Tests GenerateSaltedHash with not null result expected
        /// </summary>
        [TestMethod]
        public void GenerateSaltedHashTest()
        {
            var salt = PasswordHelper.CreateSalt(256);
            var passBytes = PasswordHelper.PasswordEncoding.GetBytes("test");
            var hash = PasswordHelper.GenerateSaltedHash(passBytes, salt);

            Assert.IsTrue(hash.Length > 0);
        }

        /// <summary>
        /// Tests ComparePasswordAndHash methos with null arguments
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComparePasswordAndHashZeroArgumentTest()
        {
            PasswordHelper.ComparePasswordAndHash(null, null, null);
        }

        /// <summary>
        /// Tests ComparePasswordAndHash same and different passwords
        /// </summary>
        [TestMethod]
        public void ComparePasswordAndHashTest()
        {
            const string password = "Test password";
            var salt = PasswordHelper.CreateSalt(PasswordHelper.PasswordEncoding.IsSingleByte ? PasswordHelper.SaltLength : PasswordHelper.SaltLength * 2);
            var hash = PasswordHelper.GenerateSaltedHash(PasswordHelper.PasswordEncoding.GetBytes(password), salt);

            string saltString = PasswordHelper.PasswordEncoding.GetString(salt);
            string hashString = PasswordHelper.PasswordEncoding.GetString(hash);
            var result = PasswordHelper.ComparePasswordAndHash(hashString, password, saltString);
            Assert.IsTrue(result);
           
            var result1 = PasswordHelper.ComparePasswordAndHash(hashString, "another pass", saltString);
            Assert.IsFalse(result1);

            var result2 = PasswordHelper.ComparePasswordAndHash(hashString, password, PasswordHelper.PasswordEncoding.GetString(PasswordHelper.CreateSalt(256)));
            Assert.IsFalse(result2);
        }
    }
}