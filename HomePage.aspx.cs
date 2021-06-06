using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace McaWebApp
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["em_no"] == null)
            {
                //Session["ret_url"] = "mypage.aspx";
                Response.Redirect("LoginPage.aspx");
               
            }
            if (Session["em_no"].Equals("1001"))
            {
                view_claims.Visible = true;
                apply_claims.Visible = false;

            }
            else {
                view_claims.Visible = false;
                apply_claims.Visible = true;

            }
            sp_name.InnerHtml = Session["em_name"].ToString();

        }
    }
}