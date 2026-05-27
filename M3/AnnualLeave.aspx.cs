using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class AnnualLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadReplacementEmployees();
            }
        }

        // Load replacement employees: same department and same role
        private void LoadReplacementEmployees()
        {
            int empId = Convert.ToInt32(Session["employee_ID"]);
            string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
            SELECT e.employee_id, e.first_name + ' ' + e.last_name AS FullName
            FROM Employee e
            JOIN Employee_Role er ON e.employee_id = er.emp_ID
            WHERE e.dept_name = (SELECT dept_name FROM Employee WHERE employee_id = @empID)
              AND er.role_name = (SELECT TOP 1 role_name 
                                  FROM Employee_Role 
                                  WHERE emp_ID = @empID
                                    AND role_name NOT LIKE 'HR_%') -- exclude HR roles
              AND e.employee_id <> @empID"; // exclude self

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@empID", empId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlReplacement.DataSource = reader;
                ddlReplacement.DataTextField = "FullName";
                ddlReplacement.DataValueField = "employee_id";
                ddlReplacement.DataBind();

                ddlReplacement.Items.Insert(0, new ListItem("-- Select Replacement --", "0"));
            }
        }



        // Calendar selection handlers
        protected void calendarStart_SelectionChanged(object sender, EventArgs e)
        {
            txtStart.Text = calendarStart.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void calendarEnd_SelectionChanged(object sender, EventArgs e)
        {
            txtEnd.Text = calendarEnd.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            errorMsg.Visible = false;

            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int empId = Convert.ToInt32(Session["employee_ID"]);

            // Validate inputs
            if (string.IsNullOrEmpty(txtStart.Text) || string.IsNullOrEmpty(txtEnd.Text))
            {
                ShowError("Please select both start and end dates.");
                return;
            }

            if (ddlReplacement.SelectedValue == "0")
            {
                ShowError("Please select a replacement employee.");
                return;
            }

            if (string.IsNullOrEmpty(txtReason.Text))
            {
                ShowError("Please enter a reason.");
                return;
            }

            DateTime startDate = DateTime.Parse(txtStart.Text);
            DateTime endDate = DateTime.Parse(txtEnd.Text);

            if (endDate < startDate)
            {
                ShowError("End date cannot be before start date.");
                return;
            }

            int replacementID = Convert.ToInt32(ddlReplacement.SelectedValue);

            // Submit to database via stored procedure
            string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Submit_annual", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Match your stored procedure parameters
                cmd.Parameters.AddWithValue("@employee_ID", empId);
                cmd.Parameters.AddWithValue("@replacement_emp", replacementID);
                cmd.Parameters.AddWithValue("@start_date", startDate);
                cmd.Parameters.AddWithValue("@end_date", endDate);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    errorMsg.ForeColor = System.Drawing.Color.Green;
                    errorMsg.Text = "Annual leave submitted successfully!";
                }
                catch (Exception ex)
                {
                    errorMsg.ForeColor = System.Drawing.Color.Red;
                    errorMsg.Text = "Error: " + ex.Message;
                }

                errorMsg.Visible = true;
            }
        }

        private void ShowError(string msg)
        {
            errorMsg.ForeColor = System.Drawing.Color.Red;
            errorMsg.Text = msg;
            errorMsg.Visible = true;
        }

        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
