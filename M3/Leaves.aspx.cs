using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class Leaves : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager
            .ConnectionStrings["University_HR_ManagementSystem"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employee_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblEmpId.Text = Session["employee_ID"].ToString();
            }
        }

        #region Helpers
        private (bool ok, string message) ExecuteProcNonQuery(string procName, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnStr))
                using (SqlCommand cmd = new SqlCommand(procName, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cn.Open();
                    cmd.CommandTimeout = 60;
                    cmd.ExecuteNonQuery();
                }
                return (true, "Success");
            }
            catch (SqlException sqlex)
            {
                return (false, "DB error: " + sqlex.Message);
            }
            catch (Exception ex)
            {
                return (false, "Error: " + ex.Message);
            }
        }

        private DateTime ParseDateOrThrow(string input, string fieldName)
        {
            if (!DateTime.TryParse(input, out DateTime dt))
                throw new ArgumentException($"Invalid date for {fieldName}.");
            return dt;
        }

        private int GetCurrentEmployeeId()
        {
            return Convert.ToInt32(Session["employee_ID"]);
        }

        private int ParseIntOrThrow(string input, string fieldName)
        {
            if (!int.TryParse(input, out int v))
                throw new ArgumentException($"Invalid integer for {fieldName}.");
            return v;
        }
        #endregion

        #region Annual Leave
        protected void btnSubmitAnnual_Click(object sender, EventArgs e)
        {
            lblAnnualMsg.Text = "";
            lblAnnualMsg.CssClass = "";

            try
            {
                int empId = GetCurrentEmployeeId();
                int replacementId = ParseIntOrThrow(txtAnnualReplacementID.Text.Trim(), "Replacement Employee ID");

                DateTime start = ParseDateOrThrow(txtAnnualStart.Text.Trim(), "Start Date");
                DateTime end = ParseDateOrThrow(txtAnnualEnd.Text.Trim(), "End Date");

                var p = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@replacement_emp", SqlDbType.Int){Value = replacementId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = start},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = end}
                };

                var result = ExecuteProcNonQuery("Submit_annual", p);
                if (result.ok)
                {
                    lblAnnualMsg.Text = "Annual leave submitted successfully.";
                    lblAnnualMsg.CssClass = "success";
                }
                else
                {
                    lblAnnualMsg.Text = result.message;
                    lblAnnualMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblAnnualMsg.Text = ex.Message;
                lblAnnualMsg.CssClass = "error";
            }
        }
        #endregion

        #region Accidental
        protected void btnSubmitAccidental_Click(object sender, EventArgs e)
        {
            lblAccidentalMsg.Text = "";
            lblAccidentalMsg.CssClass = "";

            try
            {
                int empId = GetCurrentEmployeeId();
                DateTime d = ParseDateOrThrow(txtAccDate.Text.Trim(), "Accidental Date");

                var p = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = d},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = d}
                };

                var result = ExecuteProcNonQuery("Submit_accidental", p);
                if (result.ok)
                {
                    lblAccidentalMsg.Text = "Accidental leave submitted successfully.";
                    lblAccidentalMsg.CssClass = "success";
                }
                else
                {
                    lblAccidentalMsg.Text = result.message;
                    lblAccidentalMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblAccidentalMsg.Text = ex.Message;
                lblAccidentalMsg.CssClass = "error";
            }
        }
        #endregion

        #region Medical
        protected void btnSubmitMedical_Click(object sender, EventArgs e)
        {
            lblMedicalMsg.Text = "";
            lblMedicalMsg.CssClass = "";

            try
            {
                int empId = GetCurrentEmployeeId();

                DateTime start = ParseDateOrThrow(txtMedStart.Text.Trim(), "Start Date");
                DateTime end = ParseDateOrThrow(txtMedEnd.Text.Trim(), "End Date");

                string type = ddlMedType.SelectedValue;
                bool insurance = chkInsurance.Checked;
                string disability = txtDisability.Text.Trim();
                string docDesc = txtMedDocDesc.Text.Trim();

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = start},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = end},
                    new SqlParameter("@medical_type", SqlDbType.VarChar,50){Value = type},
                    new SqlParameter("@insurance_status", SqlDbType.Bit){Value = insurance},
                    new SqlParameter("@disability_details", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(disability)? (object)DBNull.Value : disability},
                    new SqlParameter("@document_description", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(docDesc)? (object)DBNull.Value : docDesc}
                };

                var result = ExecuteProcNonQuery("Submit_medical", parameters);
                if (result.ok)
                {
                    lblMedicalMsg.Text = "Medical leave submitted successfully.";
                    lblMedicalMsg.CssClass = "success";
                }
                else
                {
                    lblMedicalMsg.Text = result.message;
                    lblMedicalMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblMedicalMsg.Text = ex.Message;
                lblMedicalMsg.CssClass = "error";
            }
        }
        #endregion

        #region Unpaid
        protected void btnSubmitUnpaid_Click(object sender, EventArgs e)
        {
            lblUnpaidMsg.Text = "";
            lblUnpaidMsg.CssClass = "";

            try
            {
                int empId = GetCurrentEmployeeId();

                DateTime start = ParseDateOrThrow(txtUnpaidStart.Text.Trim(), "Start Date");
                DateTime end = ParseDateOrThrow(txtUnpaidEnd.Text.Trim(), "End Date");
                string docDesc = txtUnpaidDesc.Text.Trim();

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = start},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = end},
                    new SqlParameter("@document_description", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(docDesc)? (object)DBNull.Value : docDesc}
                };

                var result = ExecuteProcNonQuery("Submit_unpaid", parameters);
                if (result.ok)
                {
                    lblUnpaidMsg.Text = "Unpaid leave submitted successfully.";
                    lblUnpaidMsg.CssClass = "success";
                }
                else
                {
                    lblUnpaidMsg.Text = result.message;
                    lblUnpaidMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblUnpaidMsg.Text = ex.Message;
                lblUnpaidMsg.CssClass = "error";
            }
        }
        #endregion

        #region Compensation
        protected void btnSubmitCompensation_Click(object sender, EventArgs e)
        {
            lblCompMsg.Text = "";
            lblCompMsg.CssClass = "";

            try
            {
                int empId = GetCurrentEmployeeId();

                DateTime compDate = ParseDateOrThrow(txtCompDate.Text.Trim(), "Compensation Date");
                DateTime originalWorkday = ParseDateOrThrow(txtOriginalWorkday.Text.Trim(), "Original Workday");
                int replacementId = ParseIntOrThrow(txtCompReplacement.Text.Trim(), "Replacement Employee ID");
                string reason = txtCompReason.Text.Trim();

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@compensation_date", SqlDbType.Date){Value = compDate},
                    new SqlParameter("@reason", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(reason)? (object)DBNull.Value : reason},
                    new SqlParameter("@date_of_original_workday", SqlDbType.Date){Value = originalWorkday},
                    new SqlParameter("@rep_emp_id", SqlDbType.Int){Value = replacementId}
                };

                var result = ExecuteProcNonQuery("Submit_compensation", parameters);
                if (result.ok)
                {
                    lblCompMsg.Text = "Compensation leave submitted successfully.";
                    lblCompMsg.CssClass = "success";
                }
                else
                {
                    lblCompMsg.Text = result.message;
                    lblCompMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblCompMsg.Text = ex.Message;
                lblCompMsg.CssClass = "error";
            }
        }
        #endregion

        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
