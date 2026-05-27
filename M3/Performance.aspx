<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Performance.aspx.cs" Inherits="M3.Performance" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Performance</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
<form id="form1" runat="server">

    <h2>View My Performance</h2>

    <label>Semester</label>
   <h2>My Performance</h2>
<p>
    <asp:Label ID="Label1" runat="server" 
               Text="" 
               ForeColor="Red" 
               Visible="false">
    </asp:Label>
</p>

<label>Enter Semester (e.g., W24, S24):</label>
<asp:TextBox ID="txtSemester" runat="server"></asp:TextBox>

<asp:Button ID="Button1" runat="server"
    CssClass="dashboard-button"
    Text="Load Performance" OnClick="btnLoad_Click" />

<asp:GridView ID="GridView1" runat="server" CssClass="gridview"></asp:GridView>

<asp:Button ID="Button2" runat="server" Text="Back to Home" OnClick="btnBackHome_Click" CssClass="dashboard-button"/>


   

    <asp:Label ID="errorMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>

    <asp:GridView ID="performanceGrid" runat="server" CssClass="gridview"></asp:GridView>

</form>
</body>
</html>
