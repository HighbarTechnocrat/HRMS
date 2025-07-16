using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class myaccount_EmpAccessReport : System.Web.UI.Page
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    SP_Methods spm = new SP_Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (!IsPostBack)
        {
            Loadcompany();
        }



    }


    private void Loadcompany()
    {
        DataTable ds = new DataTable();

        string Query = "select distinct(org_Comp_code),company_name from tbl_hmst_company_Location order by company_name";  // production
        ds = spm.getDataList_SQL(Query);

        if (ds != null)
        {
            if (ds.Rows.Count > 0)
            {
                ddlCompany.DataTextField = "company_name";
                ddlCompany.DataValueField = "org_Comp_code";
                ddlCompany.DataSource = ds;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }

    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Query;

        if (txtmonth.Text.ToString() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select Date";
            return;
        }

        if (ddlCompany.SelectedValue.Trim() == "0")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Select Company";
            return;
        }

        else
        {
            lblmsg.Visible = false;

            string[] date = txtmonth.Text.ToString().Split('/');


            DataSet ds = new DataSet();

            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RPT";

            spars[1] = new SqlParameter("@month", SqlDbType.Int);
            spars[1].Value = ReturnMonth(date[0].ToString());

            spars[2] = new SqlParameter("@Year", SqlDbType.VarChar);
            spars[2].Value = date[1].ToString();


            spars[3] = new SqlParameter("@compcode", SqlDbType.VarChar);
            spars[3].Value = ddlCompany.SelectedValue.ToString();


            //            if (ddlCompany.SelectedValue.ToString() == "0")
            //            {
            //                Query = @"    select em.emp_location,Location_name ,count(distinct el.emp_code) count
            //                    from EMP_LOGVisit el
            //                    inner join tbl_Employee_Mst em on el.EMP_CODE=em.Emp_Code
            //					inner join tbl_hmst_company_Location hm on hm.Comp_code = em.emp_location
            //                 
            //                  --  where logoutTime between  CONVERT(date, '01-09-2019',103) and CONVERT(date,'30-09-2019',103)
            //				    where  DATEPART(MONTH,logoutTime) = " + ReturnMonth(date[0].ToString()) + " and  DATEPART(YEAR,logoutTime) = " + date[1].ToString() + "  and emp_location in (select comp_code from  tbl_hmst_company where org_Comp_code =  isnull(null,org_Comp_code)) group by em.emp_location,Location_name";
            //            }
            //            else
            //            {

            //                Query = @"    select em.emp_location,Location_name ,count(distinct el.emp_code) count
            //                    from EMP_LOGVisit el
            //                    inner join tbl_Employee_Mst em on el.EMP_CODE=em.Emp_Code
            //					inner join tbl_hmst_company_Location hm on hm.Comp_code = em.emp_location
            //                 
            //                  --  where logoutTime between  CONVERT(date, '01-09-2019',103) and CONVERT(date,'30-09-2019',103)
            //				    where  DATEPART(MONTH,logoutTime) = " + ReturnMonth(date[0].ToString()) + " and  DATEPART(YEAR,logoutTime) = " + date[1].ToString() + " and emp_location in (select comp_code from  tbl_hmst_company where org_Comp_code =  isnull('" + ddlCompany.SelectedValue.ToString() + "',org_Comp_code)) group by em.emp_location,Location_name";

            //            }





            ds = spm.getDatasetList(spars, "[dbo].[GetHRLeaveReport]");

            dgReport.DataSource = ds;
            dgReport.DataBind();
        }

    }


    private int ReturnMonth(string month)
    {
        int mon;
        switch (month.ToUpper())
        {


            case "JAN": mon = 1;
                break;
            case "FEB": mon = 2;
                break;
            case "MAR": mon = 3;
                break;
            case "APR": mon = 4;
                break;
            case "MAY": mon = 5;
                break;
            case "JUN": mon = 6;
                break;
            case "JUL": mon = 7;
                break;
            case "AUG": mon = 8;
                break;
            case "SEP": mon = 9;
                break;
            case "OCT": mon = 10;
                break;
            case "NOV": mon = 11;
                break;
            case "DEC": mon = 12;
                break;

            default: mon = 0;
                break;
        }

        return mon;
    }
}