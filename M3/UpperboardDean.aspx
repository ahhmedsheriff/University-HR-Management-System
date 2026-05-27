<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpperboardDean.aspx.cs" Inherits="M3.UpperboardDean" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upperboard / Dean Dashboard</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">

            <div class="card">
                <h2>Upperboard / Dean Actions</h2>
                <p class="text-muted">
                    Here you can approve unpaid/annual leaves and evaluate employees.
                </p>
            </div>

            <!-- 5) Approve / Reject Unpaid Leaves -->
            <div class="card">
                <div class="card-header">Approve / Reject Unpaid Leave</div>

                <div class="form-row">
                    <span class="form-label">Request ID:</span>
                    <asp:TextBox ID="txtApproveUnpaidReqID" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Your Employee ID (Dean / VC / President):</span>
                    <asp:TextBox ID="txtApproveUnpaidUpperID" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <asp:Button ID="btnApproveUnpaid" runat="server"
                                Text="Process Unpaid Leave"
                                CssClass="dashboard-button"
                                OnClick="btnApproveUnpaid_Click" />
                    <asp:Label ID="lblApproveUnpaidMsg" runat="server"></asp:Label>
                </div>
            </div>

            <!-- 6) Approve / Reject Annual Leaves -->
            <div class="card">
                <div class="card-header">Approve / Reject Annual Leave</div>

                <div class="form-row">
                    <span class="form-label">Request ID:</span>
                    <asp:TextBox ID="txtApproveAnnualReqID" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Your Employee ID (Upperboard):</span>
                    <asp:TextBox ID="txtApproveAnnualUpperID" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Replacement Employee ID (optional):</span>
                    <asp:TextBox ID="txtApproveAnnualReplacementID" runat="server"></asp:TextBox>
                    <span class="small-note">(leave empty if no replacement)</span>
                </div>

                <div class="form-row">
                    <asp:Button ID="btnApproveAnnual" runat="server"
                                Text="Process Annual Leave"
                                CssClass="dashboard-button"
                                OnClick="btnApproveAnnual_Click" />
                    <asp:Label ID="lblApproveAnnualMsg" runat="server"></asp:Label>
                </div>
            </div>

            <!-- 7) Dean Evaluation -->
            <div class="card">
                <div class="card-header">Evaluate Employee (Dean)</div>

                <div class="form-row">
                    <span class="form-label">Employee To Evaluate (ID):</span>
                    <asp:TextBox ID="txtEvalEmpID" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Rating (1–5):</span>
                    <asp:TextBox ID="txtEvalRating" runat="server"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Comment:</span>
                    <asp:TextBox ID="txtEvalComment" runat="server" Width="350px"></asp:TextBox>
                </div>

                <div class="form-row">
                    <span class="form-label">Semester (e.g. W25):</span>
                    <asp:TextBox ID="txtEvalSemester" runat="server" Width="80px"></asp:TextBox>
                </div>

                <div class="form-row">
                    <asp:Button ID="btnEvaluate" runat="server"
                                Text="Submit Evaluation"
                                CssClass="dashboard-button"
                                OnClick="btnEvaluate_Click" />
                    <asp:Label ID="lblEvalMsg" runat="server"></asp:Label>
                </div>
            </div>

            <!-- Back -->
            <div class="card">
                <asp:Button ID="btnBackHome" runat="server"
                            Text="Back to Employee Home"
                            CssClass="dashboard-button logout-btn"
                            OnClick="btnBackHome_Click" />
            </div>

        </div>
    </form>
</body>
</html>
