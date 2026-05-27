<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_pt1.aspx.cs" Inherits="M3.Admin_pt1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <link rel="stylesheet" href="Admin.css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome, Admin!</h1>

            <div class="dashboard-vertical">

                <!-- ========= PART 1 FEATURES ========= -->

                <!-- All Employee Profiles -->
                <div class="card">
                    <h3>All Employee Profiles</h3>
                    <asp:Button ID="btnShowProfiles" runat="server" Text="Show Profiles" CssClass="btn" OnClick="btnShowProfiles_Click" />
                    <asp:Button ID="btnHideProfiles" runat="server" Text="Hide Profiles" CssClass="btn" OnClick="btnHideProfiles_Click" />
                    <div class="grid-container">
                        <asp:GridView ID="gvProfiles" runat="server" CssClass="grid" AutoGenerateColumns="True" Visible="false"></asp:GridView>
                    </div>
                </div>

                <!-- Employee Count -->
                <div class="card">
                    <h3>Employee Count Per Department</h3>
                    <asp:Button ID="btnShowEmpCount" runat="server" Text="Show Count" CssClass="btn" OnClick="btnShowEmpCount_Click" />
                    <asp:Button ID="btnHideEmpCount" runat="server" Text="Hide Count" CssClass="btn" OnClick="btnHideEmpCount_Click" />
                    <div class="grid-container" style="max-height:350px;">
                        <asp:GridView ID="gvEmpCount" runat="server" CssClass="grid" AutoGenerateColumns="True" Visible="false"></asp:GridView>
                    </div>
                </div>

                <!-- Rejected Medicals -->
                <div class="card">
                    <h3>Rejected Medical Leaves</h3>
                    <asp:Button ID="btnShowRejected" runat="server" Text="Show Rejected" CssClass="btn" OnClick="btnShowRejected_Click" />
                    <asp:Button ID="btnHideRejected" runat="server" Text="Hide Rejected" CssClass="btn" OnClick="btnHideRejected_Click" />
                    <div class="grid-container" style="max-height:350px;">
                        <asp:GridView ID="gvRejectedMed" runat="server" CssClass="grid" AutoGenerateColumns="True" Visible="false"></asp:GridView>
                    </div>
                </div>

                <!-- Update Attendance -->
                <div class="card">
                    <h3>Update Attendance for Today</h3>
                    <asp:TextBox ID="txtAttEmpID" runat="server" Placeholder="Employee ID"></asp:TextBox>
                    <asp:TextBox ID="txtCheckIn" runat="server" Placeholder="Check-in (HH:MM)"></asp:TextBox>
                    <asp:TextBox ID="txtCheckOut" runat="server" Placeholder="Check-out (HH:MM)"></asp:TextBox>
                    <asp:Button ID="btnUpdateAtt" runat="server" Text="Update" CssClass="btn" OnClick="btnUpdateAtt_Click" />
                    <asp:Label ID="lblUpdateAtt" runat="server" CssClass="lbl"></asp:Label>
                </div>

                <!-- Add Holiday -->
                <div class="card">
                    <h3>Add Holiday</h3>
                    <asp:TextBox ID="txtHolidayName" runat="server" Placeholder="Holiday Name"></asp:TextBox>
                    <asp:TextBox ID="txtHolidayFrom" runat="server" Placeholder="From Date (yyyy-mm-dd)"></asp:TextBox>
                    <asp:TextBox ID="txtHolidayTo" runat="server" Placeholder="To Date (yyyy-mm-dd)"></asp:TextBox>
                    <asp:Button ID="btnAddHoliday" runat="server" Text="Add" CssClass="btn" OnClick="btnAddHoliday_Click" />
                    <asp:Label ID="lblHoliday" runat="server" CssClass="lbl"></asp:Label>
                </div>

                <!-- Initiate Attendance -->
                <div class="card">
                    <h3>Initiate Attendance for Today</h3>
                    <asp:Button ID="btnInitAttendance" runat="server" Text="Initiate" CssClass="btn" OnClick="btnInitAttendance_Click" />
                    <asp:Label ID="lblInitAtt" runat="server" CssClass="lbl"></asp:Label>
                </div>

                <!-- Remove Deductions of Resigned Employees -->
                <div class="card">
                    <h3>Remove Deductions of Resigned Employees</h3>
                    <asp:Button ID="btnRemoveDed" runat="server" Text="Remove" CssClass="btn" OnClick="btnRemoveDed_Click" />
                    <asp:Label ID="lblRemoveDed" runat="server" CssClass="lbl"></asp:Label>
                </div>

                <!-- ========= PART 2 FEATURES ========= -->

                <!-- Yesterday Attendance -->
                <div class="card">
                    <h3>Yesterday's Attendance (All Employees)</h3>
                    <asp:Button ID="btnFetchYesterdayAttendance" runat="server"
                        Text="Fetch Yesterday Attendance"
                        CssClass="btn"
                        OnClick="btnFetchYesterdayAttendance_Click" />
                    <asp:Label ID="lblYesterdayMsg" runat="server" CssClass="msg-error"></asp:Label>
                    <div class="grid-container">
                        <asp:GridView ID="gvYesterdayAttendance" runat="server" CssClass="grid" AutoGenerateColumns="true"></asp:GridView>
                    </div>
                </div>

                <!-- Winter Performance -->
                <div class="card">
                    <h3>Performance - All Winter Semesters</h3>
                    <asp:Button ID="btnFetchWinterPerformance" runat="server"
                        Text="Fetch Winter Performance"
                        CssClass="btn"
                        OnClick="btnFetchWinterPerformance_Click" />
                    <asp:Label ID="lblWinterMsg" runat="server" CssClass="msg-error"></asp:Label>
                    <div class="grid-container">
                        <asp:GridView ID="gvWinterPerformance" runat="server" CssClass="grid" AutoGenerateColumns="true"></asp:GridView>
                    </div>
                </div>

                <!-- Remove Attendance During Official Holidays -->
                <div class="card">
                    <h3>Remove Attendance During Official Holidays</h3>
                    <asp:Button ID="btnRemoveHolidayAttendance" runat="server"
                        Text="Remove Holiday Attendance"
                        CssClass="btn"
                        OnClick="btnRemoveHolidayAttendance_Click" />
                    <asp:Label ID="lblHolidayAttendanceMsg" runat="server" CssClass="msg-error"></asp:Label>
                </div>

                <!-- Remove Unattended Dayoff -->
                <div class="card">
                    <h3>Remove Unattended Dayoff (Current Month)</h3>
                    <asp:TextBox ID="txtEmpDayOff" runat="server" Placeholder="Employee ID"></asp:TextBox>
                    <asp:Button ID="btnRemoveDayOff" runat="server"
                        Text="Remove Unattended Dayoff"
                        CssClass="btn"
                        OnClick="btnRemoveDayOff_Click" />
                    <asp:Label ID="lblDayOffMsg" runat="server" CssClass="msg-error"></asp:Label>
                </div>

                <!-- Remove Approved Leaves from Attendance -->
                <div class="card">
                    <h3>Remove Approved Leave Days from Attendance</h3>
                    <asp:TextBox ID="txtEmpLeaves" runat="server" Placeholder="Employee ID"></asp:TextBox>
                    <asp:Button ID="btnRemoveApprovedLeaves" runat="server"
                        Text="Remove Approved Leaves from Attendance"
                        CssClass="btn"
                        OnClick="btnRemoveApprovedLeaves_Click" />
                    <asp:Label ID="lblLeavesMsg" runat="server" CssClass="msg-error"></asp:Label>
                </div>

                <!-- Replace Employee -->
                <div class="card">
                    <h3>Replace Employee (Emp1 → Emp2)</h3>
                    <asp:TextBox ID="txtEmp1" runat="server" Placeholder="Emp1_ID (being replaced)"></asp:TextBox>
                    <asp:TextBox ID="txtEmp2" runat="server" Placeholder="Emp2_ID (replacement)"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnReplaceEmployee" runat="server"
                        Text="Replace Employee"
                        CssClass="btn"
                        OnClick="btnReplaceEmployee_Click" />
                    <asp:Label ID="lblReplaceMsg" runat="server" CssClass="msg-error"></asp:Label>
                </div>

                <!-- Update Employment Status (One Employee) -->
            <div class="card">
             <h3>Update Employment Status for One Employee</h3>
            <asp:TextBox ID="txtUpdateStatusEmpID" runat="server" Placeholder="Employee ID"></asp:TextBox>
               <asp:Button ID="btnUpdateEmpStatus" runat="server"
                  Text="Update Status"
                  CssClass="btn"
                   OnClick="btnUpdateEmpStatus_Click" />
                 <asp:Label ID="lblEmpStatusMsg" runat="server" CssClass="lbl"></asp:Label>
                 </div>


                <!-- Logout -->
                <div class="card">
                    <h3>Logout</h3>
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn" OnClick="btnLogout_Click" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
