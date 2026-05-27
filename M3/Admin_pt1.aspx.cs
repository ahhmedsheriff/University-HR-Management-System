using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace M3
{
    public partial class Admin_pt1 : System.Web.UI.Page
    {
        // One shared connection string for all operations
        private readonly string connStr =
            WebConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // ================== PART 1 HANDLERS ==================

        // 1) All Employee Profiles
        protected void btnShowProfiles_Click(object sender, EventArgs e)
        {
            gvProfiles.Visible = true;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM allEmployeeProfiles", conn);
                conn.Open();
                gvProfiles.DataSource = cmd.ExecuteReader();
                gvProfiles.DataBind();
            }
        }

        protected void btnHideProfiles_Click(object sender, EventArgs e)
        {
            gvProfiles.Visible = false;
        }

        // 2) Number of Employees per Department
        protected void btnShowEmpCount_Click(object sender, EventArgs e)
        {
            gvEmpCount.Visible = true;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM NoEmployeeDept", conn);
                conn.Open();
                gvEmpCount.DataSource = cmd.ExecuteReader();
                gvEmpCount.DataBind();
            }
        }

        protected void btnHideEmpCount_Click(object sender, EventArgs e)
        {
            gvEmpCount.Visible = false;
        }

        // 3) Rejected Medical Leaves
        protected void btnShowRejected_Click(object sender, EventArgs e)
        {
            gvRejectedMed.Visible = true;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM allRejectedMedicals", conn);
                conn.Open();
                gvRejectedMed.DataSource = cmd.ExecuteReader();
                gvRejectedMed.DataBind();
            }
        }

        protected void btnHideRejected_Click(object sender, EventArgs e)
        {
            gvRejectedMed.Visible = false;
        }

        // 4) Update Attendance (this is just UI messaging, your stored proc call can be added here if needed)
        protected void btnUpdateAtt_Click(object sender, EventArgs e)
        {
            lblUpdateAtt.Text = "Attendance Updated for Employee ID: " + txtAttEmpID.Text;
        }

        // 5) Add Holiday (again, currently only UI text, you can call Add_Holiday proc here if you want)
        protected void btnAddHoliday_Click(object sender, EventArgs e)
        {
            lblHoliday.Text = $"Holiday '{txtHolidayName.Text}' added from {txtHolidayFrom.Text} to {txtHolidayTo.Text}";
        }

        // 6) Initiate Attendance (you can call Initiate_Attendance proc here later)
        protected void btnInitAttendance_Click(object sender, EventArgs e)
        {
            lblInitAtt.Text = "Attendance Initiated for all employees.";
        }

        // 7) Remove Deductions of resigned employees (UI only here)
        protected void btnRemoveDed_Click(object sender, EventArgs e)
        {
            lblRemoveDed.Text = "All deductions removed for resigned employees.";
        }

        // Logout
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainHome.aspx");
        }

        // ================== PART 2 HANDLERS ==================

        // 1) Fetch attendance records for yesterday 
        protected void btnFetchYesterdayAttendance_Click(object sender, EventArgs e)
        {
            lblYesterdayMsg.Text = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM allEmployeeAttendance"; // view already filters yesterday

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvYesterdayAttendance.DataSource = dt;
                    gvYesterdayAttendance.DataBind();

                    if (dt.Rows.Count == 0)
                        lblYesterdayMsg.Text = "No attendance records found for yesterday.";
                }
            }
            catch (Exception ex)
            {
                lblYesterdayMsg.Text = "Error: " + ex.Message;
            }
        }

        // 2) Fetch details for performance in all Winter semesters 
        protected void btnFetchWinterPerformance_Click(object sender, EventArgs e)
        {
            lblWinterMsg.Text = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM allPerformance"; 

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvWinterPerformance.DataSource = dt;
                    gvWinterPerformance.DataBind();

                    if (dt.Rows.Count == 0)
                        lblWinterMsg.Text = "No Winter performance records found.";
                }
            }
            catch (Exception ex)
            {
                lblWinterMsg.Text = "Error: " + ex.Message;
            }
        }

        // 3) Remove attendance records for all employees during official holidays
        protected void btnRemoveHolidayAttendance_Click(object sender, EventArgs e)
        {
            lblHolidayAttendanceMsg.Text = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Remove_Holiday", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    lblHolidayAttendanceMsg.Text =
                        "Removed attendance records that fall in holiday ranges. Rows affected: " + rowsAffected;
                }
            }
            catch (Exception ex)
            {
                lblHolidayAttendanceMsg.Text = "Error: " + ex.Message;
            }
        }

        // 4) Remove unattended dayoff for a certain employee from current month
        protected void btnRemoveDayOff_Click(object sender, EventArgs e)
        {
            lblDayOffMsg.Text = "";

            if (!int.TryParse(txtEmpDayOff.Text.Trim(), out int empId))
            {
                lblDayOffMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Remove_DayOff", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    lblDayOffMsg.Text =
                        "Unattended dayoff records removed for employee " + empId +
                        ". Rows affected (approx): " + rows;
                }
            }
            catch (Exception ex)
            {
                lblDayOffMsg.Text = "Error: " + ex.Message;
            }
        }

        // 5) Remove approved leaves for a certain employee from attendance 
        protected void btnRemoveApprovedLeaves_Click(object sender, EventArgs e)
        {
            lblLeavesMsg.Text = "";

            if (!int.TryParse(txtEmpLeaves.Text.Trim(), out int empId))
            {
                lblLeavesMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Remove_Approved_Leaves", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_id", empId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    lblLeavesMsg.Text =
                        "Attendance for approved leave days removed for employee " + empId +
                        ". Rows affected (approx): " + rows;
                }
            }
            catch (Exception ex)
            {
                lblLeavesMsg.Text = "Error: " + ex.Message;
            }
        }

        // 6) Replace another employee 
        protected void btnReplaceEmployee_Click(object sender, EventArgs e)
        {
            lblReplaceMsg.Text = "";

            if (!int.TryParse(txtEmp1.Text.Trim(), out int emp1))
            {
                lblReplaceMsg.Text = "Emp1_ID must be a valid integer.";
                return;
            }

            if (!int.TryParse(txtEmp2.Text.Trim(), out int emp2))
            {
                lblReplaceMsg.Text = "Emp2_ID must be a valid integer.";
                return;
            }

            if (!DateTime.TryParse(txtFromDate.Text.Trim(), out DateTime from))
            {
                lblReplaceMsg.Text = "From Date must be a valid date.";
                return;
            }

            if (!DateTime.TryParse(txtToDate.Text.Trim(), out DateTime to))
            {
                lblReplaceMsg.Text = "To Date must be a valid date.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Replace_employee", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emp1_ID", emp1);
                    cmd.Parameters.AddWithValue("@Emp2_ID", emp2);
                    cmd.Parameters.AddWithValue("@from_date", from.Date);
                    cmd.Parameters.AddWithValue("@to_date", to.Date);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblReplaceMsg.Text =
                        $"Replacement added: Emp1 = {emp1}, Emp2 = {emp2}, From {from:yyyy-MM-dd} To {to:yyyy-MM-dd}.";
                }
            }


            catch (Exception ex)
            {
                lblReplaceMsg.Text = "Error: " + ex.Message;
            }

        }


        // Update Employment Status for ONE employee
        protected void btnUpdateEmpStatus_Click(object sender, EventArgs e)
        {
            lblEmpStatusMsg.Text = "";

            // Validate input
            if (!int.TryParse(txtUpdateStatusEmpID.Text.Trim(), out int empId))
            {
                lblEmpStatusMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Update_Employment_Status", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employee_ID", empId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblEmpStatusMsg.Text = $"employment_status updated successfully for Employee ID {empId}.";
            }
            catch (Exception ex)
            {
                lblEmpStatusMsg.Text = "Error: " + ex.Message;
            }
        }

    }
}
