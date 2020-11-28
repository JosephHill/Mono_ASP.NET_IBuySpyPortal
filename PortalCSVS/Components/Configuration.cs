using System;
using System.Configuration;
using System.Web;
using System.Data;
using System.Collections;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // TabStripDetails Class
    //
    // Class that encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    // -- for a specific Tab in the Portal
    //
    //*********************************************************************

    public class TabStripDetails {

        public int          TabId;
        public String       TabName;
        public int          TabOrder;
        public String       AuthorizedRoles;
    }

    //*********************************************************************
    //
    // TabSettings Class
    //
    // Class that encapsulates the detailed settings for a specific Tab 
    // in the Portal
    //
    //*********************************************************************

    public class TabSettings {
                           
        public int          TabIndex;
        public int          TabId;
        public String       TabName;
        public int          TabOrder;
        public String       MobileTabName;
        public String       AuthorizedRoles;
        public bool         ShowMobile;
        public ArrayList    Modules = new ArrayList();
    }

    //*********************************************************************
    //
    // ModuleSettings Class
    //
    // Class that encapsulates the detailed settings for a specific Tab 
    // in the Portal
    //
    //*********************************************************************

    public class ModuleSettings {

        public int          ModuleId;
        public int          ModuleDefId;
        public int          TabId;
        public int          CacheTime;
        public int          ModuleOrder;
        public String       PaneName;
        public String       ModuleTitle;
        public String       AuthorizedEditRoles;
        public bool         ShowMobile;
        public String       DesktopSrc;
        public String       MobileSrc;
    }

    //*********************************************************************
    //
    // PortalSettings Class
    //
    // This class encapsulates all of the settings for the Portal, as well
    // as the configuration settings required to execute the current tab
    // view within the portal.
    //
    //*********************************************************************

    public class PortalSettings {

        public int          PortalId;
        public String       PortalName;
        public bool         AlwaysShowEditButton;
        public ArrayList    DesktopTabs = new ArrayList();
        public ArrayList    MobileTabs = new ArrayList();
        public TabSettings  ActiveTab = new TabSettings();

        //*********************************************************************
        //
        // PortalSettings Constructor
        //
        // The PortalSettings Constructor encapsulates all of the logic
        // necessary to obtain configuration settings necessary to render
        // a Portal Tab view for a given request.
        //
        // These Portal Settings are stored within a SQL database, and are
        // fetched below by calling the "GetPortalSettings" stored procedure.
        // This stored procedure returns values as SPROC output parameters,
        // and using three result sets.
        //
        //*********************************************************************

        public PortalSettings(int tabIndex, int tabId) {

            // GetPortalSettings within SQL Server returns 3 Resultsets, as well as a set of output
            // Parameters.  For the Npgsql implementation, we will need 4 resultsets to duplicate
            // this functionality.  Also, the tabid used internal to the GetPortalSettings stored
            // procedure changes, but is not returned.  In this implementation, we will store that
            // temporary TabID value in the tmpTabId variable.
            int tmpTabId = tabId;

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetPortalSettings(:PortalAlias, :TabId)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalAlias = new NpgsqlParameter("PortalAlias", DbType.String);
            parameterPortalAlias.Value = "p_default";
            myCommand.Parameters.Add(parameterPortalAlias);

            NpgsqlParameter parameterTabId = new NpgsqlParameter("TabId", DbType.Int32);
            parameterTabId.Value = tabId;
            myCommand.Parameters.Add(parameterTabId);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader result = myCommand.ExecuteReader();

            // Read Portal Settings (Output params from SQL implementation
            if (result.Read()) {
                this.PortalId = (int) result["portalid"];
                this.PortalName = (String) result["portalname"];
                this.AlwaysShowEditButton = (bool) result["alwaysshoweditbutton"];
                tmpTabId = (int) result["tabid"];
                this.ActiveTab.TabOrder = (int) result["taborder"];
                this.ActiveTab.MobileTabName = (String) result["mobiletabname"];
                this.ActiveTab.AuthorizedRoles = (String) result["authorizedroles"];
                this.ActiveTab.TabName = (String) result["tabname"];
                this.ActiveTab.ShowMobile = (bool) result["showmobile"];
            }

            // Close the datareader
            result.Close();


            myCommand = new NpgsqlCommand("GetTabs(:PortalID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterPortalId = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalId.Value = this.PortalId;
            myCommand.Parameters.Add(parameterPortalId);

            // Open the database connection and execute the command
            result = myCommand.ExecuteReader();


            // Read Desktop Tab Information
            while(result.Read()) {

                TabStripDetails tabDetails = new TabStripDetails();
                tabDetails.TabId = (int) result["tabid"];
                tabDetails.TabName = (String) result["tabname"];
                tabDetails.TabOrder = (int) result["taborder"];
                tabDetails.AuthorizedRoles = (String) result["authorizedroles"];

                this.DesktopTabs.Add(tabDetails);
            }

            // Close the datareader
            result.Close();

            if (this.ActiveTab.TabId == 0) {
                this.ActiveTab.TabId = ((TabStripDetails) this.DesktopTabs[0]).TabId; 
            }

            // // Read Mobile Tab Information
            // result.NextResult();



            // while(result.Read()) {

            //     TabStripDetails tabDetails = new TabStripDetails();
            //     tabDetails.TabId = (int) result["TabId"];
            //     tabDetails.TabName = (String) result["MobileTabName"];
            //     tabDetails.AuthorizedRoles = (String) result["AuthorizedRoles"];

            //     this.MobileTabs.Add(tabDetails);
            // }

            // Module Tab Information

            myCommand = new NpgsqlCommand("GetModuleModuleDefinitions(:TabId)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            parameterTabId = new NpgsqlParameter("TabId", DbType.Int32);
            parameterTabId.Value = tmpTabId;
            myCommand.Parameters.Add(parameterTabId);

            // Open the database connection and execute the command
            result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);


            while(result.Read()) {

                ModuleSettings m = new ModuleSettings();
                m.ModuleId = (int) result["moduleid"];
                m.ModuleDefId = (int) result["moduledefid"];
                m.TabId = (int) result["tabid"];
                m.PaneName = (String) result["panename"];
                m.ModuleTitle = (String) result["moduletitle"];
                m.AuthorizedEditRoles = (String) result["authorizededitroles"];
                m.CacheTime = (int) result["cachetime"];
                m.ModuleOrder = (int) result["moduleorder"];
                m.ShowMobile = (bool) result["showmobile"];
                m.DesktopSrc = (String) result["desktopsrc"];
                m.MobileSrc = (String) result["mobilesrc"];

                this.ActiveTab.Modules.Add(m);
            }

            // Close the datareader
            result.Close();

            this.ActiveTab.TabIndex = tabIndex;
            this.ActiveTab.TabId = tabId;


            myConnection.Close();
        }
    

        //*********************************************************************
        //
        // GetModuleSettings Static Method
        //
        // The PortalSettings.GetModuleSettings Method returns a hashtable of
        // custom module specific settings from the database.  This method is
        // used by some user control modules (Xml, Image, etc) to access misc
        // settings.
        //
        //*********************************************************************

        public static Hashtable GetModuleSettings(int moduleId) {

            // Get Settings for this module from the database
            Hashtable _settings = new Hashtable();

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetModuleSettings(:ModuleId)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
		    NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleId", DbType.Int32);

            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            // Execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read()) {

                _settings[dr.GetString(1)] = dr.GetString(2);
            }

            dr.Close();

            return _settings;
        }
    }
}