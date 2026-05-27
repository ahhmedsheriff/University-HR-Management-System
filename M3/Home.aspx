<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="M3.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
<form id="form1" runat="server">

    <h2>Welcome to the University Employee Portal</h2>

    <!-- Employee ID Display -->
    <p>
        Logged in as Employee: 
        <asp:Label ID="empIdLabel" runat="server" CssClass="emp-id"></asp:Label>
    </p>

    <div class="actions-list">

        <p>
            <asp:Button ID="btnPerformance" runat="server" 
                        Text="View My Performance"
                        CssClass="dashboard-button" 
                        OnClick="btnPerformance_Click" />
        </p>

        <p>
            <asp:Button ID="btnAttendance" runat="server" 
                        Text="View My Attendance (Current Month)" 
                        CssClass="dashboard-button" 
                        OnClick="btnAttendance_Click" />
        </p>

        <p>
            <asp:Button ID="btnPayroll" runat="server"
                        Text="View Last Month Payroll"
                        CssClass="dashboard-button"
                        OnClick="btnPayroll_Click" />
        </p>

        <p>
            <asp:Button ID="btnDeductions" runat="server"
                        Text="View Attendance Deductions"
                        CssClass="dashboard-button"
                        OnClick="btnDeductions_Click" />
        </p>

        <!-- 🔁 changed: instead of “Apply for Annual Leave”, we go to Leaves page -->
        <p>
            <asp:Button ID="btnSubmitLeave" runat="server"
                        Text="Submit Leave (Annual / Accidental / Medical / Unpaid / Compensation)"
                        CssClass="dashboard-button"
                        OnClick="btnSubmitLeave_Click" />
        </p>

        <p>
            <asp:Button ID="btnLeaveStatus" runat="server"
                        Text="View Status of My Leaves"
                        CssClass="dashboard-button"
                        OnClick="btnLeaveStatus_Click" />
        </p>

        <!-- optional: upperboard / dean actions from part 2 -->
        <p>
            <asp:Button ID="btnUpperboardDean" runat="server"
                        Text="Upperboard / Dean Actions"
                        CssClass="dashboard-button"
                        OnClick="btnUpperboardDean_Click" />
        </p>

        <p>
            <asp:Button ID="btnLogout" runat="server"
                        Text="Logout"
                        CssClass="dashboard-button logout-btn"
                        OnClick="btnLogout_Click" />
        </p>

    </div>

</form>
</body>
</html>
