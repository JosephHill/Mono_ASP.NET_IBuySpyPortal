using System;
using System.Configuration;
using System.Data;
using ASPNetPortal;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // ContactDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // contacts within the Portal database.
    //
    //*********************************************************************

    public class ContactsDB {

        //*********************************************************************
        //
        // GetContacts Method
        //
        // The GetContacts method returns a DataSet containing all of the
        // contacts for a specific portal module from the contacts
        // database.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetContacts.htm" style="color:green">GetContacts Stored Procedure</a>
        //
        //*********************************************************************

        public DataSet GetContacts(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlDataAdapter myCommand = new NpgsqlDataAdapter("GetContacts(:ModuleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
			NpgsqlParameter parameterModuleId = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleId.Value = moduleId;
            myCommand.SelectCommand.Parameters.Add(parameterModuleId);

            // Create and Fill the DataSet
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        //*********************************************************************
        //
        // GetSingleContact Method
        //
        // The GetSingleContact method returns a SqlDataReader containing details
        // about a specific contact from the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleContact.htm" style="color:green">GetSingleContact Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleContact(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleContact(:ItemID)", myConnection);

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
        // DeleteContact Method
        //
        // The DeleteContact method deletes the specified contact from
        // the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteContact.htm" style="color:green">DeleteContact Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteContact(int itemID) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
		    NpgsqlCommand myCommand = new NpgsqlCommand("DeleteContact(:ItemID)", myConnection);

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
        // AddContact Method
        //
        // The AddContact method adds a new contact to the Contacts
        // database table, and returns the ItemId value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddContact.htm" style="color:green">AddContact Stored Procedure</a>
        //
        //*********************************************************************

        public int AddContact(int moduleId, int itemId, String userName, String name, String role, String email, String contact1, String contact2) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddContact(:ModuleID, :UserName, :Name, :Role, :Email, :Contact1, :Contact2)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterName = new NpgsqlParameter("Name", DbType.String);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            NpgsqlParameter parameterRole = new NpgsqlParameter("Role", DbType.String);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            NpgsqlParameter parameterContact1 = new NpgsqlParameter("Contact1", DbType.String);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            NpgsqlParameter parameterContact2 = new NpgsqlParameter("Contact2", DbType.String);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

            myConnection.Open();
            int retVal = (int) myCommand.ExecuteScalar();
            myConnection.Close();

            return retVal;
        }

        //*********************************************************************
        //
        // UpdateContact Method
        //
        // The UpdateContact method updates the specified contact within
        // the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateContact.htm" style="color:green">UpdateContact Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email, String contact1, String contact2) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateContact(:ItemID, :UserName, :Name, :Role, :Email, :Contact1, :Contact2)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemID = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemID.Value = itemId;
            myCommand.Parameters.Add(parameterItemID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterName = new NpgsqlParameter("Name", DbType.String);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            NpgsqlParameter parameterRole = new NpgsqlParameter("Role", DbType.String);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            NpgsqlParameter parameterContact1 = new NpgsqlParameter("Contact1", DbType.String);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            NpgsqlParameter parameterContact2 = new NpgsqlParameter("Contact2", DbType.String);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
    }
}

