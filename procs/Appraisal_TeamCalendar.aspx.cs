using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Appraisal_TeamCalendar : System.Web.UI.Page
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
    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if (ds_userdetails.Tables[0].Rows.Count > 0)
                {
                   

                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            // txtdob.Text = "";
                        }
                        else
                        {
                            //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                                //  removeprofile.Visible = false;
                            }
                            else
                            {
                                // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                                //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                                // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                            // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                            // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                        // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }

                    // fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    // fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //  fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //  ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    // txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            //Empcode_Appr
            if (Convert.ToString(Session["Empcode_Appr"]).Trim() == "" || Session["Empcode_Appr"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/Appraisal_login.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/appraisalindex");
            }
            else
            {
                Session["chkbtnStatus"] = "";

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    getMngSelfAssessList();
                    DisplayProfileProperties();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    
    
    protected void gvMngSelfAssessList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Employee  </div>";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
          
            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Self Assessment Status </div> ";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Performance Review Status </div>";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \">Recommendations </div>";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.CssClass = "font-weight: normal !important";

            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);
            
            gvMngSelfAssessList.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void gvMngSelfAssessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnViewAtt = e.Row.FindControl("lnkLeaveDetails") as LinkButton;
            hdnyear.Value = gvMngSelfAssessList.DataKeys[e.Row.RowIndex].Values[1].ToString();
            lblheading.Text = "My Team Appraisals : " + Convert.ToString(hdnyear.Value).Trim();
            hdnAssessid.Value = gvMngSelfAssessList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if ((Convert.ToString(hdnAssessid.Value).Trim() == "0") || (Convert.ToString(hdnAssessid.Value).Trim() == ""))
            {
                lbtnViewAtt.Visible = false;
            }
            else
            {
                lbtnViewAtt.Visible = true;
                //if ((Convert.ToString(hdnstatus.Value).Trim() == "3"))
                //{
                //    lnkLeaveDetails.Text = "Submitted";
                //    lnkLeaveDetails.Attributes.Add("onclick", "return false;");
                //}
                //else
                //{
                //    lnkLeaveDetails.Text = "Draft";
                //}


            }
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AppraisalTeamCalendar_" + Convert.ToString(hdnyear.Value).Trim() + "_" + DateTime.Now + ".xls";
        
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        gvMngSelfAssessList.GridLines = GridLines.Both;
        gvMngSelfAssessList.HeaderStyle.Font.Bold = true;
        gvMngSelfAssessList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    #endregion

    #region Page Methods
    private void getMngSelfAssessList()
    {
        try
        {

            DataTable dtSelfAssess = new DataTable();
            dtSelfAssess = spm.getSelfAssess_MngList(strempcode, "Teamcalendar");

            gvMngSelfAssessList.DataSource = null;
            gvMngSelfAssessList.DataBind();

            if (dtSelfAssess.Rows.Count > 0)
                {
                    gvMngSelfAssessList.DataSource = dtSelfAssess;
                    gvMngSelfAssessList.DataBind();
                }
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnAssessid.Value = Convert.ToString(gvMngSelfAssessList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnAssessStatus.Value = Convert.ToString(gvMngSelfAssessList.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnAssessTyp.Value = "RevWerEd";
        ////if (Convert.ToString(hdnAssessStatus.Value) == "6")  //6=N1Pending
        ////{
        ////Response.Redirect("Appraisal_SelfAssessment.aspx?8787hghj767=" + hdnAssessid.Value + "&mndfbmdbf=RevWerEd");     
        //Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=" + hdnAssessStatus.Value + "&mfngndfhfgnj=" + hdnAssessTyp.Value);
        ////}
        ////if (Convert.ToString(hdnAssessStatus.Value) == "7") //N1Submitted
        ////{
        ////    Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=N2&mfngfdfnfgnj=" + hdnAssessTyp.Value);
        ////}
        ////if (Convert.ToString(hdnAssessStatus.Value) == "8") //8=N2Submitted
        ////{
        ////    Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=AD1&dddf54544ddd=" + hdnAssessTyp.Value);
        ////}

        if (Convert.ToString(hdnAssessStatus.Value) == "2")  //6=N1Pending
        {
            Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=N1V&mfngndfhfgnj=" + hdnAssessTyp.Value);
        }
        if (Convert.ToString(hdnAssessStatus.Value) == "3") //N1Submitted
        {
            Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=N2V&mfngfdfnfgnj=" + hdnAssessTyp.Value);
        }
        if (Convert.ToString(hdnAssessStatus.Value) == "4") //8=N2Submitted
        {
            Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=AD1V&dddf54544ddd=" + hdnAssessTyp.Value);
        }

    }

    #endregion 


    protected void gvMngSelfAssessList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        getMngSelfAssessList();

        gvMngSelfAssessList.PageIndex = e.NewPageIndex;
        gvMngSelfAssessList.DataBind();
    }
   
    
}
