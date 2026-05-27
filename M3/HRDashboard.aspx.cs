using System;
using System.Configuration;
using System.Data.SqlClient;

namespace M3
{
    public partial class HRDashboard : System.Web.UI.Page
    {
        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["University_HR_ManagementSystem"]
                .ConnectionString;
        }

        private int GetLoggedInHRId()
        {
            if (Session["HR_ID"] == null)
                throw new InvalidOperationException("HR Employee is not logged in.");

            return (int)Session["HR_ID"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HR_ID"] == null)
            {
                // no HR logged in, back to home
                Response.Redirect("MainHome.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int hrId = (int)Session["HR_ID"];
                lblHRWelcome.Text = "Logged in as HR Employee ID: " + hrId;
            }
        }

        // 1) Approve/reject annual & accidental leaves
        protected void btnApproveAnnualAcc_Click(object sender, EventArgs e)
        {
            lblAnnualAccMsg.Text = "";

            if (!int.TryParse(txtAnnualAccRequestID.Text.Trim(), out int requestId))
            {
                lblAnnualAccMsg.Text = "Request ID must be a valid integer.";
                return;
            }

            int hrId = GetLoggedInHRId();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("HR_approval_an_acc", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestId);
                    cmd.Parameters.AddWithValue("@HR_ID", hrId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblAnnualAccMsg.Text = "Processed annual/accidental leave request " + requestId +
                                           ". Check final_approval_status and balances in DB.";
                }
            }
            catch (Exception ex)
            {
                lblAnnualAccMsg.Text = "Error: " + ex.Message;
            }
        }

        // 2) Approve/reject unpaid leaves
        protected void btnApproveUnpaid_Click(object sender, EventArgs e)
        {
            lblUnpaidMsg.Text = "";

            if (!int.TryParse(txtUnpaidRequestID.Text.Trim(), out int requestId))
            {
                lblUnpaidMsg.Text = "Request ID must be a valid integer.";
                return;
            }

            int hrId = GetLoggedInHRId();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("HR_approval_Unpaid", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestId);
                    cmd.Parameters.AddWithValue("@HR_ID", hrId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblUnpaidMsg.Text = "Processed unpaid leave request " + requestId +
                                        ". Check final_approval_status in DB.";
                }
            }
            catch (Exception ex)
            {
                lblUnpaidMsg.Text = "Error: " + ex.Message;
            }
        }

        // 3) Approve/reject compensation leaves
        protected void btnApproveComp_Click(object sender, EventArgs e)
        {
            lblCompMsg.Text = "";

            if (!int.TryParse(txtCompRequestID.Text.Trim(), out int requestId))
            {
                lblCompMsg.Text = "Request ID must be a valid integer.";
                return;
            }

            int hrId = GetLoggedInHRId();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("HR_approval_comp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestId);
                    cmd.Parameters.AddWithValue("@HR_ID", hrId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblCompMsg.Text = "Processed compensation leave request " + requestId +
                                      ". Check final_approval_status in DB.";
                }
            }
            catch (Exception ex)
            {
                lblCompMsg.Text = "Error: " + ex.Message;
            }
        }

        // 4) Deduction_hours
        protected void btnDeductHours_Click(object sender, EventArgs e)
        {
            lblHoursMsg.Text = "";

            if (!int.TryParse(txtHoursEmpID.Text.Trim(), out int empId))
            {
                lblHoursMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("Deduction_hours", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblHoursMsg.Text = "Missing hours deduction added (if applicable) for employee " + empId + ".";
                }
            }
            catch (Exception ex)
            {
                lblHoursMsg.Text = "Error: " + ex.Message;
            }
        }

        // 5) Deduction_days
        protected void btnDeductDays_Click(object sender, EventArgs e)
        {
            lblDaysMsg.Text = "";

            if (!int.TryParse(txtDaysEmpID.Text.Trim(), out int empId))
            {
                lblDaysMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("Deduction_days", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_id", empId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblDaysMsg.Text = "Missing days deduction added (if applicable) for employee " + empId + ".";
                }
            }
            catch (Exception ex)
            {
                lblDaysMsg.Text = "Error: " + ex.Message;
            }
        }

        // 6) Deduction_unpaid
        protected void btnDeductUnpaid_Click(object sender, EventArgs e)
        {
            lblUnpaidDedMsg.Text = "";

            if (!int.TryParse(txtUnpaidEmpID.Text.Trim(), out int empId))
            {
                lblUnpaidDedMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("Deduction_unpaid", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblUnpaidDedMsg.Text = "Unpaid leave deduction added (if applicable) for employee " + empId + ".";
                }
            }
            catch (Exception ex)
            {
                lblUnpaidDedMsg.Text = "Error: " + ex.Message;
            }
        }

        // 7) Add_Payroll
        protected void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            lblPayrollMsg.Text = "";

            if (!int.TryParse(txtPayrollEmpID.Text.Trim(), out int empId))
            {
                lblPayrollMsg.Text = "Employee ID must be a valid integer.";
                return;
            }

            if (!DateTime.TryParse(txtPayrollFrom.Text.Trim(), out DateTime fromDate))
            {
                lblPayrollMsg.Text = "From Date must be a valid date (yyyy-mm-dd).";
                return;
            }

            if (!DateTime.TryParse(txtPayrollTo.Text.Trim(), out DateTime toDate))
            {
                lblPayrollMsg.Text = "To Date must be a valid date (yyyy-mm-dd).";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("Add_Payroll", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);
                    cmd.Parameters.AddWithValue("@from", fromDate.Date);
                    cmd.Parameters.AddWithValue("@to", toDate.Date);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblPayrollMsg.Text = "Payroll generated for employee " + empId +
                                         " from " + fromDate.ToString("yyyy-MM-dd") +
                                         " to " + toDate.ToString("yyyy-MM-dd") + ".";
                }
            }
            catch (Exception ex)
            {
                lblPayrollMsg.Text = "Error: " + ex.Message;
            }
        }
    }
}
