using System;
using System.Collections;
using System.Configuration;
using System.Data;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // ModuleItem Class
    //
    // This class encapsulates the basic attributes of a Module, and is used
    // by the administration pages when manipulating modules.  ModuleItem implements 
    // the IComparable interface so that an ArrayList of ModuleItems may be sorted
    // by ModuleOrder, using the ArrayList's Sort() method.
    //
    //*********************************************************************

    public class ModuleItem : IComparable {

        private int      _moduleOrder;
        private String   _title;
        private String   _pane;
        private int      _id;
        private int      _defId;

        public int ModuleOrder {

            get {
                return _moduleOrder;
            }
            set {
                _moduleOrder = value;
            }
        }    

        public String ModuleTitle {

            get {
                return _title;
            }
            set {
                _title = value;
            }
        }

        public String PaneName {

            get {
                return _pane;
            }
            set {
                _pane = value;
            }
        }
        
        public int ModuleId {

            get {
                return _id;
            }
            set {
                _id = value;
            }
        }  
  
        public int ModuleDefId {

            get {
                return _defId;
            }
            set {
                _defId = value;
            }
        } 
   
        public int CompareTo(object value) {

            if (value == null) return 1;

            int compareOrder = ((ModuleItem)value).ModuleOrder;
            
            if (this.ModuleOrder == compareOrder) return 0;
            if (this.ModuleOrder < compareOrder) return -1;
            if (this.ModuleOrder > compareOrder) return 1;
            return 0;
        }
    }
    
    //*********************************************************************
    //
    // TabItem Class
    //
    // This class encapsulates the basic attributes of a Tab, and is used
    // by the administration pages when manipulating tabs.  TabItem implements 
    // the IComparable interface so that an ArrayList of TabItems may be sorted
    // by TabOrder, using the ArrayList's Sort() method.
    //
    //*********************************************************************

    public class TabItem : IComparable {

        private int      _tabOrder;
        private String   _name;
        private int      _id;

        public int TabOrder {

            get {
                return _tabOrder;
            }
            set {
                _tabOrder = value;
            }
        }    

        public String TabName {

            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        public int TabId {

            get {
                return _id;
            }
            set {
                _id = value;
            }
        }  
  
        public int CompareTo(object value) {

            if (value == null) return 1;

            int compareOrder = ((TabItem)value).TabOrder;
            
            if (this.TabOrder == compareOrder) return 0;
            if (this.TabOrder < compareOrder) return -1;
            if (this.TabOrder > compareOrder) return 1;
            return 0;
        }
    }
	
    //*********************************************************************
    //
    // AdminDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // configuration, layout and security settings values within the Portal database.
    //
    //*********************************************************************

    public class AdminDB {

        //
        // ROLES
        //

        //*********************************************************************
        //
        // GetPortalRoles() Method <a name="GetPortalRoles"></a>
        //
        // The GetPortalRoles method returns a list of all role names for the 
        // specified portal.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetPortalRoles Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetPortalRoles(int portalId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetPortalRoles(:PortalID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalID = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalID.Value = portalId;
            myCommand.Parameters.Add(parameterPortalID);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        //*********************************************************************
        //
        // AddRole() Method <a name="AddRole"></a>
        //
        // The AddRole method creates a new security role for the specified portal,
        // and returns the new RoleID value.
        //
        // Other relevant sources:
        //     + <a href="AddRole.htm" style="color:green">AddRole Stored Procedure</a>
        //
        //*********************************************************************

        public int AddRole(int portalId, String roleName) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("AddRole(:PortalID, :RoleName)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalID = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalID.Value = portalId;
            myCommand.Parameters.Add(parameterPortalID);

            NpgsqlParameter parameterRoleName = new NpgsqlParameter("RoleName", DbType.String);
            parameterRoleName.Value = roleName;
            myCommand.Parameters.Add(parameterRoleName);

            // Open the database connection and execute the command
            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // DeleteRole() Method <a name="DeleteRole"></a>
        //
        // The DeleteRole deletes the specified role from the portal database.
        //
        // Other relevant sources:
        //     + <a href="DeleteRole.htm" style="color:green">DeleteRole Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteRole(int roleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("DeleteRole(:RoleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterRoleID = new NpgsqlParameter("RoleID", DbType.Int32);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
       
        //*********************************************************************
        //
        // UpdateRole() Method <a name="UpdateRole"></a>
        //
        // The UpdateRole method updates the friendly name of the specified role.
        //
        // Other relevant sources:
        //     + <a href="UpdateRole.htm" style="color:green">UpdateRole Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateRole(int roleId, String roleName) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("UpdateRole(:RoleID, :RoleName)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterRoleID = new NpgsqlParameter("RoleID", DbType.Int32);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            NpgsqlParameter parameterRoleName = new NpgsqlParameter("RoleName", DbType.String);
            parameterRoleName.Value = roleName;
            myCommand.Parameters.Add(parameterRoleName);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        
        //
        // USER ROLES
        //

        //*********************************************************************
        //
        // GetRoleMembers() Method <a name="GetRoleMembers"></a>
        //
        // The GetRoleMembers method returns a list of all members in the specified
        // security role.
        //
        // Other relevant sources:
        //     + <a href="GetRoleMembers.htm" style="color:green">GetRoleMembers Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetRoleMembers(int roleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetRoleMembership(:RoleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter parameterRoleID = new NpgsqlParameter("RoleID", DbType.Int32);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        //*********************************************************************
        //
        // AddUserRole() Method <a name="AddUserRole"></a>
        //
        // The AddUserRole method adds the user to the specified security role.
        //
        // Other relevant sources:
        //     + <a href="AddUserRole.htm" style="color:green">AddUserRole Stored Procedure</a>
        //
        //*********************************************************************

        public void AddUserRole(int roleId, int userId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("AddUserRole(:UserID, :RoleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterRoleID = new NpgsqlParameter("RoleID", DbType.Int32);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            NpgsqlParameter parameterUserID = new NpgsqlParameter("UserID", DbType.Int32);
            parameterUserID.Value = userId;
            myCommand.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // DeleteUserRole() Method <a name="DeleteUserRole"></a>
        //
        // The DeleteUserRole method deletes the user from the specified role.
        //
        // Other relevant sources:
        //     + <a href="DeleteUserRole.htm" style="color:green">DeleteUserRole Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteUserRole(int roleId, int userId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("DeleteUserRole(:UserID, :RoleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterRoleID = new NpgsqlParameter("RoleID", DbType.Int32);
            parameterRoleID.Value = roleId;
            myCommand.Parameters.Add(parameterRoleID);

            NpgsqlParameter parameterUserID = new NpgsqlParameter("UserID", DbType.Int32);
            parameterUserID.Value = userId;
            myCommand.Parameters.Add(parameterUserID);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
       
        
        //
        // USERS
        //

        //*********************************************************************
        //
        // GetUsers() Method <a name="GetUsers"></a>
        //
        // The GetUsers method returns returns the UserID, Name and Email for 
        // all registered users.
        //
        // Other relevant sources:
        //     + <a href="GetUsers.htm" style="color:green">GetUsers Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetUsers() {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetUsers()", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }


        //
        // PORTAL
        //

        //*********************************************************************
        //
        // UpdatePortalInfo() Method <a name="UpdatePortalInfo"></a>
        //
        // The UpdatePortalInfo method updates the name and access settings for the portal.
        //
        // Other relevant sources:
        //     + <a href="UpdatePortalInfo.htm" style="color:green">UpdatePortalInfo Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdatePortalInfo (int portalId, String portalName, bool alwaysShow) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("UpdatePortalInfo(:PortalID, :PortalName, :AlwaysShowEditButton)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalId = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalId.Value = portalId;
            myCommand.Parameters.Add(parameterPortalId);

            NpgsqlParameter parameterPortalName = new NpgsqlParameter("PortalName", DbType.String);
            parameterPortalName.Value = portalName;
            myCommand.Parameters.Add(parameterPortalName);

            NpgsqlParameter parameterAlwaysShow = new NpgsqlParameter("AlwaysShowEditButton", DbType.Boolean);
            parameterAlwaysShow.Value = alwaysShow;
            myCommand.Parameters.Add(parameterAlwaysShow);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
		
        //
        // TABS
        //

        //*********************************************************************
        //
        // AddTab Method
        //
        // The AddTab method adds a new tab to the portal.
        //
        // Other relevant sources:
        //     + <a href="AddTab.htm" style="color:green">AddTab Stored Procedure</a>
        //
        //*********************************************************************

        public int AddTab (int portalId, String tabName, int tabOrder) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddTab(:PortalID, :TabName, :TabOrder, :AuthorizedRoles, :MobileTabName)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalId = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalId.Value = portalId;
            myCommand.Parameters.Add(parameterPortalId);

            NpgsqlParameter parameterTabName = new NpgsqlParameter("TabName", DbType.String);
            parameterTabName.Value = tabName;
            myCommand.Parameters.Add(parameterTabName);

            NpgsqlParameter parameterTabOrder = new NpgsqlParameter("TabOrder", DbType.Int32);
            parameterTabOrder.Value = tabOrder;
            myCommand.Parameters.Add(parameterTabOrder);

            NpgsqlParameter parameterAuthRoles = new NpgsqlParameter("AuthorizedRoles", DbType.String);
            parameterAuthRoles.Value = "All Users";
            myCommand.Parameters.Add(parameterAuthRoles);

            NpgsqlParameter parameterMobileTabName = new NpgsqlParameter("MobileTabName", DbType.String);
            parameterMobileTabName.Value = "";
            myCommand.Parameters.Add(parameterMobileTabName);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }		
        
        
        //*********************************************************************
        //
        // UpdateTab Method
        //
        // The UpdateTab method updates the settings for the specified tab.
        //
        // Other relevant sources:
        //     + <a href="UpdateTab.htm" style="color:green">UpdateTab Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateTab (int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles, String mobileTabName, bool showMobile) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateTab(:PortalID, :TabID, :TabOrder, :TabName, :AuthorizedRoles, :MobileTabName, :ShowMobile)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalId = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalId.Value = portalId;
            myCommand.Parameters.Add(parameterPortalId);
            
            NpgsqlParameter parameterTabId = new NpgsqlParameter("TabID", DbType.Int32);
            parameterTabId.Value = tabId;
            myCommand.Parameters.Add(parameterTabId);

            NpgsqlParameter parameterTabName = new NpgsqlParameter("TabName", DbType.String);
            parameterTabName.Value = tabName;
            myCommand.Parameters.Add(parameterTabName);

            NpgsqlParameter parameterTabOrder = new NpgsqlParameter("TabOrder", DbType.Int32);
            parameterTabOrder.Value = tabOrder;
            myCommand.Parameters.Add(parameterTabOrder);

            NpgsqlParameter parameterAuthRoles = new NpgsqlParameter("AuthorizedRoles", DbType.String);
            parameterAuthRoles.Value = authorizedRoles;
            myCommand.Parameters.Add(parameterAuthRoles);

            NpgsqlParameter parameterMobileTabName = new NpgsqlParameter("MobileTabName", DbType.String);
            parameterMobileTabName.Value = mobileTabName;
            myCommand.Parameters.Add(parameterMobileTabName);

            NpgsqlParameter parameterShowMobile = new NpgsqlParameter("ShowMobile", DbType.Boolean);
            parameterShowMobile.Value = showMobile;
            myCommand.Parameters.Add(parameterShowMobile);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }		
		
        //*********************************************************************
        //
        // UpdateTabOrder Method
        //
        // The UpdateTabOrder method changes the position of the tab with respect
        // to other tabs in the portal.
        //
        // Other relevant sources:
        //     + <a href="UpdateTabOrder.htm" style="color:green">UpdateTabOrder Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateTabOrder (int tabId, int tabOrder) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateTabOrder(:TabID, :TabOrder)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterTabId = new NpgsqlParameter("TabID", DbType.Int32);
            parameterTabId.Value = tabId;
            myCommand.Parameters.Add(parameterTabId);

            NpgsqlParameter parameterTabOrder = new NpgsqlParameter("TabOrder", DbType.Int32);
            parameterTabOrder.Value = tabOrder;
            myCommand.Parameters.Add(parameterTabOrder);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
		
        //*********************************************************************
        //
        // DeleteTab() Method <a name="DeleteTab"></a>
        //
        // The DeleteTab method deletes the selected tab from the portal.
        //
        // Other relevant sources:
        //     + <a href="DeleteTab.htm" style="color:green">DeleteTab Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteTab(int tabId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteTab(:TabID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterTabID = new NpgsqlParameter("TabID", DbType.Int32);
            parameterTabID.Value = tabId;
            myCommand.Parameters.Add(parameterTabID);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
       
        //
        // MODULES
        //

        //*********************************************************************
        //
        // UpdateModuleOrder Method
        //
        // The AddUserRole method adds the user to the specified security role.
        //
        // Other relevant sources:
        //     + <a href="UpdateModuleOrder.htm" style="color:green">UpdateModuleOrder Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateModuleOrder (int ModuleId, int ModuleOrder, String pane) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("UpdateModuleOrder(:ModuleID, :ModuleOrder, :PaneName)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
			NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleId.Value = ModuleId;
            myCommand.Parameters.Add(parameterModuleId);

            NpgsqlParameter parameterModuleOrder = new NpgsqlParameter("ModuleOrder", DbType.Int32);
            parameterModuleOrder.Value = ModuleOrder;
            myCommand.Parameters.Add(parameterModuleOrder);

            NpgsqlParameter parameterPaneName = new NpgsqlParameter("PaneName", DbType.String);
            parameterPaneName.Value = pane;
            myCommand.Parameters.Add(parameterPaneName);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
        
        
        //*********************************************************************
        //
        // AddModule Method
        //
        // The AddModule method updates a specified Module within
        // the Modules database table.  If the module does not yet exist,
        // the stored procedure adds it.
        //
        // Other relevant sources:
        //     + <a href="AddModule.htm" style="color:green">AddModule Stored Procedure</a>
        //
        //*********************************************************************

        public int AddModule(int tabId, int moduleOrder, String paneName, String title, int moduleDefId, int cacheTime, String editRoles, bool showMobile) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddModule(:TabID, :ModuleOrder, :ModuleTitle, :PaneName, :ModuleDefID, :CacheTime, :EditRoles, :ShowMobile)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleDefinitionId = new NpgsqlParameter("ModuleDefID", DbType.Int32);
            parameterModuleDefinitionId.Value = moduleDefId;
            myCommand.Parameters.Add(parameterModuleDefinitionId);

            NpgsqlParameter parameterTabId = new NpgsqlParameter("TabID", DbType.Int32);
            parameterTabId.Value = tabId;
            myCommand.Parameters.Add(parameterTabId);

            NpgsqlParameter parameterModuleOrder = new NpgsqlParameter("ModuleOrder", DbType.Int32);
            parameterModuleOrder.Value = moduleOrder;
            myCommand.Parameters.Add(parameterModuleOrder);

            NpgsqlParameter parameterTitle = new NpgsqlParameter("ModuleTitle", DbType.String);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            NpgsqlParameter parameterPaneName = new NpgsqlParameter("PaneName", DbType.String);
            parameterPaneName.Value = paneName;
            myCommand.Parameters.Add(parameterPaneName);

            NpgsqlParameter parameterCacheTime = new NpgsqlParameter("CacheTime", DbType.Int32);
            parameterCacheTime.Value = cacheTime;
            myCommand.Parameters.Add(parameterCacheTime);

            NpgsqlParameter parameterEditRoles = new NpgsqlParameter("EditRoles", DbType.String);
            parameterEditRoles.Value = editRoles;
            myCommand.Parameters.Add(parameterEditRoles);

            NpgsqlParameter parameterShowMobile = new NpgsqlParameter("ShowMobile", DbType.Boolean);
            parameterShowMobile.Value = showMobile;
            myCommand.Parameters.Add(parameterShowMobile);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        
        //*********************************************************************
        //
        // UpdateModule Method
        //
        // The UpdateModule method updates a specified Module within
        // the Modules database table.  If the module does not yet exist,
        // the stored procedure adds it.
        //
        // Other relevant sources:
        //     + <a href="UpdateModule.htm" style="color:green">UpdateModule Stored Procedure</a>
        //
        //*********************************************************************

        public int UpdateModule(int moduleId, int moduleOrder, String paneName, String title, int cacheTime, String editRoles, bool showMobile) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateModule(:ModuleID, :ModuleOrder, :ModuleTitle, :PaneName, :CacheTime, :EditRoles, :ShowMobile)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            NpgsqlParameter parameterModuleOrder = new NpgsqlParameter("ModuleOrder", DbType.Int32);
            parameterModuleOrder.Value = moduleOrder;
            myCommand.Parameters.Add(parameterModuleOrder);

            NpgsqlParameter parameterTitle = new NpgsqlParameter("ModuleTitle", DbType.String);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            NpgsqlParameter parameterPaneName = new NpgsqlParameter("PaneName", DbType.String);
            parameterPaneName.Value = paneName;
            myCommand.Parameters.Add(parameterPaneName);

            NpgsqlParameter parameterCacheTime = new NpgsqlParameter("CacheTime", DbType.Int32);
            parameterCacheTime.Value = cacheTime;
            myCommand.Parameters.Add(parameterCacheTime);

            NpgsqlParameter parameterEditRoles = new NpgsqlParameter("EditRoles", DbType.String);
            parameterEditRoles.Value = editRoles;
            myCommand.Parameters.Add(parameterEditRoles);

            NpgsqlParameter parameterShowMobile = new NpgsqlParameter("ShowMobile", DbType.Boolean);
            parameterShowMobile.Value = showMobile;
            myCommand.Parameters.Add(parameterShowMobile);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();

            return (int) parameterModuleId.Value;
        }

        //*********************************************************************
        //
        // DeleteModule Method
        //
        // The DeleteModule method deletes a specified Module from
        // the Modules database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteModule.htm" style="color:green">DeleteModule Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteModule(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteModule(:ModuleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }


        //*********************************************************************
        //
        // UpdateModuleSetting Method
        //
        // The UpdateModuleSetting Method updates a single module setting 
        // in the ModuleSettings database table.
        //
        //*********************************************************************

        public void UpdateModuleSetting(int moduleId, String key, String value) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateModuleSetting(:ModuleID, :SettingName, :SettingValue)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            NpgsqlParameter parameterKey = new NpgsqlParameter("SettingName", DbType.String);
            parameterKey.Value = key;
            myCommand.Parameters.Add(parameterKey);
            
            NpgsqlParameter parameterValue = new NpgsqlParameter("SettingValue", DbType.String);
            parameterValue.Value = value;
            myCommand.Parameters.Add(parameterValue);
            
            // Execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();

        }

        //
        // MODULE DEFINITIONS
        //

        //*********************************************************************
        //
        // GetModuleDefinitions() Method <a name="GetModuleDefinitions"></a>
        //
        // The GetModuleDefinitions method returns a list of all module type 
        // definitions for the portal.
        //
        // Other relevant sources:
        //     + <a href="GetModuleDefinitions.htm" style="color:green">GetModuleDefinitions Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetModuleDefinitions(int portalId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetModuleDefinitions(:PortalID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalId = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalId.Value = portalId;
            myCommand.Parameters.Add(parameterPortalId);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        //*********************************************************************
        //
        // AddModuleDefinition() Method <a name="AddModuleDefinition"></a>
        //
        // The AddModuleDefinition add the definition for a new module type
        // to the portal.
        //
        // Other relevant sources:
        //     + <a href="AddModuleDefinition.htm" style="color:green">AddModuleDefinition Stored Procedure</a>
        //
        //*********************************************************************

        public int AddModuleDefinition(int portalId, String name, String desktopSrc, String mobileSrc) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddModuleDefinition(:PortalID, :FriendlyName, :DesktopSrc, :MobileSrc)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalID = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalID.Value = portalId;
            myCommand.Parameters.Add(parameterPortalID);

            NpgsqlParameter parameterFriendlyName = new NpgsqlParameter("FriendlyName", DbType.String);
            parameterFriendlyName.Value = name;
            myCommand.Parameters.Add(parameterFriendlyName);

            NpgsqlParameter parameterDesktopSrc = new NpgsqlParameter("DesktopSrc", DbType.String);
            parameterDesktopSrc.Value = desktopSrc;
            myCommand.Parameters.Add(parameterDesktopSrc);

            NpgsqlParameter parameterMobileSrc = new NpgsqlParameter("MobileSrc", DbType.String);
            parameterMobileSrc.Value = mobileSrc;
            myCommand.Parameters.Add(parameterMobileSrc);

            // Open the database connection and execute the command
            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // DeleteModuleDefinition() Method <a name="DeleteModuleDefinition"></a>
        //
        // The DeleteModuleDefinition method deletes the specified module type 
        // definition from the portal.
        //
        // Other relevant sources:
        //     + <a href="DeleteModuleDefinition.htm" style="color:green">DeleteModuleDefinition Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteModuleDefinition(int defId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteModuleDefinition(:ModuleDefID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleDefID = new NpgsqlParameter("ModuleDefID", DbType.Int32);
            parameterModuleDefID.Value = defId;
            myCommand.Parameters.Add(parameterModuleDefID);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
       
        //*********************************************************************
        //
        // UpdateModuleDefinition() Method <a name="UpdateModuleDefinition"></a>
        //
        // The UpdateModuleDefinition method updates the settings for the 
        // specified module type definition.
        //
        // Other relevant sources:
        //     + <a href="UpdateModuleDefinition.htm" style="color:green">UpdateModuleDefinition Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateModuleDefinition(int defId, String name, String desktopSrc, String mobileSrc) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateModuleDefinition(:ModuleDefID, :FriendlyName, :DesktopSrc, :MobileSrc)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleDefID = new NpgsqlParameter("ModuleDefID", DbType.Int32);
            parameterModuleDefID.Value = defId;
            myCommand.Parameters.Add(parameterModuleDefID);

            NpgsqlParameter parameterFriendlyName = new NpgsqlParameter("FriendlyName", DbType.String);
            parameterFriendlyName.Value = name;
            myCommand.Parameters.Add(parameterFriendlyName);

            NpgsqlParameter parameterDesktopSrc = new NpgsqlParameter("DesktopSrc", DbType.String);
            parameterDesktopSrc.Value = desktopSrc;
            myCommand.Parameters.Add(parameterDesktopSrc);

            NpgsqlParameter parameterMobileSrc = new NpgsqlParameter("MobileSrc", DbType.String);
            parameterMobileSrc.Value = mobileSrc;
            myCommand.Parameters.Add(parameterMobileSrc);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // GetSingleModuleDefinition Method
        //
        // The GetSingleModuleDefinition method returns a NpgsqlDataReader containing details
        // about a specific module definition from the ModuleDefinitions table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleModuleDefinition.htm" style="color:green">GetSingleModuleDefinition Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleModuleDefinition(int defId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleModuleDefinition(:ModuleDefID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleDefID = new NpgsqlParameter("ModuleDefID", DbType.Int32);
            parameterModuleDefID.Value = defId;
            myCommand.Parameters.Add(parameterModuleDefID);

            // Execute the command
            myConnection.Open();
            NpgsqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
            // Return the datareader 
            return result;
        }
    }
}

