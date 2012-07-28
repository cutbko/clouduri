namespace CloudUri.DAL.Database
{
    /// <summary>
    /// List of db stored procedures
    /// </summary>
    public class StoredProcedureNames
    {
        #region User

        public const string UserUpdate = "Users_Update";

        public const string UserDelete = "Users_Delete";

        public const string UserInsert = "Users_Insert";

        public const string UserGetRecords = "Users_GetRecords";

        public const string UserGetById = "Users_GetById";

        public const string UserCount = "Users_Count";

        public const string AddRoleToUser = "User_AddRole";

        public const string UserNameExists = "User_NameExists";

        public const string UserEmailExists = "User_EmailExists";

        public const string UserGetByName = "Users_GetByName";

        #endregion

        #region Device types

        public const string DeviceTypeInsert = "DeviceTypes_Insert";

        public const string DeviceTypeDelete = "DeviceTypes_Delete";

        public const string DeviceTypeUpdate = "DeviceTypes_Update";

        public const string DeviceTypeGetAll = "DeviceTypes_GetAll";

        public const string DeviceTypeGetById = "DeviceTypes_GetById";

        public const string DeviceTypeCount = "DeviceTypes_Count";

        #endregion

        #region Device

        public const string DeviceInsert = "Devices_Insert";

        public const string DeviceDelete = "Devices_Delete";

        public const string DeviceUpdate = "Devices_Update";

        public const string DeviceGetAll = "Devices_GetAll";

        public const string DeviceGetById = "Devices_GetById";

        public const string DeviceCount = "Devices_Count";

        #endregion

        #region Message

        public const string MessageInsert = "Messages_Insert";

        public const string MessageDelete = "Messages_Delete";

        public const string MessageUpdate = "Messages_Update";

        public const string MessageGetAll = "Messages_GetAll";

        public const string MessageGetById = "Messages_GetById";

        public const string MessageCount = "Messages_Count";

        #endregion

        #region Role

        public const string RoleInsert = "Roles_Insert";

        public const string RoleDelete = "Roles_Delete";

        public const string RoleUpdate = "Roles_Update";

        public const string RoleGetAll = "Roles_GetAll";

        public const string RoleGetById = "Roles_GetById";

        public const string RoleCount = "Roles_Count";

        public const string RolesGetForUser = "Roles_GetForUser";

        #endregion
    }
}