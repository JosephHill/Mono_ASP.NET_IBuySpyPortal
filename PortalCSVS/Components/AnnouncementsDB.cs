using System;
using System.Configuration;
using System.Data;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // AnnounceDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // announcements within the Portal database.
    //
    //*********************************************************************

    public class AnnouncementsDB {

        //*********************************************************************
        //
        // GetAnnouncements Method
        //
        // The GetAnnouncements method returns a DataSet containing all of the
        // announcements for a specific portal module from the Announcements
        // database table.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetAnnouncements.htm" style="color:green">GetAnnouncements Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetAnnouncements(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("GetAnnouncements(:ModuleID)", myConnection);

            // Mark the Command as a SPROC
    		myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
			NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);

            parameterModuleId.Value = moduleId;
    		myCommand.Parameters.Add(parameterModuleId);

    		// Execute the command
    		myConnection.Open();

    		NpgsqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

    		// Return the datareader
    		return result;
        }

        //*********************************************************************
        //
        // GetSingleAnnouncement Method
        //
        // The GetSingleAnnouncement method returns a SqlDataReader containing details
        // about a specific announcement from the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleAnnouncement.htm" style="color:green">GetSingleAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleAnnouncement(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleAnnouncement(:ItemID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemId = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemId.Value = itemId;
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            NpgsqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // DeleteAnnouncement Method
        //
        // The DeleteAnnouncement method deletes the specified announcement from
        // the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteAnnouncement.htm" style="color:green">DeleteAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteAnnouncement(int itemID) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("DeleteAnnouncement(:ItemID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemID = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // AddAnnouncement Method
        //
        // The AddAnnouncement method adds a new announcement to the
        // Announcements database table, and returns the ItemId value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddAnnouncement.htm" style="color:green">AddAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String moreLink, String mobileMoreLink) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("AddAnnouncement(:ModuleID, :UserName, :Title, :MoreLink, :MobileMoreLink, :ExpireDate, :Description)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterTitle = new NpgsqlParameter("Title", DbType.String);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            NpgsqlParameter parameterMoreLink = new NpgsqlParameter("MoreLink", DbType.String);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            NpgsqlParameter parameterMobileMoreLink = new NpgsqlParameter("MobileMoreLink", DbType.String);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            NpgsqlParameter parameterExpireDate = new NpgsqlParameter("ExpireDate", DbType.DateTime);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            NpgsqlParameter parameterDescription = new NpgsqlParameter("Description", DbType.String);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // UpdateAnnouncement Method
        //
        // The UpdateAnnouncement method updates the specified announcement within
        // the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateAnnouncement.htm" style="color:green">UpdateAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String moreLink, String mobileMoreLink) {

            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateAnnouncement(:ItemID, :UserName, :Title, :MoreLink, :MobileMoreLink, :ExpireDate, :Description)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemID = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemID.Value = itemId;
            myCommand.Parameters.Add(parameterItemID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterTitle = new NpgsqlParameter("Title", DbType.String);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            NpgsqlParameter parameterMoreLink = new NpgsqlParameter("MoreLink", DbType.String);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            NpgsqlParameter parameterMobileMoreLink = new NpgsqlParameter("MobileMoreLink", DbType.String);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            NpgsqlParameter parameterExpireDate = new NpgsqlParameter("ExpireDate", DbType.DateTime);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            NpgsqlParameter parameterDescription = new NpgsqlParameter("Description", DbType.String);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
    }
}


