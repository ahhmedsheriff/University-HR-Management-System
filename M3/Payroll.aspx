<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payroll.aspx.cs" Inherits="M3.Payroll" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Payroll</title>
    <link rel="stylesheet" href="style.css" />
</head>

<body>
<form id="form1" runat="server">
    <p>
    <asp:Label ID="errorMsg" 
               runat="server" 
               ForeColor="Red" 
               Visible="false"></asp:Label>
</p>

    <h2>Last Month Payroll</h2>

    <asp:Button ID="btnLoadPayroll" runat="server"
        CssClass="dashboard-button"
        Text="Load Payroll" OnClick="btnLoadPayroll_Click" />

    <asp:GridView ID="payrollGrid" runat="server" CssClass="gridview"></asp:GridView>
    <asp:Button ID="btnBackHome" runat="server"
CssClass="dashboard-button"
Text="Back to Home"
OnClick="btnBackHome_Click" />
</form>
</body>
</html>
