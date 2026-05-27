using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class Attendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void btnLoadAttendance_Click(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            errorMsg.Text = "";
            errorMsg.Visible = false;
            int empId = Convert.ToInt32(Session["employee_ID"]);

            string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM dbo.MyAttendance(@employee_ID)", conn);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@employee_ID", empId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMsg.Text = "No attendance data found.";
                    errorMsg.Visible = true;
                }
                else
                {
                    errorMsg.Visible = false;
                    attendanceGrid.DataSource = dt;
                    attendanceGrid.DataBind();
                }
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
