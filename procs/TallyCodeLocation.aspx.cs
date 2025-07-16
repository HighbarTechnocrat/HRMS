using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_TallyCodeLocation : System.Web.UI.Page
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
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }

    #endregion
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                // Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    GetLocationMaster();
                    GetReq_LocationList();
                    GetLocationEnter();
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
    #region PageMethods


    public void GetLocationMaster()
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Get_LocationList";
        DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
        if (DS.Tables[0].Rows.Count > 0)
        {             
            lstSearchLocation.DataSource = DS.Tables[0];
            lstSearchLocation.DataTextField = "Location_name";
            lstSearchLocation.DataValueField = "comp_code";
            lstSearchLocation.DataBind();
            lstSearchLocation.Items.Insert(0, new ListItem("Select Location", "0"));
        }
    }

    public void GetLocationEnter()
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Get_Location_Enter";
        DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
        if (DS.Tables[0].Rows.Count > 0)
        {
            DDL_Location.DataSource = DS.Tables[0];
            DDL_Location.DataTextField = "Location_name";
            DDL_Location.DataValueField = "comp_code";
            DDL_Location.DataBind();
            DDL_Location.Items.Insert(0, new ListItem("Select Location", "0"));
             
        }
    }

    private void GetReq_LocationList()
    {
        try
        {
            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_LocationList";
            DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = DS.Tables[0];
                    gvMngTravelRqstList.DataBind();
                    lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                    lblmessage.Text = "";
                }
            }
            else
            {
                lbltotalRecords.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion
    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        if (lstSearchLocation.SelectedValue == "0")
        {
            GetReq_LocationList(); 
        }
        else
        {
            Searchmethod();
        }
    }

    public void Searchmethod()
    {
        try
        {
            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_LocationList";
            spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
            spars[1].Value = lstSearchLocation.SelectedValue;
            DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = DS.Tables[0];
                    gvMngTravelRqstList.DataBind();
                    lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                    lblmessage.Text = "";
                }
            }
            else
            {
                // lbltotalRecords.Text = "";
                lblmessage.Text = "Record not available";
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HDModuleID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_LocationData";
            spars[1] = new SqlParameter("@comp_codeUpdate", SqlDbType.VarChar);
            spars[1].Value = HDModuleID.Value;
            DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
            if (DS.Tables[0].Rows.Count > 0)
            {
                Txt_Skillset.Text = DS.Tables[0].Rows[0]["TallyCode"].ToString();
                hdnTxt_Skillset.Value = DS.Tables[0].Rows[0]["TallyCode"].ToString();
                DDL_Location.SelectedValue = DS.Tables[0].Rows[0]["comp_code"].ToString();
                ddl_department.SelectedValue = DS.Tables[0].Rows[0]["Department"].ToString();
                mobile_btnSave.Text = "Update Tally Code";
                DDL_Location.Enabled=false;
               // ddl_department.Enabled = false;

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void Link_BtnSearch_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void Link_BtnSearchClear_Click(object sender, EventArgs e)
    {
        lstSearchLocation.SelectedIndex = -1; 
        GetReq_LocationList();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (DDL_Location.SelectedValue == "" || DDL_Location.SelectedValue == "0")
        {
            lblmessage.Text = "Select Location"; 
            return;
        }
        if (Txt_Skillset.Text.Trim() == "")
        {
            lblmessage.Text = "Enter Tally Code";
            return;
        }

        if (ddl_department.SelectedValue == "" || ddl_department.SelectedValue == "0")
        {
            lblmessage.Text = "Select Department";
            return;
        }

        if (lblmessage.Text == "")
        {
            SqlParameter[] spars = new SqlParameter[7];
            DataSet DS = new DataSet();
            string department = ddl_department.SelectedValue;

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);


            if (HDModuleID.Value == "")
            {
                spars[0].Value = "INSERT";
                spars[1] = new SqlParameter("@TallyCode", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(Txt_Skillset.Text).Trim();
                spars[2] = new SqlParameter("@comp_codeUpdate", SqlDbType.VarChar);
                spars[2].Value = DDL_Location.SelectedValue;
                spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();
                spars[4] = new SqlParameter("@Department", SqlDbType.VarChar);
                spars[4].Value = Convert.ToString(department);
            }

            else
            {
                //if (string.IsNullOrEmpty(hdnTxt_Skillset.Value))
                //{
                    spars[0].Value = "Update";
                        spars[1] = new SqlParameter("@TallyCode", SqlDbType.VarChar);
                        spars[1].Value = Convert.ToString(Txt_Skillset.Text).Trim();
                        spars[2] = new SqlParameter("@comp_codeUpdate", SqlDbType.VarChar);
                        spars[2].Value = HDModuleID.Value;
                        spars[3] = new SqlParameter("@comp_code", SqlDbType.VarChar);
                        spars[3].Value = DDL_Location.SelectedValue;
                        spars[4] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToString(Session["Empcode"]).Trim();
                        spars[5] = new SqlParameter("@Department", SqlDbType.VarChar);
                        spars[5].Value = Convert.ToString(department);
                        spars[6] = new SqlParameter("@TallyCodeold", SqlDbType.VarChar);
                        spars[6].Value = hdnTxt_Skillset.Value;

              //  }

                //else
                //{
                //    lblmessage.Text = "TallyCode already exists.";
                //}
                
            }
            DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");



            //ddl_department.ClearSelection();
            Searchmethod();
            if (DS.Tables[0].Rows.Count > 0)
            {
                string strRecords = DS.Tables[0].Rows[0]["Record"].ToString();

                if (hdnTxt_Skillset.Value != "")
                {
                    if (strRecords == "1")
                    {
                        lblmessage.Text = "TallyCode Updated successfully";
                        //DDL_Location.SelectedValue = "0";
                        //Txt_Skillset.Text = "";
                    }
                    else
                    {
                        lblmessage.Text = "TallyCode Already Exit";
                        //DDL_Location.SelectedValue = "0";
                        //Txt_Skillset.Text = "";
                    }
                }

                if (HDModuleID.Value != "")
                {
                    if (strRecords == "1")
                    {
                        lblmessage.Text = "TallyCode Updated successfully";
                        //DDL_Location.SelectedValue = "0";
                        //Txt_Skillset.Text = "";
                    }
                    else
                    {
                        lblmessage.Text = "TallyCode Already Exit";
                        //DDL_Location.SelectedValue = "0";
                        //Txt_Skillset.Text = "";
                    }
                }
                else
                {
                    if (strRecords == "1")
                    {
                        lblmessage.Text = "TallyCode added successfully";
                        DDL_Location.SelectedValue = "0";
                        Txt_Skillset.Text = "";
                        ddl_department.ClearSelection();
                    }
                    else
                    {
                        lblmessage.Text = "TallyCode Already Exit";
                        //DDL_Location.SelectedValue = "0";
                        //Txt_Skillset.Text = "";
                    }
                }
            }

            Txt_Skillset.Text = "";
            hdnTxt_Skillset.Value = "";
            HDModuleID.Value = "";
            GetLocationEnter();
            ddl_department.ClearSelection();
        }
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        Txt_Skillset.Text = "";
        DDL_Location.SelectedValue = "0";
        HDModuleID.Value = "";
        Searchmethod();
        mobile_btnSave.Text = "Add Tally Code";
        ddl_department.ClearSelection();
        DDL_Location.Enabled = true;

    }

    protected void DDL_Location_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Get_LocationData";
        spars[1] = new SqlParameter("@comp_codeUpdate", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(DDL_Location.SelectedValue) ;
        DS = spm.getDatasetList(spars, "SP_Add_TallyCode_Location");
        if (DS.Tables[0].Rows.Count > 0)
        {
            Txt_Skillset.Text = DS.Tables[0].Rows[0]["TallyCode"].ToString();
            hdnTxt_Skillset.Value = DS.Tables[0].Rows[0]["TallyCode"].ToString();
            DDL_Location.SelectedValue = DS.Tables[0].Rows[0]["comp_code"].ToString();
            ddl_department.SelectedValue = DS.Tables[0].Rows[0]["Department"].ToString();
            mobile_btnSave.Text = "Update Tally Code";
            DDL_Location.Enabled = false;
            HDModuleID.Value = Convert.ToString(DDL_Location.SelectedValue);


        }
    }
}