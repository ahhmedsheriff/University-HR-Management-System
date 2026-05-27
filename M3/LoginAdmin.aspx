<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="M3.LoginAdmin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin Login</title>
    <link rel="stylesheet" href="LoginAdmin.css" />
</head>


<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Admin Login</h2>

            <asp:TextBox ID="txtUsername" runat="server" CssClass="txt" placeholder="Admin ID"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txt" TextMode="Password" placeholder="Password"></asp:TextBox>

            <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="Login" OnClick="btnLogin_Click" />

            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
