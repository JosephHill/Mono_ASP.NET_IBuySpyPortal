using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace ASPNetPortal {

    public abstract class HtmlModule : ASPNetPortal.PortalModuleControl {
        protected System.Web.UI.HtmlControls.HtmlTable t1;
        protected System.Web.UI.HtmlControls.HtmlTableCell HtmlHolder;

        //*******************************************************
        //
        // The Page_Load event handler on this User Control is
        // used to render a block of HTML or text to the page.  
        // The text/HTML to render is stored in the HtmlText 
        // database table.  This method uses the ASPNetPortal.HtmlTextDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        private void Page_Load(object sender, System.EventArgs e) {

            // Obtain the selected item from the HtmlText table
            ASPNetPortal.HtmlTextDB text = new ASPNetPortal.HtmlTextDB();
            IDataReader dr = text.GetHtmlText(ModuleId);
        
            if (dr.Read()) {

                // Dynamically add the file content into the page
                String content = Server.HtmlDecode((String) dr["desktophtml"]);
                HtmlHolder.Controls.Add(new LiteralControl(content));
            }
        
            // Close the datareader
            dr.Close();       
        }

        public HtmlModule() {
            this.Init += new System.EventHandler(Page_Init);
        }

        private void Page_Init(object sender, EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

		#region Web Form Designer generated code
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
    }
}
