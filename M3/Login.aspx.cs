using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string user = username.Text.Trim();
            string pass = password.Text.Trim();

            int empId;
            if (!int.TryParse(user, out empId))
            {
                loginError.Text = "Employee ID must be a number.";
                loginError.Visible = true;
                return;
            }

            string connStr = ConfigurationManager
                .ConnectionStrings["University_HR_ManagementSystem"]
                .ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT dbo.EmployeeLoginValidation(@employee_ID, @password)",
                    conn
                );

                cmd.Parameters.AddWithValue("@employee_ID", empId);
                cmd.Parameters.AddWithValue("@password", pass);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                bool success = (result != null && result != DBNull.Value && Convert.ToBoolean(result));

                if (success)
                {
                    Session["employee_ID"] = empId;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    loginError.Text = "Invalid ID or password.";
                    loginError.Visible = true;
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainHome.aspx");
        }

    }
}
