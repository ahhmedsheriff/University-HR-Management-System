using System;
using System.Web.UI;

namespace M3
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Hardcoded admin credentials
            if (username == "admin" && password == "1234")
            {
                Session["LoggedIn"] = true;
                Response.Redirect("Admin_pt1.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
            }
        }
    }
}
