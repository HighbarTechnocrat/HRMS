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

public partial class TeamReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
        }
        
        if (!Page.IsPostBack)
        {
            btnExport.Visible = false;
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //  getUserList_1();
        }
         
    }
    private DataTable GetData()
    {
        string[] strdate;
        string strfromDate = "";
        #region date formatting
        if (Convert.ToString(txtRequest_Date.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtRequest_Date.Text).Trim().Split('/');

            //strfromDate = Convert.ToString(strdate[2]) + "-" +
            strfromDate = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]) + "-01";
        }

        #endregion
        DataTable dt = new DataTable();
        if (RdDirect.Checked)
            dt = spm.GetTeamCalenderLeaveList_table_Direct(hdnloginempcode.Value, strfromDate);
        else
            dt = spm.GetTeamCalenderLeaveList_table(hdnloginempcode.Value, strfromDate);
        ////dt = spm.GetTeamCalenderLeaveList_table(hdnloginempcode.Value, strfromDate);
        //dt = spm.GetTeamCalenderLeaveList123("00013962", strfromDate); //00008727 //00013962 //00009990 //00630340
        return dt;    
    }

    protected void Fullday_CheckedChanged(object sender, EventArgs e)
    {

        //if (RdDirect.Checked)
        //    txtFromfor.Text = "D";
        //if (RdAll.Checked)
        //    txtFromfor.Text = "A";

        //txtempname.Text = "";
        //txtempnameNonHr.Text = "";
        //if (Convert.ToString(txtFromfor.Text).Trim() == "E")
        //{
        //    txtempname.Enabled = true;
        //    txtempnameNonHr.Enabled = true;
        //}
        //else
        //{
        //    txtempname.Enabled = false;
        //    txtempnameNonHr.Enabled = false;
        //}
    }

    private void getHtmlTable()
    {
        //Populating a DataTable from database.
        DataTable dt = this.GetData();
        //Building an HTML string.
        StringBuilder html = new StringBuilder();
        //Table start.
        html.Append("<table border = '1' cellspacing='0' id='gvMain' style=' border-collapse: collapse; border-color: black;'>");
        //Building the Header row.
        html.Append("<tr class='GridViewScrollHeader'>");
        foreach (DataColumn column in dt.Columns)
        {
            html.Append("<th>");
            if (column.ColumnName != "Employee")
            {
                string strDate = column.ColumnName.Substring(8, 2); // +"-" + column.ColumnName.Substring(5, 2) + "-" + column.ColumnName.Substring(0, 4);
                //html.Append(column.ColumnName);
                html.Append(strDate);
            }
            else
            {
                html.Append(column.ColumnName);
            }
            html.Append("</th>");
        }
        html.Append("</tr>");
        // {background-color: rgb(201, 76, 76);}
        //Building the Data rows.
        foreach (DataRow row in dt.Rows)
        {
            html.Append("<tr class='GridViewScrollItem'>");
            
            foreach (DataColumn column in dt.Columns)
            {
                var name = row[column.ColumnName].ToString();
                if (name == "Holiday" || name == "H")
                {
                    row[column.ColumnName] = "H";
                    //html.Append("<td style='background-color: rgb(255, 153, 51);'>");
                    html.Append("<td style='color: #FF9933;background-color: #FF9933;width:8px;'>"); 
                }
                else if (name == "Sat" || name == "Sun" || name == "W")
                {
                    row[column.ColumnName] = "W";
                    //cell.BackColor = System.Drawing.Color.FromArgb(252, 92, 84);
                    html.Append("<td style='color: rgb(253, 242, 185);background-color: rgb(253, 242, 185);width:8px;'>");
                }
                else if (name == "Pending Leave" || name == "PL")
                {
                    row[column.ColumnName] = "PL";
                     //html.Append("<td style='background-color: rgb(58, 125, 206);'>");
                    html.Append("<td style='color: yellow;background-color: yellow;width:8px;'>");
                }
                else if (name == "Approved Leave" || name == "AL")
                {
                    row[column.ColumnName] = "AL";
                     //html.Append("<td style='background-color: rgb(209, 127, 125);'>");                   
                    html.Append("<td style='color: green;background-color: green;width:8px;'>");
                }
                else if (name == "Correction Leave")
                {
                    html.Append("<td style='background-color: rgb(96, 73, 123);width:8px;'>");                
                }
                else  if (name == "Pending Travel")
                {
                    html.Append("<td style='background-color: rgb(112, 48, 160);width:8px;'>");                  
                }
                else if (name == "Approved Travel")
                {
                    html.Append("<td style='background-color: rgb(0, 176, 80);width:8px;'>");                   
                }
                else
                {
                    html.Append("<td style='width:8px;'>");
                }
               // html.Append("<td>");
                html.Append(row[column.ColumnName]);
                html.Append("</td>");
            }
            html.Append("</tr>");
        }

        //Table end.
        html.Append("</table>");
        //Append the HTML string to Placeholder.
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
    
    }
    private void getUserList_1()
    {
        try
        {
            if (Convert.ToString(hdnloginempcode.Value).Trim() != "")
            {
                string[] strdate;
                string strfromDate = "";
                #region date formatting
                 if (Convert.ToString(txtRequest_Date.Text).Trim() != "")
                 {
                     strdate = Convert.ToString(txtRequest_Date.Text).Trim().Split('/');
                     //strfromDate = Convert.ToString(strdate[2]) + "-" +
                     strfromDate = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]) + "-01";
                 }
                #endregion
                DataSet dsemployees = new DataSet();
                if (RdDirect.Checked)
                    dsemployees = spm.GetTeamCalenderLeaveList_Direct(hdnloginempcode.Value, strfromDate);
                else
                    dsemployees = spm.GetTeamCalenderLeaveList(hdnloginempcode.Value, strfromDate);
                //dsemployees = spm.GetTeamCalenderLeaveList("00008727", strfromDate);

                if (dsemployees != null)
                {
                    btnExport.Visible = true;
                    if (dsemployees.Tables[0].Rows.Count != 0)
                    {                       
                        GridView1.DataSource = dsemployees;
                        GridView1.DataBind();                       
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        btnExport.Visible = false;
                    }
                }
                else
                {
                    btnExport.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GV_RowDataBound(object o, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.RowIndex == 0)
            //    e.Row.Style.Add("height", "100px");

            TableCell cell = e.Row.Cells[0];
            //e.Row.Cells[0].Attributes["width"] = "500px";
            //cell.Width = new Unit("320px");
            //cell.Style["border-right"] = "2px solid #666666";
            cell.BackColor = System.Drawing.Color.LightBlue;
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
               // e.Row.Cells[0].Attributes["width"] = "200px";
                cell = e.Row.Cells[i];
                e.Row.Cells[0].CssClass = "locked";
                string name = e.Row.Cells[i].Text;
                if (name == "Holiday")
                {
                    cell.Text = "H";
                    //cell.BackColor = System.Drawing.Color.FromArgb(255, 153, 51);
                    cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9933");
                }
                if (name == "Sat" || name == "Sun")
                {
                    //cell.BackColor = System.Drawing.Color.FromArgb(252, 92, 84);
                    cell.BackColor = System.Drawing.Color.FromArgb(253, 242, 185);
                }
                if (name == "Pending Leave")
                {
                    cell.Text = "PL";
                    //cell.BackColor = System.Drawing.Color.FromArgb(58, 125, 206);
                    cell.BackColor = System.Drawing.Color.Yellow;
                }
                if (name == "Approved Leave")
                {
                    cell.Text = "AL";
                    //cell.BackColor = System.Drawing.Color.FromArgb(209, 127, 125);
                    cell.BackColor = System.Drawing.Color.Green;
                }
                if (name == "Correction Leave")
                {
                    cell.BackColor = System.Drawing.Color.FromArgb(96, 73, 123);
                    //cell.Text = "";
                }
                if (name == "Pending Travel")
                {
                    cell.BackColor = System.Drawing.Color.FromArgb(112, 48, 160);
                    //cell.Text = "";
                }
                if (name == "Approved Travel")
                {
                    cell.BackColor = System.Drawing.Color.FromArgb(0, 176, 80);
                    //cell.Text = "";
                }
               
            }

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                //cell.Style["border-bottom"] = "2px solid #666666";
                //e.Row.Cells[0].Attributes["width"] = "200px";
                cell.BackColor = System.Drawing.Color.LightGray;
                cell.CssClass = "locked";
                
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        getUserList_1();
        getHtmlTable();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "TeamCalender_" + Convert.ToString(txtRequest_Date.Text).Trim() + "_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End(); 


        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}