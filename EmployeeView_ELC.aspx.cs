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
using System.Linq;

public partial class procs_EmployeeView_ELC : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    hdnGender.Value = "";
                    HDEmpID.Value = Request.QueryString["Emp_id"];
                    GetRecords();
                    BindProjectResponsibility();
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim());
                    FilePathFirstgrid.Value = @"D:\HRMS\hrmsadmin\CandidateDocument\";
                    FilePathFirstgrid1.Value = @"D:\HRMS\hrmsadmin\files\Contract\";
                    btnTra_Details.Text = "-";
                    Div_LocationChangeList.Visible = true;

                    trvl_localbtn.Text = "-";
                    Div_DesignationList.Visible = true;

                    trvl_accmo_btn.Text = "-";
                    Div_BandList.Visible = true;

                   // lnkbtn_expdtls.Text = "-";
                    DIV_Document.Visible = true;

                    lnk_RMC.Text = "-";
                    Div_RM.Visible = true;

                    lnk_Leave.Text = "-";
                    Div_leave.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void GetRecords()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Record";
        spars[1] = new SqlParameter("@Emp_id", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(HDEmpID.Value);
        spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[2].Value = Session["Empcode"].ToString();

        DS = spm.getDatasetList(spars, "SP_ELC_EmployeeLifeCycleList");

        if (DS.Tables[0].Rows.Count > 0)
        {
            Txt_EmployeeType.Text = Convert.ToString(DS.Tables[0].Rows[0]["Particulars"]).Trim();
            Txt_EmployeeCode.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Code"]).Trim();
            Txt_EmployeeName.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Name"]).Trim();
            Txt_EmailAddress.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            Txt_DateOfJoining.Text = Convert.ToString(DS.Tables[0].Rows[0]["emp_doj"]).Trim();
            Txt_Department.Text = Convert.ToString(DS.Tables[0].Rows[0]["Department_Name"]).Trim();
            Txt_Designation.Text = Convert.ToString(DS.Tables[0].Rows[0]["DesginationName"]).Trim();
            Txt_CurrentRM.Text = Convert.ToString(DS.Tables[0].Rows[0]["RMName"]).Trim();
            Txt_Location.Text = Convert.ToString(DS.Tables[0].Rows[0]["Location_name"]).Trim();
            Txt_CurrentBand.Text = Convert.ToString(DS.Tables[0].Rows[0]["BAND"]).Trim();
            Txt_CurrentShift.Text = Convert.ToString(DS.Tables[0].Rows[0]["Shift_Name"]).Trim();
            Txt_MobileNo.Text = Convert.ToString(DS.Tables[0].Rows[0]["mobile"]).Trim();
            hdnGender.Value = Convert.ToString(DS.Tables[0].Rows[0]["Gender"]).Trim();

            if (Txt_EmployeeCode.Text.StartsWith("R") || Txt_EmployeeCode.Text.StartsWith("S") || Txt_EmployeeCode.Text.StartsWith("I") || Txt_EmployeeCode.Text.StartsWith("P"))
               Profile_Photo.ImageUrl = "~/themes/creative1.0/images/profile55x55/" + Txt_EmployeeCode.Text.Trim() + ".jpg";
            else
             Profile_Photo.ImageUrl = "~/themes/creative1.0/images/profile55x55/" + Convert.ToDouble(Txt_EmployeeCode.Text.Trim()) + ".jpg";


        }

        if (DS.Tables[1].Rows.Count > 0)
        {
            gvLocationChangeList.DataSource = DS.Tables[1];
            gvLocationChangeList.DataBind();
        }
        if (DS.Tables[2].Rows.Count > 0)
        {
            GVDesignation.DataSource = DS.Tables[2];
            GVDesignation.DataBind();
        }

        if (DS.Tables[3].Rows.Count > 0)
        {
            GVBand.DataSource = DS.Tables[3];
            GVBand.DataBind();
        }
        if (DS.Tables[4].Rows.Count > 0)
        {
            GVDocumentInfo.DataSource = DS.Tables[4];
            GVDocumentInfo.DataBind();
            Span_Document.Visible = false;
        }
        else
        {
            Span_Document.Visible = true;
        }
        if (DS.Tables[5].Rows.Count > 0)
        {
            gv_RMDetails.DataSource = DS.Tables[5];
            gv_RMDetails.DataBind();           
        }
        if (DS.Tables[6].Rows.Count > 0)
        {
            var getFileName = DS.Tables[6].Rows[0]["File"].ToString();
            if(getFileName!="")
            {
                GVFirstGridContractAppletter.DataSource = DS.Tables[6];
                GVFirstGridContractAppletter.DataBind();
                SpanGVFirstGridContractAppletter.Visible = false;
            }
            else
            {
                SpanGVFirstGridContractAppletter.Visible = true;
            }
        }
 else
        {
            SpanGVFirstGridContractAppletter.Visible = true;
        }
        if (DS.Tables[7].Rows.Count > 0)
        {
            dvDT7.DataSource = DS.Tables[7];
            dvDT7.DataBind();
            SpandvDT7.Visible = false;
        }
        else
        {
            SpandvDT7.Visible = true;
        }
        if (DS.Tables[8].Rows.Count > 0)
        {
            gvDT8.DataSource = DS.Tables[8];
            gvDT8.DataBind();
            SpangvDT8.Visible = false;
        }
        else
        {
            SpangvDT8.Visible = true;
        }
        if (DS.Tables[9].Rows.Count > 0)
        {
            gvDT9.DataSource = DS.Tables[9];
            gvDT9.DataBind();
            SpangvDT9.Visible = false;
        }
        else
        {
            SpangvDT9.Visible = true;
        }
        if (DS.Tables[10].Rows.Count > 0)
        {
            gvDT10.DataSource = DS.Tables[10];
            gvDT10.DataBind();
            SpangvDT10.Visible = false;
        }
        else
        {
            SpangvDT10.Visible = true;
        }
        if (DS.Tables[11].Rows.Count > 0)
        {
            gvDT11.DataSource = DS.Tables[11];
            gvDT11.DataBind();
            SpangvDT11.Visible = false;
        }
        else
        {
            SpangvDT11.Visible = true;
        }
        if (DS.Tables[12].Rows.Count > 0)
        {
            gvDT12.DataSource = DS.Tables[12];
            gvDT12.DataBind();
            SpangvDT12.Visible = false;
        }
        else
        {
            SpangvDT12.Visible = true;
        }

        if (DS.Tables[13].Rows.Count > 0)
        {
            gv_Leave.DataSource = DS.Tables[13];
            gv_Leave.DataBind();
        }        
    }

    // Location Button
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (btnTra_Details.Text == "+")
        {
            btnTra_Details.Text = "-";
            Div_LocationChangeList.Visible = true;
        }
        else
        {
            btnTra_Details.Text = "+";
            Div_LocationChangeList.Visible = false;
        }
    }

    // Designation button
    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {
        if (trvl_localbtn.Text == "+")
        {
            trvl_localbtn.Text = "-";
            Div_DesignationList.Visible = true; 
        }
        else
        {
            trvl_localbtn.Text = "+";
            Div_DesignationList.Visible = false;
        }
    }

    // Band Button
    protected void trvl_accmo_btn_Click(object sender, EventArgs e)
    {
        if (trvl_accmo_btn.Text == "+")
        {
            trvl_accmo_btn.Text = "-";
            Div_BandList.Visible = true; 
        }
        else
        {
            trvl_accmo_btn.Text = "+";
            Div_BandList.Visible = false;
        }
    }

    // Document Details
    protected void lnkbtn_expdtls_Click(object sender, EventArgs e)
    {
        //if (lnkbtn_expdtls.Text == "+")
        //{
        //    lnkbtn_expdtls.Text = "-";
        //    DIV_Document.Visible = true;
        //}
        //else
        //{
        //    lnkbtn_expdtls.Text = "+";
        //    DIV_Document.Visible = false;
        //}
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList_ELC.aspx");
    }

    protected void lnk_RMC_Click(object sender, EventArgs e)
    {
        if (lnk_RMC.Text == "+")
        {
            lnk_RMC.Text = "-";
            Div_RM.Visible = true;
        }
        else
        {
            lnk_RMC.Text = "+";
            Div_RM.Visible = false;
        }
    }

    protected void GVFirstGridContractAppletter_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lnk_Project_Click(object sender, EventArgs e)
    {
        if (lnk_Project.Text == "+")
        {
            lnk_Project.Text = "-";
            Div_PR.Visible = true;
        }
        else
        {
            lnk_Project.Text = "+";
            Div_PR.Visible = false;
        }
    }

    protected void gvPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPR.PageIndex = e.NewPageIndex;
        BindProjectResponsibility();
    }

    public void BindProjectResponsibility()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "ProjectResponsibilities";
            spars[1] = new SqlParameter("@Emp_id", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(HDEmpID.Value);
            spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();

            DS = spm.getDatasetList(spars, "SP_ELC_EmployeeLifeCycleList");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvPR.DataSource = DS.Tables[0];
                gvPR.DataBind();
                SpangvPR.Visible = false;
            }
            else
            {
                SpangvPR.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_Leave_Click(object sender, EventArgs e)
    {
        if (lnk_Leave.Text == "+")
        {
            lnk_Leave.Text = "-";
            Div_leave.Visible = true;
        }
        else
        {
            lnk_Leave.Text = "+";
            Div_leave.Visible = false;
        }
    }

    protected void gv_Leave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int indexOfColumn = 6;
        try
        {
            var getGenderStatus = hdnGender.Value;
            if(getGenderStatus== "Male")
            {
                if (e.Row.Cells.Count > indexOfColumn)
                {
                    e.Row.Cells[indexOfColumn].Visible = false;
                }
            }           
        }
        catch (Exception)
        {

        }
    }
}