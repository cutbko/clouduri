namespace CloudUri.DAL.Entities
{
    /// <summary>
    /// Entity for Messages table
    /// </summary>
    public class Message : IEntity
    {
        /// <summary>
        /// Gets or sets the message key
        /// </summary>
        /// <value>
        /// Match to Id (int) column
        /// </value>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the message text
        /// </summary>
        /// <value>
        /// Match to MessageText (nvarchar(max)) column
        /// </value>
        public string MessageText { get; set; }

        /// <summary>
        ///  Gets or sets fromId
        /// </summary>
        /// <value>
        /// Match to FromId (int) column
        /// </value>
        public int FromId { get; set; }

        /// <summary>
        /// Gets or sets sender device
        /// </summary>
        /// <value>
        /// Expands sender device by FromId field
        /// </value>
        public Device SenderDevice { get; set; }

        /// <summary>
        /// Gets or sets the to id
        /// </summary>
        /// <value>
        /// Match to ToId (int) column 
        /// </value>
        public int? ToId { get; set; }

        /// <summary>
        /// Gets or sets the receiver device
        /// </summary>
        /// <value>
        /// Expands reciever device by ToId field
        /// </value>
        public Device RecieverDevice { get; set; }
    }
}