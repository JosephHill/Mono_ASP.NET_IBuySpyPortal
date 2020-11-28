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

    public class EditLinks : System.Web.UI.Page {
        protected System.Web.UI.WebControls.TextBox TitleField;
        protected System.Web.UI.WebControls.RequiredFieldValidator Req1;
        protected System.Web.UI.WebControls.TextBox UrlField;
        protected System.Web.UI.WebControls.RequiredFieldValidator Req2;
        protected System.Web.UI.WebControls.TextBox MobileUrlField;
        protected System.Web.UI.WebControls.TextBox DescriptionField;
        protected System.Web.UI.WebControls.TextBox ViewOrderField;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredViewOrder;
        protected System.Web.UI.WebControls.CompareValidator VerifyViewOrder;
        protected System.Web.UI.WebControls.LinkButton updateButton;
        protected System.Web.UI.WebControls.LinkButton cancelButton;
        protected System.Web.UI.WebControls.LinkButton deleteButton;
        protected System.Web.UI.WebControls.Label CreatedBy;
        protected System.Web.UI.WebControls.Label CreatedDate;
    
        int itemId = 0;
        int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the 
        // ItemId of the link to edit.
        //
        // It then uses the ASPNetPortal.LinkDB() data component
        // to populate the page's edit controls with the links details.
        //
        //****************************************************************

        private void Page_Load(object sender, System.EventArgs e) {

            // Determine ModuleId of Links Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false) {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Link to Update
            if (Request.Params["ItemId"] != null) {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // link itemId value is specified, and if so populate page
            // contents with the link details

            if (Page.IsPostBack == false) {

                if (itemId != 0) {

                    // Obtain a single row of link information
                    ASPNetPortal.LinkDB links = new ASPNetPortal.LinkDB();
                    IDataReader dr = links.GetSingleLink(itemId);
                
                    // Read in first row from database
                    dr.Read();

                    TitleField.Text = (String) dr["title"];
                    DescriptionField.Text = (String) dr["description"];
                    UrlField.Text = (String) dr["url"];
                    MobileUrlField.Text = (String) dr["mobileurl"];
                    ViewOrderField.Text = dr["vieworder"].ToString();
                    CreatedBy.Text = (String) dr["createdbyuser"];
                    CreatedDate.Text = ((DateTime) dr["createddate"]).ToShortDateString();
                
                    // Close datareader
                    dr.Close();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a link.  It  uses the ASPNetPortal.LinkDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        private void UpdateBtn_Click(Object sender, EventArgs e) {

            if (Page.IsValid == true) {

                // Create an instance of the Link DB component
                ASPNetPortal.LinkDB links = new ASPNetPortal.LinkDB();

                if (itemId == 0) {

                    // Add the link within the Links table
                    links.AddLink( moduleId, itemId, Context.User.Identity.Name, TitleField.Text, UrlField.Text, MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text );
                }
                else {

                    // Update the link within the Links table
                    links.UpdateLink( moduleId, itemId, Context.User.Identity.Name, TitleField.Text, UrlField.Text, MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text );
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete 
        // a link.  It  uses the ASPNetPortal.LinksDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        private void DeleteBtn_Click(Object sender, EventArgs e) {

            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (itemId != 0) {

                ASPNetPortal.LinkDB links = new ASPNetPortal.LinkDB();
                links.DeleteLink(itemId);
            }

            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page, and return the user back to the portal home
        // page.
        //
        //****************************************************************

        private void CancelBtn_Click(Object sender, EventArgs e) {

            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }
        
        public EditLinks() {
            Page.Init += new System.EventHandler(Page_Init);
        }

        private void Page_Init(object sender, EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

		#region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {    
            this.updateButton.Click += new System.EventHandler(this.UpdateBtn_Click);
            this.cancelButton.Click += new System.EventHandler(this.CancelBtn_Click);
            this.deleteButton.Click += new System.EventHandler(this.DeleteBtn_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
    }
}
