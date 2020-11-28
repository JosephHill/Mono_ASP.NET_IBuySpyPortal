using System;
using System.Configuration;
using System.Data;
using Npgsql;
using ASPNetPortal;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // DocumentDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // documents within the Portal database.
    //
    //*********************************************************************

    public class DocumentDB {

        //*********************************************************************
        //
        // GetDocuments Method
        //
        // The GetDocuments method returns a NpgsqlDataReader containing all of the
        // documents for a specific portal module from the documents
        // database.
        //
        // Other relevant sources:
        //     + <a href="GetDocuments.htm" style="color:green">GetDocuments Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetDocuments(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("GetDocuments(:ModuleID)", myConnection);

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
        // GetSingleDocument Method
        //
        // The GetSingleDocument method returns a NpgsqlDataReader containing details
        // about a specific document from the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleDocument.htm" style="color:green">GetSingleDocument Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleDocument(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleDocument(:ItemID)", myConnection);

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
        // GetDocumentContent Method
        //
        // The GetDocumentContent method returns the contents of the specified
        // document from the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="GetDocumentContent.htm" style="color:green">GetDocumentContent</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetDocumentContent(int itemId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetDocumentContent(:ItemID)", myConnection);

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
        // DeleteDocument Method
        //
        // The DeleteDocument method deletes the specified document from
        // the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteDocument.htm" style="color:green">DeleteDocument Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteDocument(int itemID) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteDocument(:ItemID)", myConnection);

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
        // UpdateDocument Method
        //
        // The UpdateDocument method updates the specified document within
        // the Documents database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateDocument.htm" style="color:green">UpdateDocument Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category, byte[] content, int size, String contentType) {

            if (userName.Length < 1) {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateDocument(:ItemID, :ModuleID, :FileFriendlyName, :FileNameUrl, :UserName, :Category, :Content, :ContentType, :ContentSize)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterItemID = new NpgsqlParameter("ItemID", DbType.Int32);
            parameterItemID.Value = itemId;
            myCommand.Parameters.Add(parameterItemID);

            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            NpgsqlParameter parameterUserName = new NpgsqlParameter("UserName", DbType.String);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            NpgsqlParameter parameterName = new NpgsqlParameter("FileFriendlyName", DbType.String);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            NpgsqlParameter parameterFileUrl = new NpgsqlParameter("FileNameUrl", DbType.String);
            parameterFileUrl.Value = url;
            myCommand.Parameters.Add(parameterFileUrl);

            NpgsqlParameter parameterCategory = new NpgsqlParameter("Category", DbType.String);
            parameterCategory.Value = category;
            myCommand.Parameters.Add(parameterCategory);

            NpgsqlParameter parameterContent = new NpgsqlParameter("Content", DbType.Object);
            parameterContent.Value = content;
            myCommand.Parameters.Add(parameterContent);

            NpgsqlParameter parameterContentType = new NpgsqlParameter("ContentType", DbType.String);
            parameterContentType.Value = contentType;
            myCommand.Parameters.Add(parameterContentType);

            NpgsqlParameter parameterContentSize = new NpgsqlParameter("ContentSize", DbType.Int32);
            parameterContentSize.Value = size;
            myCommand.Parameters.Add(parameterContentSize);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }   
    }
}

