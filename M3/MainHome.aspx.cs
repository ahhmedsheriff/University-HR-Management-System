using System;
using System.Web.UI;

namespace M3
{
    public partial class MainHome : Page
    {
        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }

        protected void btnHR_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRLogin.aspx");
        }

        protected void btnEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
