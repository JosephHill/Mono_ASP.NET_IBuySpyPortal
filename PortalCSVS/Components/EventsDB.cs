using System;
using System.Configuration;
using System.Data;
using ASPNetPortal;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // EventDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // events within the Portal database.
    //
    //*********************************************************************

    public class EventsDB {

        //*********************************************************************
        //
        // GetEvents Method
        //
        // The GetEvents method returns a DataSet containing all of the
        // events for a specific portal module from the events
        // database.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetEvents.htm" style="color:green">GetEvents Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetEvents(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetEvents(:ModuleID)", myConnection);

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
        // GetSingleEvent Method
        //
        // The GetSingleEvent method returns a NpgsqlDataReader containing details
        // about a specific event from the events database.
        //
        // Other relevant sources:
        //     + <a href="GetSingleEvent.htm" style="color:green">GetSingleEvent Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleEvent(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleEvent(:ItemID)", myConnection);

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
        // DeleteEvent Method
        //
        // The DeleteEvent method deletes a specified event from
        // the events database.
        //
        // Other relevant sources:
        //     + <a href="DeleteEvent.htm" style="color:green">DeleteEvent Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteEvent(int itemID) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteEvent(:ItemID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemID = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            // Open the database connection and execute SQL Command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // AddEvent Method
        //
        // The AddEvent method adds a new event within the Events database table, 
        // and returns the ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddEvent.htm" style="color:green">AddEvent Stored Procedure</a>
        //
        //*********************************************************************

        public int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String wherewhen) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddEvent(:ModuleID, :UserName, :Title, :ExpireDate, :Description, :WhereWhen)", myConnection);

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

            NpgsqlParameter parameterWhereWhen = new NpgsqlParameter("WhereWhen", DbType.String);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);

            NpgsqlParameter parameterExpireDate = new NpgsqlParameter("ExpireDate", DbType.DateTime);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            NpgsqlParameter parameterDescription = new NpgsqlParameter("Description", DbType.String);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            // Open the database connection and execute SQL Command
            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // UpdateEvent Method
        //
        // The UpdateEvent method updates the specified event within
        // the Events database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateEvent.htm" style="color:green">UpdateEvent Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate, String description, String wherewhen) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateEvent(:ItemID, :UserName, :Title, :ExpireDate, :Description, :WhereWhen)", myConnection);

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

            NpgsqlParameter parameterWhereWhen = new NpgsqlParameter("WhereWhen", DbType.String);
            parameterWhereWhen.Value = wherewhen;
            myCommand.Parameters.Add(parameterWhereWhen);

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