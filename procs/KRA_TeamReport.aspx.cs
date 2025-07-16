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

public partial class KRA_TeamReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
        }

        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRAFiles"]).Trim());
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRAFiles"]).Trim());
            get_KRAPeriodList();
            get_Departmen_Employee_List();




        } 
    }

    protected void lstPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Departmen_Employee_List();
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(lstPeriod.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select period";
            return;
        }

        string strempcode = "";
        string strApprovalType = "";

        if (DDLApprovalType.SelectedItem.Text != "Approval Type")
        {
            strApprovalType = DDLApprovalType.SelectedValue;
        }

        foreach (ListItem item in ddl_Employees.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    if (Convert.ToString(strempcode).Trim() == "")
                        strempcode = "'" + item.Value + "'";
                    else
                        strempcode = strempcode + ",'" + item.Value + "'";


                }
            }
        }

        string strwhere = "";
        string strwhereApproval = "";
        string strOrderby = " order by Emp_Name;";
        string strsql = " Select  KRA_ID,e.Emp_code,Emp_Name,k.band,p.period_name,k.project_location,d.Department_Name,ds.DesginationName,k.submitted_on,CASE WHEN k.ISDeemedAcceptance =1 THEN 'Yes' ELSE 'No' END AS 'DeemedApproval',k.approved_On,k.Approved_KRA_path as KRAFileName " +
                       " from tblDepartmentMaster d " +
                       " inner join tbl_Employee_Mst e on d.Department_id = e.dept_id " +
                       " inner join tbl_KRA_trn_Main k on k.department_id = d.Department_id and k.emp_code = e.Emp_Code " +
                       "  inner join tbl_KRA_mst_period p on p.period_id = k.period_id " +
                       " inner join tbldesignationMaster ds on ds.Designation_iD = k.designation_id " +
                       " where k.status_id=2 and k.period_id=" + lstPeriod.SelectedValue + " and k.emp_code in (Select ap.emp_code from tbl_KRA_approval_matrix ap where ap.Approver_emp_code='"+ hdnloginempcode.Value + "')"; //and k.department_id in (Select department_id  from tblDepartmentMaster where HOD='"+ hdnloginempcode .Value+ "')";

            if(Convert.ToString(strempcode).Trim()!="")
            {
                strwhere = " and k.emp_code in ("+strempcode+")";
            }
        if (Convert.ToString(strApprovalType).Trim() != "")
        {
            if (DDLApprovalType.SelectedItem.Text == "Deemed Approval")
            {
                strwhereApproval = " and k.ISDeemedAcceptance in (" + strApprovalType + ")";
            }
            else
            {
                strwhereApproval = " and k.ISDeemedAcceptance  " + strApprovalType + " ";
            }
        }

        strsql = strsql + Convert.ToString(strwhere).Trim() + Convert.ToString(strwhereApproval).Trim() + strOrderby;
        DataTable dtTeamRpt = spm.getDataList_SQL(strsql);

        dgKRA_Team.DataSource = null;
        dgKRA_Team.DataBind();

        if (dtTeamRpt.Rows.Count>0)
        {
            dgKRA_Team.DataSource = dtTeamRpt;
            dgKRA_Team.DataBind();
        }


    }
   

    public void get_Departmen_Employee_List()
    {
        DataSet dtDepartment = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_myteam_Employee_KRA_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnloginempcode.Value;

        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
        if (Convert.ToString(lstPeriod.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToInt32(lstPeriod.SelectedValue);
        else
            spars[2].Value = DBNull.Value;

           dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
      
        ddl_Employees.DataSource = null;
        ddl_Employees.DataBind();
        if (dtDepartment.Tables[0].Rows.Count > 0)
        {
          
            ddl_Employees.DataSource = dtDepartment.Tables[0];
            ddl_Employees.DataTextField = "Emp_Name";
            ddl_Employees.DataValueField = "emp_code";
            ddl_Employees.DataBind();

            //foreach (DataRow item in dtDepartment.Tables[0].Rows)
            //{
            //    var getName = item["Emp_Name"].ToString();
            //    foreach (ListItem itm in ddl_Employees.Items)
            //    {
            //        if (itm.Text == getName)
            //        {
            //            itm.Attributes.Add("disabled", "disabled");
            //        }
            //    }
            //}

        }
       

    }


    public void get_KRAPeriodList()
    {
        DataSet dtPeriod= new DataSet();

        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_KRAPeriod_list";

        dtPeriod = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if (dtPeriod.Tables[0].Rows.Count > 0)
        {
            lstPeriod.DataSource = dtPeriod.Tables[0];
            lstPeriod.DataTextField = "period_name";
            lstPeriod.DataValueField = "period_id";
            lstPeriod.DataBind();
            lstPeriod.Items.Insert(0, new ListItem("Select Period", "0"));
        }

    }

 



}