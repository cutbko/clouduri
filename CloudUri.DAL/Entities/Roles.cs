namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for Roles table
    /// </summary>
    public class Role : IEntity
    {
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
    }
}
