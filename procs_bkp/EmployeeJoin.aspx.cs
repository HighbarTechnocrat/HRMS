using System;
using System.Data;
using System.Configuration;
using System.Collections;
using ClosedXML.Excel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;

public partial class EmployeeJoin : System.Web.UI.Page
{

    int Emp_Code;
    Admin_Methods adm = new Admin_Methods();
    Image sortImage = new Image();
    public string SortDireaction
    {
        get
        {
            if (ViewState["SortDireaction"] == null)
                return string.Empty;
            else
                return ViewState["SortDireaction"].ToString();
        }
        set
        {
            ViewState["SortDireaction"] = value;
        }
    }
    private string _sortDirection;

    public string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
                return string.Empty;
            else
                return ViewState["SortExpression"].ToString();
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    private string _SortExpression;

    protected void Page_Load(object sender, EventArgs e)
    {
        string flag;

        if (Request.QueryString["flag"] != "" && Request.QueryString["flag"] != null)
        {
            flag = Convert.ToString(commonclass.GetSafeFlagFromURL(Request.QueryString["flag"]));
            if (flag == "Y")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "<font color='green'><b>Record Created successfully</b></font>";
            }
            else if (flag == "U")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "<font color='green'><b>Record updated successfully</b></font>";
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["sitepathadmin"] + "error.aspx");
            }
        }

        if (!Page.IsPostBack)
        {
            Fill_Employment();
            Fill_Location();
            Fill_Department();
            loadgrid();
            lblName.Text = highbarconfiguration.SiteName +
                       ": Admin: Employee";
        }
        this.Title = highbarconfiguration.SiteName + ": Admin: Employee";

    }

    public void Fill_Location()
    {
        string search_str = "";

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Location";

        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (search_str.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = search_str.ToString();

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_GetDropdown");
        ddl_Location.DataSource = dt;

        ddl_Location.DataTextField = "t_Name";
        ddl_Location.DataValueField = "t_ID";
        ddl_Location.DataBind();
        ListItem item = new ListItem("Select Location", "0");
        ddl_Location.Items.Insert(0, item);
    }

    public void Fill_Department()
    {
        string search_str = "";

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Department";

        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (search_str.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = search_str.ToString();

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_GetDropdown");

        ddl_Department.DataSource = dt;

        ddl_Department.DataTextField = "t_Name";
        ddl_Department.DataValueField = "t_ID";
        ddl_Department.DataBind();
        ListItem item = new ListItem("Select Department", "0");
        ddl_Department.Items.Insert(0, item);
    }

    public void Fill_Employment()
    {
        string search_str = "";

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Employment";

        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (search_str.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = search_str.ToString();

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_GetDropdown");

        ddlEmployment.DataSource = dt;

        ddlEmployment.DataTextField = "t_Name";
        ddlEmployment.DataValueField = "t_ID";
        ddlEmployment.DataBind();
        ListItem item = new ListItem("Select Employment Type", "0");
        ddlEmployment.Items.Insert(0, item);
    }

    protected void gvproject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edititem")
        {
            Emp_Code = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(string.Format(ConfigurationManager.AppSettings["sitepath"] + "EmployeeJoinadd.aspx?id={0}", Emp_Code.ToString()));
        }

        else if (e.CommandName.ToLower() == "deleteitem")
        {
            Emp_Code = Convert.ToInt32(e.CommandArgument);
            classproject.deleteprojects(Emp_Code);
            loadgrid();
            lblmsg.Text = "<font color='green'><b>Location deleted successfully.</b></font>";
        }
    }
    public void loadgrid()
    {
        string ntitle = "";
        if (txttitle.Text != null && txttitle.Text != "")
        {
            ntitle = commonclass.GetSafeSearchString(txttitle.Text.ToString().Trim());
        }

        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "list";

        spars[1] = new SqlParameter("@Emp_Name", SqlDbType.VarChar);
        if (ntitle.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = ntitle;

        spars[2] = new SqlParameter("@dept_id", SqlDbType.Int);
        if (ddl_DepartmentId.Text.ToString() == "" || ddl_DepartmentId.Text.ToString() == "0")
            spars[2].Value = DBNull.Value;
        else
            spars[2].Value = Convert.ToInt32(ddl_DepartmentId.Text);

        spars[3] = new SqlParameter("@EMPLOYMENT_TYPE", SqlDbType.Int);
        if (ddlEmploymentId.Text.ToString() == "" || ddlEmploymentId.Text.ToString() == "0")
            spars[3].Value = DBNull.Value;
        else
            spars[3].Value = Convert.ToInt32(ddlEmploymentId.Text);

        spars[4] = new SqlParameter("@emp_location", SqlDbType.VarChar);
        if (ddl_LocationId.Text.ToString() == "" || ddl_LocationId.Text.ToString() == "0")
            spars[4].Value = DBNull.Value;
        else
            spars[4].Value = Convert.ToString(ddl_LocationId.Text);
            
        DataTable dt = adm.getDataList(spars, "SP_Admin_Employee_Join");
        //DataTable dt = adm.getDataList("list",ntitle);

        if (dt.Rows.Count > 0)
        {
            gvproject.DataSource = dt;
            gvproject.DataBind();
            if (SortDireaction != null && SortDireaction != "")
            {
                dt.DefaultView.Sort = SortExpression + " " + SortDireaction;
                gvproject.DataSource = dt;
                gvproject.DataBind();
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in gvproject.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == SortExpression)
                    {
                        columnIndex = gvproject.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }

                gvproject.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
            lblmainmsg1.Visible = true;
            if (ntitle != "" && ntitle != null)
            {
                lblmainmsg1.Visible = true;
                lblmessage1.Visible = true;
                lblmessage1.Text = ntitle;
            }
            else if (ntitle != "" && ntitle != null)
            {
                lblmainmsg1.Visible = true;
                lblmessage1.Visible = true;
                lblmessage1.Text = ntitle;
            }
            else
            {
                lblmainmsg1.Visible = false;
                lblmessage1.Visible = false;
            }

            lblmainmsg.Visible = false;
            lblerror.Visible = false;
            lblerror.Text = "";
        }
        else
        {
            if (ntitle != "" && ntitle != null)
            {
                lblmainmsg1.Visible = true;
                lblmessage1.Visible = true;
                lblmessage1.Text = ntitle;
            }
            else if (ntitle != "" && ntitle != null)
            {
                lblmainmsg1.Visible = true;
                lblmessage1.Visible = true;
                lblmessage1.Text = ntitle;
            }
            else
            {
                lblmainmsg1.Visible = false;
                lblmessage1.Visible = false;
            }
            lblmainmsg.Visible = false;
            lblerror.Text = "<br><font color='Red'>No record found!</font><br>";
            lblerror.Visible = true;
        }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["sitepath"] + "EmployeeJoinadd.aspx");
    }

    //protected void btnpadd_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(ConfigurationManager.AppSettings["sitepath"] + "EmployeeJoinadd.aspx");
    //}

    protected void gvproject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproject.PageIndex = e.NewPageIndex;
        loadgrid();
        ////string ntitle = "";
        ////if (txttitle.Text != null && txttitle.Text != "")
        ////{
        ////    ntitle = commonclass.GetSafeSearchString(txttitle.Text.ToString().Trim());
        ////}

        ////SqlParameter[] spars = new SqlParameter[2];

        ////spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        ////spars[0].Value = "list";

        ////spars[1] = new SqlParameter("@Emp_Name", SqlDbType.VarChar);
        ////if (ntitle.ToString() == "")
        ////    spars[1].Value = DBNull.Value;
        ////else
        ////    spars[1].Value = ntitle;

        ////gvproject.DataSource = adm.getDataList(spars, "SP_Admin_Employee_Join");
        ////gvproject.DataBind();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Visible = false;
            loadgrid();
            ////string ntitle = "";
            ////if (txttitle.Text != null && txttitle.Text != "")
            ////{
            ////    ntitle = commonclass.GetSafeSearchString(txttitle.Text.ToString().Trim());
            ////}

            ////SqlParameter[] spars = new SqlParameter[2];

            ////spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            ////spars[0].Value = "list";

            ////spars[1] = new SqlParameter("@Emp_Name", SqlDbType.VarChar);
            ////if (ntitle.ToString() == "")
            ////    spars[1].Value = DBNull.Value;
            ////else
            ////    spars[1].Value = ntitle;

            ////DataTable dt = adm.getDataList(spars, "SP_Admin_Employee_Join");
            //////DataTable dt = adm.getDataList("list",ntitle);

            ////if (dt.Rows.Count > 0)
            ////{
            ////    gvproject.DataSource = dt;
            ////    gvproject.DataBind();
            ////    lblmainmsg1.Visible = true;
            ////    if (ntitle != "" && ntitle != null)
            ////    {
            ////        lblmainmsg1.Visible = true;
            ////        lblmessage1.Visible = true;
            ////        lblmessage1.Text = ntitle;
            ////    }
            ////    else if (ntitle != "" && ntitle != null)
            ////    {
            ////        lblmainmsg1.Visible = true;
            ////        lblmessage1.Visible = true;
            ////        lblmessage1.Text = ntitle;
            ////    }
            ////    else
            ////    {
            ////        lblmainmsg1.Visible = false;
            ////        lblmessage1.Visible = false;
            ////    }

            ////    lblmainmsg.Visible = false;
            ////    lblerror.Visible = false;
            ////    lblerror.Text = "";
            ////}
            ////else
            ////{
            ////    if (ntitle != "" && ntitle != null)
            ////    {
            ////        lblmainmsg1.Visible = true;
            ////        lblmessage1.Visible = true;
            ////        lblmessage1.Text = ntitle;
            ////    }
            ////    else if (ntitle != "" && ntitle != null)
            ////    {
            ////        lblmainmsg1.Visible = true;
            ////        lblmessage1.Visible = true;
            ////        lblmessage1.Text = ntitle;
            ////    }
            ////    else
            ////    {
            ////        lblmainmsg1.Visible = false;
            ////        lblmessage1.Visible = false;
            ////    }
            ////    lblmainmsg.Visible = false;
            ////    lblerror.Text = "<br><font color='Red'>No record found!</font><br>";
            ////    lblerror.Visible = true;
            ////}

            //txttitle.Text = "";

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString(), "btnsearch_Click");
        }
    }
    protected void gvproject_Sorting(object sender, GridViewSortEventArgs e)
    {
        //string SortDireaction = _sortDirection;
        SetSortDirection(SortDireaction);
        string ntitle = "";

        if (txttitle.Text != null && txttitle.Text != "")
        {
            ntitle = commonclass.GetSafeSearchString(txttitle.Text.ToString().Trim());
        }
        DataTable dataTable = new DataTable();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "list";

        spars[1] = new SqlParameter("@Emp_Name", SqlDbType.VarChar);
        if (ntitle.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = ntitle;

        spars[2] = new SqlParameter("@dept_id", SqlDbType.Int);
        if (ddl_DepartmentId.Text.ToString() == "" || ddl_DepartmentId.Text.ToString() == "0")
            spars[2].Value = DBNull.Value;
        else
            spars[2].Value = Convert.ToInt32(ddl_DepartmentId.Text);

        spars[3] = new SqlParameter("@EMPLOYMENT_TYPE", SqlDbType.Int);
        if (ddlEmploymentId.Text.ToString() == "" || ddlEmploymentId.Text.ToString() == "0")
            spars[3].Value = DBNull.Value;
        else
            spars[3].Value = Convert.ToInt32(ddlEmploymentId.Text);

        spars[4] = new SqlParameter("@emp_location", SqlDbType.VarChar);
        if (ddl_LocationId.Text.ToString() == "" || ddl_LocationId.Text.ToString() == "0")
            spars[4].Value = DBNull.Value;
        else
            spars[4].Value = Convert.ToString(ddl_LocationId.Text);

        dataTable = adm.getDataList(spars, "SP_Admin_Employee_Join");
        if (dataTable != null)
        {
            //Sort the data.
            dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
            gvproject.DataSource = dataTable;
            gvproject.DataBind();
            SortDireaction = _sortDirection;
            _SortExpression = e.SortExpression;
            SortExpression = _SortExpression;
            int columnIndex = 0;
            foreach (DataControlFieldHeaderCell headerCell in gvproject.HeaderRow.Cells)
            {
                if (headerCell.ContainingField.SortExpression == e.SortExpression)
                {
                    columnIndex = gvproject.HeaderRow.Cells.GetCellIndex(headerCell);
                }
            }

            gvproject.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
        }
    }

    protected void SetSortDirection(string sortDirection)
    {
        if (sortDirection == "ASC")
        {
            _sortDirection = "DESC";
            sortImage.ImageUrl = "sq_downs.png";

        }
        else
        {
            _sortDirection = "ASC";
            sortImage.ImageUrl = "sq_ups.png";
        }
    }

    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        DataTable dtEmpDetails = new DataTable();
        DataTable dtEmpLeaveBal = new DataTable();
        DataTable dtEmpLeaveDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Emp_Dump";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;

        dtEmpDetails = getDropdownList(spars, "rpt_dataProcedure");

        spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Emp_Dump_LeaveBalance";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;

        dtEmpLeaveBal = getDropdownList(spars, "rpt_dataProcedure");

        spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Emp_Dump_LeaveDetails";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;

        dtEmpLeaveDetails = getDropdownList(spars, "rpt_dataProcedure");

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtEmpDetails, "Employee List");
            wb.Worksheets.Add(dtEmpLeaveBal, "Employee Leave Balances");
            wb.Worksheets.Add(dtEmpLeaveDetails, "Employee Leave Details");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EmployeeDetails.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }


    public static DataTable getDropdownList(SqlParameter[] parameters, string strspname)
    {
        DataTable dtUserLogin = new DataTable();
        SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
        if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
            scon.Open();
        SqlCommand sCommand = new SqlCommand();
        sCommand = new SqlCommand();
        sCommand.Connection = scon;
        sCommand.CommandText = strspname;
        sCommand.CommandType = CommandType.StoredProcedure;
        foreach (SqlParameter p in parameters)
        {
            if (p != null)
            {
                sCommand.Parameters.Add(p);
            }
        }
        SqlDataAdapter sadp = new SqlDataAdapter();
        sadp.SelectCommand = sCommand;
        sadp.Fill(dtUserLogin);
        return dtUserLogin;
    }
}
