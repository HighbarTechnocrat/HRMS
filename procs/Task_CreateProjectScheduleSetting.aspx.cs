using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Linq;

public partial class procs_Task_CreateProjectScheduleSetting : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    ProjectDuration();
                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                    {
                        HDID.Value = Request.QueryString["ID"];
                        ProjectDurationEdit(Convert.ToInt32(HDID.Value));
                        LIDeActivationRemarks.Visible = true;
                        LIDeActivationRemark1.Visible = true;
                    }
                    else
                    {
                        HDID.Value = "0";
                        LIDeActivationRemarks.Visible = false;
                        LIDeActivationRemark1.Visible = false;
                        chk_ISActive.Visible = false;
                        Labelchk_ISActive.Visible = false;
                    }

                    
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }


    public void ProjectDuration()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (HDID.Value == "0")
        {
            spars[0].Value = "GetDropDownProjectList";
        }
        else
        {
            spars[0].Value = "GetDropDownProjectListEdit";
        }
        

        DataTable dtTaskDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS");
        ddlProjectLocation.DataSource = dtTaskDetails;
        ddlProjectLocation.DataTextField = "LocationName";
        ddlProjectLocation.DataValueField = "comp_code";
        ddlProjectLocation.DataBind();
        ddlProjectLocation.Items.Insert(0, new ListItem("Select Project / Location Code", "0"));
    }

    public void ProjectDurationEdit( int ID)
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetProjectSettingEdit";

        spars[1] = new SqlParameter("@Id", SqlDbType.Int);
        spars[1].Value = ID;

        DataTable dt = spm.getDropdownList(spars, "SP_TASK_M_DETAILS");
        if (dt.Rows.Count > 0)
        {
            ddlProjectLocation.SelectedValue = dt.Rows[0]["Projectcode"].ToString();
            txt_ProjectStartDate.Text = dt.Rows[0]["StartDate"].ToString();
            txt_ProjectEndDate.Text = dt.Rows[0]["ENDDate"].ToString();
            HFDateendDB.Value = dt.Rows[0]["ENDDate"].ToString();
            Txt_Durationindays.Text = dt.Rows[0]["Duration_indays"].ToString();
            Txt_Taskcreationbeforedateindays.Text = dt.Rows[0]["TaskCreation_beforedateindays"].ToString();

            HFFrequencyDays.Value = dt.Rows[0]["Duration_indays"].ToString();
            HFCreationDays.Value = dt.Rows[0]["TaskCreation_beforedateindays"].ToString();


            txt_DeactivationRemark.Text = dt.Rows[0]["Deactivation_Remarks"].ToString();
            chk_ISActive.Enabled = true;
            ddlProjectLocation.Enabled = false;
            txt_ProjectStartDate.Enabled = false;
        }
     }

    protected void btn_ATT_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); 
            if (confirmValue != "Yes")
            {
                return;
            }

                if (ddlProjectLocation.SelectedValue == "0")
                {
                    lblmessage.Text = "Please select Project / Location Code";
                    return;
                }
                if (Convert.ToString(txt_ProjectStartDate.Text.Trim()) == "")
                {
                    lblmessage.Text = "Enter the Project Start Date";
                    return;
                }
                if (Convert.ToString(txt_ProjectEndDate.Text.Trim()) == "")
                {
                    lblmessage.Text = "Enter the Project End Date";
                    return;
                }
                if (Convert.ToString(Txt_Durationindays.Text.Trim()) == "")
                {
                    lblmessage.Text = "Enter the Frequency (in Days)";
                    return;
                }
                if (Convert.ToString(Txt_Taskcreationbeforedateindays.Text.Trim()) == "")
                {
                    lblmessage.Text = "Enter the Task Creation before due date (in Days)";
                    return;
                }

                var VarProjectStartDate = "";
                var VarProjectEndDate = "";
                if (txt_ProjectStartDate.Text.ToString() != "")
                {
                    DateTime temp = DateTime.ParseExact(txt_ProjectStartDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    VarProjectStartDate = temp.ToString("yyyy-MM-dd");
                }
                if (txt_ProjectEndDate.Text.ToString() != "")
                {
                    DateTime tempp = DateTime.ParseExact(txt_ProjectEndDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    VarProjectEndDate = tempp.ToString("yyyy-MM-dd");
                }

                string strQtype = "";

            if (HDID.Value == "0")
            {
                strQtype = "Insert_ProjectDuration";
            }
            else
            {
                if (Convert.ToString(txt_DeactivationRemark.Text.Trim()) == "")
                {
                    lblmessage.Text = "Enter the Remarks"; 
                    return;
                }
                if (HFDateendDB.Value == txt_ProjectEndDate.Text && HFFrequencyDays.Value == Txt_Durationindays.Text && HFCreationDays.Value == Txt_Taskcreationbeforedateindays.Text)
                {
                    strQtype = "Update_ProjectDuration";
                }
                else
                {
                    if (chk_ISActive.Checked == false)
                    {
                        lblmessage.Text = "Please Select Active Flag";
                        return;
                    }

                    strQtype = "InsertUpdate_ProjectDuration";
                }
            }

                SqlParameter[] spars = new SqlParameter[10];
                spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars[0].Value = strQtype;

                spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
                spars[1].Value = ddlProjectLocation.SelectedValue;

                spars[2] = new SqlParameter("@Project_StartDate", SqlDbType.VarChar);
                spars[2].Value = VarProjectStartDate;

                spars[3] = new SqlParameter("@Project_EndDate", SqlDbType.VarChar);
                spars[3].Value = VarProjectEndDate;

                spars[4] = new SqlParameter("@Duration_indays", SqlDbType.VarChar);
                spars[4].Value = Txt_Durationindays.Text.Trim();

                spars[5] = new SqlParameter("@TaskCreation_beforedateindays", SqlDbType.VarChar);
                spars[5].Value = Txt_Taskcreationbeforedateindays.Text.Trim();

                spars[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                spars[6].Value = Convert.ToString(Session["Empcode"]).Trim();

                spars[7] = new SqlParameter("@Id", SqlDbType.Int);
                spars[7].Value = Convert.ToInt32(HDID.Value);

                spars[8] = new SqlParameter("@DeactiveDiscription", SqlDbType.VarChar);
                spars[8].Value = txt_DeactivationRemark.Text;

            if (chk_ISActive.Checked == true)
            {
                spars[9] = new SqlParameter("@ISActiveflag", SqlDbType.VarChar);
                spars[9].Value = "A";
            }
            else
            {
                spars[9] = new SqlParameter("@ISActiveflag", SqlDbType.VarChar);
                spars[9].Value = "D";
            }
           
                

            DataTable dt = spm.getDropdownList(spars, "SP_TASK_M_DETAILS");
            if (dt.Rows.Count > 0)
            {
                string str = dt.Rows[0]["Record"].ToString();
                if (str == "Duplicate")
                {
                    lblmessage.Text = "Already exist Project Please Select other Project";
                    return;
                }
            }
            else
            {
                Response.Redirect("~/procs/Task_ProjectScheduleSettingInfo.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btn_ATT_Update_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Task_ProjectScheduleSettingInfo.aspx");
    }

    protected void txt_ProjectEndDate_TextChanged(object sender, EventArgs e)
    {
        var varnewtargetdate = "";

        if (txt_ProjectStartDate.Text.Trim() == "")
        {
            lblmessage.Text = "Please Select project Start Date";
            txt_ProjectEndDate.Text = "";
            return;
        }
        else
        {
            DateTime temp = DateTime.ParseExact(txt_ProjectEndDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            varnewtargetdate = temp.ToString("yyyy-MM-dd");

            DateTime tempsystemtargetdate = DateTime.ParseExact(txt_ProjectStartDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var vardatestemtargetdate = tempsystemtargetdate.ToString("yyyy-MM-dd");

            if (temp < tempsystemtargetdate)
            {
                lblmessage.Text = " The project end date must be after the project start date. Please enter a valid end date.";
                txt_ProjectEndDate.Text = "";
                return;
            }
            else
            {
                lblmessage.Text = "";
            }
        }
    }
}