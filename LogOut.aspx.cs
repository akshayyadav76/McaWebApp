using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace McaWebApp
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["em_no"] = null;
            Session["em_name"] = null;
            Session["em_email"] = null;
            Response.Redirect("LoginPage.aspx");
        }
    }
}