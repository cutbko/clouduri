using System;
using System.Data;

namespace CloudUri.DAL.Database
{
    /// <summary>
    /// Stored procedure parameter
    /// </summary>
    public class DbParam : IEquatable<DbParam>
    {
        /// <summary>
        /// Gets or sets the parameter name (should start from @)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets parameter value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets SQL type
        /// </summary>
        public SqlDbType Type { get; set; }

        /// <summary>
        /// Gets or sets parameter direction
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets parameter size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="obj"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DbParam)) return false;
            return Equals((DbParam) obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(DbParam other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && Equals(other.Value, Value) && Equals(other.Type, Type) && Equals(other.Direction, Direction) && Equals(other.Size, Size);
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
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ (Value != null ? Value.GetHashCode() : 0);
                result = (result * 397) ^ Type.GetHashCode();
                result = (result * 397) ^ Direction.GetHashCode();
                result = (result * 397) ^ Size.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object. 
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Value: {1}, Type: {2}, Direction: {3}, Size: {4}", Name, Value, Type, Direction, Size);
        }
    }
}