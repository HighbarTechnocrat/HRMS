using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appreciation_old_letter : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appreciation_Letter_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;

                    hdnhrappType.Value = "0";
                    if (Request.QueryString.Count > 0)
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);



                    InboxLeaveReqstList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }



    private void InboxLeaveReqstList()
    {
        try
        {
            DataSet dtleaveInbox = new DataSet();
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAppreciationLetter_Draftlist"; 
            dtleaveInbox = spm.getDatasetList(spars, "Appreciation_Letter");
            if (dtleaveInbox != null && dtleaveInbox.Tables.Count > 0 && dtleaveInbox.Tables[0].Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        InboxLeaveReqstList();
    }


    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnReqid.Value != "0")
            {
                Response.Redirect("Appreciation_oldletter_details.aspx?reqid=" + hdnReqid.Value);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }



}
