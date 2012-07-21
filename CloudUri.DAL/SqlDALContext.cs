using System;
using CloudUri.DAL.Database;
using CloudUri.DAL.Repository;

namespace CloudUri.DAL
{
    /// <summary>
    /// Provides access with lazy initialization to all the SQL repositories
    /// </summary>
    public class SqlDALContext : IDALContext
    {
        private readonly IDbWrapper _dbWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDALContext"/> class
        /// </summary>
        /// <param name="dbWrapper"></param>
        public SqlDALContext(IDbWrapper dbWrapper)
        {
            if (dbWrapper == null)
            {
                throw new ArgumentNullException("dbWrapper");    
            }

            _dbWrapper = dbWrapper;
        }

        private readonly object _messagesLocker = new object();

        private volatile MessagesRepository _messagesRepository;

        /// <summary>
        /// Gets the messages repository
        /// </summary>
        public IMessagesRepository MessagesRepository
        {
            get
            {
                if (_messagesRepository == null)
                {
                    lock (_messagesLocker)
                    {
                        if (_messagesRepository == null)
                        {
                            _messagesRepository = new MessagesRepository(_dbWrapper);
                        }
                    }
                }

                return _messagesRepository;
            }
        }

        private readonly object _usersLocker = new object();

        private volatile UsersRepository _usersRepository;

        /// <summary>
        /// Gets the users repository
        /// </summary>
        public IUsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    lock (_usersLocker)
                    {
                        if (_usersRepository == null)
                        {
                            _usersRepository = new UsersRepository(_dbWrapper);
                        }
                    }
                }

                return _usersRepository;
            }
        }

        private readonly object _rolesLocker = new object();

        private volatile RolesRepository _rolesRepository;

        /// <summary>
        /// Gets the roles repository
        /// </summary>
        public IRolesRepository RolesRepository
        {
            get
            {
                if (_rolesRepository == null)
                {
                    lock (_rolesLocker)
                    {
                        if (_rolesRepository == null)
                        {
                            _rolesRepository = new RolesRepository(_dbWrapper);
                        }
                    }
                }

                return _rolesRepository;
            }
        }

        private readonly object _devicesLocker = new object();

        private volatile DevicesRepository _devicesRepository;

        /// <summary>
        /// Gets the devices repository
        /// </summary>
        public IDevicesRepository DevicesRepository
        {
            get
            {
                if (_devicesRepository == null)
                {
                    lock (_devicesLocker)
                    {
                        if (_devicesRepository == null)
                        {
                            _devicesRepository = new DevicesRepository(_dbWrapper);
                        }
                    }
                }

                return _devicesRepository;
            }
        }

        private readonly object _deviceTypesLocker = new object();

        private volatile DeviceTypesRepository _deviceTypesRepository;

        /// <summary>
        /// Gets the deviceTypes repository
        /// </summary>
        public IDeviceTypesRepository DeviceTypesRepository
        {
            get
            {
                if (_deviceTypesRepository == null)
                {
                    lock (_deviceTypesLocker)
                    {
                        if (_deviceTypesRepository == null)
                        {
                            _deviceTypesRepository = new DeviceTypesRepository(_dbWrapper);
                        }
                    }
                }

                return _deviceTypesRepository;
            }
        }
    }
}