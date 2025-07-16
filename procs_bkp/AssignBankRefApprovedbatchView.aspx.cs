using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_AssignBankRefApprovedbatchView : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {

                lblmessage.Text = "";
                Page.SmartNavigation = true;
                txtEmpCode.Text = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    PopulateEmployeeData();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Convert.ToString(Request.QueryString["batchid"]).Trim();
                        get_Batch_details();
                        idBankRef_1.Visible = true;
                        idBankRef_2.Visible = true;
                        idBankRef_3.Visible = true;
                    }
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

   

    #endregion

    #region Page Methods
   
    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Code = txtEmpCode.Text;
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmpName.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtbatchCreatedBy.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void get_Batch_details()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myBatch_Details";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();
            if (dsList.Tables[0].Rows.Count > 0)
            {
                txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
                txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
                txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();
                txtBankname.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Name"]);
                txtBankRefNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_no"]).Trim();
                txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Date"]).Trim();
                txtbatchNo.Visible = true;
                spnBatchNo.Visible = true;
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    gvMngPaymentList_Batch.DataSource = dsList.Tables[1];
                    gvMngPaymentList_Batch.DataBind();

                    gvMngPaymentList_Batch.Columns[0].Visible = false;
                    gvMngPaymentList_Batch.Visible = true;
                }
                if (dsList.Tables[3].Rows.Count > 0)
                {
                    HDPaymentRelDate.Value = Convert.ToString(dsList.Tables[3].Rows[0]["Paymentreldate"]).Trim();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    
    #endregion 

    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Deate_Batch_items_Temptable";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]);

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        gvMngPaymentList_Batch.DataSource = null;
        gvMngPaymentList_Batch.DataBind();
        txtbatchTotalPayment.Text = "";
        txtbatchNoOfRequest.Text = "";
        if (dsList.Tables[0].Rows.Count > 0)
        {
            gvMngPaymentList_Batch.DataSource = dsList.Tables[0];
            gvMngPaymentList_Batch.DataBind();

            txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["TotalPaymentAmt"]).Trim();
            txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows.Count);
        }
    }

    protected void gvMngPaymentList_Batch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // if(Request.QueryString.Count>0)
        // e.Row.Cells[10].Visible = false;
    }
    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;
      
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }

            #region Validation

            lblmessage.Text = "";
            string StrEmailAddress = "";
            foreach (GridViewRow gvrow in gvMngPaymentList_Batch.Rows)
            {
                TextBox TxtPayRefNo      = (TextBox)gvrow.FindControl("TxtPaymentRefNo");
                Label HFEmail            = (Label)gvrow.FindControl("HFEmail");
                Label HDVendorName       = (Label)gvrow.FindControl("HDVendorName");
                string strEmailAddress   = HFEmail.Text;
                string strVendorName     = HDVendorName.Text;
                if (TxtPayRefNo.Text.Trim() == "")
                {
                    lblmessage.Text = "Please Enter the Payment Ref. No.";
                    return;
                }
                if (HFEmail.Text.Trim() == "")
                {
                    StrEmailAddress += strVendorName + ", ";
                }
            }
            if (StrEmailAddress != "")
            {
                string str = "Vendor EmailID not found as below ";
                lblmessage.Text = str.ToString().Replace(Environment.NewLine, "<br/><br/>") + StrEmailAddress;
                return;
            }

            #endregion

            foreach (GridViewRow gvrow in gvMngPaymentList_Batch.Rows)
            {
                string strPOID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[0]).Trim();
                string strPayment_ID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[2]).Trim();
                string strInvoiceID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[3]).Trim();
                TextBox TxtPayRefNo = (TextBox)gvrow.FindControl("TxtPaymentRefNo");

                Label HFEmail = (Label)gvrow.FindControl("HFEmail");
                Label HDVendorName = (Label)gvrow.FindControl("HDVendorName");
                Label LblAmounttobepaid = (Label)gvrow.FindControl("LblAmounttobepaid");
                Label lblPaymentCreatorEmailaddress = (Label)gvrow.FindControl("lblPaymentCreatorEmailaddress");
                Label lblAmountINWord = (Label)gvrow.FindControl("lblAmountINWord");

                DataSet dsList = new DataSet();
                SqlParameter[] spars = new SqlParameter[6];
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "get_myBatch_DetailsUpdateOnebyOne";

                spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
                spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

                spars[2] = new SqlParameter("@POID", SqlDbType.BigInt);
                spars[2].Value = Convert.ToDouble(strPOID);
                spars[3] = new SqlParameter("@spaymentId", SqlDbType.VarChar);
                spars[3].Value = strPayment_ID.Trim();
                spars[4] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
                spars[4].Value = Convert.ToDouble(strInvoiceID);
                spars[5] = new SqlParameter("@costcenter", SqlDbType.VarChar);
                spars[5].Value = TxtPayRefNo.Text.Trim();

               // dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

                #region send Email to  Batch Approval
                string strInvoiceURL = "";
                string strsubject = "Payment Advice";
                StringBuilder sbBatchDetails    = new StringBuilder();
                StringBuilder strbuildBodyMsg   = new StringBuilder();
                StringBuilder strbuildFooterMsg = new StringBuilder();

                sbBatchDetails.Append("<table cellpadding='0' cellspacing='0' width='100%' style='font-size: 9pt;font-family:Trebuchet MS'>");
                sbBatchDetails.Append("<tr><td style='height:30px;text-align:center' colspan='3'>Payment Advice </td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>Beneficiary Code :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> XXX</td>");
                sbBatchDetails.Append("<td style='height:20px;width:200px'>Payment Ini. Date :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> "+ HDPaymentRelDate.Value +" </td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>Beneficiary Name :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> HIGHBAR TECHNOCRAT LIMITED</td>");
                sbBatchDetails.Append("<td style='height:20px;width:200px'>Amount :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> "+ LblAmounttobepaid .Text.Trim() + "</td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>Beneficiary A/c No :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> XXXXXXXXXX0098</td>");
                sbBatchDetails.Append("<td style='height:20px;width:200px'>Company Name :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> "+HDVendorName.Text.Trim() +"</td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>Beneficiary IFSC Code :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> BARB0ILTHAN</td>");
                sbBatchDetails.Append("<td style='height:20px;width:200px'>Payment Ref. No. :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> "+ TxtPayRefNo.Text.Trim() +"</td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>UTR Number :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> AXISCN0196641033</td>");
                sbBatchDetails.Append("<td style='height:20px;width:200px'>Bank Ref. No :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px'> "+ txtBankRefNo.Text.Trim() +"</td></tr>");

                sbBatchDetails.Append("<tr><td style='height:20px;width:200px'>Amount in Words :</td>");
                sbBatchDetails.Append("<td style='height:20px;width:300px' colspan='3'> "+ lblAmountINWord .Text.Trim() + " </td></tr>");
               // sbBatchDetails.Append("<td style='height:20px;width:200px'></td>");
               // sbBatchDetails.Append("<td style='height:20px;width:300px'></td></tr>");

                sbBatchDetails.Append("</table>");

                strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Trebuchet MS;margin-top:30px'>");
                strbuildBodyMsg.Append("<tr><td>Dear Sir/Madam, </td></tr>");
                strbuildBodyMsg.Append("<tr><td style='height:7px'></td></tr>");
                strbuildBodyMsg.Append("<tr><td colspan='2'> We have initiated your payment to RBI through NEFT on " + HDPaymentRelDate.Value + " for an amount of INR <b> " + LblAmounttobepaid.Text.Trim() + " (" + lblAmountINWord.Text.Trim() + ") </b>, the details of which are mentioned below. In case of any clarifications related to this transaction, kindly contact the concerned officials at HIGHBAR TECHNOCRAT LIMITED.</td></tr>");
                strbuildBodyMsg.Append("</table>");

                strbuildFooterMsg.Append("<table cellspacing='0' style='font-size: 9pt;font-family:Trebuchet MS;margin-top:30px'>");
                strbuildFooterMsg.Append("<tr><td>Enrichment: </td></tr>");
                strbuildFooterMsg.Append("<tr><td>JMCPROIL_AXI16CA001_20230109_HO096_20230</td></tr>");
                strbuildFooterMsg.Append("<tr><td>100012003092452022</td></tr>");
                strbuildFooterMsg.Append("<tr><td>HIGHBAR TECHNOCRAT LIMITED</td></tr>");
                strbuildFooterMsg.Append("</table>");


                string strmailcontain = Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(strbuildFooterMsg).Trim();
                spm.sendMail_VSCB(HFEmail.Text.Trim(), strsubject, strmailcontain, "", lblPaymentCreatorEmailaddress.Text.Trim());


                #endregion


            }

            Response.Redirect("VSCB_AssignBankRefApprovedbatch.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw;
        }
    }
}