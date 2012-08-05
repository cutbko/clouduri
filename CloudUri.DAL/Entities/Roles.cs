using System;

namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for Roles table
    /// </summary>
    public class Role : IEntity, IEquatable<Role>
    {
        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Key == other.Key && string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Key;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Role left, Role right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Role left, Role right)
        {
            return !Equals(left, right);
        }

        #endregion

        /// <summary>
        /// Gets or sets the Key 
        /// </summary>
        /// <value>
        /// Match to Id (int) column
        /// </value>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets role name 
        /// </summary>
        /// <value>
        /// Match to Name (nvarchar(max)) column
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets role description
        /// </summary>
        /// <value>
        /// Match to Description (nvarchar(max)) column 
        /// </value>
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Role) obj);
        }
    }
}
