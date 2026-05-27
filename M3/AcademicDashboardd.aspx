<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademicDashboardd.aspx.cs" Inherits="M3.AcademicDashboardd" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Dashboard</title>
    <style>
        /* Simple clean styles for cards + collapse */
        .card { border: 1px solid #ccc; border-radius: 6px; margin: 12px 0; padding: 0; box-shadow: 0 1px 2px rgba(0,0,0,0.05); }
        .card-header { padding: 12px 16px; cursor: pointer; background: #f7f7f7; font-weight: bold; }
        .card-body { padding: 12px 16px; display: none; }
        .form-row { margin-bottom: 8px; }
        .form-label { display:inline-block; width:180px; font-weight:600; }
        .small-note { font-size: 0.9em; color:#666; margin-left: 6px; }
        .btn { padding:6px 12px; border-radius:4px; border:1px solid #888; background:#eee; cursor:pointer; }
        .btn-primary { background:#2d89ef; color:white; border-color:#1b62c9; }
        .success { color: green; font-weight:600; }
        .error { color: darkred; font-weight:600; }
        .section-help { font-size:0.9em; color:#555; margin-bottom:8px; }
    </style>
    <script type="text/javascript">
        function toggleCardBody(id) {
            var el = document.getElementById(id);
            if (!el) return;
            el.style.display = (el.style.display === 'block') ? 'none' : 'block';
        }
        window.onload = function () {
            // Optionally open first section
            var first = document.getElementById('cardBodyAccidental');
            if (first) first.style.display = 'block';
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="max-width:900px;margin:20px auto;font-family:Segoe UI, Arial;">
        <h2>Academic Dashboard — Part 2 (All features)</h2>
        <asp:Label ID="lblGlobalMessage" runat="server" Text="" EnableViewState="false"></asp:Label>

        <!-- Accidental Leave -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyAccidental')">Apply Accidental Leave</div>
            <div id="cardBodyAccidental" class="card-body">
                <div class="section-help">Accidental leave: 1 day only. Must be requested within 48 hours (DB checks).</div>
                <div class="form-row">
                    <span class="form-label">Employee ID:</span>
                    <asp:TextBox ID="txtAccEmpID" runat="server" Width="160px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Date (the accidental day):</span>
                    <asp:TextBox ID="txtAccDate" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnSubmitAccidental" runat="server" CssClass="btn btn-primary" Text="Submit Accidental Leave" OnClick="btnSubmitAccidental_Click" />
                    <asp:Label ID="lblAccidentalMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Medical Leave -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyMedical')">Apply Medical Leave</div>
            <div id="cardBodyMedical" class="card-body">
                <div class="section-help">Medical leave requires a medical document (description saved in DB).</div>
                <div class="form-row">
                    <span class="form-label">Employee ID:</span>
                    <asp:TextBox ID="txtMedEmpID" runat="server" Width="160px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Start Date:</span>
                    <asp:TextBox ID="txtMedStart" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">End Date:</span>
                    <asp:TextBox ID="txtMedEnd" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Type:</span>
                    <asp:DropDownList ID="ddlMedType" runat="server" Width="170px">
                        <asp:ListItem Text="sick" Value="sick" />
                        <asp:ListItem Text="maternity" Value="maternity" />
                    </asp:DropDownList>
                </div>
                <div class="form-row">
                    <span class="form-label">Insurance?</span>
                    <asp:CheckBox ID="chkInsurance" runat="server" />
                </div>
                <div class="form-row">
                    <span class="form-label">Disability Details:</span>
                    <asp:TextBox ID="txtDisability" runat="server" Width="300px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Document Description:</span>
                    <asp:TextBox ID="txtMedDocDesc" runat="server" Width="300px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnSubmitMedical" runat="server" CssClass="btn btn-primary" Text="Submit Medical Leave" OnClick="btnSubmitMedical_Click" />
                    <asp:Label ID="lblMedicalMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Unpaid Leave -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyUnpaid')">Apply Unpaid Leave</div>
            <div id="cardBodyUnpaid" class="card-body">
                <div class="section-help">Unpaid leave requires a memo description. DB enforces one unpaid per year + duration rules.</div>
                <div class="form-row">
                    <span class="form-label">Employee ID:</span>
                    <asp:TextBox ID="txtUnpaidEmpID" runat="server" Width="160px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Start Date:</span>
                    <asp:TextBox ID="txtUnpaidStart" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">End Date:</span>
                    <asp:TextBox ID="txtUnpaidEnd" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Memo Description:</span>
                    <asp:TextBox ID="txtUnpaidDesc" runat="server" Width="300px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnSubmitUnpaid" runat="server" CssClass="btn btn-primary" Text="Submit Unpaid Leave" OnClick="btnSubmitUnpaid_Click" />
                    <asp:Label ID="lblUnpaidMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Compensation Leave -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyComp')">Apply Compensation Leave</div>
            <div id="cardBodyComp" class="card-body">
                <div class="section-help">Compensation leave must reference the original day off and a replacement employee.</div>
                <div class="form-row">
                    <span class="form-label">Employee ID:</span>
                    <asp:TextBox ID="txtCompEmpID" runat="server" Width="160px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Compensation Date (the day off you take):</span>
                    <asp:TextBox ID="txtCompDate" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Original Workday (when you worked):</span>
                    <asp:TextBox ID="txtOriginalWorkday" runat="server" Width="160px" placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Replacement Employee ID:</span>
                    <asp:TextBox ID="txtCompReplacement" runat="server" Width="160px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Reason:</span>
                    <asp:TextBox ID="txtCompReason" runat="server" Width="400px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnSubmitComp" runat="server" CssClass="btn btn-primary" Text="Submit Compensation Leave" OnClick="btnSubmitCompensation_Click" />
                    <asp:Label ID="lblCompMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Approve Annual Leave (Upperboard) -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyApproveAnnual')">Approve/Reject Annual Leave (Upperboard)</div>
            <div id="cardBodyApproveAnnual" class="card-body">
                <div class="section-help">For Upperboard: provide leave request ID and replacement ID (if needed).</div>
                <div class="form-row">
                    <span class="form-label">Request ID:</span>
                    <asp:TextBox ID="txtApproveAnnualReqID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Your Upperboard Employee ID:</span>
                    <asp:TextBox ID="txtApproveAnnualUpperID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Replacement Employee ID (if applicable):</span>
                    <asp:TextBox ID="txtApproveAnnualReplacementID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnApproveAnnual" runat="server" CssClass="btn btn-primary" Text="Approve Annual (mark approved)" OnClick="btnApproveAnnual_Click" />
                    <asp:Label ID="lblApproveAnnualMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Approve Unpaid Leave (Upperboard) -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyApproveUnpaid')">Approve/Reject Unpaid Leave (Upperboard)</div>
            <div id="cardBodyApproveUnpaid" class="card-body">
                <div class="section-help">Upperboard approves unpaid leaves. DB checks memo and other conditions.</div>
                <div class="form-row">
                    <span class="form-label">Request ID:</span>
                    <asp:TextBox ID="txtApproveUnpaidReqID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Your Upperboard Employee ID:</span>
                    <asp:TextBox ID="txtApproveUnpaidUpperID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnApproveUnpaid" runat="server" CssClass="btn btn-primary" Text="Approve Unpaid" OnClick="btnApproveUnpaid_Click" />
                    <asp:Label ID="lblApproveUnpaidMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

        <!-- Evaluate Employee (Dean) -->
        <div class="card">
            <div class="card-header" onclick="toggleCardBody('cardBodyEvaluate')">Evaluate Employee (Dean)</div>
            <div id="cardBodyEvaluate" class="card-body">
                <div class="section-help">Dean can evaluate employees in their department. Rating 1-5 (DB enforces range).</div>
                <div class="form-row">
                    <span class="form-label">Dean Employee ID:</span>
                    <asp:TextBox ID="txtDeanID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Employee To Evaluate (ID):</span>
                    <asp:TextBox ID="txtEvalEmpID" runat="server" Width="140px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Rating (1-5):</span>
                    <asp:TextBox ID="txtEvalRating" runat="server" Width="80px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Comment:</span>
                    <asp:TextBox ID="txtEvalComment" runat="server" Width="400px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <span class="form-label">Semester (e.g. W25):</span>
                    <asp:TextBox ID="txtEvalSemester" runat="server" Width="80px"></asp:TextBox>
                </div>
                <div class="form-row">
                    <asp:Button ID="btnEvaluate" runat="server" CssClass="btn btn-primary" Text="Submit Evaluation" OnClick="btnEvaluate_Click" />
                    <asp:Label ID="lblEvalMsg" runat="server" Text="" />
                </div>
            </div>
        </div>

    </div>
    </form>
</body>
</html>