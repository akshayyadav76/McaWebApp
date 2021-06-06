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
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e) {
            if (TxtLogin.Text == "")
            {
                AlertDialog.MyAlert(this, "Enter employee no.", "123");
                return;
            }
            if (TxtPassword.Text == "")
            {
                AlertDialog.MyAlert(this, "Enter password.", "123");
                return;
            }
            else {

                SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-4P5P3F5\SQLEXPRESS;Initial Catalog=WCPS_Database;Integrated Security=True");
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("select em_no,em_name,em_lname,em_email,em_dept,em_des,em_mobile from emp_master where em_no="+ TxtLogin.Text + " and em_password=" + TxtPassword.Text + "", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {

                    Session["em_no"] = dt.Rows[0]["em_no"].ToString();
                    Session["em_name"] =  dt.Rows[0]["em_name"].ToString() + " " + dt.Rows[0]["em_lname"].ToString() + " [" + dt.Rows[0]["em_des"].ToString() + "]";
                    Session["em_email"] = dt.Rows[0]["em_email"].ToString();

                    Session["name"] = dt.Rows[0]["em_name"].ToString();
                    Session["lname"] = dt.Rows[0]["em_lname"].ToString();

                    if (Session["ret_url"] == null)
                    {
                        Response.Redirect("HomePage.aspx");
                    }
                    else
                    {
                        Response.Redirect(Session["ret_url"].ToString());
                    }
                }
                else {
                    AlertDialog.MyAlert(this, "Please enter right credentials", "123");
                }
               
            }
        }
    }
}