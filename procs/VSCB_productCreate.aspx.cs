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

public partial class procs_VSCB_productCreate : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string StrEmpCode = "";
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

            StrEmpCode = Convert.ToString(Session["Empcode"]).Trim();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtGST.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    TxtIGST.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    get_myPOWOMilestone_DropdownList();
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    private void get_myPOWOMilestone_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Product_dropdown_ListCreate";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = StrEmpCode;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstproductName.DataSource = dsList.Tables[0];
            lstproductName.DataTextField = "Item_detail";
            lstproductName.DataValueField = "Item_details";
            lstproductName.DataBind();

            
        }
        if (dsList.Tables[1].Rows.Count > 0)
        {
            LStUNitOfMeasurement.DataSource = dsList.Tables[1];
            LStUNitOfMeasurement.DataTextField = "UOM";
            LStUNitOfMeasurement.DataValueField = "UOMS";
            LStUNitOfMeasurement.DataBind();
        }
        lstproductName.Items.Insert(0, new ListItem("Select Product Name", "0"));
        LStUNitOfMeasurement.Items.Insert(0, new ListItem("Select UOM", "0"));
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
          #region

        if (lstproductName.SelectedValue =="0" || lstproductName.SelectedValue == "")
        {
            lblmessage.Text = "Select Product";
            return;
        }
        if (LStUNitOfMeasurement.SelectedValue == "0" || LStUNitOfMeasurement.SelectedValue == "") 
        {
            lblmessage.Text = "Select Unit Of Measurement";
            return;
        }
        if (txtDate.Text.Trim() =="")
        {
            lblmessage.Text = "Select Applicable From Date"; 
            return; 
        }

        if (txtDate.Text.Trim() == HDDate.Value)
        {
            lblmessage.Text = "Change Applicable From Date";
            return;
        }

        if (lstproductName.SelectedValue == "Others")
        {
            if (txtproductName.Text.Trim() == "") 
            {
                lblmessage.Text = "Enter Product Name";
                return;
            }
        }

        if (LStUNitOfMeasurement.SelectedValue == "Others")
        {
            if (Txt_UnitofMeasurement.Text.Trim() == "") 
            {
                lblmessage.Text = "Enter Unit of Measurement";
                return;
            }
        }

        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Product_dropdown_ListCreate";
        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (txtproductName.Text.Trim() != "")
        {
            DataRow[] drr1 = dsList.Tables[0].Select("Item_detail ='" + txtproductName.Text.Trim() + "'");
            if (drr1.Length != 0)
            {
                lblmessage.Text = "Already Exit Product";
                return;
            }
        }
        if (Txt_UnitofMeasurement.Text.Trim() != "")
        {
            DataRow[] drr1 = dsList.Tables[1].Select("UOM ='" + Txt_UnitofMeasurement.Text.Trim() + "'");
            if (drr1.Length != 0)
            {
                lblmessage.Text = "Already Exit Unit of Measurement";
                return;
            }
        }

        #endregion

        string strtoDate = "";
        string[] strdate;

        strdate = Convert.ToString(txtDate.Text).Trim().Split('-');
        strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            DataSet DS = new DataSet();
            SqlParameter[] spars1 = new SqlParameter[8];

            spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars1[0].Value = "Insert_ProductMaster";

            spars1[1] = new SqlParameter("@Item_detail", SqlDbType.VarChar);

        if (lstproductName.SelectedValue == "Others")
        {
            spars1[1].Value = txtproductName.Text.Trim();
        }
        else
        {
            spars1[1].Value = lstproductName.SelectedValue;
        }

            spars1[2] = new SqlParameter("@UOM", SqlDbType.VarChar);

        if (LStUNitOfMeasurement.SelectedValue == "Others")
        {
            spars1[2].Value = Txt_UnitofMeasurement.Text.Trim();
        }
        else
        {
            spars1[2].Value = LStUNitOfMeasurement.SelectedValue;
        }

        

            spars1[3] = new SqlParameter("@HSN_SAC_Code", SqlDbType.VarChar);
            spars1[3].Value = txtHSNSACCode.Text.Trim();

            spars1[4] = new SqlParameter("@GST_Per", SqlDbType.Decimal);
            if (txtGST.Text.Trim() == "")
            {
            spars1[4].Value = DBNull.Value;
            }
            else
            {
            spars1[4].Value = Convert.ToDecimal(txtGST.Text);
            }

            spars1[5] = new SqlParameter("@IGST_Per", SqlDbType.Decimal);
            
            if (TxtIGST.Text.Trim() == "")
            {
              spars1[5].Value = DBNull.Value;
            }
           else
            {
              spars1[5].Value = Convert.ToDecimal(TxtIGST.Text);
            }

            spars1[6] = new SqlParameter("@Start_Date", SqlDbType.VarChar);
            spars1[6].Value = strtoDate;

            spars1[7] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars1[7].Value = Convert.ToString(Session["Empcode"]);

        DS = spm.getDatasetList(spars1, "SP_VSCB_CreateProduct");

            Response.Redirect("VSCB_ProductList.aspx");
    }

    #endregion

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_ProductList.aspx");

    }

    protected void lstproductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstproductName.SelectedItem.Text == "Others")
        {
            SpanProductName.Visible = true;
            txtproductName.Visible = true;
            //lstproductName.SelectedValue = "0";
            LStUNitOfMeasurement.SelectedValue = "0";
            txtHSNSACCode.Text = "";
            txtGST.Text = "";
            TxtIGST.Text = "";
            txtDate.Text = "";
            gvMngTravelRqstList.Visible = false;
            RecordCount.Visible = false;
        }
        else
        {
            SpanProductName.Visible = false;
            txtproductName.Visible = false;
            txtproductName.Text = "";
            DataBindFromDBONProduct();
            getMngTravelReqstList();

        }
    }

    private void getMngTravelReqstList()
    {
        try
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_msgProductList";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = StrEmpCode;

            spars[2] = new SqlParameter("@spaymentId", SqlDbType.VarChar);
            if (Convert.ToString(lstproductName.SelectedValue).Trim() != "0")
                spars[2].Value = lstproductName.SelectedItem.Text.Trim();
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@POType", SqlDbType.VarChar);
            if (Convert.ToString(LStUNitOfMeasurement.SelectedValue).Trim() != "0")
                spars[3].Value = LStUNitOfMeasurement.SelectedItem.Text.Trim(); 
            else
                spars[3].Value = DBNull.Value;

            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();

            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {

                gvMngTravelRqstList.DataSource = dsmyInvoice.Tables[0];
                gvMngTravelRqstList.DataBind();
                gvMngTravelRqstList.Visible = true;
                RecordCount.Visible = true;
            }
            else
            {
                gvMngTravelRqstList.Visible = false;
                RecordCount.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void LStUNitOfMeasurement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LStUNitOfMeasurement.SelectedItem.Text == "Others")
        {
            SpanUnitofMeasurement.Visible = true;
            Txt_UnitofMeasurement.Visible = true;
        }
        else
        {
            SpanUnitofMeasurement.Visible = false;
            Txt_UnitofMeasurement.Visible = false;
            Txt_UnitofMeasurement.Text = "";
        }
    }

    private void DataBindFromDBONProduct()
    {
        DataSet DSDataBind = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "ProductsMasterSelectRecord";

        spars[1] = new SqlParameter("@POType", SqlDbType.VarChar);
        spars[1].Value = lstproductName.SelectedItem.Text.Trim();

        DSDataBind = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (DSDataBind.Tables[0].Rows.Count > 0)
        {
            lstproductName.SelectedValue = DSDataBind.Tables[0].Rows[0]["Item_detail"].ToString();
            LStUNitOfMeasurement.SelectedValue = DSDataBind.Tables[0].Rows[0]["UOM"].ToString();
            txtHSNSACCode.Text = DSDataBind.Tables[0].Rows[0]["HSN_SAC_Code"].ToString();
            txtGST.Text = DSDataBind.Tables[0].Rows[0]["GST_Per"].ToString();
            TxtIGST.Text = DSDataBind.Tables[0].Rows[0]["IGST_Per"].ToString();
            txtDate.Text = DSDataBind.Tables[0].Rows[0]["Start_Datee"].ToString();
            HDDate.Value = DSDataBind.Tables[0].Rows[0]["Start_Datee"].ToString();
        }
    }
}