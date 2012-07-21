using CloudUri.DAL.Repository;

namespace CloudUri.DAL
{
    /// <summary>
    /// Provides access to all the repositories
    /// </summary>
    public interface IDALContext
    {
        /// <summary>
        /// Gets the messages repository
        /// </summary>
        IMessagesRepository MessagesRepository { get; }

        /// <summary>
        /// Gets the users repository
        /// </summary>
        IUsersRepository UsersRepository { get; }

        /// <summary>
        /// Gets the roles repository
        /// </summary>
        IRolesRepository RolesRepository { get; }

        /// <summary>
        /// Gets the devices repository
        /// </summary>
        IDevicesRepository DevicesRepository { get; }

        /// <summary>
        /// Gets the device types repository
        /// </summary>
        IDeviceTypesRepository DeviceTypesRepository { get; }
    }
}