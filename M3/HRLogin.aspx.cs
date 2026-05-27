using System;
using System.Configuration;
using System.Data.SqlClient;

namespace M3
{
    public partial class HRLogin : System.Web.UI.Page
    {
        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["University_HR_ManagementSystem"]
                .ConnectionString;
        }

        protected void btnHRLogin_Click(object sender, EventArgs e)
        {
            lblHRError.Visible = false;
            lblHRError.Text = "";

            if (!int.TryParse(txtHRID.Text.Trim(), out int hrId))
            {
                lblHRError.Visible = true;
                lblHRError.Text = "HR ID must be a valid integer.";
                return;
            }

            string password = txtHRPassword.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT dbo.HRLoginValidation(@employee_ID, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("@employee_ID", hrId);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    bool success = (result != null &&
                                    result != DBNull.Value &&
                                    Convert.ToBoolean(result));

                    if (success)
                    {
                        // Save who is logged in
                        Session["HR_ID"] = hrId;
                        Response.Redirect("HRDashboard.aspx");
                    }
                    else
                    {
                        lblHRError.Visible = true;
                        lblHRError.Text = "Invalid HR ID or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblHRError.Visible = true;
                lblHRError.Text = "Error: " + ex.Message;
            }
        }
    }
}
