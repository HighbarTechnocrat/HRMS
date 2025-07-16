using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Collections.Generic;

public partial class procs_SearchCandidate : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecCandidate;
    public DataTable  dtRecCandidate, dtcandidateDetails, dtmainSkillSet;
    


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
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
            // lpm.Emp_Code = Session["Empcode"].ToString();
            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    getMainSkillset();
                    getMngCandidateInfoList();
                    TxtExpectedCTCSerchFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    TxtExpectedCTCSerchTo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtExperienceYearSearchFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtExperienceYearSearchTo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_Candidatemobile.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
				}
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdCandidate_ID.Value = Convert.ToString(gvSearchCandidateList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("ViewCandidate.aspx?reqCandidate_ID=" + hdCandidate_ID.Value);
       
    }

    protected void gvSearchCandidateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "" || lstMainSkillset.SelectedValue =="0")
        {
            getMngCandidateInfoList();
            gvSearchCandidateList.PageIndex = e.NewPageIndex;
            gvSearchCandidateList.DataSource = dsRecCandidate;
            gvSearchCandidateList.DataBind();
        }
        else
        {
            gvSearchCandidateList.PageIndex = e.NewPageIndex;
            dsRecCandidate = spm.getSearchCandidateListOnButtonSearch(Txt_CandidateName.Text.Trim(), Txt_CandidateEmail.Text.Trim(), txt_AdditionalSkillset.Text.Trim(), lstMainSkillset.SelectedValue,Txt_Candidatemobile.Text);
            DataView dataView = dsRecCandidate.Tables[0].DefaultView;

            if (txtExperienceYearSearchFrom.Text != "" && txtExperienceYearSearchTo.Text != "")
            {
                dataView.RowFilter = " Convert(CandidateExperience_Years, 'System.Decimal') >= " + Convert.ToString(txtExperienceYearSearchFrom.Text.Trim()) + " AND Convert(CandidateExperience_Years, 'System.Decimal') <= " + Convert.ToString(txtExperienceYearSearchTo.Text.Trim()) + "";
            }

            if (TxtExpectedCTCSerchFrom.Text != "" && TxtExpectedCTCSerchTo.Text != "")
            {
                dataView.RowFilter = " Convert(CandidateExpectedCTC, 'System.Decimal') >= " + Convert.ToString(TxtExpectedCTCSerchFrom.Text.Trim()) + " AND Convert(CandidateExpectedCTC, 'System.Decimal') <= " + Convert.ToString(TxtExpectedCTCSerchTo.Text.Trim()) + "";
            }
            if (dataView.Count > 0)
            {
                gvSearchCandidateList.DataSource = dataView;
                gvSearchCandidateList.DataBind();
            }
            else
            {
                lblmessage.Text = "No record found. Search other criteria";
                gvSearchCandidateList.DataSource = null;
                gvSearchCandidateList.DataBind();
            }
        }

       
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";

            //if (Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "")
            //{
            //    //lblmessage.Text = "Please select MainSkillset";
            //    //return;
            //}
            //else
            {
                dsRecCandidate = spm.getSearchCandidateListOnButtonSearch(Txt_CandidateName.Text.Trim(), Txt_CandidateEmail.Text.Trim(), txt_AdditionalSkillset.Text.Trim(), lstMainSkillset.SelectedValue, Txt_Candidatemobile.Text);
                DataView dataView = dsRecCandidate.Tables[0].DefaultView;

                if (txtExperienceYearSearchFrom.Text != "" && txtExperienceYearSearchTo.Text != "")
                {
                    dataView.RowFilter = " Convert(CandidateExperience_Years, 'System.Decimal') >= " + Convert.ToString(txtExperienceYearSearchFrom.Text.Trim()) + " AND Convert(CandidateExperience_Years, 'System.Decimal') <= " + Convert.ToString(txtExperienceYearSearchTo.Text.Trim()) + "";
                }

                if (TxtExpectedCTCSerchFrom.Text != "" && TxtExpectedCTCSerchTo.Text != "")
                {
                    dataView.RowFilter = " Convert(CandidateExpectedCTC, 'System.Decimal') >= " + Convert.ToString(TxtExpectedCTCSerchFrom.Text.Trim()) + " AND Convert(CandidateExpectedCTC, 'System.Decimal') <= " + Convert.ToString(TxtExpectedCTCSerchTo.Text.Trim()) + "";
                }
                if (dataView.Count > 0)
                {
                    gvSearchCandidateList.DataSource = dataView;
                    gvSearchCandidateList.DataBind();
                }
                else
                {
                    lblmessage.Text = "No record found. Search other criteria";
                    gvSearchCandidateList.DataSource = null;
                    gvSearchCandidateList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region page Methods

    private void getMngCandidateInfoList()
    {
        try
        {
            string strreqCandidate_ID = "";
            dsRecCandidate = spm.getSearchCandidateList(strreqCandidate_ID);
                if (dsRecCandidate.Tables[0].Rows.Count > 0)
                {
                    gvSearchCandidateList.DataSource = dsRecCandidate.Tables[0];
                    gvSearchCandidateList.DataBind();
                }
                else
                {
                    gvSearchCandidateList.DataSource = null;
                    gvSearchCandidateList.DataBind();
                }
        }
        catch (Exception ex)
        {

        }
    }
    private void getMainSkillset()
    {
        dtmainSkillSet = spm.GetMainSkillset();
        lstMainSkillset.DataSource = dtmainSkillSet;
        lstMainSkillset.DataTextField = "ModuleDesc";
        lstMainSkillset.DataValueField = "ModuleId";
        lstMainSkillset.DataBind();
        lstMainSkillset.Items.Insert(0, new ListItem("Select SkillSet", ""));
    }

    

    #endregion
    protected void linkClearSearch_Click(object sender, EventArgs e)
    {
        txt_AdditionalSkillset.Text = "";
        Txt_CandidateEmail.Text = "";
        Txt_CandidateName.Text = "";
		Txt_Candidatemobile.Text = "";
		txtExperienceYearSearchFrom.Text = "";
        txtExperienceYearSearchTo.Text = "";
        TxtExpectedCTCSerchFrom.Text = "";
        TxtExpectedCTCSerchTo.Text = "";
        getMngCandidateInfoList();
        getMainSkillset();
    }
}