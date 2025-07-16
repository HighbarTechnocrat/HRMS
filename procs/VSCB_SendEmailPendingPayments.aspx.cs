using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_SendEmailPendingPayments : System.Web.UI.Page
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
   public DataSet dsPendingPaymentReq;
    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    

  
    #endregion
    public DataTable dtPOWONo;
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
                

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_InboxInvoices_DropdownList();
                    getMngInvoiceReqstList();
                    Get_Pending_StatusReport_Access();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

     
    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngInvoiceReqstList();

    }
    #endregion

    #region Page Methods
    private void getMngInvoiceReqstList()
    {
        try
        {
            string[] strdate;
            string strPODate = "";
             

            if (Convert.ToString(lstPOWODate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstPOWODate.SelectedValue).Trim().Split('-');
                strPODate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            string strInvoiceDateDate = "";
            if (Convert.ToString(lstpInvoiceDate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstpInvoiceDate.SelectedValue).Trim().Split('-');
                strInvoiceDateDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }


            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_PaymentApprovalStatusreport";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

            spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@Costcenter", SqlDbType.VarChar);
            if (Convert.ToString(lstcostCenter.SelectedValue).Trim() != "0")
                spars[3].Value = Convert.ToString(lstcostCenter.SelectedValue).Trim();
            else
                spars[3].Value = DBNull.Value;


            spars[4] = new SqlParameter("@VendorID", SqlDbType.Int);
            if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
                spars[4].Value = Convert.ToInt32(lstVendorName.SelectedValue);
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0")
                spars[5].Value = Convert.ToString(lstInvoiceNo.SelectedValue);
            else
                spars[5].Value = DBNull.Value;


            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            RecordCount.Text = "";
            //lblmessage.Text = "";
            if (dsmyInvoice.Tables[1].Rows.Count > 0)
            {
                RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[1].Rows.Count);
                gvMngTravelRqstList.DataSource = dsmyInvoice.Tables[1];
                gvMngTravelRqstList.DataBind();

                gv_emailAddress.DataSource = dsmyInvoice.Tables[2];
                gv_emailAddress.DataBind();
                //dsPendingPaymentReq = dsmyInvoice;

                foreach (GridViewRow row in gvMngTravelRqstList.Rows)
                {

                    #region check Approved and set Back-color as Green
                    if (Convert.ToString(row.Cells[12].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[12].Text).Trim().Contains("Approved"))
                        {
                            row.Cells[12].BackColor = System.Drawing.Color.Green;
                        }
                    }

                    if (Convert.ToString(row.Cells[13].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[13].Text).Trim().Contains("Approved"))
                        {
                            row.Cells[13].BackColor = System.Drawing.Color.Green;
                        }
                    }

                    if (Convert.ToString(row.Cells[14].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[14].Text).Trim().Contains("Approved"))
                        {
                            row.Cells[14].BackColor = System.Drawing.Color.Green;
                        }
                    }
                    if (Convert.ToString(row.Cells[15].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[15].Text).Trim().Contains("Approved"))
                        {
                            row.Cells[15].BackColor = System.Drawing.Color.Green;
                        }
                    }
                    if (Convert.ToString(row.Cells[16].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[16].Text).Trim().Contains("Approved"))
                        {
                            row.Cells[16].BackColor = System.Drawing.Color.Green;
                        }
                    }
                    #endregion


                    #region check Pending and set Back-Color as Yellow

                    if (Convert.ToString(row.Cells[12].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[12].Text).Trim().Contains("Pending"))
                        {
                            row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                        }
                    }

                    if (Convert.ToString(row.Cells[13].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[13].Text).Trim().Contains("Pending"))
                        {
                            row.Cells[13].BackColor = System.Drawing.Color.Yellow;
                        }
                    }

                    if (Convert.ToString(row.Cells[14].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[14].Text).Trim().Contains("Pending"))
                        {
                            row.Cells[14].BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                    if (Convert.ToString(row.Cells[15].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[15].Text).Trim().Contains("Pending"))
                        {
                            row.Cells[15].BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                    if (Convert.ToString(row.Cells[16].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[16].Text).Trim().Contains("Pending"))
                        {
                            row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                        }
                    }

                    #endregion

                    #region check Correction and set Back-Color as Tomato

                    if (Convert.ToString(row.Cells[12].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[12].Text).Trim().Contains("Correction"))
                        {
                            row.Cells[12].BackColor = System.Drawing.Color.Tomato;
                        }
                    }

                    if (Convert.ToString(row.Cells[13].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[13].Text).Trim().Contains("Correction"))
                        {
                            row.Cells[13].BackColor = System.Drawing.Color.Tomato;
                        }
                    }

                    if (Convert.ToString(row.Cells[14].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[14].Text).Trim().Contains("Correction"))
                        {
                            row.Cells[14].BackColor = System.Drawing.Color.Tomato;
                        }
                    }
                    if (Convert.ToString(row.Cells[15].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[15].Text).Trim().Contains("Correction"))
                        {
                            row.Cells[15].BackColor = System.Drawing.Color.Tomato;
                        }
                    }
                    if (Convert.ToString(row.Cells[16].Text).Trim() != "")
                    {
                        if (Convert.ToString(row.Cells[16].Text).Trim().Contains("Correction"))
                        {
                            row.Cells[16].BackColor = System.Drawing.Color.Tomato;
                        }
                    }

                    #endregion
                }

            }
            else
            {
                lblmessage.Text = "Record's not found.!";
                lblmessage.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void get_InboxInvoices_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWOs_Invoices_List_for_Send_PaymentPending_Email_Dropdown";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPOWONo.DataSource = dsList.Tables[0];
            lstPOWONo.DataTextField = "PONumber";
            lstPOWONo.DataValueField = "POID";
            lstPOWONo.DataBind();
        }
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));


        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstPOWODate.DataSource = dsList.Tables[1];
            lstPOWODate.DataTextField = "PODate";
            lstPOWODate.DataValueField = "PODate";
            lstPOWODate.DataBind();
        }
        lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", "0"));


        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstVendorName.DataSource = dsList.Tables[2];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "VendorId";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            lstInvoiceNo.DataSource = dsList.Tables[3];
            lstInvoiceNo.DataTextField = "InvoiceNo";
            lstInvoiceNo.DataValueField = "InvoiceNo";
            lstInvoiceNo.DataBind();
        }
        lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", "0"));

        if (dsList.Tables[4].Rows.Count > 0)
        {
            lstpInvoiceDate.DataSource = dsList.Tables[4];
            lstpInvoiceDate.DataTextField = "InvoiceDate";
            lstpInvoiceDate.DataValueField = "InvoiceDate";
            lstpInvoiceDate.DataBind();
        }
        lstpInvoiceDate.Items.Insert(0, new ListItem("Select Invoice Date", "0"));

         
        lstDepartment.Items.Insert(0, new ListItem("Select Project/Department", "0"));

        if (dsList.Tables[5].Rows.Count > 0)
        {
            lstcostCenter.DataSource = dsList.Tables[5];
            lstcostCenter.DataTextField = "CostCentre";
            lstcostCenter.DataValueField = "CostCentre";
            lstcostCenter.DataBind();
        }
        lstcostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0")); 

    }

    private void Get_Pending_StatusReport_Access()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "CheckIsShow_Reports"; //"Get_Pending_Status_Report_Access"; 

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[3] = new SqlParameter("@ReportName", SqlDbType.VarChar);
        spars[3].Value = "VSCB_SendPendingPaymentReqMail";

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        //if (dsList.Tables[0].Rows.Count > 0)
        //{
        //    mobile_cancel.Visible = true;
        //}

        if (dsList.Tables[0].Rows.Count > 0)
        {
            var getStatus = Convert.ToString(dsList.Tables[0].Rows[0]["IsAccess"]);
            if (getStatus == "SHOW")
            {
                trvl_btnSave.Visible = true;
            }
        }

    }
    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    { 
        getMngInvoiceReqstList();

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_InboxInvoices_DropdownList();
        getMngInvoiceReqstList();  
    }


    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

   
        if(gvMngTravelRqstList.Rows.Count >0)
        {
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbdata = new StringBuilder();
            StringBuilder sbmail = new StringBuilder();
            string sapproverEmailAddress = "";

            sbmail.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Arial'>");
            sbmail.Append("<tr><td>Dear Sir </td></tr>");
            sbmail.Append("<tr><td style='height:10px'></td></tr>");
            sbmail.Append("<tr><td colspan='2'>Following Payment Requests are pending for approval with approver marked with yellow colour. Please approve the Payment Request from OneHr, so that it goes to next approver.</td></tr>");
            sbmail.Append("</table>");

            string sthStype = "background-color: #B8DBFD;border: 1px solid #ccc";
            string stdStyle = "border: 1px solid #ccc;";
            string stdbackgroundcolor_1 = "background:white";
            string stdbackgroundcolor_2 = "background:white";
            string stdbackgroundcolor_3 = "background:white";
            string stdbackgroundcolor_4 = "background:white";
            string stdbackgroundcolor_5 = "background:white";


            sbHeader.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:200%;'>");
            sbHeader.Append("<tr><th style='width:10%;"+ sthStype + "'>PO/ WO No</th>");
            //sbHeader.Append("<th style="+ sthStype + ">PO created by</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Cost Center</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Vendor Name</th>");
           // sbHeader.Append("<th style="+ sthStype + ">Resource Name</th>");
           // sbHeader.Append("<th style="+ sthStype + ">Milestone Description</th>");
            //sbHeader.Append("<th style="+ sthStype + ">Milestone Due date</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Invoice No</th>");
            //sbHeader.Append("<th style='width:8%;" + sthStype + "'>Invoice Date</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Payment Request No</th>");
            sbHeader.Append("<th style='width:8%;" + sthStype + "'>Payment Request Date</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Payment Request Created By</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Approver 1</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Approver 2</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Approver 3</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Approver 4</th>");
            sbHeader.Append("<th style='width:10%;" + sthStype + "'>Approver 5</th></tr>"); 


             
            foreach (GridViewRow row in gvMngTravelRqstList.Rows)
            {
                if (Convert.ToString(sapproverEmailAddress).Trim() != "")
                    sapproverEmailAddress = sapproverEmailAddress + ";" + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
                else
                    sapproverEmailAddress = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

                stdbackgroundcolor_1 = "background:white";
                stdbackgroundcolor_2 = "background:white";
                stdbackgroundcolor_3 = "background:white";
                stdbackgroundcolor_4 = "background:white";
                stdbackgroundcolor_5 = "background:white";

                #region check Approved and set Back-color as Green
                if (Convert.ToString(row.Cells[12].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[12].Text).Trim().Contains("Approved"))
                    {
                        stdbackgroundcolor_1 = "background: green;";
                    }
                }

                if (Convert.ToString(row.Cells[13].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[13].Text).Trim().Contains("Approved"))
                    {
                        stdbackgroundcolor_2 = "background: green;";
                    }
                }

                if (Convert.ToString(row.Cells[14].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[14].Text).Trim().Contains("Approved"))
                    {
                        stdbackgroundcolor_3 = "background: green;";
                    }
                }
                if (Convert.ToString(row.Cells[15].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[15].Text).Trim().Contains("Approved"))
                    {
                        stdbackgroundcolor_4 = "background: green";
                    }
                }
                if (Convert.ToString(row.Cells[16].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[16].Text).Trim().Contains("Approved"))
                    {
                        stdbackgroundcolor_5 = "background: green";
                    }
                }
                #endregion


                #region check Pending and set Back-color as Yellow
                if (Convert.ToString(row.Cells[12].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[12].Text).Trim().Contains("Pending"))
                    {
                        stdbackgroundcolor_1 = "background: #FFFA00;"; 
                    }
                }

                if (Convert.ToString(row.Cells[13].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[13].Text).Trim().Contains("Pending"))
                    {
                        stdbackgroundcolor_2 = "background: #FFFA00;";
                    }
                }

                if (Convert.ToString(row.Cells[14].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[14].Text).Trim().Contains("Pending"))
                    {
                        stdbackgroundcolor_3 = "background: #FFFA00;";
                    }
                }
                if (Convert.ToString(row.Cells[15].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[15].Text).Trim().Contains("Pending"))
                    {
                        stdbackgroundcolor_4 = "background: #FFFA00";
                    }
                }
                if (Convert.ToString(row.Cells[16].Text).Trim() != "")
                {
                    if (Convert.ToString(row.Cells[16].Text).Trim().Contains("Pending"))
                    {
                        stdbackgroundcolor_5 = "background: #FFFA00";
                    }
                }
                #endregion
                 
                sbdata.Append("<tr>");
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[0].Text).Trim() + "</td>"); //PO/ WO No
                //sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[1].Text).Trim() + "</td>"); //PO created by
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[2].Text).Trim() + "</td>"); //Cost Center
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[3].Text).Trim() + "</td>"); //Vendor Name
                //sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[4].Text).Trim() + "</td>"); //Resource Name
                //sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[5].Text).Trim() + "</td>"); //Milestone Description
                // sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[6].Text).Trim() + "</td>"); //Milestone Due date
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[7].Text).Trim() + "</td>"); //Invoice No
                //sbdata.Append("<td style='width:8%;" + stdStyle + "'>" + Convert.ToString(row.Cells[8].Text).Trim() + "</td>"); //Invoice Date
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[9].Text).Trim() + "</td>"); //Payment Request No
                sbdata.Append("<td style='width:8%;" + stdStyle + "'>" + Convert.ToString(row.Cells[10].Text).Trim() + "</td>"); //Payment Request Date
                sbdata.Append("<td style='width:10%;" + stdStyle + "'>" + Convert.ToString(row.Cells[11].Text).Trim() + "</td>"); //Payment Request Creation
                sbdata.Append("<td style='width:10%;" + stdStyle + stdbackgroundcolor_1 + "'>" + Convert.ToString(row.Cells[12].Text).Trim() + "</td>"); //Approver 1
                sbdata.Append("<td style='width:10%;" + stdStyle + stdbackgroundcolor_2 +"'>" + Convert.ToString(row.Cells[13].Text).Trim() + "</td>"); //Approver 2
                sbdata.Append("<td style='width:10%;" + stdStyle + stdbackgroundcolor_3 + "'>" + Convert.ToString(row.Cells[14].Text).Trim() + "</td>"); //Approver 3
                sbdata.Append("<td style='width:10%;" + stdStyle + stdbackgroundcolor_4 + "'>" + Convert.ToString(row.Cells[15].Text).Trim() + "</td>"); //Approver 4
                sbdata.Append("<td style='width:10%;" + stdStyle + stdbackgroundcolor_5 +"'>" + Convert.ToString(row.Cells[16].Text).Trim() + "</td>"); //Approver 5


                sbdata.Append("</tr>");

            }
            sbHeader.Append(sbdata);
            sbHeader.Append("</table>");

            string approveremail_cc = "";
            string approveremail_to = "";
            approveremail_cc = Session["LoginEmpmail"].ToString();  

            if (gv_emailAddress.Rows.Count > 0)
            {

                foreach (GridViewRow row_email in gv_emailAddress.Rows)
                {
                    if (Convert.ToString(approveremail_to).Trim() != "")
                        approveremail_to = approveremail_to + ";" + Convert.ToString(Convert.ToString(row_email.Cells[0].Text).Trim()).Trim();
                    else
                        approveremail_to = Convert.ToString(Convert.ToString(row_email.Cells[0].Text).Trim()).Trim();
                }
            }
           

            if(Convert.ToString(approveremail_to).Trim()!="")
            spm.sendMail_VSCB(approveremail_to, "Payment Requests Pending for Approval", Convert.ToString(sbmail).Trim() + Convert.ToString(sbHeader).Trim() , "", approveremail_cc);



        }
    }



    protected void lstPOWONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();
    }
}