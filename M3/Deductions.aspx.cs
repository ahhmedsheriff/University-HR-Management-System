using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class Deductions : System.Web.UI.Page
    {
        protected void btnLoadDeductions_Click(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Validate month
            int month;
            if (!int.TryParse(ddlMonth.SelectedValue, out month) || month < 1 || month > 12)
            {
                errorMsg.Text = "Please enter a valid month (1–12).";
                errorMsg.Visible = true;
                return;
            }

            int empId = Convert.ToInt32(Session["employee_ID"]);

            string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM dbo.Deductions_Attendance(@employee_ID, @month)", conn);

                cmd.Parameters.AddWithValue("@employee_ID", empId);
                cmd.Parameters.AddWithValue("@month", month);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMsg.Text = "No deductions found for this month.";
                    errorMsg.Visible = true;
                }
                else
                {
                    errorMsg.Visible = false;
                    deductionsGrid.DataSource = dt;
                    deductionsGrid.DataBind();
                }
            }
        }
        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
