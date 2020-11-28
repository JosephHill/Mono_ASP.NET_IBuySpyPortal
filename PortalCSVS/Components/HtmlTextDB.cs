using System;
using System.Configuration;
using System.Data;
using ASPNetPortal;
using Npgsql;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // HtmlTextDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // HTML/text within the Portal database.
    //
    //*********************************************************************

    public class HtmlTextDB {


        //*********************************************************************
        //
        // GetHtmlText Method
        //
        // The GetHtmlText method returns a SqlDataReader containing details
        // about a specific item from the HtmlText database table.
        //
        // Other relevant sources:
        //     + <a href="GetHtmlText.htm" style="color:green">GetHtmlText Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetHtmlText(int moduleId) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
			NpgsqlCommand myCommand = new NpgsqlCommand("GetHtmlText(:ModuleID)", myConnection);

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
        // UpdateHtmlText Method
        //
        // The UpdateHtmlText method updates a specified item within
        // the HtmlText database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateHtmlText.htm" style="color:green">UpdateHtmlText Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateHtmlText(:ModuleID, :DesktopHtml, :MobileSummary, :MobileDetails)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            NpgsqlParameter parameterDesktopHtml = new NpgsqlParameter("DesktopHtml", DbType.String);
            parameterDesktopHtml.Value = desktopHtml;
            myCommand.Parameters.Add(parameterDesktopHtml);

            NpgsqlParameter parameterMobileSummary = new NpgsqlParameter("MobileSummary", DbType.String);
            parameterMobileSummary.Value = mobileSummary;
            myCommand.Parameters.Add(parameterMobileSummary);

            NpgsqlParameter parameterMobileDetails = new NpgsqlParameter("MobileDetails", DbType.String);
            parameterMobileDetails.Value = mobileDetails;
            myCommand.Parameters.Add(parameterMobileDetails);

            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }
    }
}

