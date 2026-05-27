<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveStatus.aspx.cs" Inherits="M3.LeaveStatus" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Leave Status</title>
    <link rel="stylesheet" href="Styles/style.css" />
</head>

<body>
<form id="form1" runat="server">

    <h2>My Leave Status</h2>
    <asp:Label ID="errorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>

    <asp:Button ID="btnLoad" runat="server"
        CssClass="dashboard-button"
        Text="Load Status" OnClick="btnLoad_Click" />

    <asp:GridView ID="statusGrid" runat="server" CssClass="gridview"></asp:GridView>
    <asp:Button ID="btnBackHome" runat="server"
CssClass="dashboard-button"
Text="Back to Home"
OnClick="btnBackHome_Click" />
</form>
</body>
</html>
