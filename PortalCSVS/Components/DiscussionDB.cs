using System;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Web;
using ASPNetPortal;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // DiscussionDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // discussions within the Portal database.
    //
    //*********************************************************************

    public class DiscussionDB {

        //*******************************************************
        //
        // GetTopLevelMessages Method
        //
        // Returns details for all of the messages in the discussion specified by ModuleID.
        //
        // Other relevant sources:
        //     + <a href="GetTopLevelMessages.htm" style="color:green">GetTopLevelMessages Stored Procedure</a>
        //
        //*******************************************************

        public NpgsqlDataReader GetTopLevelMessages(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("GetTopLevelMessages(:ModuleID)", myConnection);

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

        //*******************************************************
        //
        // GetThreadMessages Method
        //
        // Returns details for all of the messages the thread, as identified by the Parent id string.
        //
        // Other relevant sources:
        //     + <a href="GetThreadMessages.htm" style="color:green">GetThreadMessages Stored Procedure</a>
        //
        //*******************************************************

        public NpgsqlDataReader GetThreadMessages(String parent) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetThreadMessages(:Parent)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterParent = new NpgsqlParameter("Parent", DbType.String);
            parameterParent.Value = parent;
            myCommand.Parameters.Add(parameterParent);

            // Execute the command
            myConnection.Open();
            NpgsqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
			
            // Return the datareader 
            return result;
        }

        //*******************************************************
        //
        // GetSingleMessage Method
        //
        // The GetSingleMessage method returns the details for the message
        // specified by the itemId parameter.
        //
        // Other relevant sources:
        //     + <a href="GetSingleMessage.htm" style="color:green">GetSingleMessage Stored Procedure</a>
        //
        //*******************************************************

        public NpgsqlDataReader GetSingleMessage(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleMessage(:ItemID)", myConnection);

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
        // AddMessage Method
        //
        // The AddMessage method adds a new message within the
        // Discussions database table, and returns ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddMessage.htm" style="color:green">AddMessage Stored Procedure</a>
        //
        //*********************************************************************

        public int AddMessage(int moduleId, int parentId, String userName, String title, String body) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddMessage(:Title, :Body, :ParentID, :UserName, :ModuleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterTitle = new NpgsqlParameter("Title", DbType.String);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            NpgsqlParameter parameterBody = new NpgsqlParameter("Body", DbType.String);
            parameterBody.Value = body;
            myCommand.Parameters.Add(parameterBody);

            NpgsqlParameter parameterParentID = new NpgsqlParameter("ParentID", DbType.Int32);
            parameterParentID.Value = parentId;
            myCommand.Parameters.Add(parameterParentID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }
    }
}

