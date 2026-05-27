<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leaves.aspx.cs" Inherits="M3.Leaves" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Submit Leaves</title>
    <link rel="stylesheet" href="style.css" />
    <script type="text/javascript">
        function toggleCardBody(id) {
            var el = document.getElementById(id);
            if (!el) return;
            el.style.display = (el.style.display === 'block') ? 'none' : 'block';
        }
        window.onload = function () {
            var first = document.getElementById('cardBodyAnnual');
            if (first) first.style.display = 'block';
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="leaves-container">
            <h2>Submit Leave Requests</h2>
            <p>Logged in as Employee: 
                <asp:Label ID="lblEmpId" runat="server"></asp:Label>
            </p>

            <!-- ANNUAL LEAVE -->
            <div class="card">
                <div class="card-header" onclick="toggleCardBody('cardBodyAnnual')">
                    Annual Leave
                </div>
                <div id="cardBodyAnnual" class="card-body">
                    <div class="small-note">
                        Annual leave requires a replacement employee and a date range.
                    </div>

                    <div class="form-row">
                        <span class="form-label">Replacement Employee ID:</span>
                        <asp:TextBox ID="txtAnnualReplacementID" runat="server" Width="160px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">Start Date:</span>
                        <asp:TextBox ID="txtAnnualStart" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">End Date:</span>
                        <asp:TextBox ID="txtAnnualEnd" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <asp:Button ID="btnSubmitAnnual" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Submit Annual Leave"
                                    OnClick="btnSubmitAnnual_Click" />
                        <asp:Label ID="lblAnnualMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <!-- ACCIDENTAL LEAVE -->
            <div class="card">
                <div class="card-header" onclick="toggleCardBody('cardBodyAccidental')">
                    Accidental Leave
                </div>
                <div id="cardBodyAccidental" class="card-body">
                    <div class="small-note">
                        Accidental leave: 1 day only. Must be requested within 48 hours (DB checks).
                    </div>

                    <div class="form-row">
                        <span class="form-label">Date (accidental day):</span>
                        <asp:TextBox ID="txtAccDate" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <asp:Button ID="btnSubmitAccidental" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Submit Accidental Leave"
                                    OnClick="btnSubmitAccidental_Click" />
                        <asp:Label ID="lblAccidentalMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <!-- MEDICAL LEAVE -->
            <div class="card">
                <div class="card-header" onclick="toggleCardBody('cardBodyMedical')">
                    Medical Leave
                </div>
                <div id="cardBodyMedical" class="card-body">
                    <div class="small-note">
                        Medical leave requires a medical document description.
                    </div>

                    <div class="form-row">
                        <span class="form-label">Start Date:</span>
                        <asp:TextBox ID="txtMedStart" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">End Date:</span>
                        <asp:TextBox ID="txtMedEnd" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">Type:</span>
                        <asp:DropDownList ID="ddlMedType" runat="server" Width="180px">
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
                        <asp:Button ID="btnSubmitMedical" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Submit Medical Leave"
                                    OnClick="btnSubmitMedical_Click" />
                        <asp:Label ID="lblMedicalMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <!-- UNPAID LEAVE -->
            <div class="card">
                <div class="card-header" onclick="toggleCardBody('cardBodyUnpaid')">
                    Unpaid Leave
                </div>
                <div id="cardBodyUnpaid" class="card-body">
                    <div class="small-note">
                        DB enforces one unpaid per year and duration rules.
                    </div>

                    <div class="form-row">
                        <span class="form-label">Start Date:</span>
                        <asp:TextBox ID="txtUnpaidStart" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">End Date:</span>
                        <asp:TextBox ID="txtUnpaidEnd" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">Memo Description:</span>
                        <asp:TextBox ID="txtUnpaidDesc" runat="server" Width="300px"></asp:TextBox>
                    </div>

                    <div class="form-row">
                        <asp:Button ID="btnSubmitUnpaid" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Submit Unpaid Leave"
                                    OnClick="btnSubmitUnpaid_Click" />
                        <asp:Label ID="lblUnpaidMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <!-- COMPENSATION LEAVE -->
            <div class="card">
                <div class="card-header" onclick="toggleCardBody('cardBodyComp')">
                    Compensation Leave
                </div>
                <div id="cardBodyComp" class="card-body">
                    <div class="small-note">
                        Requires the original workday and a replacement employee.
                    </div>

                    <div class="form-row">
                        <span class="form-label">Compensation Date (day off you take):</span>
                        <asp:TextBox ID="txtCompDate" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <span class="form-label">Original Workday (when you worked):</span>
                        <asp:TextBox ID="txtOriginalWorkday" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
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
                        <asp:Button ID="btnSubmitComp" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Submit Compensation Leave"
                                    OnClick="btnSubmitCompensation_Click" />
                        <asp:Label ID="lblCompMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div style="margin-top:15px;">
                <asp:Button ID="btnBackHome" runat="server"
                            CssClass="btn"
                            Text="Back to Home"
                            OnClick="btnBackHome_Click" />
            </div>

        </div>
    </form>
</body>
</html>
