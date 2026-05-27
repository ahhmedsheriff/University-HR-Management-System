<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnnualLeave.aspx.cs" Inherits="M3.AnnualLeave" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Annual Leave Submission</title>
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body>
<form id="form1" runat="server">
    <h2>Submit Annual Leave</h2>

    <asp:Label ID="errorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <br /><br />

    <label>Start Date:</label>
    <asp:TextBox ID="txtStart" runat="server" ReadOnly="true"></asp:TextBox>
    <asp:Calendar ID="calendarStart" runat="server" OnSelectionChanged="calendarStart_SelectionChanged"></asp:Calendar>
    <br /><br />

    <label>End Date:</label>
    <asp:TextBox ID="txtEnd" runat="server" ReadOnly="true"></asp:TextBox>
    <asp:Calendar ID="calendarEnd" runat="server" OnSelectionChanged="calendarEnd_SelectionChanged"></asp:Calendar>
    <br /><br />

    <label>Replacement Employee:</label>
    <asp:DropDownList ID="ddlReplacement" runat="server"></asp:DropDownList>
    <br /><br />

    <label>Reason:</label>
    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Leave" OnClick="btnSubmit_Click" CssClass="dashboard-button" />
    <asp:Button ID="btnBackHome" runat="server" Text="Back to Home" OnClick="btnBackHome_Click" CssClass="dashboard-button" />
</form>
</body>
</html>
