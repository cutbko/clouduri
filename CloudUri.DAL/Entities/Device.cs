namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for Devices table
    /// </summary>
    public class Device : IEntity
    {
        /// <summary>
        /// Gets to sets the key
        /// </summary>
        /// <value>
        /// Match to Id (int) column
        /// </value>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the name field
        /// </summary>
        /// <value>
        /// Match to Name (nvarchar(max)) column
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type id 
        /// </summary>
        /// <value>
        /// Match to TypeId (int) column
        /// </value>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the device type
        /// </summary>
        /// <value>
        /// Expands type of device by TypeId field
        /// </value>
        public DeviceType Type { get; set; }

        /// <summary>
        /// Gets or sets the owner id
        /// </summary>
        /// <value>
        /// Match to OwnerId (int) column 
        /// </value>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the device owner
        /// </summary>
        /// <value>
        /// Expands user by OwnerId field
        /// </value>
        public User Owner { get; set; }
    }
}