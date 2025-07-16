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

public partial class Default2 : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource;
    public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet, dtInterviewer1;
    public string filename = "";
    public string InterviewernameEmail = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getMainSkillset();
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
}