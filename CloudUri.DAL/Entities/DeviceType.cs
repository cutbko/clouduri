namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for DeviceTypes table
    /// </summary>
    public class DeviceType : IEntity
    {
        /// <summary>
        /// Gets or sets the device type key
        /// </summary>
        /// <value>
        /// Match to Id (int) column
        /// </value>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the device type name
        /// </summary>
        /// <value>
        /// Match to Name (nvarchar(max)) column
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets device type description
        /// </summary>
        /// <value>
        /// Match to DownloadUrl (nvarchar(max)) column 
        /// </value>
        public string DownloadUrl { get; set; }
    }
}