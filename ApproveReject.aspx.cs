using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace McaWebApp
{
    public partial class ApproveReject : System.Web.UI.Page
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

            string s3;
            s3 = "SELECT * FROM dbo.claims WHERE ref_no ="+ Request.QueryString["id"].ToString();

            SqlDataAdapter da = new SqlDataAdapter(s3, cn);
            DataTable dt = new DataTable();

            da.Fill(dt);

            DataRow r;
            r = dt.Rows[0];
            TxtEmployee.Text = r["em_name"].ToString() + r["em_lname"].ToString() + " [" + r["em_no"].ToString() + "]";
            TxtApplyDate.Text = r["apply_date"].ToString();
            TxtClaimType.Text = r["type"].ToString();
            TxtAmount.Text = r["amount"].ToString();
            TxtDescription.Text = r["des"].ToString();


        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-4P5P3F5\SQLEXPRESS;Initial Catalog=WCPS_Database;Integrated Security=True");
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string s1;
                if (RdApprove.Checked == true)
                {
                    s1 = "Approve";
                }
                else
                {
                    s1 = "Reject";
                }

                SqlCommand com = new SqlCommand("UPDATE dbo.claims set action ='" +s1 +"'WHERE ref_no=" + Request.QueryString["id"].ToString(), cn);
                com.ExecuteNonQuery();
                Response.Redirect("ApproveClaimsPage.aspx");
  
               // AlertDialog.MyAlert(this, "Changes saved successfully!", "123");


            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
 }
