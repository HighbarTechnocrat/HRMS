using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class procs_VSCB_InvoicePaymentMakerCreate : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    public DataSet ldsEmployees;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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
            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                HDInPEmpcode.Value = Convert.ToString(Session["Empcode"]).Trim();
                if (!Page.IsPostBack)
                {
                    get_InvoicePaymentmaker_BindData();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPrjDeptId.Value = Request.QueryString[0]; 
                        getSelectedCostCenterEmp(); 
                        lstCostCenter.Enabled = false;
                        trvl_btnSave.Text = "Update";
                    } 
                     
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Select Cost Center";
            return;
        }

        var getSelectedInvoice = false;
        var getSelectedPayment = false;

        foreach (GridViewRow crow in gvMngTravelRqstList.Rows)
        {
            string sPotypeId = Convert.ToString(gvMngTravelRqstList.DataKeys[crow.RowIndex].Values[0]).Trim();

            ListBox lstempnameInvoice_Create = (ListBox)crow.FindControl("lstempname_Create_Invoice");
            ListBox lstempnamePayment_Create = (ListBox)crow.FindControl("lstempname_Create_Payment");

            var getEmpInvoice = "";
            var getEmpPayment = "";
            foreach (ListItem item in lstempnameInvoice_Create.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        getSelectedInvoice = true;
                        if (getEmpInvoice == "")
                        {
                            getEmpInvoice = item.Value;
                        }
                        else
                        {
                            getEmpInvoice = getEmpInvoice + "," + item.Value;
                        }
                    }
                }
            }
            
            foreach (ListItem item in lstempnamePayment_Create.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        getSelectedPayment = true;
                        if (getEmpPayment == "")
                        {
                            getEmpPayment = item.Value;
                        }
                        else
                        {
                            getEmpPayment = getEmpPayment + "," + item.Value;
                        }
                    }
                }
            }

            if (getSelectedInvoice == true || getSelectedPayment == true)
            {
                bool Status = spm.InsertUpdateInvoicePaymentCreators(Convert.ToInt32(lstCostCenter.SelectedValue), Convert.ToInt32(sPotypeId), getEmpInvoice,getEmpPayment, HDInPEmpcode.Value);
            }
        }

        if (getSelectedInvoice == false && getSelectedPayment == false)
        {
            lblmessage.Text = "Select Employee Name";
            return;
        }

        lblmessage.Text = "";

        Response.Redirect("VSCB_InvoicePaymentMakerList.aspx");
    }

    #endregion

    #region PageMethods
    private void get_InvoicePaymentmaker_BindData()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        if (Convert.ToString(HDInPEmpcode.Value) == "99999999")
            spars[0].Value = "get_InvoicePaymentmakerCreate_dropdown_List_ACC";
        else
            spars[0].Value = "get_InvoicePaymentmakerCreate_dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = HDInPEmpcode.Value;

        spars[2] = new SqlParameter("@Prj_Dept_id", SqlDbType.VarChar);
        if (Request.QueryString.Count > 0)
        {
            spars[2].Value =Convert.ToString(Request.QueryString[0]);
        }
        else
        {
            spars[2].Value = DBNull.Value;
        }

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        ldsEmployees =  dsList;

        if (dsList.Tables[1].Rows.Count > 0)
        {
            gvMngTravelRqstList.DataSource = dsList.Tables[1];
            gvMngTravelRqstList.DataBind();
        }
        ligrd.Visible = true;

        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsList.Tables[2];
            lstCostCenter.DataTextField = "Tallycode";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind(); 
        }
        else
        {
            if (Request.QueryString.Count == 0)
            {
                lblmessage.Text = "There is no Cost Center pending to assign employee for Invoice and Payment creator.";
                ligrd.Visible = false;
                trvl_btnSave.Visible = false;
            }
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));


    }

    #endregion

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_InvoicePaymentMakerList.aspx");
    }
    protected void gvMngTravelRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ListBox lstempname_Create_Invoice = (e.Row.FindControl("lstempname_Create_Invoice") as ListBox);
            lstempname_Create_Invoice.DataSource = ldsEmployees.Tables[0];
            lstempname_Create_Invoice.DataTextField = "Emp_Name";
            lstempname_Create_Invoice.DataValueField = "Emp_Code";
            lstempname_Create_Invoice.DataBind();
            lstempname_Create_Invoice.Items.Insert(0, new ListItem("select Employee", "0"));

            ListBox lstempname_Create_Payment = (e.Row.FindControl("lstempname_Create_Payment") as ListBox);
            lstempname_Create_Payment.DataSource = ldsEmployees.Tables[0];
            lstempname_Create_Payment.DataTextField = "Emp_Name";
            lstempname_Create_Payment.DataValueField = "Emp_Code";
            lstempname_Create_Payment.DataBind();
            lstempname_Create_Payment.Items.Insert(0, new ListItem("select Employee", "0"));
        }
    }
    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsList = new DataSet();
        //SqlParameter[] spars = new SqlParameter[2];

        //spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        //spars[0].Value = "get_InvoicePaymentmakerCreate_dropdown_List";

        //spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        //spars[1].Value = HDInPEmpcode.Value;

        //dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        //ldsEmployees = dsList;
        //if (dsList.Tables[1].Rows.Count > 0)
        //{
        //    gvMngTravelRqstList.DataSource = dsList.Tables[1];
        //    gvMngTravelRqstList.DataBind();
        //}
        //getSelectedCostCenterEmp();
    }
    public void getSelectedCostCenterEmp()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Selected_Costcenter_Employee_list";

        spars[1] = new SqlParameter("@Prj_Dept_id", SqlDbType.Int);
        spars[1].Value = hdnPrjDeptId.Value; //lstCostCenter.SelectedValue;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
      

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsList.Tables[0].Rows.Count; irow++)
                {
                    foreach (GridViewRow crow in gvMngTravelRqstList.Rows)
                    {
                        string sPotypeId = Convert.ToString(gvMngTravelRqstList.DataKeys[crow.RowIndex].Values[0]).Trim();
                        ListBox lstempname_Create_Invoice = (ListBox)crow.FindControl("lstempname_Create_Invoice");
                        ListBox lstempname_Create_Payment = (ListBox)crow.FindControl("lstempname_Create_Payment");

                        if (Convert.ToString(dsList.Tables[0].Rows[irow]["POTypeID"]).Trim() == sPotypeId)
                        {
                            if (Convert.ToString(dsList.Tables[0].Rows[irow]["Type"]).Trim() == "Invoice")
                            {
                                var getEmp = dsList.Tables[0].Rows[irow]["Emp_Code"].ToString();

                                if (getEmp != null && getEmp != "")
                                {
                                    var splitEmp = getEmp.Split(',');
                                    foreach (var item in splitEmp)
                                    {
                                        var getV = item.ToString();
                                        foreach (ListItem itm in lstempname_Create_Invoice.Items)
                                        {
                                            if (itm.Value == getV)
                                            {
                                                itm.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }

                            if (Convert.ToString(dsList.Tables[0].Rows[irow]["Type"]).Trim() == "Payment")
                            {
                                var getEmp = dsList.Tables[0].Rows[irow]["Emp_Code"].ToString();

                                if (getEmp != null && getEmp != "")
                                {
                                    var splitEmp = getEmp.Split(',');
                                    foreach (var item in splitEmp)
                                    {
                                        var getV = item.ToString();
                                        foreach (ListItem itm in lstempname_Create_Payment.Items)
                                        {
                                            if (itm.Value == getV)
                                            {
                                                itm.Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if (dsList.Tables[1].Rows.Count > 0)
            {
                lstCostCenter.DataSource = dsList.Tables[1];
                lstCostCenter.DataTextField = "Tallycode";
                lstCostCenter.DataValueField = "Dept_ID";
                lstCostCenter.DataBind();
                lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));
                lstCostCenter.SelectedValue = hdnPrjDeptId.Value;
            }
    }
    }
    
}