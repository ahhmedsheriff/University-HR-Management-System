<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="M3.Attendance" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>My Attendance</title>
    <link rel="stylesheet" href="Styles/style.css" />
</head>

<body>
<form id="form1" runat="server">

    <h2>My Attendance</h2>
    <p>
    <asp:Label ID="errorMsg" runat="server" 
               Text="" 
               ForeColor="Red" 
               Visible="false">
    </asp:Label>
</p>

    <asp:Button ID="btnLoadAttendance" runat="server"
        CssClass="dashboard-button"
        Text="Load Attendance" OnClick="btnLoadAttendance_Click" />

    <asp:GridView ID="attendanceGrid" runat="server" CssClass="gridview"></asp:GridView>
        <asp:Button ID="btnBackHome" runat="server"
CssClass="dashboard-button"
Text="Back to Home"
OnClick="btnBackHome_Click" />
</form>
</body>
</html>
