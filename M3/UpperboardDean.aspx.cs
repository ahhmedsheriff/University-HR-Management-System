using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace M3
{
    public partial class UpperboardDean : System.Web.UI.Page
    {
        // Use your main connection string name
        private string ConnStr =>
            ConfigurationManager.ConnectionStrings["University_HR_ManagementSystem"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // optional: you can check some session role here if you want
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

        private int ParseIntOrThrow(string input, string fieldName)
        {
            if (!int.TryParse(input, out int v))
                throw new ArgumentException($"Invalid integer for {fieldName}.");
            return v;
        }

        #endregion

        #region Approve Unpaid Leave

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
                    new SqlParameter("@request_ID", SqlDbType.Int)   { Value = reqId   },
                    new SqlParameter("@upperboard_ID", SqlDbType.Int){ Value = upperId }
                };

                var result = ExecuteProcNonQuery("Upperboard_approve_unpaids", parameters);

                if (result.ok)
                {
                    lblApproveUnpaidMsg.Text = "Unpaid leave processed. Check the DB for final status.";
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

        #region Approve Annual Leave

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
                    int replacementId = ParseIntOrThrow(
                        txtApproveAnnualReplacementID.Text.Trim(),
                        "Replacement ID"
                    );
                    repParam = new SqlParameter("@replacement_ID", SqlDbType.Int)
                    {
                        Value = replacementId
                    };
                }
                else
                {
                    repParam = new SqlParameter("@replacement_ID", SqlDbType.Int)
                    {
                        Value = DBNull.Value
                    };
                }

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@request_ID", SqlDbType.Int)     { Value = reqId   },
                    new SqlParameter("@Upperboard_ID", SqlDbType.Int)  { Value = upperId },
                    repParam
                };

                var result = ExecuteProcNonQuery("Upperboard_approve_annual", parameters);

                if (result.ok)
                {
                    lblApproveAnnualMsg.Text = "Annual leave processed. Check the DB for final status.";
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

        #region Dean Evaluation

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
                    new SqlParameter("@employee_ID", SqlDbType.Int)   { Value = empToEval },
                    new SqlParameter("@rating",      SqlDbType.Int)   { Value = rating    },
                    new SqlParameter("@comment",     SqlDbType.VarChar, 50)
                    {
                        Value = string.IsNullOrEmpty(comment)
                                ? (object)DBNull.Value
                                : comment
                    },
                    new SqlParameter("@semester",    SqlDbType.Char, 3)
                    {
                        Value = string.IsNullOrEmpty(semester)
                                ? (object)DBNull.Value
                                : semester
                    }
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

        #region Navigation

        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        #endregion
    }
}
