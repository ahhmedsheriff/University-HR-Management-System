<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRDashboard.aspx.cs" Inherits="M3.HRDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Dashboard</title>
    <link href="HR.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="main-bg">
            <div class="page-overlay">
                <div class="home-card fade-in" style="max-width: 650px; text-align: left;">
                    <h1 class="home-title">HR Employee Dashboard</h1>
                    <asp:Label ID="lblHRWelcome" runat="server" CssClass="home-subtitle" />

                    <hr />

                    <!-- 2. Approve/reject annual/accidental leaves -->
                    <h3 style="color: #e5e7eb;">1. Approve/Reject Annual & Accidental Leaves</h3>
                    <p class="home-subtitle">Use HR_approval_an_acc (@request_ID, @HR_ID)</p>
                    <label>Leave Request ID:</label><br />
                    <asp:TextBox ID="txtAnnualAccRequestID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnApproveAnnualAcc" runat="server"
                        Text="Process Annual/Accidental Leave"
                        CssClass="role-button hr-btn"
                        OnClick="btnApproveAnnualAcc_Click" />
                    <br />
                    <asp:Label ID="lblAnnualAccMsg" runat="server" ForeColor="LightGreen" />
                    <hr />
                    
                    <!-- 3. Approve/reject unpaid leaves -->
                    <h3 style="color: #e5e7eb;">2. Approve/Reject Unpaid Leaves</h3>
                    <p class="home-subtitle">HR_approval_Unpaid (@request_ID, @HR_ID)</p>
                    <label>Leave Request ID:</label><br />
                    <asp:TextBox ID="txtUnpaidRequestID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnApproveUnpaid" runat="server"
                        Text="Process Unpaid Leave"
                        CssClass="role-button hr-btn"
                        OnClick="btnApproveUnpaid_Click" />
                    <br />
                    <asp:Label ID="lblUnpaidMsg" runat="server" ForeColor="LightGreen" />
                    <hr />

                    <!-- 4. Approve/reject compensation leaves -->
                    <h3 style="color: #e5e7eb;">3. Approve/Reject Compensation Leaves</h3>
                    <p class="home-subtitle">HR_approval_comp (@request_ID, @HR_ID)</p>
                    <label>Leave Request ID:</label><br />
                    <asp:TextBox ID="txtCompRequestID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnApproveComp" runat="server"
                        Text="Process Compensation Leave"
                        CssClass="role-button hr-btn"
                        OnClick="btnApproveComp_Click" />
                    <br />
                    <asp:Label ID="lblCompMsg" runat="server" ForeColor="LightGreen" />
                    <hr />

                    <!-- 5. Deduction missing hours -->
                    <h3 style="color: #e5e7eb;">4. Add Deduction - Missing Hours</h3>
                    <p class="home-subtitle">Deduction_hours (@employee_ID)</p>
                    <label>Employee ID:</label><br />
                    <asp:TextBox ID="txtHoursEmpID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnDeductHours" runat="server"
                        Text="Add Missing Hours Deduction"
                        CssClass="role-button admin-btn"
                        OnClick="btnDeductHours_Click" />
                    <br />
                    <asp:Label ID="lblHoursMsg" runat="server" ForeColor="LightGreen" />
                    <hr />

                    <!-- 6. Deduction missing days -->
                    <h3 style="color: #e5e7eb;">5. Add Deduction - Missing Days</h3>
                    <p class="home-subtitle">Deduction_days (@employee_ID)</p>
                    <label>Employee ID:</label><br />
                    <asp:TextBox ID="txtDaysEmpID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnDeductDays" runat="server"
                        Text="Add Missing Days Deduction"
                        CssClass="role-button admin-btn"
                        OnClick="btnDeductDays_Click" />
                    <br />
                    <asp:Label ID="lblDaysMsg" runat="server" ForeColor="LightGreen" />
                    <hr />

                    <!-- 7. Deduction unpaid leave (optional, if you keep it) -->
                    <h3 style="color: #e5e7eb;">6. Add Deduction - Unpaid Leave</h3>
                    <p class="home-subtitle">Deduction_unpaid (@employee_ID)</p>
                    <label>Employee ID:</label><br />
                    <asp:TextBox ID="txtUnpaidEmpID" runat="server" CssClass="text-input" />
                    <br />
                    <br />
                    <asp:Button ID="btnDeductUnpaid" runat="server"
                        Text="Add Unpaid Leave Deduction"
                        CssClass="role-button admin-btn"
                        OnClick="btnDeductUnpaid_Click" />
                    <br />
                    <asp:Label ID="lblUnpaidDedMsg" runat="server" ForeColor="LightGreen" />
                    <hr />

                    <!-- 8. Generate monthly payroll -->
                    <h3 style="color: #e5e7eb;">7. Generate Monthly Payroll</h3>
                    <p class="home-subtitle">Add_Payroll (@employee_ID, @from, @to)</p>
                    <label>Employee ID:</label><br />
                    <asp:TextBox ID="txtPayrollEmpID" runat="server" CssClass="text-input" />
                    <br />
                    <label>From Date (yyyy-mm-dd):</label><br />
                    <asp:TextBox ID="txtPayrollFrom" runat="server" TextMode="Date" CssClass="text-input" />


                    <br />
                    <label>To Date (yyyy-mm-dd):</label><br />
                    <asp:TextBox ID="txtPayrollTo" runat="server" TextMode="Date" CssClass="text-input" />

                    <br />
                    <br />
                    <asp:Button ID="btnGeneratePayroll" runat="server"
                        Text="Generate Payroll"
                        CssClass="role-button emp-btn"
                        OnClick="btnGeneratePayroll_Click" />
                    <br />
                    <asp:Label ID="lblPayrollMsg" runat="server" ForeColor="LightGreen" />

                    <br />
                    <br />
                    <asp:HyperLink ID="lnkLogout" runat="server" NavigateUrl="MainHome.aspx" CssClass="home-footer">
                        &larr; Logout / Back to Home
                    </asp:HyperLink>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
