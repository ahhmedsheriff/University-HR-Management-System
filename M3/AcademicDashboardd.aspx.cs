using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M3
{
    public partial class AcademicDashboardd : System.Web.UI.Page
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["HRConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // nothing special required on load
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

        private int ParseIntOrThrow(string input, string fieldName)
        {
            if (!int.TryParse(input, out int v))
                throw new ArgumentException($"Invalid integer for {fieldName}.");
            return v;
        }
        #endregion

        #region Accidental Leave
        protected void btnSubmitAccidental_Click(object sender, EventArgs e)
        {
            lblAccidentalMsg.Text = "";
            lblAccidentalMsg.CssClass = "";
            try
            {
                int empId = ParseIntOrThrow(txtAccEmpID.Text.Trim(), "Employee ID");
                DateTime start = ParseDateOrThrow(txtAccDate.Text.Trim(), "Accidental Date");
                DateTime end = start; // accidental leave is one day in your DB design

                var p = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = start},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = end}
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

        #region Medical Leave
        protected void btnSubmitMedical_Click(object sender, EventArgs e)
        {
            lblMedicalMsg.Text = "";
            lblMedicalMsg.CssClass = "";
            try
            {
                int empId = ParseIntOrThrow(txtMedEmpID.Text.Trim(), "Employee ID");
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
                    new SqlParameter("@disability_details", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(disability) ? (object)DBNull.Value : disability},
                    new SqlParameter("@document_description", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(docDesc) ? (object)DBNull.Value : docDesc}
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

        #region Unpaid Leave
        protected void btnSubmitUnpaid_Click(object sender, EventArgs e)
        {
            lblUnpaidMsg.Text = "";
            lblUnpaidMsg.CssClass = "";
            try
            {
                int empId = ParseIntOrThrow(txtUnpaidEmpID.Text.Trim(), "Employee ID");
                DateTime start = ParseDateOrThrow(txtUnpaidStart.Text.Trim(), "Start Date");
                DateTime end = ParseDateOrThrow(txtUnpaidEnd.Text.Trim(), "End Date");
                string docDesc = txtUnpaidDesc.Text.Trim();

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empId},
                    new SqlParameter("@start_date", SqlDbType.Date){Value = start},
                    new SqlParameter("@end_date", SqlDbType.Date){Value = end},
                    new SqlParameter("@document_description", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(docDesc) ? (object)DBNull.Value : docDesc}
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

        #region Compensation Leave
        protected void btnSubmitCompensation_Click(object sender, EventArgs e)
        {
            lblCompMsg.Text = "";
            lblCompMsg.CssClass = "";
            try
            {
                int empId = ParseIntOrThrow(txtCompEmpID.Text.Trim(), "Employee ID");
                DateTime compDate = ParseDateOrThrow(txtCompDate.Text.Trim(), "Compensation Date");
                DateTime originalWorkday = ParseDateOrThrow(txtOriginalWorkday.Text.Trim(), "Original Workday");
                int replacementId = ParseIntOrThrow(txtCompReplacement.Text.Trim(), "Replacement ID");
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

        #region Approve Annual
        protected void btnApproveAnnual_Click(object sender, EventArgs e)
        {
            lblApproveAnnualMsg.Text = "";
            lblApproveAnnualMsg.CssClass = "";
            try
            {
                int reqId = ParseIntOrThrow(txtApproveAnnualReqID.Text.Trim(), "Request ID");
                int upperId = ParseIntOrThrow(txtApproveAnnualUpperID.Text.Trim(), "Upperboard ID");

                SqlParameter repParam;
                if (!string.IsNullOrWhiteSpace(txtApproveAnnualReplacementID.Text))
                {
                    int replacementId = ParseIntOrThrow(txtApproveAnnualReplacementID.Text.Trim(), "Replacement ID");
                    repParam = new SqlParameter("@replacement_ID", SqlDbType.Int) { Value = replacementId };
                }
                else
                {
                    repParam = new SqlParameter("@replacement_ID", SqlDbType.Int) { Value = DBNull.Value };
                }

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@request_ID", SqlDbType.Int){Value = reqId},
                    new SqlParameter("@Upperboard_ID", SqlDbType.Int){Value = upperId},
                    repParam
                };

                var result = ExecuteProcNonQuery("Upperboard_approve_annual", parameters);
                if (result.ok)
                {
                    lblApproveAnnualMsg.Text = "Upperboard approval (annual) executed — check DB for final status.";
                    lblApproveAnnualMsg.CssClass = "success";
                }
                else
                {
                    lblApproveAnnualMsg.Text = result.message;
                    lblApproveAnnualMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblApproveAnnualMsg.Text = ex.Message;
                lblApproveAnnualMsg.CssClass = "error";
            }
        }
        #endregion

        #region Approve Unpaid
        protected void btnApproveUnpaid_Click(object sender, EventArgs e)
        {
            lblApproveUnpaidMsg.Text = "";
            lblApproveUnpaidMsg.CssClass = "";
            try
            {
                int reqId = ParseIntOrThrow(txtApproveUnpaidReqID.Text.Trim(), "Request ID");
                int upperId = ParseIntOrThrow(txtApproveUnpaidUpperID.Text.Trim(), "Upperboard ID");

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@request_ID", SqlDbType.Int){Value = reqId},
                    new SqlParameter("@upperboard_ID", SqlDbType.Int){Value = upperId}
                };

                var result = ExecuteProcNonQuery("Upperboard_approve_unpaids", parameters);
                if (result.ok)
                {
                    lblApproveUnpaidMsg.Text = "Upperboard processed unpaid leave approval — check DB for final status.";
                    lblApproveUnpaidMsg.CssClass = "success";
                }
                else
                {
                    lblApproveUnpaidMsg.Text = result.message;
                    lblApproveUnpaidMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblApproveUnpaidMsg.Text = ex.Message;
                lblApproveUnpaidMsg.CssClass = "error";
            }
        }
        #endregion

        #region Evaluation
        protected void btnEvaluate_Click(object sender, EventArgs e)
        {
            lblEvalMsg.Text = "";
            lblEvalMsg.CssClass = "";
            try
            {
                int empToEval = ParseIntOrThrow(txtEvalEmpID.Text.Trim(), "Employee To Evaluate");
                int rating = ParseIntOrThrow(txtEvalRating.Text.Trim(), "Rating");
                string comment = txtEvalComment.Text.Trim();
                string semester = txtEvalSemester.Text.Trim();

                if (rating < 1 || rating > 5)
                    throw new ArgumentException("Rating must be between 1 and 5.");

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@employee_ID", SqlDbType.Int){Value = empToEval},
                    new SqlParameter("@rating", SqlDbType.Int){Value = rating},
                    new SqlParameter("@comment", SqlDbType.VarChar,50){Value = string.IsNullOrEmpty(comment) ? (object)DBNull.Value : comment},
                    new SqlParameter("@semester", SqlDbType.Char,3){Value = string.IsNullOrEmpty(semester)? (object)DBNull.Value : semester}
                };

                var result = ExecuteProcNonQuery("Dean_andHR_Evaluation", parameters);
                if (result.ok)
                {
                    lblEvalMsg.Text = "Evaluation submitted successfully.";
                    lblEvalMsg.CssClass = "success";
                }
                else
                {
                    lblEvalMsg.Text = result.message;
                    lblEvalMsg.CssClass = "error";
                }
            }
            catch (Exception ex)
            {
                lblEvalMsg.Text = ex.Message;
                lblEvalMsg.CssClass = "error";
            }
        }
        #endregion
    }
}