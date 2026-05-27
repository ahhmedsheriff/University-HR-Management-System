<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deductions.aspx.cs" Inherits="M3.Deductions" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Attendance Deductions</title>
    <link rel="stylesheet" href="Styles/style.css" />
</head>

<body>
<form id="form1" runat="server">

    <h2>Attendance Deductions</h2>
    <p>
    <asp:Label ID="errorMsg" runat="server" 
               Text="" 
               ForeColor="Red" 
               Visible="false">
    </asp:Label>
</p>

    <label>Select Month:</label>
<asp:DropDownList ID="ddlMonth" runat="server">
    <asp:ListItem Text="January" Value="1" />
    <asp:ListItem Text="February" Value="2" />
    <asp:ListItem Text="March" Value="3" />
    <asp:ListItem Text="April" Value="4" />
    <asp:ListItem Text="May" Value="5" />
    <asp:ListItem Text="June" Value="6" />
    <asp:ListItem Text="July" Value="7" />
    <asp:ListItem Text="August" Value="8" />
    <asp:ListItem Text="September" Value="9" />
    <asp:ListItem Text="October" Value="10" />
    <asp:ListItem Text="November" Value="11" />
    <asp:ListItem Text="December" Value="12" />
</asp:DropDownList>


    <asp:Button ID="btnLoadDeductions" runat="server"
        CssClass="dashboard-button"
        Text="Load Deductions" OnClick="btnLoadDeductions_Click" />

    <asp:GridView ID="deductionsGrid" runat="server" CssClass="gridview"></asp:GridView>
    
    <asp:Button ID="btnBackHome" runat="server"
    CssClass="dashboard-button"
    Text="Back to Home"
    OnClick="btnBackHome_Click" />

</form>
</body>
</html>
