using System;
using System.Collections;
using System.Configuration;
using System.Data;
using Npgsql;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ASPNetPortal {

    //*********************************************************************
    //
    // PortalSecurity Class
    //
    // The PortalSecurity class encapsulates two helper methods that enable
    // developers to easily check the role status of the current browser client.
    //
    //*********************************************************************

    public class PortalSecurity {

       //*********************************************************************
       //
       // Security.Encrypt() Method
       //
       // The Encrypt method encrypts a clean string into a hashed string
       //
       //*********************************************************************
       public static string Encrypt(string cleanString) {

            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);

        }

        //*********************************************************************
        //
        // PortalSecurity.IsInRole() Method
        //
        // The IsInRole method enables developers to easily check the role
        // status of the current browser client.
        //
        //*********************************************************************

        public static bool IsInRole(String role) {

            return HttpContext.Current.User.IsInRole(role);
        }

        //*********************************************************************
        //
        // PortalSecurity.IsInRoles() Method
        //
        // The IsInRoles method enables developers to easily check the role
        // status of the current browser client against an array of roles
        //
        //*********************************************************************

        public static bool IsInRoles(String roles) {

            HttpContext context = HttpContext.Current;

            foreach (String role in roles.Split( new char[] {';'} )) {
            
                if (role != "" && role != null && ((role == "All Users") || (context.User.IsInRole(role)))) {
                    return true;
                }
            }

            return false;
        }

        //*********************************************************************
        //
        // PortalSecurity.HasEditPermissions() Method
        //
        // The HasEditPermissions method enables developers to easily check 
        // whether the current browser client has access to edit the settings
        // of a specified portal module
        //
        //*********************************************************************

        public static bool HasEditPermissions(int moduleId) {

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetAuthRoles(:PortalID, :ModuleID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterModuleID = new NpgsqlParameter("ModuleID", DbType.Int32);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            NpgsqlParameter parameterPortalID = new NpgsqlParameter("PortalID", DbType.Int32);
            parameterPortalID.Value = portalSettings.PortalId;
            myCommand.Parameters.Add(parameterPortalID);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            bool retVal = false;

            if (result.Read()) {
              if ((PortalSecurity.IsInRoles((String) result["accessroles"]) == false) || (PortalSecurity.IsInRoles((String) result["editroles"]) == false)) {
                retVal = false;
            }
            else {
                retVal = true;
            }
        }

            result.Close();

			return retVal;
        }
    }

    //*********************************************************************
    //
    // UsersDB Class
    //
    // The UsersDB class encapsulates all data logic necessary to add/login/query
    // users within the Portal Users database.
    //
    // Important Note: The UsersDB class is only used when forms-based cookie
    // authentication is enabled within the portal.  When windows based
    // authentication is used instead, then either the Windows SAM or Active Directory
    // is used to store and validate all username/password credentials.
    //
    //*********************************************************************

    public class UsersDB {

        //*********************************************************************
        //
        // UsersDB.AddUser() Method <a name="AddUser"></a>
        //
        // The AddUser method inserts a new user record into the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="AddUser.htm" style="color:green">AddUser Stored Procedure</a>
        //
        //*********************************************************************

        public int AddUser(String fullName, String email, String password) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("AddUser(:Name, :Email, :Password)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterFullName = new NpgsqlParameter("Name", DbType.String);
            parameterFullName.Value = fullName;
            myCommand.Parameters.Add(parameterFullName);

            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            NpgsqlParameter parameterPassword = new NpgsqlParameter("Password", DbType.String);
            if (password.Length > 20) {
                password = password.Substring(0, 20);
            }
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            int retVal = -1;

            // Execute the command in a try/catch to catch duplicate username errors
            try 
            {
                // Open the connection and execute the Command
                myConnection.Open();
                retVal = (int) myCommand.ExecuteScalar();
            }
            catch 
            {

                // failed to create a new user
                retVal = -1;
            }
            finally 
            {

                // Close the Connection
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }

            return retVal;
        }

        //*********************************************************************
        //
        // UsersDB.DeleteUser() Method <a name="DeleteUser"></a>
        //
        // The DeleteUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteUser.htm" style="color:green">DeleteUser Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteUser(int userId) 
        {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("DeleteUser(:UserID)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter parameterUserId = new NpgsqlParameter("UserID", DbType.Int32);
            parameterUserId.Value = userId;
            myCommand.Parameters.Add(parameterUserId);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // UsersDB.UpdateUser() Method <a name="DeleteUser"></a>
        //
        // The UpdateUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateUser.htm" style="color:green">UpdateUser Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateUser(int userId, String email, String password) 
        {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UpdateUser(:UserID, :Email, :Password)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter parameterUserId = new NpgsqlParameter("UserID", DbType.Int32);
            parameterUserId.Value = userId;
            myCommand.Parameters.Add(parameterUserId);

            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            NpgsqlParameter parameterPassword = new NpgsqlParameter("Password", DbType.String);
            if (password.Length > 20) {
                password = password.Substring(0, 20);
            }
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            // Open the database connection and execute the command
            myConnection.Open();
			//HACK: ExecuteScalar should be myCommand.ExecuteNonQuery();
			myCommand.ExecuteScalar();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // UsersDB.GetRolesByUser() Method <a name="GetRolesByUser"></a>
        //
        // The DeleteUser method deleted a  user record from the "Users" database table.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        //
        //*********************************************************************

        public NpgsqlDataReader GetRolesByUser(String email)
        {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetRolesByUser(:Email)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        //*********************************************************************
        //
        // GetSingleUser Method
        //
        // The GetSingleUser method returns a NpgsqlDataReader containing details
        // about a specific user from the Users database table.
        //
        //*********************************************************************

        public NpgsqlDataReader GetSingleUser(String email)
        {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetSingleUser(:Email)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            myConnection.Open();
            NpgsqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }
        //*********************************************************************
        //
        // GetRoles() Method <a name="GetRoles"></a>
        //
        // The GetRoles method returns a list of role names for the user.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        //
        //*********************************************************************

        public String[] GetRoles(String email) 
        {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("GetRolesByUser(:Email)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Open the database connection and execute the command
            NpgsqlDataReader dr;

            myConnection.Open();
            dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // create a String array from the data
            ArrayList userRoles = new ArrayList();

            while (dr.Read()) {
                userRoles.Add(dr["rolename"]);
            }

            dr.Close();

            // Return the String array of roles
            return (String[]) userRoles.ToArray(typeof(String));
        }

        //*********************************************************************
        //
        // UsersDB.Login() Method <a name="Login"></a>
        //
        // The Login method validates a email/password pair against credentials
        // stored in the users database.  If the email/password pair is valid,
        // the method returns user's name.
        //
        // Other relevant sources:
        //     + <a href="UserLogin.htm" style="color:green">UserLogin Stored Procedure</a>
        //
        //*********************************************************************

        public String Login(String email, String password) {

            // Create Instance of Connection and Command Object
            NpgsqlConnection myConnection = new NpgsqlConnection(ConfigurationSettings.AppSettings["NpgsqlConnectionString"]);
            NpgsqlCommand myCommand = new NpgsqlCommand("UserLogin(:Email, :Password)", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            NpgsqlParameter parameterEmail = new NpgsqlParameter("Email", DbType.String);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            // Only the first 20 characters of the encrypted password are stored.
            NpgsqlParameter parameterPassword = new NpgsqlParameter("Password", DbType.String);
            if (password.Length > 20) {
                password = password.Substring(0, 20);
            }
            parameterPassword.Value = password;
            myCommand.Parameters.Add(parameterPassword);

            // Open the database connection and execute the command
            myConnection.Open();
            Object userName = (Object) myCommand.ExecuteScalar();
            myConnection.Close();

            if ((userName != null) && (!DBNull.Value.Equals(userName))) {
                return ((string)userName).Trim();
            }
            else {
                return String.Empty;
            }
        }
    }
}