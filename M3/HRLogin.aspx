<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRLogin.aspx.cs" Inherits="M3.HRLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Login</title>
    <link href="HR.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="hr-bg">
            <div class="page-overlay">
                <div class="home-card fade-in">
                    <h1 class="home-title">HR Employee Login</h1>
                    <p class="home-subtitle">
                        Please enter your ID and password
                    </p>

                    <div style="text-align:left; margin-bottom:10px;">
                        <label for="txtHRID">HR Employee ID</label><br />
                        <asp:TextBox ID="txtHRID" runat="server" CssClass="text-input" />
                    </div>

                    <div style="text-align:left; margin-bottom:16px;">
                        <label for="txtHRPassword">Password</label><br />
                        <asp:TextBox ID="txtHRPassword" runat="server" TextMode="Password" CssClass="text-input" />
                    </div>

                    <asp:Button ID="btnHRLogin" runat="server"
                        Text="Login"
                        CssClass="role-button hr-btn"
                        OnClick="btnHRLogin_Click" />

                    <br /><br />
                    <asp:Label ID="lblHRError" runat="server" ForeColor="Red" Visible="false" />

                    <br /><br />
                    <asp:HyperLink ID="lnkBackHome" runat="server" NavigateUrl="MainHome.aspx" CssClass="home-footer">
                        &larr; Back to Home
                    </asp:HyperLink>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
