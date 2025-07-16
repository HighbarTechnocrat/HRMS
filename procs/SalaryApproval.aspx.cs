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
using System.IO;
using ClosedXML.Excel;
using System.Data.Common;
using System.Data.OleDb;

public partial class procs_SalaryApproval : System.Web.UI.Page
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
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            Emp_Code = Session["Empcode"].ToString();

            

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (CheckUserAccess(Emp_Code))
                    {
                        hasKeys = Request.QueryString.HasKeys();
                        if (hasKeys)
                        {
                            string monthyear = Request.QueryString["id"];

                            if (Request.QueryString["RMSalStatusId"] != null)
                            {
                                int RMSalStatusId = Convert.ToInt32(Request.QueryString["RMSalStatusId"]);
                                dt = spm.CheckEmpDetailsForSalaryApproval(RMSalStatusId);
                                if (dt.Rows.Count > 0)
                                {
                                    //lblmsg.Text = "You have already submitted salary data for the month of " + monthyear;
                                    mobile_btnSave.Visible = false;
                                    divPara.Visible = false;
                                    divexport.Visible = false;
                                    LoadSavedData(RMSalStatusId);
                                }
                                else
                                {

                                    mobile_btnSave.Visible = true;
                                    divPara.Visible = true;
                                    LoadDataWithRMSalStatusId(RMSalStatusId);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void LoadSavedData(int RMSalStatusId)
    {
        //string monthyear = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString().Substring(2);
        dt = spm.GetSavedEmpDetailsForSalaryApproval(RMSalStatusId);
        dgSalApproved.Visible = true;
        dgSalApp.Visible = false;
        mobile_btnSave.Visible = false;
        divexport.Visible = false;
        dgSalApproved.DataSource = null;
        dgSalApproved.DataBind();
        if (dt.Rows.Count > 0)
        {
            dgSalApproved.DataSource = dt;
            dgSalApproved.DataBind();
        }
        
    }

    //public void LoadProcessedData(string empCode,string monthYear)
    //{
    //    dt = spm.GetSavedEmpDetailsForSalaryApproval(empCode, monthYear);
    //    dgSalApproved.Visible = true;
    //    dgSalApp.Visible = false;
    //    dgSalApproved.DataSource = null;
    //    dgSalApproved.DataBind();
    //    if (dt.Rows.Count > 0)
    //    {
    //        dgSalApproved.DataSource = dt;
    //        dgSalApproved.DataBind();
    //        ViewState["Data"] = dt;
    //        divexport.Visible = true;
    //    }
    //    else
    //    {
    //        divexport.Visible = false;
    //    }
    //}

    public bool CheckUserAccess(string empCode)
    {
        bool useraccess = false;
        dt = spm.getEmpDetailsForSalaryApproval(empCode);
        dgSalApproved.Visible = false;
        dgSalApp.Visible = true;
        dgSalApp.DataSource = null;
        dgSalApp.DataBind();
        if (dt.Rows.Count <= 0)
        {
            lblmsg.Text = "You have not access of this page.";
            mobile_btnSave.Visible = false;
            divPara.Visible = false;
            divexport.Visible = false;
            dgSalApp.Visible = false;
            dgSalApproved.Visible = false;
            aSalApp.HRef = "../default.aspx";
            useraccess = false;
        }
        else
        {
            useraccess = true;
        }
        return useraccess;
    }
    public void LoadDataWithRMSalStatusId(int RMStatusId)
    {
        dt = spm.GetEmpDatawithSalStatusId(RMStatusId);
        dgSalApproved.Visible = false;
        dgSalApp.Visible = true;
        dgSalApp.DataSource = null;
        dgSalApp.DataBind();
        if (dt.Rows.Count > 0)
        {
            dgSalApp.DataSource = dt;
            dgSalApp.DataBind();
        }
        else
        {
            lblmsg.Text = "You have not access of this page.";
            mobile_btnSave.Visible = false;
            divPara.Visible = false;
            divexport.Visible = false;
            dgSalApp.Visible = false;
            dgSalApproved.Visible = false;
            aSalApp.HRef = "../default.aspx";
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string empcode,remarks = string.Empty;
        int SalStatus;
        bool isValidate = true;
        foreach (GridViewRow row in dgSalApp.Rows)
        {
            //Label lblempCode = (Label)row.FindControl("lblEmp_Code");
            //empcode = lblempCode.Text;

            DropDownList ddl = (DropDownList)row.FindControl("ddlSalStatus");
            SalStatus = Convert.ToInt32(ddl.SelectedValue);

            TextBox txtremarks = (TextBox)row.FindControl("txtRemarks");
            remarks = txtremarks.Text;

            if (SalStatus==0 && txtremarks.Text.Trim()=="")
            {
                Label lblGDmsg = (Label)row.FindControl("lblGDmsg");
                lblGDmsg.Text="Please enter remarks.";
                isValidate = false;
            }

            //string monthyear = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString().Substring(2);

            //bool saved = spm.InsertEmpDataForSalaryApproval(empcode,SalStatus,remarks, monthyear, Emp_Code);
        }
        if (isValidate==false)
        {
            lblmsg.Text = "Please enter remarks against current salary status as HOLD.";
            return;
        }
        string monthyear = "";
        if (Request.QueryString["month"]!=null)
        {
            monthyear = Request.QueryString["month"];
        }
        else
        {
            monthyear = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString().Substring(2);
        }
        int RMSalStatusId = Convert.ToInt32(Request.QueryString["RMSalStatusId"]);

        foreach (GridViewRow row in dgSalApp.Rows)
        {
            Label lblempCode = (Label)row.FindControl("lblEmp_Code");
            empcode = lblempCode.Text;

            DropDownList ddl = (DropDownList)row.FindControl("ddlSalStatus");
            SalStatus = Convert.ToInt32(ddl.SelectedValue);

            TextBox txtremarks = (TextBox)row.FindControl("txtRemarks");
            remarks = txtremarks.Text;

            //if (SalStatus == 0 && txtremarks.Text.Trim() == "")
            //{
            //    Label lblGDmsg = (Label)row.FindControl("lblGDmsg");
            //    lblGDmsg.Text = "Please enter remarks.";
            //    return;
            //}

            bool saved = spm.InsertEmpDataForSalaryApproval(empcode, SalStatus, remarks, monthyear, Emp_Code,RMSalStatusId);
            lblmsg.Text = "";

        }

        //if (saved)
        //{
        //    //alert to show successful
        //    ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('Inserted successfully');</script>", false);
        //}
        //else
        //{
        //    //alert to show unsuccessful
        //    ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('Insertion Unsuccessfully');</script>", false);
        //}

        bool insert = spm.InsertRMEmpDataForSalaryApproval(Emp_Code,1,monthyear);

        lblmsg.Text = "Records updated successfully.";
        LoadSavedData(RMSalStatusId);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["Data"]; //your datatable  
            //dt.Columns.Remove("MonthAndYear");
            //t.Columns.RemoveAt(6);
            //dt.Columns.Remove("Status");
            //t.Columns.RemoveAt(7);

            string FileName = "SalaryApproval_" + DateTime.Now + ".xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "SalaryStatusData");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    dt.Dispose();
                    Response.End();
                }
            }
        }
        catch (Exception ex) { }
    }
}