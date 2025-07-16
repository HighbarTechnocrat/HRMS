using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;

public partial class procs_Policies : System.Web.UI.Page
{
    DataTable dtdocument;
    SP_Methods spm = new SP_Methods();
    int DocID;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }

        bool hasKeys = Request.QueryString.HasKeys();

        if (hasKeys)
        {
            DocID = Convert.ToInt32(Request.QueryString["Id"]);
        }

        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
        }
        else
        {
            Page.SmartNavigation = true;
            if (!Page.IsPostBack)
            {
                LoadData(DocID);
                HDEmpCode.Value = Convert.ToString(Session["Empcode"]);
                GetEmployeeDetails();
            }
        }
    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(HDEmpCode.Value);
            if (dtEmpDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "3")
                {
                    lblmsg.Visible = true;
                    DivMessage.Visible = true;
                    dghbtPolicies.Visible = false;
                }
                else
                {
                    lblmsg.Visible = false;
                    dghbtPolicies.Visible = true;
                    DivMessage.Visible = false;
                }

            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }
    }




    public void LoadData(int MainDocID)
    {
        dtdocument = spm.GetDocumentUploadList(MainDocID);
        dghbtPolicies.DataSource = null;
        dghbtPolicies.DataBind();
        if (dtdocument.Rows.Count > 0)
        {
            dghbtPolicies.DataSource = dtdocument;
            dghbtPolicies.DataBind();
        }
    }
    protected void dghbtPolicies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName== "ViewFile")
        {
            string filepath= Convert.ToString(ConfigurationManager.AppSettings["DocumentPolicyPath"]).Trim();
            string fileName = filepath + e.CommandArgument;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open(ViewPoliciesDocument.aspx?fileName="+fileName+",'_blank');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ViewPoliciesDocument.aspx?fileName=" + fileName + "','_blank');", true);
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(fileName);
            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //}
        }
    }
}