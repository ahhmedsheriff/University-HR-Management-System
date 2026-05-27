<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainHome.aspx.cs" Inherits="M3.MainHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>University HR System - Home</title>
    <!-- Link to your CSS file -->
    <link href="Site.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="home-bg">
            <div class="page-overlay">
                <div class="home-card fade-in">

                    <h1 class="home-title">University HR Management System</h1>
                    <p class="home-subtitle">
                        Please select your role to continue
                    </p>

                    <div class="role-buttons">

                        <asp:Button ID="btnAdmin" runat="server"
                            Text="Admin"
                            CssClass="role-button admin-btn"
                            OnClick="btnAdmin_Click" />

                        <asp:Button ID="btnHR" runat="server"
                            Text="HR Employee"
                            CssClass="role-button hr-btn"
                            OnClick="btnHR_Click" />

                        <asp:Button ID="btnEmployee" runat="server"
                            Text="Academic Employee"
                            CssClass="role-button emp-btn"
                            OnClick="btnEmployee_Click" />

         
                    <p class="home-footer">
                        <span class="auto-style1">GUC HR Project team</span>
                    </p>

         
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
