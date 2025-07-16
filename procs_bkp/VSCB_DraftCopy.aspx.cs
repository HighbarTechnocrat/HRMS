using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;

public partial class procs_VSCB_DraftCopy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DraftCopyLoad();
        }
    }

    public void DraftCopyLoad()
    {

        string strID = Request.QueryString[0];
   
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString);
        DataSet ds = new DataSet();
        SqlCommand sqlComm = new SqlCommand("SP_VSCB_Reports_Details", cn);

        if (strID == "0")
        {
            sqlComm.Parameters.AddWithValue("@stype", "get_ViewDraftCopy_FromTallyWithoutSubmitData");
        }
        else
        {
            sqlComm.Parameters.AddWithValue("@stype", "get_POWO_ContentWordDraftCopy_FromTally");
        }
        sqlComm.Parameters.AddWithValue("@empcode", Convert.ToString(Session["Empcode"]));

        sqlComm.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblSerialno.Text = ds.Tables[0].Rows[0]["PONumber"].ToString().Trim();
            lblDated.Text = ds.Tables[0].Rows[0]["PODate"].ToString().Trim();
            lblModeofpayment.Text = ds.Tables[0].Rows[0]["powoterms"].ToString().Trim();
            lblReferenceNo.Text = ds.Tables[0].Rows[0]["PONumber"].ToString().Trim();
            lblVendorname.Text = ds.Tables[0].Rows[0]["vendorName"].ToString().Trim();
            lblvendorAddress.Text = ds.Tables[0].Rows[0]["vendor_address"].ToString().Trim();
            lblHPOType.Text= ds.Tables[0].Rows[0]["HPOType"].ToString().Trim();
            lblshippingAddress.Text= ds.Tables[0].Rows[0]["ShippingAddress"].ToString().Trim();
            #region MileStone Detail table

            StringBuilder sMilestoneDetails = new StringBuilder();
            StringBuilder sMilestoneDetails_Rows = new StringBuilder();

            string smsfont_headerCenter = @"style='padding-left:20px;text-align:center;border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;'";

            sMilestoneDetails.Append("<table cellpadding=0 cellspacing=0  style='mso-fareast-font-family:&quot;Trebuchet MS&quot;;mso-ansi-language:EN-US;font-size:10.0pt;border:solid windowtext 1.0pt;mso-border-top-alt:solid windowtext .5pt;mso-border-bottom-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;;mso-border-right-alt:solid windowtext .5pt;'>");
            sMilestoneDetails.Append("<tr>");
            sMilestoneDetails.Append("<td width = 40px  " + smsfont_headerCenter + "><b>Srno</b></td>");
            sMilestoneDetails.Append("<td width = 240px " + smsfont_headerCenter + "><b>Description</b></td>");
            sMilestoneDetails.Append("<td width = 100px " + smsfont_headerCenter + "><b>Due Date</b></td>");
            sMilestoneDetails.Append("<td width = 80px " + smsfont_headerCenter + "><b>Quantity</b></td>");
            sMilestoneDetails.Append("<td width = 100px " + smsfont_headerCenter + "><b>Rate</b></td>");
            sMilestoneDetails.Append("<td width = 70px " + smsfont_headerCenter + "><b>Per</b></td>");
            sMilestoneDetails.Append("<td width = 130px " + smsfont_headerCenter + "><b>Amount (" + Convert.ToString(ds.Tables[4].Rows[0]["CurSign"]).Trim() + ")</b></td>");
            sMilestoneDetails.Append("</tr>");
            for (int irow = 0; irow < ds.Tables[3].Rows.Count; irow++)
            {
                if (Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "Total without GST" || Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "Total with GST")
                {
                    sMilestoneDetails_Rows.Append("<tr>");
                    //sMilestoneDetails_Rows.Append("<td width = 60px colspan=2  style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:center'>" + Convert.ToString(ds.Tables[10].Rows[irow]["Srno"]) + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 240px colspan='2' style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt; text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Milestone_due_date"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 80px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Quantity"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Rate"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 70px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["stype"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 130px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;font-weight: bold;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["milestonebaseAmt"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("</tr>");
                }
                else
                {
                    sMilestoneDetails_Rows.Append("<tr>");
                    sMilestoneDetails_Rows.Append("<td width = 40px  style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:center'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Srno"]) + "</td>");
                    if (Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "SGST" || Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "CGST" || Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "IGST")
                    {
                        sMilestoneDetails_Rows.Append("<td width = 240px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                    }
                    else
                    {
                        sMilestoneDetails_Rows.Append("<td width = 240px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                    }
                    sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Milestone_due_date"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 80px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Quantity"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["Rate"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 70px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["stype"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("<td width = 130px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[3].Rows[irow]["milestonebaseAmt"]).Trim() + "</td>");
                    sMilestoneDetails_Rows.Append("</tr>");
                    sMilestoneDetails_Rows.Append("<tr>");
                }
            }

            sMilestoneDetails.Append(sMilestoneDetails_Rows + "</table>");
            SpanIDmilestonedetail.InnerHtml = Convert.ToString(sMilestoneDetails);

            #endregion

            #region ConvertHTML To Docs 

            string StrHTMLAmountInWord = Convert.ToString(ds.Tables[2].Rows[0]["HTMLAmountInWord"]).Trim();
            string StrHTMLOtherTermsCondition = Convert.ToString(ds.Tables[2].Rows[0]["OtherTermsCondition"]).Trim();
            string StrHTMLpowocontent = Convert.ToString(ds.Tables[2].Rows[0]["powocontent"]).Trim();
            if (StrHTMLpowocontent == "")
            {
                StrHTMLOtherTermsCondition = "";
            }
            string htmlString = "</br>" + StrHTMLAmountInWord + StrHTMLOtherTermsCondition + StrHTMLpowocontent + "</br></br</br>";
            SpanIDHTMLContainDetail.InnerHtml = htmlString;

            #endregion
        }
    }
}