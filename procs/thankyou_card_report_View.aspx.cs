
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Reporting.WebForms;




public partial class procs_thankyou_card_report_View : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    bool isMGR = false;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        lblmessage.Text = "";
        hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
        SelectedImage.Visible = false;
        spnThankYouNote.Visible = false;
        txt_remakrs.Visible = false;

        if (!Page.IsPostBack)
        {
            GetRecordGVBind();
        }

    }

 

    public void GetRecordGVBind()
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "thankyoucard_SendReceived_Count";

        spars[1] = new SqlParameter("@send_to", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnloginempcode.Value).Trim();

        DataSet dsThankyoucardCnt = spm.getDatasetList(spars, "SP_thank_you_card");
        gv_MyProcessedTaskExecuterList.DataSource = null;
        gv_MyProcessedTaskExecuterList.DataBind();

        if (dsThankyoucardCnt != null)
        {
            if (dsThankyoucardCnt.Tables[0].Rows.Count > 0)
            {
                gv_MyProcessedTaskExecuterList.DataSource = dsThankyoucardCnt.Tables[0];
                gv_MyProcessedTaskExecuterList.DataBind();
            }
        }



    }

    protected void lnkSendThankYouCardCnt_Click(object sender, EventArgs e)
    {

        getSendThankyouCardList();
    }

    public void getSendThankyouCardList()
    {
        DataSet dsList = getThankYouCard_List(hdnloginempcode.Value, "thankyoucard_Send_list");
        lblheading_Thankyou.InnerText = "No Records Found";
        gv_ThankyouCard_Send.DataSource = null;
        gv_ThankyouCard_Send.DataBind();

        gv_ThankyouCard_Received.Visible = false;
        gv_ThankyouCard_Send.Visible = true;
        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                lblheading_Thankyou.InnerText = "List of Thankyou Card Send";
                gv_ThankyouCard_Send.DataSource = dsList.Tables[0];
                gv_ThankyouCard_Send.DataBind();
            }
        }
    }

    public void getReceivedThankyouCardList()
    {
        DataSet dsList = getThankYouCard_List(hdnloginempcode.Value, "thankyoucard_Received_list");
        lblheading_Thankyou.InnerText = "No Records Found";
        gv_ThankyouCard_Received.DataSource = null;
        gv_ThankyouCard_Received.DataBind();

        gv_ThankyouCard_Received.Visible = true;
        gv_ThankyouCard_Send.Visible = false;

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                lblheading_Thankyou.InnerText = "List of Thankyou Card Received";
                gv_ThankyouCard_Received.DataSource = dsList.Tables[0];
                gv_ThankyouCard_Received.DataBind();
            }
        }
    }
    protected void lnkReceivedThankYouCardCnt_Click(object sender, EventArgs e)
    {
        getReceivedThankyouCardList();
    }


    private DataSet getThankYouCard_List(string sempcode, string sqtype)
    {
        DataSet dsThankyoucardlist = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = sqtype;

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(sempcode).Trim();

            dsThankyoucardlist = spm.getDatasetList(spars, "SP_thank_you_card");

        }
        catch (Exception e) { }
        return dsThankyoucardlist;


    }



    public void getThankyouNote(string sthankyouid)
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "getThankyou_Note";

        spars[1] = new SqlParameter("@thankyouid", SqlDbType.Decimal);
        if (Convert.ToString(sthankyouid).Trim() != "")
        {
            spars[1].Value = Convert.ToDecimal(sthankyouid);
        }
        else
            spars[1].Value = DBNull.Value;

        DataSet dsThankYourNote = spm.getDatasetList(spars, "SP_thank_you_card");

        txt_remakrs.Visible = false;
        spnThankYouNote.Visible = false;

        if (dsThankYourNote != null)
        {
            if (dsThankYourNote.Tables[0].Rows.Count > 0)
            {
                if(Convert.ToString(dsThankYourNote.Tables[0].Rows[0]["Description"]) !="")
                {
                    txt_remakrs.Visible = true;
                    spnThankYouNote.Visible = true;
                    txt_remakrs.Text = Convert.ToString(dsThankYourNote.Tables[0].Rows[0]["Description"]);
                }
            }
        }



    }


    protected void gv_ThankyouCard_Send_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Add JavaScript for row click
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_ThankyouCard_Send, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gv_ThankyouCard_Send_SelectedIndexChanged(object sender, EventArgs e)
    {      
        
        foreach (GridViewRow row in gv_ThankyouCard_Send.Rows)
        {
            row.BackColor = System.Drawing.Color.White;
            row.ForeColor = System.Drawing.Color.Black;
        }
        GridViewRow selectedRow = gv_ThankyouCard_Send.SelectedRow;
        //selectedRow.BackColor = System.Drawing.Color.Yellow;
        selectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#712B75");
        selectedRow.ForeColor = System.Drawing.Color.White;


        if (gv_ThankyouCard_Send.SelectedDataKey != null)
        {
            string thankyouCard = gv_ThankyouCard_Send.SelectedDataKey["photo"].ToString();
            string thankyouId = gv_ThankyouCard_Send.SelectedDataKey["thankyouid"].ToString();
            getThankyouNote(thankyouId);
            string virtualPath = ResolveUrl("~/thankyoucard/"+ thankyouCard);
            SelectedImage.ImageUrl = virtualPath;
            SelectedImage.Visible = true;

        }

       
    }


    protected void gv_ThankyouCard_Received_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_ThankyouCard_Received.Rows)
        {
            row.BackColor = System.Drawing.Color.White;
            row.ForeColor = System.Drawing.Color.Black;
        }
        GridViewRow selectedRow = gv_ThankyouCard_Received.SelectedRow;
        // selectedRow.BackColor = System.Drawing.Color.Yellow;
        selectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#712B75");
        selectedRow.ForeColor = System.Drawing.Color.White;

        if (gv_ThankyouCard_Received.SelectedDataKey != null)
        {
            string thankyouCard = gv_ThankyouCard_Received.SelectedDataKey["photo"].ToString();
            string thankyouId = gv_ThankyouCard_Received.SelectedDataKey["thankyouid"].ToString();
            getThankyouNote(thankyouId);
            string virtualPath = ResolveUrl("~/thankyoucard/" + thankyouCard);
            SelectedImage.ImageUrl = virtualPath;
            SelectedImage.Visible = true;

        }

    }

    protected void gv_ThankyouCard_Received_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Add JavaScript for row click
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_ThankyouCard_Received, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gv_ThankyouCard_Send_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ThankyouCard_Send.PageIndex = e.NewPageIndex;
        this.getSendThankyouCardList();
    }

    protected void gv_ThankyouCard_Received_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ThankyouCard_Received.PageIndex = e.NewPageIndex;
        this.getReceivedThankyouCardList();
    }
}