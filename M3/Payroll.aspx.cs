using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class Payroll : System.Web.UI.Page
    {
        protected void btnLoadPayroll_Click(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int empId = Convert.ToInt32(Session["employee_ID"]);

            string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM dbo.Last_month_payroll(@employee_ID)", conn);

                cmd.Parameters.AddWithValue("@employee_ID", empId);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMsg.Text = "No payroll data found for last month.";
                    errorMsg.Visible = true;
                }
                else
                {
                    errorMsg.Visible = false;
                    payrollGrid.DataSource = dt;
                    payrollGrid.DataBind();
                }
            }
        }
        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
