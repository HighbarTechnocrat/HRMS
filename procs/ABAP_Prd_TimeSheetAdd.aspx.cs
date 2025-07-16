using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
//using System.Windows.Forms;

public partial class procs_ABAP_Prd_TimeSheetAdd : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    
    SP_Methods spm = new SP_Methods();
    string strempcode = ""; int AddnewCheck=0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/abap_index");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    CheckABAPRights();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Get_RecordsEdit();
                       // hdnProjectRowCnt.Value = hdnId.Value;
                    }
                    else
                    {
                        AddnewCheck = 1;
                        Get_EmployeeInfo();
                    }

                }
            }
        }
        catch (Exception ex)
        {
           // ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods

    public void CheckABAPRights()
    {

        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparsd[0].Value = "GetApproverPageButton";
        sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
        DataSet DSApprover = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");

        if (DSApprover.Tables[0].Rows.Count == 0)
        {
            Response.Redirect("~/procs/Timesheet.aspx");
        }
    }

    public void Get_EmployeeInfo()
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetEmployeeData";
        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

        if (DS.Tables[0].Rows.Count > 0)
        {
            txtEmpCode.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Code"]).ToString();
            txtEmpName.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Name"]).ToString();
            txtDepartment.Text = Convert.ToString(DS.Tables[0].Rows[0]["Department_Name"]).ToString();
            txtDesignation.Text = Convert.ToString(DS.Tables[0].Rows[0]["DesginationName"]).ToString();
        }

        if (DS.Tables[1].Rows.Count > 0)
        {
            dgTimesheet.DataSource = DS.Tables[1];
            dgTimesheet.DataBind();
            Int32 irowcnt = 1;
            foreach (GridViewRow row in dgTimesheet.Rows)
            {
                if (irowcnt < 6)
                {
                    row.Visible = true;
                    hdnProjectRowCnt.Value = Convert.ToString(irowcnt);
                }
                else
                {
                    row.Visible = false;
                }

                irowcnt += 1;
            }
        }

    }

    public void Get_RecordsEdit()
    {
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetRecordEdit";
        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;
        spars[2] = new SqlParameter("@MainID", SqlDbType.Int);
        spars[2].Value = hdnId.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

        if (DS.Tables[4].Rows.Count > 0)
        {
            dgTimesheetView.Visible = false;
            txtEmpCode.Text = Convert.ToString(DS.Tables[4].Rows[0]["Emp_Code"]).ToString();
            txtEmpName.Text = Convert.ToString(DS.Tables[4].Rows[0]["Emp_Name"]).ToString();
            txtDepartment.Text = Convert.ToString(DS.Tables[4].Rows[0]["Department_Name"]).ToString();
            txtDesignation.Text = Convert.ToString(DS.Tables[4].Rows[0]["DesginationName"]).ToString();
            txtFromdate.Text = Convert.ToString(DS.Tables[4].Rows[0]["Start_Datee"]).ToString();
            txtToDate.Text = Convert.ToString(DS.Tables[4].Rows[0]["End_datee"]).ToString();
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;

        }
        // After approver Grid Bind only View
        if (DS.Tables[1].Rows.Count > 0)
        {
            dgTimesheet.Visible = false;
            trvldeatils_btnSave.Visible = false;
            mobile_btnSave.Visible = false;

            dgTimesheetView.Visible = true;
            dgTimesheetView.DataSource = DS.Tables[2];
            dgTimesheetView.DataBind();

        }
        else // befor Approver grid Bind
        {
            hdnProjectRowCnt.Value = Convert.ToString(DS.Tables[3].Rows.Count);

            dgTimesheet.DataSource = DS.Tables[0];
            dgTimesheet.DataBind();
            Int32 irowcnt = 0;
            foreach (GridViewRow row in dgTimesheet.Rows)
            {
                if (irowcnt < Convert.ToInt32(hdnProjectRowCnt.Value))
                {
                    row.Visible = true;
                   // hdnProjectRowCnt.Value = Convert.ToString(irowcnt);
                }
                else
                {
                    row.Visible = false;
                }

                irowcnt += 1;
            }
        }

       

    }

    #endregion
    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AddnewCheck = 1;

            Int32 irowcnt = 0;
            Int32 irw = 1;
            if (Convert.ToString(hdnProjectRowCnt.Value).Trim() != "")
                irowcnt = Convert.ToInt32(hdnProjectRowCnt.Value);

            lblMilestoneCostCenter_Err.Text = "";
            if (irowcnt > 0)
            {
                irowcnt += 1;
                foreach (GridViewRow row in dgTimesheet.Rows)
                {
                    if (irowcnt == irw)
                    {
                        row.Visible = true;
                        hdnProjectRowCnt.Value = Convert.ToString(irowcnt);
                        break;
                    }
                    else
                    {
                        DropDownList lstProjectName = (DropDownList)row.FindControl("lstProjectName");
                        if (Convert.ToString(lstProjectName.SelectedValue).Trim() == "0")
                        {
                            lblMilestoneCostCenter_Err.Text = "Please Select All Project";
                            return;
                        }
                        irw += 1;
                    }
                }
                
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void dgTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList lstProjectName = (e.Row.FindControl("lstProjectName") as DropDownList);
            DropDownList lstIsCompleted = (e.Row.FindControl("lstIsCompleted") as DropDownList); 
            DropDownList lstObjectType = (e.Row.FindControl("lstObjectType") as DropDownList);

            DataSet dsCostcenters = GetProjectList_list();
            lstProjectName.DataSource = dsCostcenters.Tables[0];
            lstProjectName.DataTextField = "LocationCompany";
            lstProjectName.DataValueField = "comp_code";
            lstProjectName.DataBind();
            //Add Default Item in the DropDownList
            lstProjectName.Items.Insert(0, new ListItem("Select Project Name", "0"));

            lstObjectType.DataSource = dsCostcenters.Tables[1];
            lstObjectType.DataTextField = "Object_TypeName";
            lstObjectType.DataValueField = "Object_TypeId";
            lstObjectType.DataBind();
            //Add Default Item in the DropDownList
            lstObjectType.Items.Insert(0, new ListItem("Select Object Type", "0"));

            lstIsCompleted.DataSource = dsCostcenters.Tables[2];
            lstIsCompleted.DataTextField = "ISCompleted";
            lstIsCompleted.DataValueField = "ISCompleted";
            lstIsCompleted.DataBind();
            //Add Default Item in the DropDownList
            //lstObjectType.Items.Insert(0, new ListItem("Select Object Type", "0"));


            if (AddnewCheck == 1)
            {
                string txtObjectCode = (e.Row.FindControl("txtObjectCode") as TextBox).Text;
                if (txtObjectCode.Trim() != "")
                {
                    string lblprojectname = (e.Row.FindControl("lblprojectname") as Label).Text;
                    if (lblprojectname != "")
                    {
                        lstProjectName.Items.FindByValue(lblprojectname).Selected = true;
                    }
                    string lblIsCompleted = (e.Row.FindControl("lblIsCompleted") as Label).Text;
                    if (lblIsCompleted != "")
                    {
                        lstIsCompleted.Items.FindByValue(lblIsCompleted).Selected = true;
                    }

                    string lblObjectype = (e.Row.FindControl("lblobjectType") as Label).Text;
                    if (lblObjectype != "")
                    {
                        lstObjectType.Items.FindByValue(lblObjectype).Selected = true;
                    }
                }
            }
            else
            {
                //Select the Country of Customer in DropDownList
                string lblprojectname = (e.Row.FindControl("lblprojectname") as Label).Text;
                if (lblprojectname != "")
                {
                    lstProjectName.Items.FindByValue(lblprojectname).Selected = true;
                }
                string lblIsCompleted = (e.Row.FindControl("lblIsCompleted") as Label).Text;
                if (lblIsCompleted != "")
                {
                    lstIsCompleted.Items.FindByValue(lblIsCompleted).Selected = true;
                }

                string lblObjectype = (e.Row.FindControl("lblobjectType") as Label).Text;
                if (lblObjectype != "")
                {
                    lstObjectType.Items.FindByValue(lblObjectype).Selected = true;
                }

            }
            
        }
    }

    public DataSet GetProjectList_list()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetLocationProject";
        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");
        return DS;
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblMilestoneCostCenter_Err.Text = "Please Select Week Start date.";
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblMilestoneCostCenter_Err.Text = "Please Select Week End Date.";
            return;
        }

        Boolean blncheck = false;
        foreach (GridViewRow crow in dgTimesheet.Rows)
        {
            DropDownList lstProjectName = (DropDownList)crow.FindControl("lstProjectName");
            DropDownList lstIsCompleted = (DropDownList)crow.FindControl("lstIsCompleted");
            TextBox txtObjectCode = (TextBox)crow.FindControl("txtObjectCode");
            TextBox TxtObjectDescription = (TextBox)crow.FindControl("TxtObjectDescription");
            DropDownList lstObjectType = (DropDownList)crow.FindControl("lstObjectType");

           
            //Check Project Name is Selected then Validate all field
            if (Convert.ToString(lstProjectName.SelectedValue).Trim() != "0")
            {
                blncheck = true;
                if (Convert.ToString(txtObjectCode.Text).Trim()=="")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Name (TCode)";
                    return;
                }
                if (Convert.ToString(lstObjectType.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Object Type";
                    return;
                }
                if (Convert.ToString(TxtObjectDescription.Text).Trim() == "")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Description";
                    return;
                }
            }

            //Check Object Code is Selected then Validate all field
            if (Convert.ToString(txtObjectCode.Text).Trim() != "")
            {
                if (Convert.ToString(lstProjectName.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Project Name";
                    return;
                }
                if (Convert.ToString(lstObjectType.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Object Type";
                    return;
                }
                if (Convert.ToString(TxtObjectDescription.Text).Trim() == "")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Description";
                    return;
                }
            }

            //Check Object Type is Selected then Validate all field
            if (Convert.ToString(lstObjectType.SelectedValue).Trim() != "0")
            {
                if (Convert.ToString(lstProjectName.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Project Name";
                    return;
                }
                if (Convert.ToString(txtObjectCode.Text).Trim() == "")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Name (TCode)";
                    return;
                }
                if (Convert.ToString(TxtObjectDescription.Text).Trim() == "")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Description";
                    return;
                }
            }

            //Check Object Description is enter then Validate all field
            if (Convert.ToString(TxtObjectDescription.Text).Trim() != "")
            {
                if (Convert.ToString(lstProjectName.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Project Name";
                    return;
                }
                if (Convert.ToString(txtObjectCode.Text).Trim() == "")
                {
                    lblMilestoneCostCenter_Err.Text = "Please enter Object Name (TCode)";
                    return;
                }
                if (Convert.ToString(lstObjectType.SelectedValue).Trim() == "0")
                {
                    lblMilestoneCostCenter_Err.Text = "Please select Object Type";
                    return;
                }
            }

        }

        if (blncheck == false)
        {
            lblMilestoneCostCenter_Err.Text = "Please check Object Completion Sheet";
            return;
        }
            int isrno = 1;

        DataTable dt_duplicatefind = new DataTable();
        dt_duplicatefind.Columns.Add();
        dt_duplicatefind.Columns.Add();
        dt_duplicatefind.Columns.Add();

        foreach (GridViewRow crow in dgTimesheet.Rows)
        {
            DropDownList lstProjectName = (DropDownList)crow.FindControl("lstProjectName");
            DropDownList lstObjectType = (DropDownList)crow.FindControl("lstObjectType");
            TextBox      txtObjectCode = (TextBox)crow.FindControl("txtObjectCode");

            if (Convert.ToString(lstProjectName.SelectedValue) != "0" && Convert.ToString(lstObjectType.SelectedValue) != "0" && Convert.ToString(txtObjectCode.Text).Trim() !="")
            {
                dt_duplicatefind.Rows.Add(lstProjectName.SelectedValue.ToString(), lstObjectType.SelectedValue.ToString(), txtObjectCode.Text.Trim());
             }
        }

        lblMilestoneCostCenter_Err.Text = "";
        for (int ii = 0; ii < dt_duplicatefind.Rows.Count; ii++)
        {

            DataRow[] result = dt_duplicatefind.Select("Column1 = '" + dt_duplicatefind.Rows[ii]["Column1"].ToString() + "' AND Column2 = '" + dt_duplicatefind.Rows[ii]["Column2"].ToString() + "' AND Column3= '" + dt_duplicatefind.Rows[ii]["Column3"].ToString() + "' ");
            if (result.Length > 1)
            {
                lblMilestoneCostCenter_Err.Text = "Please Don't Select Duplicate values (Project Name , Object Type, Enter Object Code)";
                return;
            }
        }
          //var duplicates = dt_duplicatefind.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();

            string[] strdate;
        string strtoDate = "";
        string strfromDate_tt = "";

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion

        if (blncheck == true)
        {
            #region Insert/Update record


            string StrMainID = "";
            if (Convert.ToString(hdnId.Value).Trim() == "")
            {
                SqlParameter[] sparsd = new SqlParameter[4];
                sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsd[0].Value = "Insert";
                sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
                sparsd[2] = new SqlParameter("@StartDate", SqlDbType.VarChar);
                sparsd[2].Value = strfromDate_tt;
                sparsd[3] = new SqlParameter("@EndDate", SqlDbType.VarChar);
                sparsd[3].Value = strtoDate;
                DataSet DSInsert = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");
                StrMainID = Convert.ToString(DSInsert.Tables[0].Rows[0]["MAXIDD"]).ToString();
            }
            else
            {
                StrMainID = hdnId.Value;
                SqlParameter[] sparsd = new SqlParameter[2];
                sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsd[0].Value = "DeleteRecord";
                sparsd[1] = new SqlParameter("@MainID", SqlDbType.VarChar);
                sparsd[1].Value = hdnId.Value;
                DataSet DSDelete = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");
            }

            foreach (GridViewRow crow in dgTimesheet.Rows)
            {
                DropDownList lstProjectName = (DropDownList)crow.FindControl("lstProjectName");
                DropDownList lstIsCompleted = (DropDownList)crow.FindControl("lstIsCompleted");
                TextBox txtObjectCode = (TextBox)crow.FindControl("txtObjectCode");
                TextBox TxtObjectDescription = (TextBox)crow.FindControl("TxtObjectDescription");
                DropDownList lstObjectType = (DropDownList)crow.FindControl("lstObjectType");
                Label lblIDEdit = (Label)crow.FindControl("lblIDEdit");
                Label lblAppr_Status = (Label)crow.FindControl("lblAppr_Status");

                string strDetailIDReference = lblIDEdit.Text;

                if (Convert.ToString(lstProjectName.SelectedValue).Trim() != "0")
                {
                    if (Convert.ToString(txtObjectCode.Text).Trim() != "" && Convert.ToString(TxtObjectDescription.Text).Trim() != "")
                    {
                        SqlParameter[] spars = new SqlParameter[9];
                        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                        spars[0].Value = "InsertDetail";

                        spars[1] = new SqlParameter("@Comp_Code", SqlDbType.VarChar);
                        spars[1].Value = Convert.ToString(lstProjectName.SelectedValue);

                        spars[2] = new SqlParameter("@MainID", SqlDbType.Int);
                        spars[2].Value = StrMainID;

                        spars[3] = new SqlParameter("@Sr_No", SqlDbType.VarChar);
                        spars[3].Value = Convert.ToString(isrno);

                        spars[4] = new SqlParameter("@IsCompleted", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToString(lstIsCompleted.SelectedValue);

                        spars[5] = new SqlParameter("@ObjectCode", SqlDbType.VarChar);
                        if (Convert.ToString(txtObjectCode.Text.Trim()) != "")
                            spars[5].Value = Convert.ToString(txtObjectCode.Text.Trim());
                        else
                            spars[5].Value = DBNull.Value;

                        spars[6] = new SqlParameter("@ObjectDescription", SqlDbType.VarChar);
                        if (Convert.ToString(TxtObjectDescription.Text.Trim()) != "")
                            spars[6].Value = Convert.ToString(TxtObjectDescription.Text.Trim());
                        else
                            spars[6].Value = DBNull.Value;

                        spars[7] = new SqlParameter("@ObjectType", SqlDbType.Int);
                        if (Convert.ToString(lstObjectType.SelectedValue).Trim() != "")
                            spars[7].Value = Convert.ToInt32(lstObjectType.SelectedValue);
                        else
                            spars[7].Value = DBNull.Value;

                        spars[8] = new SqlParameter("@DetailIDReference", SqlDbType.Int);
                        if (lblAppr_Status.Text.Trim() == "")
                        {
                            spars[8].Value = "0";
                        }
                        else
                        {
                            spars[8].Value = strDetailIDReference;
                        }


                        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");
                        isrno += 1;
                    }
                }
            }


            SqlParameter[] sparmail = new SqlParameter[2];
            sparmail[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparmail[0].Value = "GetEmailABAPToApprover";
            sparmail[1] = new SqlParameter("@MainID", SqlDbType.VarChar);
            sparmail[1].Value = StrMainID;
            DataSet DSEmail = spm.getDatasetList(sparmail, "SP_ABAP_Productivity_CompletionSheet");
            string StrSubject = "OneHr :- ABAP Object Completion Sheet";

            string strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLinkABAP_Object"]).Trim() + "?id=" + StrMainID).Trim();

            if (DSEmail.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DSEmail.Tables[0].Rows.Count; i++)
                {
                    SqlParameter[] sparmaill = new SqlParameter[3];
                    sparmaill[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    sparmaill[0].Value = "GetEmailABAPToApprover";
                    sparmaill[1] = new SqlParameter("@MainID", SqlDbType.VarChar);
                    sparmaill[1].Value = StrMainID;
                    sparmaill[2] = new SqlParameter("@Comp_Code", SqlDbType.VarChar);
                    sparmaill[2].Value = Convert.ToString(DSEmail.Tables[0].Rows[i]["Comp_Code"]).ToString();
                    DataSet DSEmaill = spm.getDatasetList(sparmaill, "SP_ABAP_Productivity_CompletionSheet");

                    spm.SendEmailForABAPToApprover(DSEmaill, txtFromdate.Text.Trim(), txtToDate.Text.Trim(), StrSubject, Convert.ToString(txtEmpName.Text).Trim(), strInvoiceURL);
                }
            }

            #endregion

            Response.Redirect("~/procs/Timesheet.aspx");
        }

    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnId.Value).Trim()== "")
        {
            Response.Redirect("~/procs/Timesheet.aspx");
        }
        else
        {
            Response.Redirect("~/procs/ABAP_Prd_TimeSheetList.aspx");
        }
        
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMilestoneCostCenter_Err.Text = "";
            var getDate = txtFromdate.Text.ToString();
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;
            if (Today < SelectedDate)
            {
                lblMilestoneCostCenter_Err.Text = "Future Week Start Date Not Allowed for Object Completion";
                txtFromdate.Text = "";
            }
            else
            {
                string[] strdate;
                string strfromDate_tt = "";

                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }
                #endregion

                lblMilestoneCostCenter_Err.Text = "";
                SqlParameter[] spars = new SqlParameter[3];
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "DateBindWithValidation";
                spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[1].Value = hdnEmpCode.Value;
                spars[2] = new SqlParameter("@StartDate", SqlDbType.VarChar);
                spars[2].Value = strfromDate_tt;
                DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

                if (DS.Tables[1].Rows.Count > 0)
                {
                    txtFromdate.Text = Convert.ToString(DS.Tables[1].Rows[0]["WeekStartDate"]).ToString();
                }
                if (DS.Tables[0].Rows.Count > 0)
                {
                    txtToDate.Text = Convert.ToString(DS.Tables[0].Rows[0]["EndDate"]).ToString();
                    var getDateend = txtToDate.Text.ToString();
                    DateTime DateEnd = DateTime.ParseExact(getDateend, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (DateEnd <= SelectedDate)
                    {
                        lblMilestoneCostCenter_Err.Text = "Week Start Date not match with Week End Date.";
                        txtFromdate.Text = "";
                        return;
                    }
                }

                if (DS.Tables[2].Rows.Count > 0)
                {
                    if (Convert.ToString(DS.Tables[2].Rows[0]["Messge"]).Trim() != "")
                    {
                        lblMilestoneCostCenter_Err.Text = Convert.ToString(DS.Tables[2].Rows[0]["Messge"]).ToString();
                        txtFromdate.Text = "";
                        return;
                    }
                    else
                    {
                        lblMilestoneCostCenter_Err.Text = "";
                    }
                   
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private Boolean Validate_ABAP_CompletationSheet()
    {
        Boolean Validate_ABAP = false;
        try
        {
            string[] strdate;
            string strtoDate = "";
            string strfromDate_tt = "";

            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            SqlParameter[] sparsError = new SqlParameter[3];
            sparsError[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsError[0].Value = "DateValidationLastWeekCheck";
            sparsError[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsError[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            sparsError[2] = new SqlParameter("@TimeSheetDate", SqlDbType.VarChar);
            sparsError[2].Value = strfromDate_tt;
            DataSet DS = spm.getDatasetList(sparsError, "SP_ABAP_Productivity_CompletionSheet");

            if (DS.Tables[0].Rows[0]["MainID"].ToString() == "00")
            {
                Validate_ABAP = true;
            }

        }
        catch (Exception ex)
        {
        }
        return Validate_ABAP;
    }
     

}