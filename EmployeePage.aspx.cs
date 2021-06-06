using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace McaWebApp
{
    public partial class EmployeePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["em_no"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (Session["em_no"].Equals("1001"))
            {
                view_claims.Visible = true;
                apply_claims.Visible = false;

            }
            else
            {
                view_claims.Visible = false;
                apply_claims.Visible = true;

            }
            sp_name.InnerHtml = Session["em_name"].ToString();
            displaygrid();
        }

        private void displaygrid()
        {
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-4P5P3F5\SQLEXPRESS;Initial Catalog=WCPS_Database;Integrated Security=True");
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string s1 = " and ec_approve_date is null";
            string s2 = " ";

            string s3;

            s3 = "SELECT em_no as [Emp No],em_name as [Emp Name] , em_lname as [Last Name],em_dept as [Department ],em_des as [Designation], em_email as [Email] , em_mobile as [Mobile No],em_created_date as [Created Date] from emp_master";



            SqlDataAdapter da = new SqlDataAdapter(s3,cn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            sp_count.InnerHtml = "<strong>" + dt.Rows.Count.ToString() + " Records Found.</strong> ";

            ViewState["dt"] = dt;
            GridView1.DataSource = ViewState["dt"];
            GridView1.DataBind();

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = ViewState["dt"];
            GridView1.DataBind();
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
        protected void BtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}