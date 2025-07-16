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

public partial class KRA_Reset : System.Web.UI.Page
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
            get_Employee_List();
        } 
    }

 

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        
        string strempcode = "";
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
        string strOrderby = "order by Emp_Name;";
        string strsql = " Select  KRA_ID,e.Emp_code,Emp_Name,k.band,p.period_name,k.project_location,d.Department_Name,ds.DesginationName,k.submitted_on,k.approved_On,k.Approved_KRA_path as KRAFileName " +
                       " from tblDepartmentMaster d " +
                       " inner join tbl_Employee_Mst e on d.Department_id = e.dept_id " +
                       " inner join tbl_KRA_trn_Main k on k.department_id = d.Department_id and k.emp_code = e.Emp_Code " +
                       "  inner join tbl_KRA_mst_period p on p.period_id = k.period_id " +
                       " inner join tbldesignationMaster ds on ds.Designation_iD = k.designation_id ";
                      // " where  k.period_id=3";

            if(Convert.ToString(strempcode).Trim()!="")
            {
                strwhere = " where k.emp_code in ("+strempcode+")";
            }

        strsql = strsql + Convert.ToString(strwhere).Trim() + strOrderby;
        DataTable dtTeamRpt = spm.getDataList_SQL(strsql);

        dgEmployee_KRA.DataSource = null;
        dgEmployee_KRA.DataBind();

        if (dtTeamRpt.Rows.Count>0)
        {
            dgEmployee_KRA.DataSource = dtTeamRpt;
            dgEmployee_KRA.DataBind();
        }


    }
   

    public void get_Employee_List()
    {
        DataSet dtDepartment = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Current_Period_Employee_KRA_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnloginempcode.Value;

        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
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
            ddl_Employees.Items.Insert(0, new ListItem("Select Employee", "0"));
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



    public void get_Employee_KRA_Details_forReset()
    {
        DataSet dtDepartment = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Employee_KRA_Details_for_Reset";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(ddl_Employees.SelectedValue).Trim();

        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
        spars[2].Value = DBNull.Value;

        dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

        
        dgEmployee_KRA.DataSource = null;
        dgEmployee_KRA.DataBind();
        txtKRA_Id.Text = "";
        if (dtDepartment.Tables[0].Rows.Count > 0)
        {
            dgEmployee_KRA.DataSource = dtDepartment.Tables[0];
            dgEmployee_KRA.DataBind();
            txtKRA_Id.Text = Convert.ToString(dtDepartment.Tables[0].Rows[0]["KRA_ID"]).Trim();
        }


    }





    protected void claimfuel_btnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            lblmessage.Text = "";

            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }


            if (Convert.ToString(ddl_Employees.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Employee";
                return;
            }

            DataSet dtDepartment = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Reset_Employee_KRA";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);            
            spars[1].Value =ddl_Employees.SelectedValue;
            
            spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            if (Convert.ToString(txtKRA_Id.Text).Trim() != "")
                spars[2].Value = Convert.ToDecimal(txtKRA_Id.Text);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@empcodeResetBy", SqlDbType.VarChar);
            spars[3].Value = hdnloginempcode.Value;

            dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
             
                dgEmployee_KRA.DataSource = null;
                dgEmployee_KRA.DataBind();
                get_Employee_List();
                lblmessage.Text = "KRA Reset successfully";
            txtKRA_Id.Text = "";


            #region multiple Reset KRA option not required
           /* string strempcode_list = "";
             
            foreach (GridViewRow row in dgEmployee_KRA.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect"); 
                if(chkSelect.Checked)
                { 
                    if (Convert.ToString(strempcode_list).Trim() == "")
                    {    
                        strempcode_list = "'" + Convert.ToString(dgEmployee_KRA.DataKeys[row.RowIndex].Values[1]).Trim() + "'";
                    }
                    else
                    {
                        strempcode_list = strempcode_list + ", '" + Convert.ToString(dgEmployee_KRA.DataKeys[row.RowIndex].Values[1]).Trim() + "'" ;
                    }
                }
            }

            if (Convert.ToString(strempcode_list).Trim() != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Delete from tbl_KRA_trn_Measurement_dtls_tmp_tmp where emp_code in (" + strempcode_list + ") ");
                sb.Append(" Delete from tbl_KRA_trn_Measurement_dtls_tmp where emp_code in (" + strempcode_list + ") ");
                sb.Append(" Delete from tbl_KRA_trn_Goal_tmp where emp_code in (" + strempcode_list + ") ");

                sb.Append(" Delete from tbl_KRA_trn_approval_status where KRA_ID in (Select m.KRA_ID from tbl_KRA_trn_Main m where m.emp_code in (" + strempcode_list + ")");
                sb.Append(" and period_id  in (Select p.period_id from tbl_KRA_mst_period p where p.iscurrent_period = 1 and p.is_active = 1))");

                sb.Append(" Delete from tbl_KRA_trn_Measurement_dtls where KRA_ID in (Select m.KRA_ID from tbl_KRA_trn_Main m where m.emp_code in (" + strempcode_list + ")");
                sb.Append(" and period_id  in (Select p.period_id from tbl_KRA_mst_period p where p.iscurrent_period = 1 and p.is_active = 1))");

                sb.Append(" Delete from tbl_KRA_trn_Goal where KRA_ID in (Select m.KRA_ID from tbl_KRA_trn_Main m where m.emp_code in (" + strempcode_list + ")");
                sb.Append(" and period_id  in (Select p.period_id from tbl_KRA_mst_period p where p.iscurrent_period = 1 and p.is_active = 1))");


                sb.Append(" Delete from tbl_KRA_trn_Main where KRA_ID in (Select m.KRA_ID from tbl_KRA_trn_Main m where m.emp_code in (" + strempcode_list + ")");
                sb.Append(" and period_id  in (Select p.period_id from tbl_KRA_mst_period p where p.iscurrent_period = 1 and p.is_active = 1))");

                DataTable dtTeamRpt = spm.getDataList_SQL(sb.ToString());
                dgEmployee_KRA.DataSource = null;
                dgEmployee_KRA.DataBind();
                get_Employee_List();
                lblmessage.Text = "KRA Reset successfully";
            }
            */
            #endregion

        }
        catch
        {

        }
    }

    protected void ddl_Employees_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Convert.ToString(ddl_Employees.SelectedValue).Trim()!="0")
        get_Employee_KRA_Details_forReset();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        get_Employee_List();
        dgEmployee_KRA.DataSource = null;
        dgEmployee_KRA.DataBind();
        lblmessage.Text = "";
    }
}