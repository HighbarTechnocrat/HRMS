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

public partial class procs_Req_Prosepect_Cust : System.Web.UI.Page
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
					GetProsepectCust();
					GetDelievryHead();
					GetReq_Prosepect_Cust_List();

					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	public void GetDelievryHead()
	{
		DataTable dtProsepectCust = new DataTable();
		dtProsepectCust = spm.GetRecruitment_SkillsetName();
		dtProsepectCust = spm.Insert_Req_Prosepect_Cust_Details("Select_Emp_Name", Convert.ToInt32(0), "", "", Convert.ToString(Session["Empcode"]).Trim());
		if (dtProsepectCust.Rows.Count > 0)
		{
			lstEmpName.DataSource = dtProsepectCust;
			lstEmpName.DataTextField = "Emp_Name";
			lstEmpName.DataValueField = "Emp_Code";
			lstEmpName.DataBind();
			lstEmpName.Items.Insert(0, new ListItem("Select Employee Name", "0"));
		}
	}
	public void GetProsepectCust()
	{
		DataTable dtProsepectCust = new DataTable();
		dtProsepectCust = spm.GetRecruitment_SkillsetName();
		dtProsepectCust = spm.Insert_Req_Prosepect_Cust_Details("Select", Convert.ToInt32(0), "", "", Convert.ToString(Session["Empcode"]).Trim());
		if (dtProsepectCust.Rows.Count > 0)
		{
			lstProsepectcust.DataSource = dtProsepectCust;
			lstProsepectcust.DataTextField = "Prosepect_Cust_Name";
			lstProsepectcust.DataValueField = "Prosepect_Cust_ID";
			lstProsepectcust.DataBind();
			lstProsepectcust.Items.Insert(0, new ListItem("Select Prosepect Cust", "0"));
		}
	}
	private void GetReq_Prosepect_Cust_List()
	{
		try
		{
			int Quest_ID = 0;
			DataTable QuestionnaireList = new DataTable();
			lblmessage.Text = "";
			string Stype = "Select";
			Quest_ID = Convert.ToString(lstProsepectcust.SelectedValue).Trim() != "" ? Convert.ToInt32(lstProsepectcust.SelectedValue) : 0;
			QuestionnaireList = spm.Insert_Req_Prosepect_Cust_Details(Stype, Convert.ToInt32(Quest_ID),"","", Convert.ToString(Session["Empcode"]).Trim());
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (QuestionnaireList.Rows.Count > 0)
			{
				gvMngTravelRqstList.DataSource = QuestionnaireList;
				gvMngTravelRqstList.DataBind();
			}
			else
			{
				lblmessage.Text = "Record not available";
			}

		}
		catch (Exception ex)
		{

		}
	}
	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetReq_Prosepect_Cust_List();
	}
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetReq_Prosepect_Cust_List();

	}
	private void ClearProsepect()
	{
		txtProsepectcust.Text = "";
		lstEmpName.SelectedIndex = -1;
		chkActive.Checked = true;
		hdnProsepect_ID.Value = "0";
		trvl_accmo_btn.Text = "Save";
		trvl_accmo_btn.ToolTip = "Save";
	}
	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		lstProsepectcust.SelectedIndex = -1;
		GetReq_Prosepect_Cust_List();	
	}
	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnProsepect_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			Get_Edit_Prosepect_Cust();
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	private void Get_Edit_Prosepect_Cust()
	{
		try
		{			
			DataTable ProsepectList = new DataTable();
			lblmessage.Text = "";
			string Stype = "getEdit";
			ProsepectList = spm.Insert_Req_Prosepect_Cust_Details(Stype, Convert.ToInt32(hdnProsepect_ID.Value), "", "", Convert.ToString(Session["Empcode"]).Trim());
			if (ProsepectList.Rows.Count > 0)
			{
				 string Result	=Convert.ToString(ProsepectList.Rows[0]["Prosepect_Cust_Name"]).Trim();
				string[] authorsList = Result.Split('_');
				txtProsepectcust.Text = authorsList[1].TrimEnd();
				lstEmpName.SelectedValue= Convert.ToString(ProsepectList.Rows[0]["DH"]).Trim();
				if (Convert.ToString(ProsepectList.Rows[0]["IsActive"]).Trim() != "1")
				{
					chkActive.Checked = false;
				}
				trvl_accmo_btn.Text = "Modify";
				trvl_accmo_btn.ToolTip = "Modify";
			}						
		}
		catch (Exception ex)
		{

		}
	}

	

	protected void btnTra_Details_Click(object sender, EventArgs e)
	{
		ClearProsepect();
		lblmessage.Text = "";
	}

	protected void trvl_accmo_btn_Click(object sender, EventArgs e)
	{
		int Prosepect_Cust_ID = 0;
		string Stype = "INSERT", EmpCode, IsActive = "0",msg = "Saved"; lblmessage.Text = "";

		DataTable dtVendor = new DataTable();
		DataTable dt2 = new DataTable(); ;
		try
		{
			#region Check For Blank Fields
			if (Convert.ToString(txtProsepectcust.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter prospect customers name";
				return;
			}
			if (Convert.ToString(lstEmpName.SelectedValue).Trim() == "" || Convert.ToString(lstEmpName.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Delivery Head";
				return;
			}
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			#endregion
			Prosepect_Cust_ID = Convert.ToString(hdnProsepect_ID.Value).Trim() != "" ? Convert.ToInt32(hdnProsepect_ID.Value) : 0;

			if (Prosepect_Cust_ID==0) msg = "Saved"; else msg = "Modify";

			 EmpCode = Convert.ToString(Session["Empcode"]).Trim();
			if (chkActive.Checked)//if checked, uncheck it
			{
				IsActive = "1";
			}
			if (Prosepect_Cust_ID != 0)
			{
				if (IsActive != "1")
				{
					dt2 = spm.Insert_Req_Prosepect_Cust_Details("Get_Prose_ClosedRequistion", Prosepect_Cust_ID, txtProsepectcust.Text.Trim(), "0", EmpCode);
					if (dt2.Rows.Count > 0)
					{
						lblmessage.Text = "Please closed all requisition this PROSP_" + txtProsepectcust.Text + ", then deactive prospect customer.!";
						return;
					}
				}
			}
			//dtVendor = spm.Insert_Req_Prosepect_Cust_Details(Stype, Prosepect_Cust_ID, txtProsepectcust.Text.Trim(), IsActive, EmpCode);
			dtVendor = Insert_Prospect_Cust(Stype, Prosepect_Cust_ID, txtProsepectcust.Text.Trim(), IsActive, EmpCode, lstEmpName.SelectedValue);
			if (dtVendor.Rows.Count > 0)
			{
				//Response.Redirect("~/procs/Req_JobSites_Details.aspx");
				ClearProsepect();
				GetReq_Prosepect_Cust_List();
				lblmessage.Text = "Record "+ msg + " Successfully!.";
			}
			else
			{
				lblmessage.Text = "Record already exists!.";
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	public DataTable Insert_Prospect_Cust( string Stype, int Prosepect_Cust_ID,string Prosepectcust, string IsActive,string EmpCode,  string lstEmpName)
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = Stype;
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@Prosepect_Cust_ID", SqlDbType.Int);
			spars[2].Value = Prosepect_Cust_ID;
			spars[3] = new SqlParameter("@IsActive", SqlDbType.VarChar);
			spars[3].Value = IsActive;
			spars[4] = new SqlParameter("@Prosepect_Cust_Name", SqlDbType.NVarChar);
			spars[4].Value = Prosepectcust.Trim();
			spars[5] = new SqlParameter("@DHEmpCode", SqlDbType.NVarChar);
			spars[5].Value = lstEmpName;
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_Rec_ProsepectCust_Details");

			return dtRequisitionDetails;
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		return dtRequisitionDetails;
	}
}