using System;
using System.Configuration;
using System.Data;
using ASPNetPortal;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // LinkDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // links within the Portal database.
    //
    //*********************************************************************

    public class LinkDB {

        //*********************************************************************
        //
        // GetLinks Method
        //
        // The GetLinks method returns a NpgsqlDataReader containing all of the
        // links for a specific portal module from the announcements
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetLinks.htm" style="color:green">GetLinks Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetLinks(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetLinks(:ModuleID)", myConnection);

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
        // GetSingleLink Method
        //
        // The GetSingleLink method returns a NpgsqlDataReader containing details
        // about a specific link from the Links database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleLink.htm" style="color:green">GetSingleLink Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleLink(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleLink(:ItemID)", myConnection);

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
        // DeleteLink Method
        //
        // The DeleteLink method deletes a specified link from
        // the Links database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteLink.htm" style="color:green">DeleteLink Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteLink(int itemID) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("DeleteLink(:ItemID)", myConnection);

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
        // AddLink Method
        //
        // The AddLink method adds a new link within the
        // links database table, and returns ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddLink.htm" style="color:green">AddLink Stored Procedure</a>
        //
        //*********************************************************************

        public int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl, int viewOrder, String description) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddLink(:ModuleID, :UserName, :Title, :Url, :MobileUrl, :ViewOrder, :Description)", myConnection);

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

            NpgsqlParameter parameterDescription = new NpgsqlParameter("Description", DbType.String);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            NpgsqlParameter parameterUrl = new NpgsqlParameter("Url", DbType.String);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            NpgsqlParameter parameterMobileUrl = new NpgsqlParameter("MobileUrl", DbType.String);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            NpgsqlParameter parameterViewOrder = new NpgsqlParameter("ViewOrder", DbType.Int32);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // UpdateLink Method
        //
        // The UpdateLink method updates a specified link within
        // the Links database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateLink.htm" style="color:green">UpdateLink Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl, int viewOrder, String description) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateLink(:ItemID, :UserName, :Title, :Url, :MobileUrl, :ViewOrder, :Description)", myConnection);

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

            NpgsqlParameter parameterDescription = new NpgsqlParameter("Description", DbType.String);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            NpgsqlParameter parameterUrl = new NpgsqlParameter("Url", DbType.String);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            NpgsqlParameter parameterMobileUrl = new NpgsqlParameter("MobileUrl", DbType.String);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            NpgsqlParameter parameterViewOrder = new NpgsqlParameter("ViewOrder", DbType.Int32);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
            myCommand.ExecuteScalar();
            myConnection.Close();
        }
    }
}

