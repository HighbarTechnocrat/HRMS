using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

public partial class PersonalDocuments : System.Web.UI.Page
{
    public static string userid;
    public static int PageSize = 12, RecordCount = 0;
    public static string pstname = "";
    public string fname = "";
    public static int pgi = 1;
    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;
    public string dTypename = null;
    public string dTypenameDB = null;
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Convert.ToString(Session["Empcode"]).Trim() == "")
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

        lpm.Emp_Code = Session["Empcode"].ToString();

        if (Page.User.Identity.IsAuthenticated)
        {
            userid = Page.User.Identity.Name.ToString().Trim();
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["cat"] != "" && Request.QueryString["cat"] != null && Request.QueryString.Count == 1 && Request.QueryString["cat"].Length <= 2)
            {
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "PersonalDocuments.aspx";
                gobackbtn.Visible = true;
                gobackbtn.Title = "My Files";
                gobackbtn.InnerText = "My Files";

                if (Request.QueryString["cat"] == "P")
                {
                    dTypename = "Payslip";
                    dTypenameDB = "1";
                    dvmonth.Attributes.Add("style", "width: 10%; display: table-cell;padding-right: 50px;");
                }
                if (Request.QueryString["cat"] == "PF")
                {
                    dTypename = "Provident Fund";
                    dTypenameDB = "2";

                }
                if (Request.QueryString["cat"] == "F")
                {
                    dTypename = "Form 16";
                    dTypenameDB = "3";
                }
                if (Request.QueryString["cat"] == "S")
                {
                    dTypename = "Superannuation Fund";
                    dTypenameDB = "4";
                }


                if (Request.QueryString["cat"] == "CT")
                {
                    dTypename = "My CTC";
                    dTypenameDB = "5";
                   
                }

                LoadEmpwiseData(Request.QueryString["cat"].ToString(), Session["Empcode"].ToString());

                //  LoadEmpwiseData(Request.QueryString["cat"].ToString(), "99912438");

            }
            lblcatname.Text = dTypename;
            pgi = 1;
            //bindwall(pgi);

        }
    }



    private void LoadMonth()
    {
        if (ddlYear.Items.Count > 0)
        {
            DataSet ds = new DataSet();

            SqlParameter[] spara = new SqlParameter[1];
            spara[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
            spara[0].Value = "MON";
            ds = spm.getDatasetList(spara, "Get_Emp_DocDetails");
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "MonthValue";
            ddlMonth.DataSource = ds;
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("--Select--", "0"));


            //ddlMonth.Items.Insert(1, new ListItem("January", "Jan"));
            //ddlMonth.Items.Insert(2, new ListItem("February", "Feb"));
            //ddlMonth.Items.Insert(3, new ListItem("March", "Mar"));
            //ddlMonth.Items.Insert(4, new ListItem("April", "Apr"));
            //ddlMonth.Items.Insert(5, new ListItem("May", "May"));
            //ddlMonth.Items.Insert(6, new ListItem("June", "Jun"));
            //ddlMonth.Items.Insert(7, new ListItem("July", "Jul"));
            //ddlMonth.Items.Insert(8, new ListItem("August", "Aug"));
            //ddlMonth.Items.Insert(9, new ListItem("September", "Sep"));
            //ddlMonth.Items.Insert(9, new ListItem("October", "Oct"));
            //ddlMonth.Items.Insert(9, new ListItem("November", "Nov"));
            //ddlMonth.Items.Insert(9, new ListItem("December", "Dec"));
        }


    }

    private void LoadEmpwiseData(string ftype, string EmpID)
    {
        DataSet ds = new DataSet();

        SqlParameter[] spara = new SqlParameter[2];
        spara[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
        spara[0].Value = ftype;

        spara[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spara[1].Value = EmpID.Trim();
        ds = spm.getDatasetList(spara, "Get_Emp_DocDetails");

        if (ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataSource = ds;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
                if (ftype == "P")
                {
                    ddlYear.SelectedIndex = 1;
                    LoadMonth();
                    ddlYear_SelectedIndexChanged(null, null);
                    ddlMonth.SelectedValue = "0";
                    dvmonth.Attributes.Add("style", "width: 10%; display: table-cell;padding-right: 50px;");
                }

                CheckFileExists();
                if (ftype == "CT")
                {
                    type.Attributes.Add("style", "width: 10%; display:none;");
                }
                else
                {
                    type.Attributes.Add("style", "width: 10%; display: table-cell;padding-right:150px;");
                }

               // btnSearch_Click(null, null);
                lblmsg.Visible = false;
            }


            else
            {
                type.Attributes.Add("style", "width: 10%; display:none;");
                dvmonth.Attributes.Add("style", "width: 10%; display:none;");
                lblmsg.Visible = true;
                lblmsg.Text = "No files found";
            }
        }

    }

    protected void View(object sender, EventArgs e)
    {
        string filepath = (sender as LinkButton).CommandArgument;


        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"500px\">";
        embed += "If you are unable to view file, click on Month-Year to download";
        //you can download from <a href = \"{0}\">here</a> ";
        embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        embed += "</object>";
        ltEmbed.Text = string.Format(embed, ResolveUrl(filepath.ToString()));


    }

    protected void lnksearch_Click(object sender, EventArgs e)
    {
        rptwall.DataSource = null;
        rptwall.DataBind();
        if (Request.QueryString["cat"] != "" && Request.QueryString["cat"] != null && Request.QueryString.Count == 1 && Request.QueryString["cat"].Length <= 2)
        {
            gobackbtn.HRef = ReturnUrl("sitepathmain") + "PersonalDocuments.aspx";
            gobackbtn.Visible = true;
            gobackbtn.Title = "Personal Documents";
            gobackbtn.InnerText = "Personal Documents";

            if (Request.QueryString["cat"] == "P")
            {
                dTypename = "Payslip";
                dTypenameDB = "1";
            }
            if (Request.QueryString["cat"] == "PF")
            {
                dTypename = "Provident Fund";
                dTypenameDB = "2";
            }
            if (Request.QueryString["cat"] == "F")
            {
                dTypename = "Form 16";
                dTypenameDB = "3";
            }
            if (Request.QueryString["cat"] == "S")
            {
                dTypename = "Superannuation Fund";
                dTypenameDB = "4";
            }


            if (Request.QueryString["cat"] == "CT")
            {
                dTypename = "My CTC";
                dTypenameDB = "5";
            }
        }
        lblcatname.Text = dTypename;
        pgi = 1;
        bindwall(pgi);
    }

    protected void lnkreset_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        txtsdate.Text = "";
        txtedate.Text = "";

        rptwall.DataSource = null;
        rptwall.DataBind();
        bindwall(1);
    }
    protected void ViewOne(object sender, EventArgs e)
    {
        try
        {
            string filepath = (sender as LinkButton).CommandArgument;
            String strfilepath = Path.Combine(Server.MapPath(filepath.ToString()));
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void bindwall(int PageIndex)
    {
        DateTime dat2 = DateTime.Now;

        if (txtsearch.Text == "")
        {
            // pstname = "";
            pstname = dat2.Year.ToString();
        }
        else
        {
            pstname = txtsearch.Text;
            pstname = commonclass.GetSafeSearchString(pstname);
        }
        string useridcode = null;
        //dTypename="Payslip";
        useridcode = Session["Empcode"].ToString();
        //useridcode = "00999124";
        pnlpst.Visible = true;
        DataSet dswall = new DataSet();
        dswall = SP_Methods.getDocumentDetail(useridcode, dTypenameDB, pstname, PageIndex, PageSize, out RecordCount);
        rptwall.DataSource = null;
        rptwall.DataBind();
        if (dswall.Tables.Count > 0)
        {
            if (dswall.Tables[0].Rows.Count > 0)
            {
                lblmsg.Visible = false;
                //  rptwall.DataSource = dswall.Tables[0];
                //  rptwall.DataBind();
            }
            else
            {
                pnlpst.Visible = false;
                lblmsg.Visible = true;
                //lblmsg.Text = "No Document Found ";//In " + lblcatname.Text.ToString() + " for year " + pstname;
            }
        }
        else
        {
            pnlpst.Visible = false;
            lblmsg.Visible = true;
            //lblmsg.Text = "No Document Found ";//" + lblcatname.Text.ToString();
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {

    }
    private void maxpager(int recordCount, int currentPage, int mpg)
    {

    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        rptwall.DataSource = null;
        rptwall.DataBind();
        this.bindwall(pageIndex);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        CheckFileExists();
        //try
        //{
        //    string filetype = string.Empty;
        //    string status = string.Empty;
        //    string foldername = string.Empty;
        //    HtmlAnchor anchrPF = (HtmlAnchor)rptanchor.FindControl("anchrPF");

        //    switch (Request.QueryString["cat"].ToString())
        //    {
        //        case "P":
        //            filetype = "P"; foldername = "PaySlip";
        //            break;
        //        case "PF":
        //            filetype = "PF"; foldername = "PF";
        //            break;
        //        case "F":
        //            filetype = "F"; foldername = "Form16";
        //            break;
        //        case "S":
        //            filetype = "S"; foldername = "SuperannuationFund";
        //            break;
        //        default:
        //            break;
        //    }


        //    PDFService.Service1Client service = new PDFService.Service1Client();
        //    string Empid = Session["Empcode"].ToString().Trim(); //"99912438";
        //    if (filetype != "P")
        //    {

        //        status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + ddlYear.SelectedValue.Trim() + "/" + Empid + ".pdf");
        //    }
        //    else
        //    {
        //        if (ddlMonth.SelectedValue.Trim() == "0")
        //        {
        //            #region Check Months for Salary slip

        //            DataSet ds = new DataSet();
        //            string EmpID = string.Empty;
        //            EmpID = Session["Empcode"].ToString();  //"99912438";

        //            SqlParameter[] spara = new SqlParameter[3];
        //            spara[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
        //            spara[0].Value = "PAIDMON";

        //            spara[1] = new SqlParameter("@year", SqlDbType.Int);
        //            spara[1].Value = ddlYear.SelectedValue.Trim();

        //            spara[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        //            spara[2].Value = EmpID.Trim();


        //            ds = spm.getDatasetList(spara, "Get_Emp_DocDetails");

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                ds.Tables[0].Columns.Add("Status");
        //                ds.Tables[0].AcceptChanges();

        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + ddlYear.SelectedValue.Trim() + "/" + ds.Tables[0].Rows[i]["MonthValue"].ToString().Trim() + "/" + Empid + ".pdf");

        //                    ds.Tables[0].Rows[i]["Status"] = status;
        //                    ds.Tables[0].AcceptChanges();
        //                }


        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    if (dr["Status"].ToString() == "0")
        //                        dr.Delete();
        //                }
        //                ds.Tables[0].AcceptChanges();


        //                rptanchor.DataSource = ds.Tables[0];
        //                rptanchor.DataBind();
        //            }

        //            #endregion
        //        }

        //        else
        //        {

        //            status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + ddlYear.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.ToString().Trim() + "/" + Empid + ".pdf");
        //        }
        //    }

        //    if (status == "1")
        //    {                

        //        lblmsgnew.Visible = false;

        //        if (filetype != "P")
        //        {
        //            DataSet ds = new DataSet();

        //            ds.Tables.Add("dt");
        //            ds.Tables["dt"].AcceptChanges();
        //            ds.Tables["dt"].Columns.Add("MonthValue");
        //            DataRow dr = ds.Tables["dt"].NewRow();
        //            ds.Tables["dt"].Rows.Add(dr);
        //            ds.Tables["dt"].AcceptChanges();
        //            ds.Tables["dt"].Rows[0]["MonthValue"] = "0";
        //            ds.Tables["dt"].AcceptChanges();

        //            rptanchor.DataSource = ds;
        //            rptanchor.DataBind();
        //        }
        //        else if (filetype == "P" && ddlMonth.SelectedValue.Trim() != "0")
        //        {
        //            DataSet ds = new DataSet();

        //            ds.Tables.Add("dt");
        //            ds.Tables["dt"].AcceptChanges();
        //            ds.Tables["dt"].Columns.Add("MonthValue");
        //            DataRow dr = ds.Tables["dt"].NewRow();
        //            ds.Tables["dt"].Rows.Add(dr);
        //            ds.Tables["dt"].AcceptChanges();
        //            ds.Tables["dt"].Rows[0]["MonthValue"] = "0";
        //            ds.Tables["dt"].AcceptChanges();

        //            rptanchor.DataSource = ds;
        //            rptanchor.DataBind();
        //        }

        //        //// anchrPF.Visible = true;
        //        //if (filetype != "P")
        //        //{
        //        //    if (filetype == "PF")
        //        //    { anchrPF.InnerText = Empid + "_PF_" + ddlYear.SelectedValue.ToString().Trim() + ".pdf"; }
        //        //    else if (filetype == "F")
        //        //    { anchrPF.InnerText = Empid + "_Form16_" + ddlYear.SelectedValue.ToString().Trim() + ".pdf"; }
        //        //    else if (filetype == "S")
        //        //    { anchrPF.InnerText = Empid + "_Superannuation_" + ddlYear.SelectedValue.ToString().Trim() + ".pdf"; }

        //        //    anchrPF.HRef = "ShowPDFfile.aspx?cat=" + filetype + "&yr=" + ddlYear.SelectedValue.Trim() + "&id=" + Empid.Trim() + "";
        //        //    anchrPF.Attributes.Add("target", "_blank");
        //        //}
        //        //else if (filetype == "P" && ddlMonth.SelectedValue.Trim () != "0")
        //        //{
        //        //    anchrPF.InnerText = Empid + "_Payslip_" + ddlYear.SelectedValue.ToString().Trim() + "_" + ddlMonth.SelectedValue.ToString().Trim() + ".pdf";
        //        //    anchrPF.HRef = "ShowPDFfile.aspx?cat=" + filetype + "&yr=" + ddlYear.SelectedValue.Trim() + "&id=" + Session["Empcode"].ToString().Trim() + "&mon=" + ddlMonth.SelectedValue.Trim() + "";
        //        //}

        //    }
        //    else
        //    {
        //        lblmsgnew.Visible = true;
        //        //anchrPF.Visible = false;
        //    }

        //}
        //catch (Exception ex)
        //{


        //}


    }


    protected void CheckFileExists()
    {
        try
        {
            string filetype = string.Empty;
            string status = string.Empty;
            string foldername = string.Empty;
            HtmlAnchor anchrPF = (HtmlAnchor)rptanchor.FindControl("anchrPF");



            switch (Request.QueryString["cat"].ToString())
            {
                case "P":
                    filetype = "P"; foldername = "PaySlip";
                    break;
                case "PF":
                    filetype = "PF"; foldername = "PF";
                    break;
                case "F":
                    filetype = "F"; foldername = "Form16";
                    break;
                case "S":
                    filetype = "S"; foldername = "SuperannuationFund";
                    break;

                case "CT":
                    filetype = "CT"; foldername = "MyCTC";
                    break;
                default:
                    break;
            }


            
            string Empid = Session["Empcode"].ToString().Trim(); //"99912438";
            if (filetype != "P")
            {
                DataSet dsyear = new DataSet();

                SqlParameter[] sparayr = new SqlParameter[3];

                sparayr[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
                sparayr[0].Value = Request.QueryString["cat"].ToString().Trim();

                sparayr[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                sparayr[1].Value = Session["Empcode"].ToString().Trim();

                sparayr[2] = new SqlParameter("@year", SqlDbType.VarChar);

                if (ddlYear.SelectedValue.Trim() == "0")
                    sparayr[2].Value = DBNull.Value;
                else
                    sparayr[2].Value = ddlYear.SelectedValue.Trim();


                dsyear = spm.getDatasetList(sparayr, "Get_Emp_DocDetails");
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                  

                    rptanchor.DataSource = dsyear.Tables[0];
                    rptanchor.DataBind();
                }
                else
                {
                    rptanchor.DataSource = null;
                    rptanchor.DataBind();
                }
            }
            else
            {
               
                #region Check Months for Salary slip

                DataSet ds = new DataSet();
                string EmpID = string.Empty;
                EmpID = Session["Empcode"].ToString();  //"99912438";

                SqlParameter[] spara = new SqlParameter[4];
                spara[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
                spara[0].Value = "PAIDMON";

                spara[1] = new SqlParameter("@year", SqlDbType.NVarChar);
                spara[1].Value = ddlYear.SelectedValue.Trim();

                spara[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spara[2].Value = EmpID.Trim();


                spara[3] = new SqlParameter("@month", SqlDbType.VarChar);
                if (ddlMonth.SelectedValue.Trim() == "0")
                    spara[3].Value = DBNull.Value;
                else
                    spara[3].Value = ddlMonth.SelectedValue.Trim();

                ds = spm.getDatasetList(spara, "Get_Emp_DocDetails");

                if (ds.Tables[0].Rows.Count > 0)
                {
                   

                    rptanchor.DataSource = ds.Tables[0];
                    rptanchor.DataBind();
                }
                else
                {
                    rptanchor.DataSource = null;
                    rptanchor.DataBind();
                }

                #endregion
               
            }



            lblmsg.Visible = false;

          

        }
        catch (Exception ex)
        {


        }

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptanchor.DataSource = null;
        rptanchor.DataBind();
        //  HtmlAnchor anchrPF = (HtmlAnchor)rptanchor.FindControl("anchrPF");
        // anchrPF.InnerText = "";
        // anchrPF.Visible = false;
        if (ddlYear.SelectedValue == "0")
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Insert(0, new ListItem("--Select--", "0"));

        }



        if (Request.QueryString["cat"] != null && Request.QueryString["cat"] == "P")
        {
            DataSet ds = new DataSet();
            string EmpID = string.Empty;
            EmpID = Session["Empcode"].ToString();  //"99912411";

            SqlParameter[] spara = new SqlParameter[4];
            spara[0] = new SqlParameter("@sptype", SqlDbType.VarChar);
            spara[0].Value = "PAIDMON";

            spara[1] = new SqlParameter("@year", SqlDbType.NVarChar);
            spara[1].Value = ddlYear.SelectedValue.Trim();

            spara[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spara[2].Value = EmpID.Trim();

            spara[3] = new SqlParameter("@month", SqlDbType.VarChar);            
            spara[3].Value = DBNull.Value;

            ds = spm.getDatasetList(spara, "Get_Emp_DocDetails");

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMonth.DataTextField = "MonthName";
                ddlMonth.DataValueField = "MonthValue";
                ddlMonth.DataSource = ds;
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        CheckFileExists();
        //else
        //{


        //}
    }
    //protected void rptanchor_ItemDataBound(object sender, RepeaterItemEventArgs e)

    protected void rptanchor_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string filetype = string.Empty;
        string Empid = string.Empty;
        Empid = Session["Empcode"].ToString(); //"99912411";
        HtmlAnchor anchrPF = (HtmlAnchor)e.Item.FindControl("anchrPF");
        Label lblmonthvalue = (Label)e.Item.FindControl("lblmonthvalue");
        Label lblyear = (Label)e.Item.FindControl("lblyear");
        Label yearname = (Label)e.Item.FindControl("lblyrname");
        switch (Request.QueryString["cat"].ToString())
        {
            case "P":
                filetype = "P";
                break;
            case "PF":
                filetype = "PF";
                break;
            case "F":
                filetype = "F";
                break;
            case "S":
                filetype = "S";
                break;

            case "CT":
                filetype = "CT";
                break;

            default:
                break;
        }

        //&yrname=" + UrlRewritingVM.Encrypt(yearname.Text.Trim()) + "
        if (filetype != "P")
        {

            //if (filetype == "PF")
            //{
            //    if (ddlYear.SelectedValue == "0")
            //    {
               
            //        // anchrPF.InnerText = lblyear.Text.Trim();
            //        anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(filetype) + "&yr=" + UrlRewritingVM.Encrypt(lblyear.Text.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid.Trim()) + "&yrname=" + UrlRewritingVM.Encrypt(yearname.Text.Trim()) + "";
            //    }
            //    else
            //    {

            //        anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(filetype) + "&yr=" + UrlRewritingVM.Encrypt(ddlYear.SelectedValue.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid.Trim()) + "&yrname=" + UrlRewritingVM.Encrypt(yearname.Text.Trim()) + "";
            //    }
            //}


            //else
            //{

                if (ddlYear.SelectedValue == "0")
                {
                    // anchrPF.InnerText = lblyear.Text.Trim();
                    anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt("" + filetype + "&yr=" + lblyear.Text.Trim() + "&id=" + Empid.Trim() + "");
                  //  anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(filetype) + "&yr=" + UrlRewritingVM.Encrypt(lblyear.Text.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid.Trim()) + "";
                }
                else
                {
                    anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt("" + filetype + "&yr=" + ddlYear.SelectedValue.Trim() + "&id=" + Empid.Trim() + "");
                   // anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(filetype) + "&yr=" + UrlRewritingVM.Encrypt(ddlYear.SelectedValue.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid.Trim()) + "";
                   
                }


           // }

        }
        else if (filetype == "P" && ddlMonth.SelectedValue.Trim() != "0")
        {

            
           // anchrPF.InnerText = ddlMonth.SelectedValue.ToString().Trim()+"-"+ddlYear.SelectedValue.ToString().Trim();
            // anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(filetype) + "&yr=" + UrlRewritingVM.Encrypt(ddlYear.SelectedValue.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid.Trim()) + "&mon=" + UrlRewritingVM.Encrypt(ddlMonth.SelectedValue.Trim()) + "";

             anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt(""+filetype + "&yr=" +ddlYear.SelectedValue.Trim() + "&id=" + Empid.Trim() + "&mon=" + ddlMonth.SelectedValue.Trim() + "");
            
        }

        else
        {

           
          //  anchrPF.InnerText = lblmonthvalue.Text.Trim().Trim()+"-"+lblyear.Text.Trim();
            //anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt("P") + "&yr=" + UrlRewritingVM.Encrypt(ddlYear.SelectedValue.Trim()) + "&id=" + UrlRewritingVM.Encrypt(Empid) + "&mon=" + UrlRewritingVM.Encrypt(lblmonthvalue.Text.Trim()) + "";

            anchrPF.HRef = "ShowPDFfile.aspx?cat=" + UrlRewritingVM.Encrypt("" + filetype + "&yr=" + ddlYear.SelectedValue.Trim() + "&id=" + Empid.Trim() + "&mon=" + lblmonthvalue.Text.Trim() + "");

        }

        //anchrPF.Attributes.Add("target", "_blank"); //**** commented to show pdf inside iframe
        anchrPF.Attributes.Add("target", "ifrmPDF");


    }

    protected void anchrPF_onclick(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$('#coverScreen').show();", false);

            PDFService.Service1Client service = new PDFService.Service1Client();
            string filetype = string.Empty;
            string status = string.Empty;
            string foldername = string.Empty;

            string year = string.Empty;
            string month = string.Empty;
            string yearvalue = string.Empty;
            Int64 Empid =Convert.ToInt64(Session["Empcode"].ToString().Trim());

            switch (Request.QueryString["cat"].ToString())
            {
                case "P":
                    filetype = "P"; foldername = "PaySlip";
                    break;
                case "PF":
                    filetype = "PF"; foldername = "PF";
                    break;
                case "F":
                    filetype = "F"; foldername = "Form16";
                    break;
                case "S":
                    filetype = "S"; foldername = "SuperannuationFund";
                    break;
                case "CT":
                    filetype = "CT"; foldername = "MyCTC";
                    break;
                default:
                    break;
            }


            ;
            HtmlAnchor btn = (HtmlAnchor)sender;
            DataList row = (DataList)btn.NamingContainer;
            year = row.FindControl("lblyear").ToString().Trim();
            month = row.FindControl("lblmonthvalue").ToString().Trim();
            yearvalue = row.FindControl("yrvalue").ToString().Trim();

            if (filetype != "P")
            {
                if (filetype == "PF")
                {
                    status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + year.Trim() + "/" + Empid +"_"+yearvalue.Trim()+ ".pdf");
                }
                else
                {
                    status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + year.Trim() + "/" + Empid + ".pdf");
                }
            }

            else
            {
                status = service.CheckFileExists("~/MyFiles/" + foldername + "/" + year + "/" + month + "/" + Empid + ".pdf");
            }


            if (status == "0")
            {
                lblmsg.Visible = true;
            }

        }
        catch (Exception ex)
        {
            
           
        }
       

    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckFileExists();
    }


    public static string Encrypt(string input, string key)
    {
        byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;        
        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
    
      
}