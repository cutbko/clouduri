using System;
using System.Collections.Generic;
using CloudUri.DAL.Helpers;

namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for Users table
    /// </summary>
    public class User : IEntity
    {
        /// <summary>
        /// Initializes new instance of the <see cref="User"/> class
        /// </summary>
        public User()
        {
            Roles = new List<Role>();    
        }

        /// <summary>
        /// Initializes new instance of the <see cref="User"/> class
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="name">User name</param>
        /// <param name="email">User email</param>
        /// <param name="hash">Password hash</param>
        /// <param name="salt">Pasword salt</param>
        public User(int id, string name, string email, string hash, string salt)
            : this()
        {
            Key = id;
            Username = name;
            Email = email;
            PasswordHash = hash;
            Salt = salt;
        }

        /// <summary>
        /// Gets or sets user key
        /// </summary>
        /// <value>
        /// Match to Id (int) column
        /// </value>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        /// <value>
        /// Match to Username (nvarchar(max)) column
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets user email
        /// </summary>
        /// <value>
        /// Match to Email (nvarchar(max)) column
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets the password hash if computed
        /// </summary>
        /// <value>
        /// Match to PasswordHash (nvarchar(max)) column. 
        /// </value>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Gets the password hash if computed 
        /// </summary>
        /// <value>
        /// Match to Salt (nvarchar(max)) column.
        /// </value>
        public string Salt { get; private set; }

        /// <summary>
        /// Sets the user password by creating <c>PasswordHash</c> and <c>Salt</c>
        /// </summary>
        public string Password
        {
            set
            {
                string salt;
                string passwordHash;
                PasswordHelper.CreateHashAndSalt(value, out passwordHash, out salt);

                Salt = salt;
                PasswordHash = passwordHash;
            }
        }

        /// <summary>
        /// Gets user the roles
        /// </summary>
        /// <value>
        /// Match to all roles of current user
        /// </value>
        public IList<Role> Roles { get; private set; }

    }
}