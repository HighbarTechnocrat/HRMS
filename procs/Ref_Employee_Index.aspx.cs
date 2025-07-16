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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
public partial class Ref_Employee_Index : System.Web.UI.Page
{

	SP_Methods spm = new SP_Methods();
	public DataTable dtEmp, dtRectruter, DTPendingPosition, DTPendingModule;
	DataSet dsRecruterInox, dsInterviewerInox;
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			if (Convert.ToString(Session["Empcode"]) == "R-001")
			{
				Session["Empcode"] = "00631013";
			}
			hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
			//  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
			if (Page.User.Identity.IsAuthenticated == false)
			{
				//Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					CheckModeratorEmp();
					GetReferralPendingCount();
					BindPendingPosition();
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
		public void CheckModeratorEmp()
		{
			DataTable dtScreener = spm.GetRef_CandidatedList(Convert.ToString(Session["Empcode"]).Trim(), "CheckModeratorEmp");
			if (dtScreener.Rows.Count > 0)
			{
					span_App_head.Visible = true;
					lnk_CreateJDBank.Visible = true;
					lnk_MyJDBank.Visible = true;
			}
		}
	public void GetReferralPendingCount()
	{
		DataTable dtScreener = spm.GetRef_CandidatedList(Convert.ToString(Session["Empcode"]).Trim(), "GetReferralPendingCount");
		if (dtScreener.Rows.Count > 0)
		{
			lnk_CreateJDBank.Text = "Inbox:(" + dtScreener.Rows.Count + ")";
		}

	}
	public void BindPendingPosition()
	{
		try
		{
			
			StringBuilder html = new StringBuilder();
			DTPendingModule = spm.GetRef_CandidatedList("", "check_Pending_Position");
			if (DTPendingModule.Rows.Count > 0)
			{
				int i = 0;
				html.Append("<div class='edit-contact'>");
				html.Append("<ul id='editform' runat='server' style='padding-left:10px;'>");
				foreach (DataRow item in DTPendingModule.Rows)
				{
					var ModuleId = Convert.ToInt32(item["ModuleId"]);
					var ModuleName = Convert.ToString(item["ModuleDesc"]);

					DTPendingPosition = spm.GetRef_PendingPosition(ModuleId);
					if (DTPendingPosition.Rows.Count > 0)
					{
						html.Append("<li  style='margin-right: 4%!important;padding-bottom:20px; vertical-align:top;'>");
						//Table start. border = '1' cellspacing='0'border-radius: 25px; style='border-collapse: collapse; border-color: black;!important; border-radius: 10px;'

						html.Append("<table id='gvMain' cellspacing='0'>");
						//Building the Header row.
						html.Append("<tr class='GridViewScrollItem'>");
						html.Append("<th colspan='2' style='background-color:#603F83;color:#C7D3D4;text-align:center;height: 30px;font-weight: 500 !important;'>");//background-color:#F28820
						html.Append(ModuleName);
						html.Append("</th>");
						html.Append("</tr>");
						foreach (DataColumn column in DTPendingPosition.Columns)
						{
							var name = column.ColumnName.ToString();
							if (name == "PositionTitle")
							{
								html.Append("<th  style='background-color:#C7D3D4;color:#603F83;width:70%;'>");//background-color: #3D1956;width:70%;color: #febf39 !important;
								html.Append("Position Title");
								html.Append("</th>");
							}
                            if (name == "NoPositions")
                            {
                                html.Append("<th  width='10px' style='background-color:#C7D3D4;color:#603F83;width:30%;'>");
                                html.Append("Positions");
                                html.Append("</th>");
                            }
                        }

                        html.Append("</tr>");
						html.Append("<tr class='GridViewScrollItem'>");
						foreach (DataRow row in DTPendingPosition.Rows)
						{
                            string PositionTitle_ID = row["PositionTitle_ID"].ToString();

                            html.Append("<tr class='GridViewScrollItem'>");
							foreach (DataColumn column in DTPendingPosition.Columns)
							{
								var name = column.ColumnName.ToString();
                                if (name == "PositionTitle")
                                {
                                    string strlink = "EmployeeReferralView.aspx?Pos_ID=" + PositionTitle_ID.Trim() + "&modl_ID=" + ModuleId + "";
                                    html.Append("<td width='20px' style='color: black;width:8px;'>");
                                    html.Append("<a href="+ strlink + " style='text-decoration:underline' class='Atagv'> " + row[column.ColumnName] + " </a>");
                                    html.Append("</td>");
                                }
                                if (name == "NoPositions")
                                {
                                    html.Append("<td width='20px' style='color: black;width:8px;'>");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }
							}
							html.Append("</tr>");
						}
						//Table end.
						html.Append("</table>");
						html.Append("</li>");
						//i++;
						//int result = (authors.Length);
						//if (result == i)
						//{
						//	i = 0;
						//}
					}
				}
				html.Append("</ul>");
				html.Append("</div>");
				html.Append("</br>");
				html.Append("</br>");
			}
			//Append the HTML string to Placeholder.
			PlaceHolder1.Controls.Add(new Literal { Text = "" });
			PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
		}
		catch (Exception )
		{
			throw;
		}
	}
	


}