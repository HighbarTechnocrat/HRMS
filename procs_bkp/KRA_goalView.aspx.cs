using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KRA_goalView : System.Web.UI.Page
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
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    if (Request.QueryString.Count > 0)
                    {
                        hdnKRA_ID.Value = Request.QueryString[0];
                        hdnGoal_ID.Value = Request.QueryString[1];
                        hdnEmpCode.Value = strempcode;

                        getMeasurements_List();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    #endregion

    #region Page Methods
    private void getMeasurements_List()
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_list_by_KRA_ID_appr";

        spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
        if (Convert.ToString(hdnKRA_ID.Value).Trim() != "")
            spars[1].Value = Convert.ToDecimal(hdnKRA_ID.Value);
        else
            spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(hdnGoal_ID.Value).Trim() != "")
            spars[2].Value = Convert.ToInt32(hdnGoal_ID.Value);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


       if(dtmeasurement.Tables.Count>0)
        {
            if (dtmeasurement.Tables[0].Rows.Count > 0)
            {
                txt_Measurement_dtls.Text = Convert.ToString(dtmeasurement.Tables[0].Rows[0]["Measurement_Details"]).Trim();
                txt_unit.Text = Convert.ToString(dtmeasurement.Tables[0].Rows[0]["unit_short_name"]).Trim();
                txt_Mqty.Text = Convert.ToString(dtmeasurement.Tables[0].Rows[0]["Quantity"]).Trim();
                txt_remakrs.Text = Convert.ToString(dtmeasurement.Tables[0].Rows[0]["remarks"]).Trim();
            }


            if (dtmeasurement.Tables[1].Rows.Count > 0)
            {
                txt_goal_title.Text = Convert.ToString(dtmeasurement.Tables[1].Rows[0]["Goal_Title"]).Trim();
                txt_goal_description.Text = Convert.ToString(dtmeasurement.Tables[1].Rows[0]["Goal_Description"]).Trim();
                txt_Weightage.Text = Convert.ToString(dtmeasurement.Tables[1].Rows[0]["Weightage"]).Trim(); 
            }
        }


    }

    #endregion







    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    { 
       Response.Redirect("KRA_Appr.aspx?kraid=" + hdnKRA_ID.Value + "&goalid=" + hdnGoal_ID.Value);
    }
}