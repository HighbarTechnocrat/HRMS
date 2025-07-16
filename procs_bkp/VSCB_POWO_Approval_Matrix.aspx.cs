using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class procs_VSCB_POWO_Approval_Matrix : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    public DataSet ldsEmployees;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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
            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                HDInPEmpcode.Value = Convert.ToString(Session["Empcode"]).Trim();
                if (!Page.IsPostBack)
                {
                    get_POWOApprovalMatrix_BindData();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPrjDeptId.Value = Request.QueryString[0];
                       
                        lstCostCenter.Enabled = false;
                        trvl_btnSave.Text = "Update";
                        DataBindOndropdownlistCostCenterSelected();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Select Cost Center";
            return;
        }

        foreach (GridViewRow crow in gvMngTravelRqstList.Rows)
        {
            DropDownList lstApproval1 = (DropDownList)crow.FindControl("lstApproval1");
            DropDownList lstApproval2 = (DropDownList)crow.FindControl("lstApproval2");
            DropDownList lstApproval3 = (DropDownList)crow.FindControl("lstApproval3");
            DropDownList lstApproval4 = (DropDownList)crow.FindControl("lstApproval4");

            string StrApproval1 = Convert.ToString(lstApproval1.SelectedValue).Trim();
            string StrApproval2 = Convert.ToString(lstApproval2.SelectedValue).Trim();
            string StrApproval3 = Convert.ToString(lstApproval3.SelectedValue).Trim();
            string StrApproval4 = Convert.ToString(lstApproval4.SelectedValue).Trim();

            
                if (StrApproval1 == StrApproval2 || StrApproval1 == StrApproval3 || StrApproval1 == StrApproval4)
                {
                    if (StrApproval1 !="0")
                     {
                       lblmessage.Text = "Same Approval name not allowed against PO type";
                       return;
                     }
                    
                }
                else if (StrApproval2 == StrApproval1 || StrApproval2 == StrApproval3 || StrApproval2 == StrApproval4)
                {
                     if (StrApproval2 != "0")
                      {
                        lblmessage.Text = "Same Approval name not allowed against PO type";
                        return;
                       }
                }
                else if (StrApproval3 == StrApproval1 || StrApproval3 == StrApproval2 || StrApproval3 == StrApproval4)
                {
                if (StrApproval3 != "0")
                {
                    lblmessage.Text = "Same Approval name not allowed against PO type";
                    return;
                }
               }
                else if (StrApproval4 == StrApproval1 || StrApproval4 == StrApproval2 || StrApproval4 == StrApproval3)
                {
                  if (StrApproval4 != "0")
                  {
                      lblmessage.Text = "Same Approval name not allowed against PO type";
                      return;
                   }
               }
        }

           foreach (GridViewRow crow in gvMngTravelRqstList.Rows)
           {
            string sPotypeId = Convert.ToString(gvMngTravelRqstList.DataKeys[crow.RowIndex].Values[0]).Trim();
            DropDownList lstApproval1 = (DropDownList)crow.FindControl("lstApproval1");
            DropDownList lstApproval2 = (DropDownList)crow.FindControl("lstApproval2");
            DropDownList lstApproval3 = (DropDownList)crow.FindControl("lstApproval3");
            DropDownList lstApproval4 = (DropDownList)crow.FindControl("lstApproval4");


            SqlParameter[] spars1 = new SqlParameter[14];
            spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars1[0].Value = "Insert";
            spars1[1] = new SqlParameter("@Tallycode", SqlDbType.VarChar);
            spars1[1].Value = Convert.ToString(lstCostCenter.SelectedItem).Trim();

            spars1[2] = new SqlParameter("@Appr_ID_5", SqlDbType.VarChar);
            spars1[2].Value = "5";
            spars1[3] = new SqlParameter("@Appr_ID_7", SqlDbType.VarChar);
            spars1[3].Value = "7";
            spars1[4] = new SqlParameter("@Appr_ID_11", SqlDbType.VarChar);
            spars1[4].Value = "11";
            spars1[5] = new SqlParameter("@Appr_ID_13", SqlDbType.VarChar);
            spars1[5].Value = "13";

            spars1[6] = new SqlParameter("@EmpCode_1", SqlDbType.VarChar);
            spars1[6].Value = Convert.ToString(lstApproval1.SelectedValue);
            spars1[7] = new SqlParameter("@EmpCode_2", SqlDbType.VarChar);
            spars1[7].Value = Convert.ToString(lstApproval2.SelectedValue);
            spars1[8] = new SqlParameter("@EmpCode_3", SqlDbType.VarChar);
            spars1[8].Value = Convert.ToString(lstApproval3.SelectedValue);
            spars1[9] = new SqlParameter("@EmpCode_4", SqlDbType.VarChar);
            spars1[9].Value = Convert.ToString(lstApproval4.SelectedValue);

            spars1[10] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
            spars1[10].Value = sPotypeId;
            spars1[11] = new SqlParameter("@Prj_Dept_id", SqlDbType.VarChar);
            spars1[11].Value = Convert.ToString(lstCostCenter.SelectedValue).Trim();
            spars1[12] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars1[12].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DSInsert = spm.getDatasetList(spars1, "SP_VSCB_POWOApprovalMatrix_Namaste");


        }

        lblmessage.Text = "";

       Response.Redirect("VSCB_POWO_Approval_MatrixList.aspx");
    }

    #endregion

    #region PageMethods
    private void get_POWOApprovalMatrix_BindData()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWOApprovalMatrix_Namaste_dropdown_List_ACC";

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        ldsEmployees = dsList;

        if (dsList.Tables[1].Rows.Count > 0)
        {
            gvMngTravelRqstList.DataSource = dsList.Tables[1];
            gvMngTravelRqstList.DataBind();
        }
        ligrd.Visible = true;
        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsList.Tables[2];
            lstCostCenter.DataTextField = "Tallycode";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind();
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));
    }

    public DataSet GetEmployee_list()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWOApprovalMatrix_Namaste_dropdown_List_ACC";
        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        return dsList;
    }


    #endregion

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_POWO_Approval_MatrixList.aspx");
    }
    protected void gvMngTravelRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dsCostcenters = GetEmployee_list();
            

            DropDownList lstApproval1 = (e.Row.FindControl("lstApproval1") as DropDownList);
            lstApproval1.DataSource = dsCostcenters.Tables[0];
            lstApproval1.DataTextField = "Emp_Name";
            lstApproval1.DataValueField = "Emp_Code";
            lstApproval1.DataBind();
            lstApproval1.Items.Insert(0, new ListItem("", "0"));

            DropDownList lstApproval2 = (e.Row.FindControl("lstApproval2") as DropDownList);
            lstApproval2.DataSource = dsCostcenters.Tables[0];
            lstApproval2.DataTextField = "Emp_Name";
            lstApproval2.DataValueField = "Emp_Code";
            lstApproval2.DataBind();
            lstApproval2.Items.Insert(0, new ListItem("", "0"));

            DropDownList lstApproval3 = (e.Row.FindControl("lstApproval3") as DropDownList);
            lstApproval3.DataSource = dsCostcenters.Tables[0];
            lstApproval3.DataTextField = "Emp_Name";
            lstApproval3.DataValueField = "Emp_Code";
            lstApproval3.DataBind();
            lstApproval3.Items.Insert(0, new ListItem("", "0"));

            DropDownList lstApproval4 = (e.Row.FindControl("lstApproval4") as DropDownList);
            lstApproval4.DataSource = dsCostcenters.Tables[0];
            lstApproval4.DataTextField = "Emp_Name";
            lstApproval4.DataValueField = "Emp_Code";
            lstApproval4.DataBind();
            lstApproval4.Items.Insert(0, new ListItem("", "0"));

            string lblApproval1 = (e.Row.FindControl("lblApproval1") as Label).Text;
            string lblApproval2 = (e.Row.FindControl("lblApproval2") as Label).Text;
            string lblApproval3 = (e.Row.FindControl("lblApproval3") as Label).Text;
            string lblApproval4 = (e.Row.FindControl("lblApproval4") as Label).Text;

            if (lblApproval1 != "")
            {
                lstApproval1.Items.FindByValue(lblApproval1).Selected = true;
            }
            if (lblApproval2 != "")
            {
                lstApproval2.Items.FindByValue(lblApproval2).Selected = true;
            }
            if (lblApproval3 != "")
            {
                lstApproval3.Items.FindByValue(lblApproval3).Selected = true;
            }
            if (lblApproval4 != "")
            {
                lstApproval4.Items.FindByValue(lblApproval4).Selected = true;
            }
        }
    }
    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindOndropdownlistCostCenterSelected();
    }

    public void DataBindOndropdownlistCostCenterSelected()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWOApprovalMatrix_Namaste_dropdown_List_ACC";

        if (hdnPrjDeptId.Value == "")
        {
            spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(lstCostCenter.SelectedItem.Text.Trim());
        }
        else
        {
            spars[1] = new SqlParameter("@Prj_Dept_id", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnPrjDeptId.Value);
        }

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        ldsEmployees = dsList;

        if (dsList.Tables[3].Rows.Count > 0)
        {
            ligrd.Visible = true;
            DivbuttonAll.Visible = true;
            gvMngTravelRqstList.DataSource = dsList.Tables[3];
            gvMngTravelRqstList.DataBind();
        }
        if (dsList.Tables[4].Rows.Count > 0)
        {
            lstCostCenter.SelectedValue = dsList.Tables[4].Rows[0]["Dept_ID"].ToString();
        }
    }
    
}