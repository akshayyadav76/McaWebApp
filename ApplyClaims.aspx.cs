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
    public partial class ApplyClaims : System.Web.UI.Page
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

            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-4P5P3F5\SQLEXPRESS;Initial Catalog=WCPS_Database;Integrated Security=True");
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

          /*  string s3;
            s3 = "SELECT * FROM dbo.claims WHERE ref_no =" + Request.QueryString["id"].ToString();

            SqlDataAdapter da = new SqlDataAdapter(s3, cn);
            DataTable dt = new DataTable();

            da.Fill(dt);*/

            //DataRow r;
            //r = dt.Rows[0];
            TxtEmployee.Text = Session["em_name"].ToString();
            ///TxtApplyDate.Text = r["apply_date"].ToString();
           // TxtClaimType.Text = r["type"].ToString();
           // TxtAmount.Text = r["amount"].ToString();
           // TxtDescription.Text = r["des"].ToString();

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (TxtApplyDate.Text == "" && TxtClaimType.Text == "" && TxtAmount.Text == "" && TxtDescription.Text == "")
            {
                AlertDialog.MyAlert(this, "Please enter right credentials", "123");
            }
            else {
                SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-4P5P3F5\SQLEXPRESS;Initial Catalog=WCPS_Database;Integrated Security=True");
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlCommand com = new SqlCommand("SELECT COUNT(*) from dbo.claims", cn);
                com.ExecuteNonQuery();
                int count = (int)com.ExecuteScalar();
                count++;
                String refNo = count.ToString();
                String emNO = Session["em_no"].ToString();
                String name = Session["name"].ToString();
                String lname = Session["lname"].ToString();
                String date = TxtApplyDate.Text;
                String type = TxtClaimType.Text;
                String amount = TxtAmount.Text;
                String des = TxtDescription.Text;

                SqlCommand insertClaim = new SqlCommand("INSERT INTO dbo.claims(ref_no, em_name, em_lname, em_no, type, amount, des, apply_date, action)"+
                                                        $"VALUES('{refNo}', '{name}', '{lname}', '{emNO}', '{type}', '{amount}', '{des}', '{date}', NULL)", cn);
                insertClaim.ExecuteNonQuery();
                AlertDialog.MyAlert(this, "Request claimed successfully!", "123");
                Response.Redirect("ClaimsStatusPage.aspx");


            }
           
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}