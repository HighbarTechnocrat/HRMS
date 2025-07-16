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
using System.Text;

public partial class procs_ExitProcess_SurveyForm : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitSurvey, dtEmpCode;
    string Emp_Code;
    bool hasKeys;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            //lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            Emp_Code = Session["Empcode"].ToString();

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
                    hasKeys = Request.QueryString.HasKeys();
                    if (hasKeys)
                    {
                        LoadUserData(Session["Empcode"].ToString(), Convert.ToInt32(Request.QueryString["resignationid"]));
                        GetExitSurveyData(Convert.ToInt32(Request.QueryString["resignationid"]), Session["Empcode"].ToString());
                        SetControl();
                        lblmsg2.Text = "";
                    }
                    else
                    {
                        LoadUserData(Session["Empcode"].ToString(), 0);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
        //ds = spm.getExitSurveyForm();
        //CreateExitSurveyForm(ds);
    }

    public void LoadUserData(string empCode, int ResignationID)
    {
            dt = spm.getUserDetailsForExitProcess(empCode, ResignationID, "ExitSurveyForm");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["ExitSurveyEligible"]) == "Yes")
                {
                    txtfullName.Text = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                    txtempCode.Text = Convert.ToString(dt.Rows[0]["Emp_Code"]);
                    txtDesignation.Text = Convert.ToString(dt.Rows[0]["DesginationName"]);
                    txtBand.Text = Convert.ToString(dt.Rows[0]["BAND"]);
                    txtDept.Text = Convert.ToString(dt.Rows[0]["Department_Name"]);
                    txtMobileNo.Text = Convert.ToString(dt.Rows[0]["mobile"]);
                    txtRMName.Text = Convert.ToString(dt.Rows[0]["RMName"]);
                    hdnResId.Value = Convert.ToString(dt.Rows[0]["ResignationID"]);

                    if (Convert.ToString(dt.Rows[0]["ExitSurveyFilled"]) == "Yes" && hasKeys == false)
                    {
                        lblmessage.Text = "You have already submitted Exit Survey Form.";
                        lblmsg2.Text = "";
                        SetControl();
                        mobile_btnSave.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Text = "You are not eligible to submit Exit Survey Form.";
                    lblmsg2.Text = "";
                    SetControl();
                    mobile_btnSave.Visible = false;
                }

            }
        
        else
        {
            lblmessage.Text = "You do not have acess to this page";
            lblmsg2.Text = "";
            SetControl();
            mobile_btnSave.Visible = false;
        }
     }
    

    public void CreateExitSurveyForm(DataSet ds)
    {
        DataTable dtQustions = ds.Tables[0];
        DataTable dtQustionsOpts = ds.Tables[1];

        Table tbl = new Table();
        tbl.Width = Unit.Percentage(100);
        TableRow tr;
        TableCell tc;
        TextBox txt;
        CheckBox cbk;
        DropDownList ddl;
        RadioButtonList rdbtnlst;

        foreach (DataRow dr in dtQustions.Rows)
        {
            tr = new TableRow();
            tc = new TableCell();
            tc.Width = Unit.Percentage(35);
            tc.Text = dr["ExitSurveyQstDesc"].ToString();
            tc.Attributes.Add("id", dr["ExitSurveyQstID"].ToString());
            tr.Cells.Add(tc);
            tc = new TableCell();

            if (dr["ControlType"].ToString() == "Textbox")
            {
                txt = new TextBox();
                txt.ID = "txt_" + dr["ExitSurveyQstID"].ToString();
                txt.Width = Unit.Percentage(40);
                tc.Controls.Add(txt);
            }

            if (dr["ControlType"].ToString() == "MultilineTextbox")
            {
                txt = new TextBox();
                txt.ID = "txt_" + dr["ExitSurveyQstID"].ToString();
                txt.TextMode = TextBoxMode.MultiLine;
                txt.Width = Unit.Percentage(40);
                tc.Controls.Add(txt);
            }

            if (dr["ControlType"].ToString() == "Radio")
            {

                for (int i = 0; i < dtQustionsOpts.Rows.Count - 1; i++)
                {
                    if (dr["ExitSurveyQstID"].ToString() == dtQustionsOpts.Rows[i]["ExitSurveyQstID"].ToString())
                    {

                    }
                }

                //    rdbtnlst = new RadioButtonList();
                //    rdbtnlst.ID = "ddl_" + dr["ExitSurveyQstOptID"].ToString();
                //    rdbtnlst.Width = Unit.Percentage(41);
                //    rdbtnlst.RepeatDirection = RepeatDirection.Horizontal;
                //    rdbtnlst.Items.Add(new ListItem(dr["ExitSurveyQstOptDesc"].ToString(), dr["ExitSurveyQstOptID"].ToString()));

                //    //rdbtnlst = new RadioButtonList();
                //    //rdbtnlst.ID = "ddl_" + dr["ExitSurveyQstOptID"].ToString();
                //    //rdbtnlst.Width = Unit.Percentage(41);
                //    //rdbtnlst.RepeatDirection = RepeatDirection.Horizontal;
                //    //rdbtnlst.Items.Add(new ListItem(dr["ExitSurveyQstOptDesc"].ToString(), dr["ExitSurveyQstOptID"].ToString()));
                //    //tc.Controls.Add(rdbtnlst);
                //}

                //if (dr["ControlType"].ToString() == "Dropdown")
                //{
                //    ddl = new DropDownList();
                //    ddl.ID = "ddl_" + dr["ExitSurveyQstID"].ToString();
                //    ddl.Width = Unit.Percentage(41);
                //    for (int i = 0; i < dtQustionsOpts.Rows.Count-1; i++)
                //    {
                //        if (dr["ExitSurveyQstID"].ToString()== dtQustionsOpts.Rows[i]["ExitSurveyQstID"].ToString())
                //        {
                //            //if (!string.IsNullOrEmpty(dr["ExitSurveyQstOptID"].ToString()))
                //            //{
                //                ddl.Items.Add(dtQustionsOpts.Rows[i]["ExitSurveyQstOptDesc"].ToString());
                //            //}

                //        }
                //    }
                //    tc.Controls.Add(ddl);
                //    //ddl = new DropDownList();
                //    //ddl.ID = "ddl_" + dr["ExitSurveyQstID"].ToString();
                //    //ddl.Width = Unit.Percentage(41);
                //    //if (!string.IsNullOrEmpty(dr["ExitSurveyQstOptID"].ToString()))
                //    //{
                //    //    ddl.Items.Add(dr["ExitSurveyQstOptDesc"].ToString());
                //    //}
                //    //tc.Controls.Add(ddl);
                //
            }

            tc.Width = Unit.Percentage(80);
            tr.Cells.Add(tc);
            tbl.Rows.Add(tr);
        }
        //pnlSurvey.Controls.Add(tbl);

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int ExitSurveyQstID;
        int ExitSurveyQstOptID;
        int OptionResultValue;
        string ExitSurveyTextAns = "";

        for (int i = 8; i <= 17; i++)
        {
            if (i == 8)
            {
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = 0;
                OptionResultValue = 0;
                ExitSurveyTextAns = txtAddress.Text;
                bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 9)
            {

                string selectedValue = "";
                bool result;
                selectedValue = rdlst_91.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(1);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_92.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(2);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_93.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(3);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_94.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(4);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_95.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(5);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_96.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(6);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_97.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(7);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                //string OptResultForm = Convert.ToString(i) + Convert.ToString(i2);
                //string OptResultFormValue = "0";
                //if (Request.Form[OptResultForm] == null)
                //{
                //    OptionResultValue = 0;
                //}
                //else
                //{
                //    OptResultFormValue = Request.Form[OptResultForm].ToString();
                //}

                //ExitSurveyQstID = i;
                //ExitSurveyQstOptID = i2;
                //OptionResultValue = Convert.ToInt32(OptResultFormValue);
                //ExitSurveyTextAns = "";
                //bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 10)
            {
                string selectedValue = "";
                bool result;
                selectedValue = rdlst_101.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(1);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_102.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(2);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_103.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(3);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_104.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(4);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_105.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(5);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_106.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(6);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_107.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(7);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
                //for (int i2 = 1; i2 <= 7; i2++)
                //{
                //    string OptResultForm = Convert.ToString(i) + Convert.ToString(i2);
                //    string OptResultFormValue = "0";
                //    if (Request.Form[OptResultForm] == null)
                //    {
                //        OptionResultValue = 0;
                //    }
                //    else
                //    {
                //        OptResultFormValue = Request.Form[OptResultForm].ToString();
                //    }

                //    ExitSurveyQstID = i;
                //    ExitSurveyQstOptID = i2;
                //    OptionResultValue = Convert.ToInt32(OptResultFormValue);
                //    ExitSurveyTextAns = "";
                //    bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
                //}
            }
            else if (i == 11)
            {
                string selectedValue = "";
                bool result;
                selectedValue = rdlst_111.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(1);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_112.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(2);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_113.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(3);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_114.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(4);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_115.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(5);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_116.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(6);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_117.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(7);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_118.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(8);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_119.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(9);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);

                selectedValue = rdlst_1110.SelectedValue;
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = Convert.ToInt32(10);
                OptionResultValue = Convert.ToInt32(selectedValue);
                ExitSurveyTextAns = "";
                result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 12)
            {
                foreach (ListItem item in checklst12.Items)
                {
                    string selectedValue = "";
                    if (item.Selected)
                    {
                        selectedValue = item.Value;
                        ExitSurveyQstID = i;
                        ExitSurveyQstOptID = Convert.ToInt32(selectedValue);
                        OptionResultValue = Convert.ToInt32(selectedValue);
                        ExitSurveyTextAns = "";
                        bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
                    }
                }
            }
            else if (i == 13)
            {
                foreach (ListItem item in checklst13.Items)
                {
                    string selectedValue = "";
                    if (item.Selected)
                    {
                        selectedValue = item.Value;
                        ExitSurveyQstID = i;
                        ExitSurveyQstOptID = Convert.ToInt32(selectedValue);
                        OptionResultValue = Convert.ToInt32(selectedValue);
                        ExitSurveyTextAns = "";
                        bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
                    }
                }
            }
            else if (i == 14)
            {
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = 0;
                OptionResultValue = 0;
                ExitSurveyTextAns = txt14.Text;
                bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 15)
            {
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = 0;
                OptionResultValue = 0;
                ExitSurveyTextAns = txt15.Text;
                bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 16)
            {
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = 0;
                OptionResultValue = 0;
                ExitSurveyTextAns = txt16.Text;
                bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }
            else if (i == 17)
            {
                ExitSurveyQstID = i;
                ExitSurveyQstOptID = 0;
                OptionResultValue = 0;
                ExitSurveyTextAns = txt17.Text;
                bool result = spm.InserExitSurveyFormDetails(Emp_Code, ExitSurveyQstID, ExitSurveyQstOptID, OptionResultValue, ExitSurveyTextAns);
            }

        }

        bool resultFinal = spm.UpdateUserDataInAdminExit(Emp_Code, "UpdateInAdminExitSurvey", 0);

        //Send Mail-To all approver
        //Send Mail-To all approver
        //Get CC mail-ids
        string RM_EMAIL = "";
        string approveremailaddress = "";
        DataTable dtApproverEmailIds = spm.GetExitProcApproverDetails(Emp_Code);
		//if (dtApproverEmailIds.Rows.Count > 0)
		//{
		//    approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
		//    apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
		//    Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
		//    hflapprcode.Value = Approvers_code;
		//}
		foreach (DataRow row in dtApproverEmailIds.Rows)
		{
			if (dtApproverEmailIds.Rows.Count == 1)
			{
				RM_EMAIL += Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
				///approveremailaddress += Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
			}
			else
			{
				if (Convert.ToString(row["APPR_ID"]) == "1")
				{
					RM_EMAIL += Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
				}
				else
				{
					approveremailaddress += Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
				}
			}
		}

		//Get To mail-ids
		string emp_name = "", LWD = "";
        StringBuilder strbuild = new StringBuilder();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ResignationMail_EmployeeDetails";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = Emp_Code;

        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

        string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProccess_InterviewForm.aspx";
        //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProccess_InterviewForm.aspx";
        //string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProccess_InterviewForm.aspx";

        string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + hdnResId.Value + "&Type=Add";
		redirectURL = "";

		if (tt.Rows.Count > 0)
        {
            strbuild.Length = 0;
            strbuild.Clear();

            strbuild.Append("<table border='1'>");
           

            foreach (DataRow row in tt.Rows)
            {
                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                {

                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                    + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                    + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
				    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
					+ "</td></tr>");
                }
                else
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                    + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                    + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                    + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                    + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
					 + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
					+ "</td></tr>");
                }
                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                LWD = Convert.ToString(row["LastWorkingDate"].ToString());
            }
            strbuild.Append("</table>");
        }
        string strsubject = emp_name + " has filled Exit Survey Form";
       // spm.SendMailOnExitSurveySubmitted(RM_EMAIL, strsubject, emp_name, Convert.ToString(strbuild), approveremailaddress, redirectURL, LWD);


        lblmessage.Visible = true;
        lblmessage.Text = "Exit Survey Form Submitted Successfully";

        //string message = "Exit Interview Form Submitted Successfully.";
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload=function(){");
        //sb.Append("alert('");
        //sb.Append(message);
        //sb.Append("')};");
        //sb.Append("</script>");
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        Response.Redirect("ExitProcess_Index.aspx");


    }
    public void GetExitSurveyData(int ResignationID,string EmpCode)
    {
        dtExitSurvey = spm.GetExitFormDataByResignationID(ResignationID, EmpCode);
        if (dtExitSurvey.Rows.Count > 0)
        {
            for (int i = 0; i < dtExitSurvey.Rows.Count; i++)
            {
                if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "8")
                {
                    txtAddress.Text = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyTextAns"]);
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "9")
                {
                    //for (int i2 = 1; i2 <= 7; i2++)
                    //{

                    //string value = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) + Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstOptID"]) + Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]);

                    string value = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) + Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstOptID"]);

                    if (value == "91")
                    {
                        foreach (ListItem item in rdlst_91.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "92")
                    {
                        foreach (ListItem item in rdlst_92.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "93")
                    {
                        foreach (ListItem item in rdlst_93.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "94")
                    {
                        foreach (ListItem item in rdlst_94.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "95")
                    {
                        foreach (ListItem item in rdlst_95.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "96")
                    {
                        foreach (ListItem item in rdlst_96.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "97")
                    {
                        foreach (ListItem item in rdlst_97.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    //}
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "10")
                {
                    //for (int i2 = 1; i2 <= 7; i2++)
                    //{
                    string value = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) + Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstOptID"]);

                    if (value == "101")
                    {
                        foreach (ListItem item in rdlst_101.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "102")
                    {
                        foreach (ListItem item in rdlst_102.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "103")
                    {
                        foreach (ListItem item in rdlst_103.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "104")
                    {
                        foreach (ListItem item in rdlst_104.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "105")
                    {
                        foreach (ListItem item in rdlst_105.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "106")
                    {
                        foreach (ListItem item in rdlst_106.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "107")
                    {
                        foreach (ListItem item in rdlst_107.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    //}
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "11")
                {
                    //for (int i2 = 1; i2 <= 7; i2++)
                    //{
                    string value = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) + Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstOptID"]);

                    if (value == "111")
                    {
                        foreach (ListItem item in rdlst_111.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "112")
                    {
                        foreach (ListItem item in rdlst_112.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "113")
                    {
                        foreach (ListItem item in rdlst_113.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "114")
                    {
                        foreach (ListItem item in rdlst_114.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "115")
                    {
                        foreach (ListItem item in rdlst_115.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "116")
                    {
                        foreach (ListItem item in rdlst_116.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    else if (value == "117")
                    {
                        foreach (ListItem item in rdlst_117.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "118")
                    {
                        foreach (ListItem item in rdlst_118.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "119")
                    {
                        foreach (ListItem item in rdlst_119.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    else if (value == "1110")
                    {
                        foreach (ListItem item in rdlst_1110.Items)
                        {
                            if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    //}
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "12")
                {
                    foreach (ListItem item in checklst12.Items)
                    {
                        if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "13")
                {
                    foreach (ListItem item in checklst13.Items)
                    {
                        if (Convert.ToString(dtExitSurvey.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "14")
                {
                    txt14.Text = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyTextAns"]);
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "15")
                {
                    txt15.Text = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyTextAns"]);
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "16")
                {
                    txt16.Text = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyTextAns"]);
                }
                else if (Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyQstID"]) == "17")
                {
                    txt17.Text = Convert.ToString(dtExitSurvey.Rows[i]["ExitSurveyTextAns"]);
                }

            }
        }
    }
    public void SetControl()
    {
        mobile_btnSave.Visible = false;
        mobile_cancel.Visible = false;
        txtAddress.Enabled = false;

        rdlst_91.Enabled = false;
        rdlst_92.Enabled = false;
        rdlst_93.Enabled = false;
        rdlst_94.Enabled = false;
        rdlst_95.Enabled = false;
        rdlst_96.Enabled = false;
        rdlst_97.Enabled = false;

        rdlst_101.Enabled = false;
        rdlst_102.Enabled = false;
        rdlst_103.Enabled = false;
        rdlst_104.Enabled = false;
        rdlst_105.Enabled = false;
        rdlst_106.Enabled = false;
        rdlst_107.Enabled = false;

        rdlst_111.Enabled = false;
        rdlst_112.Enabled = false;
        rdlst_113.Enabled = false;
        rdlst_114.Enabled = false;
        rdlst_115.Enabled = false;
        rdlst_116.Enabled = false;
        rdlst_117.Enabled = false;
        rdlst_118.Enabled = false;
        rdlst_119.Enabled = false;
        rdlst_1110.Enabled = false;

        checklst12.Enabled = false;
        checklst13.Enabled = false;
        txt14.Enabled = false;
        txt15.Enabled = false;
        txt16.Enabled = false;
        txt17.Enabled = false;
        mobile_cancel.Visible = false;
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {

    }
}