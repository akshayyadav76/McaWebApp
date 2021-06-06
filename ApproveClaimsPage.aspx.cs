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
    public partial class ApproveClaimsPage : System.Web.UI.Page
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
        s3 = "SELECT ref_no AS [Ref. No.],em_name AS [Emp. Name],em_lname AS [Last Name],em_no AS [Emp.No],type AS [Type],amount AS [Amount],des AS [Description],apply_date AS [Apply Date], action AS [Approve/Raject] from dbo.claims WHERE action IS NULL";

        SqlDataAdapter da = new SqlDataAdapter(s3, cn);
        DataTable dt = new DataTable();

        da.Fill(dt);
        sp_count.InnerHtml = "<strong>" + dt.Rows.Count.ToString() + " Application pending for approve/reject.</strong> ";

        ViewState["dt"] = dt;
        GridView1.DataSource = ViewState["dt"];
        GridView1.DataBind();

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        displaygrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            string s1, s2;
            s2 = "";
            s1 = "<a href='ApproveReject.aspx?id=" + e.Row.Cells[0].Text + "'>Edit</a>";

            e.Row.Cells[8].Text = s1;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = ViewState["dt"];
        GridView1.DataBind();
    }
  }
}