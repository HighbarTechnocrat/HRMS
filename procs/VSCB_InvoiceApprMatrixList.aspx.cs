using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_InvoiceApprMatrixList : System.Web.UI.Page
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

    SP_Methods spm = new SP_Methods();
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

                    getCostCenterList();
                    getApproversList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim(); 
        
        Response.Redirect("VSCB_CreateInvoice.aspx?invid=" + hdnInvoiceId.Value + "&mngexp=1");
    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }
    #endregion

    #region Page Methods 

    private void getCostCenterList()
    {
        try
        {

            DataSet dsProjectsVendors = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Costcenter_List";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;


            dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                lstCostCenter.DataSource = dsProjectsVendors.Tables[0];
                lstCostCenter.DataTextField = "Tallycode";
                lstCostCenter.DataValueField = "Tallycode";
                lstCostCenter.DataBind();                 
            }
            lstCostCenter.Items.Insert(0, new ListItem("Select CostCenter", ""));
        }
        catch (Exception)
        {

        }
    }

    private void getApproversList()
    {
        try
        {

            DataSet dsProjectsVendors = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Approvers_List";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;


            dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                lstAppr1.DataSource = dsProjectsVendors.Tables[0];
                lstAppr1.DataTextField = "Emp_Name";
                lstAppr1.DataValueField = "Emp_Code";
                lstAppr1.DataBind();

                lstAppr2.DataSource = dsProjectsVendors.Tables[0];
                lstAppr2.DataTextField = "Emp_Name";
                lstAppr2.DataValueField = "Emp_Code";
                lstAppr2.DataBind();

                lstAppr3.DataSource = dsProjectsVendors.Tables[0];
                lstAppr3.DataTextField = "Emp_Name";
                lstAppr3.DataValueField = "Emp_Code";
                lstAppr3.DataBind();

                lstAppr4.DataSource = dsProjectsVendors.Tables[0];
                lstAppr4.DataTextField = "Emp_Name";
                lstAppr4.DataValueField = "Emp_Code";
                lstAppr4.DataBind();
                


            }
            lstAppr1.Items.Insert(0, new ListItem("Select Select Approver", ""));
            lstAppr2.Items.Insert(0, new ListItem("Select Select Approver", ""));
            lstAppr3.Items.Insert(0, new ListItem("Select Select Approver", ""));
            lstAppr4.Items.Insert(0, new ListItem("Select Select Approver", ""));

            
        }
        catch (Exception)
        {

        }
    }

    private void getCostCenter_Approver()
    {
        try
        {

            DataSet dsProjectsVendors = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_CostCenter_Approvers";

            spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(lstCostCenter.SelectedValue);

            //Response.Write(lstCostCenter.SelectedValue);
            //Response.End();
            dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            chkApprover1.Checked = false;
            chkApprover2.Checked = false;
            chkApprover3.Checked = false;
            chkApprover4.Checked = false;

            hdnFisrtAppr_Code.Value = "";
            hdnSecondAppr_Code.Value = "";
            hdnThirdAppr_Code.Value = "";
            hdnFourthAppr_Code.Value = "";

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
               
               
                if (dsProjectsVendors.Tables[0].Rows.Count == 4)
                {
                    lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
                    lstAppr3.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();
                    lstAppr4.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["Emp_Code"]).Trim();

                    hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
                    hdnThirdAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();
                    hdnFourthAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["Emp_Code"]).Trim();


                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover1.Checked = true;
                     
                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover2.Checked = true;
                     
                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover3.Checked = true;
                     
                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover4.Checked = true;
                     

                }
                if (dsProjectsVendors.Tables[0].Rows.Count == 3)
                {
                    lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
                    lstAppr3.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();

                    hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
                    hdnThirdAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover1.Checked = true;

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover2.Checked = true;

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover3.Checked = true;

                     
                }
                if (dsProjectsVendors.Tables[0].Rows.Count == 2)
                {
                    lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();

                    hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover1.Checked = true;

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover2.Checked = true;
                     
                }
                if (dsProjectsVendors.Tables[0].Rows.Count == 1)
                {
                    lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();

                    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
                        chkApprover1.Checked = true;
 
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private DataSet getCostCenter_Approver(string sTallyCode)
    {
        DataSet dsProjectsVendors = new DataSet();
        try
        { 
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_CostCenter_Approvers";

            spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(sTallyCode);
             
            dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            
        }
        catch (Exception)
        {

        }
        return dsProjectsVendors;
    }

    private void insert_update_Invoice_Payment_Approvers(string sQueryType, string sTallyCode,string sApproverEmp_Id,string sApproverEmp_code,
        string isSelected,string sOldApprover_empcode)
    {
        DataSet dsProjectsVendors = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[6]; 

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = sQueryType;

            spars[1] = new SqlParameter("@Tallycode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(sTallyCode);

            spars[2] = new SqlParameter("@APPR_ID", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(sApproverEmp_Id);

            spars[3] = new SqlParameter("@APPR_Emp_Code", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(sApproverEmp_code);

            spars[4] = new SqlParameter("@IS_SELECTED", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(isSelected);

            spars[5] = new SqlParameter("@APPR_Emp_Code_old", SqlDbType.VarChar);
            if (Convert.ToString(sOldApprover_empcode).Trim() != "")
                spars[5].Value = Convert.ToString(sOldApprover_empcode);
            else
                spars[5].Value = DBNull.Value;

           dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_insert_update_Invocie_paymentApprovers");

        }
        catch (Exception)
        {

        }
        
    }
    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean blnAppr = false;
            string s1Appr = "";
            string s2Appr = "";
            string s3Appr = "";
            string s4Appr = "";
            string s1Appr_selected ="N";
            string s2Appr_selected = "N";
            string s3Appr_selected = "N";
            string s4Appr_selected = "N";
            string sInsertUpdateFlg = "insert_InvoiceApprovers";
            


            #region Add or Update Approver


            if (Convert.ToString(lstAppr1.SelectedValue).Trim() != "")
            {
                s1Appr = Convert.ToString(lstAppr1.SelectedValue).Trim();
                hdnFisrtAppr_ID.Value = "1";
            }
            if (Convert.ToString(lstAppr2.SelectedValue).Trim() != "")
            {
                s2Appr = Convert.ToString(lstAppr2.SelectedValue).Trim();
                hdnSecondAppr_ID.Value = "2";
            }
            if (Convert.ToString(lstAppr3.SelectedValue).Trim() != "")
            {
                s3Appr = Convert.ToString(lstAppr3.SelectedValue).Trim();
                hdnThirdAppr_ID.Value = "3";
            }
            if (Convert.ToString(lstAppr4.SelectedValue).Trim() != "")
            {
                s4Appr = Convert.ToString(lstAppr4.SelectedValue).Trim();
                hdnFourthAppr_ID.Value = "4";
            }
            if (chkApprover1.Checked)
                s1Appr_selected = "Y";

            if (chkApprover2.Checked)
                s2Appr_selected = "Y";

            if (chkApprover3.Checked)
                s3Appr_selected = "Y";

            if (chkApprover4.Checked)
                s4Appr_selected = "Y";

            //checked Selected Tally code Approval Matrix already set
            DataSet dsApprovers= getCostCenter_Approver(Convert.ToString(lstCostCenter.SelectedValue).Trim());
            if(dsApprovers !=null)
            {
                if(dsApprovers.Tables.Count>0)
                {
                    sInsertUpdateFlg = "update_InvoiceApprovers";
                    if (dsApprovers.Tables[0].Rows.Count > 0)
                    {
                        if (dsApprovers.Tables[0].Rows.Count == 4)
                        {
                            hdnFisrtAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[0]["Emp_Code"]).Trim();
                            hdnSecondAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[1]["Emp_Code"]).Trim();
                            hdnThirdAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[2]["Emp_Code"]).Trim();
                            hdnFourthAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[3]["Emp_Code"]).Trim();
                        }
                        if (dsApprovers.Tables[0].Rows.Count == 3)
                        {
                            hdnFisrtAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[0]["Emp_Code"]).Trim();
                            hdnSecondAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[1]["Emp_Code"]).Trim();
                            hdnThirdAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[2]["Emp_Code"]).Trim();
                        }
                        if (dsApprovers.Tables[0].Rows.Count == 2)
                        {
                            hdnFisrtAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[0]["Emp_Code"]).Trim();
                            hdnSecondAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[1]["Emp_Code"]).Trim(); 
                        }
                        if (dsApprovers.Tables[0].Rows.Count == 1)
                        {
                            hdnFisrtAppr_Code.Value = Convert.ToString(dsApprovers.Tables[0].Rows[0]["Emp_Code"]).Trim();
                        }
                    }
                }
            }

             
            if (Convert.ToString(lstAppr1.SelectedValue).Trim() != "")
            {
                insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(lstCostCenter.SelectedValue).Trim(), hdnFisrtAppr_ID.Value, lstAppr1.SelectedValue, s1Appr_selected, hdnFisrtAppr_Code.Value);
            }
            if (Convert.ToString(lstAppr2.SelectedValue).Trim() != "")
            {
                insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(lstCostCenter.SelectedValue).Trim(), hdnSecondAppr_ID.Value, lstAppr2.SelectedValue, s2Appr_selected, hdnSecondAppr_Code.Value);
            }
            if (Convert.ToString(lstAppr3.SelectedValue).Trim() != "")
            {
                insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(lstCostCenter.SelectedValue).Trim(), hdnThirdAppr_ID.Value, lstAppr3.SelectedValue, s3Appr_selected, hdnThirdAppr_Code.Value);
            }
            if (Convert.ToString(lstAppr4.SelectedValue).Trim() != "")
            {
                insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(lstCostCenter.SelectedValue).Trim(), hdnFourthAppr_ID.Value, lstAppr4.SelectedValue, s4Appr_selected, hdnFourthAppr_Code.Value);
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        
         
    }



    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        getApproversList();
        chkApprover1.Checked = false;
        chkApprover2.Checked = false;
        chkApprover3.Checked = false;
        chkApprover4.Checked = false;
        getCostCenter_Approver();

    }
}