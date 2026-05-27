<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M3.Login" %>

<!DOCTYPE html>

<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        Log In<br />
        Username:
        <p>
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
</p>
        <p>
            Password:</p>
        <p>
            <asp:TextBox ID="password" TextMode="Password" runat="server"></asp:TextBox>

        </p>
        <p>
            <asp:Button ID="signin" runat="server" Text="Login"
    OnClick="LoginButton_Click" />
        </p>
        <p>
            &nbsp;</p>
         <p>
        <asp:Label ID="loginError" runat="server" 
                   Text="" 
                   ForeColor="Red" 
                   Visible="false">
        </asp:Label>
    </p>

            <asp:Button ID="btnBackHome" runat="server"
CssClass="dashboard-button"
Text="Back to Home"
OnClick="btnBack_Click" />

    </form>


</body>
</html>
