using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace M3
{
    public partial class Performance : System.Web.UI.Page
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
                errorMsg.Visible = false;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = int.Parse(Session["employee_ID"].ToString());
                string semester = txtSemester.Text.Trim().ToUpper(); // W24, S24, etc.

                if (string.IsNullOrEmpty(semester))
                {
                    errorMsg.Text = "Please enter a semester (e.g., W24, S24).";
                    errorMsg.Visible = true;
                    return;
                }

                if (semester.Length != 3)
                {
                    errorMsg.Text = "Semester format must be 3 characters (e.g., W24).";
                    errorMsg.Visible = true;
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT * FROM dbo.MyPerformance(@id, @semester)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = empId;
                    cmd.Parameters.Add("@semester", SqlDbType.Char, 3).Value = semester;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        errorMsg.Text = "No performance records found for the entered semester.";
                        errorMsg.ForeColor = System.Drawing.Color.Red;
                        errorMsg.Visible = true;
                        performanceGrid.DataSource = null;
                        performanceGrid.DataBind();
                    }
                    else
                    {
                        performanceGrid.DataSource = dt;
                        performanceGrid.DataBind();
                        errorMsg.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg.Text = "Error: " + ex.Message;
                errorMsg.ForeColor = System.Drawing.Color.Red;
                errorMsg.Visible = true;
            }
        }

        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
