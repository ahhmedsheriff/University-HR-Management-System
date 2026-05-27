using System;
using System.Web.UI;

namespace M3
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if not logged in
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Load employee ID only once
            if (!IsPostBack)
            {
                empIdLabel.Text = Session["employee_ID"].ToString();
            }
        }

        protected void btnPerformance_Click(object sender, EventArgs e)
        {
            Response.Redirect("Performance.aspx");
        }

        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("Attendance.aspx");
        }

        protected void btnPayroll_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payroll.aspx");
        }

        protected void btnDeductions_Click(object sender, EventArgs e)
        {
            Response.Redirect("Deductions.aspx");
        }

        // 🔁 new – goes to unified leaves page
        protected void btnSubmitLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("Leaves.aspx");
        }

        protected void btnLeaveStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveStatus.aspx");
        }

        // 🔁 new – page for upperboard/dean actions from part 2
        protected void btnUpperboardDean_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpperboardDean.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
