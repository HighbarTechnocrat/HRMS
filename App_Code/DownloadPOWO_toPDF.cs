using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;
using System.Linq;

/// <summary>
/// Summary description for DownloadPOWO_toPDF
/// </summary>
public  static class clsDownloadPOWO
{ 
    public  static void POWODownload_Word_PDFNew(string sPOID, string sponumber)
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString);
        DataSet ds = new DataSet();
        SqlCommand sqlComm = new SqlCommand("SP_VSCB_Reports_Details", cn);
        sqlComm.Parameters.AddWithValue("@stype", "get_POWO_ContentWord_FromTally");
        sqlComm.Parameters.AddWithValue("@POWOID", sPOID);
        sqlComm.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sqlComm;
        da.Fill(ds);

        #region MileStone Detail table 
        StringBuilder sMilestoneDetails = new StringBuilder();
        StringBuilder sMilestoneDetails_Rows = new StringBuilder();
        string smsfont_headerCenter = @"style='padding-left:20px;text-align:center;border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;'";

        // sMilestoneDetails.Append("<table cellpadding=0 cellspacing=0  style='mso-fareast-font-family:&quot;Trebuchet MS&quot;;mso-ansi-language:EN-US;font-size:10.0pt;border:solid windowtext 1.0pt;mso-border-top-alt:solid windowtext .5pt;mso-border-bottom-alt:solid windowtext .5pt;mso-border-left-alt:solid windowtext .5pt;;mso-border-right-alt:solid windowtext .5pt;'>");
        sMilestoneDetails.Append("<table cellpadding=0 cellspacing=0  style='mso-fareast-font-family:&quot;Trebuchet MS&quot;;mso-ansi-language:EN-US;font-size:10.0pt;border:solid windowtext 1.0pt;mso-border-top-alt:solid windowtext .5pt;border-bottom:none;mso-border-left-alt:solid windowtext .5pt;border-right:none;'>");
        sMilestoneDetails.Append("<tr>");
        sMilestoneDetails.Append("<td width = 40px  " + smsfont_headerCenter + "><b>Srno</b></td>");
        sMilestoneDetails.Append("<td width = 240px " + smsfont_headerCenter + "><b>Description</b></td>");
        sMilestoneDetails.Append("<td width = 100px " + smsfont_headerCenter + "><b>Due Date</b></td>");
        sMilestoneDetails.Append("<td width = 90px " + smsfont_headerCenter + "><b>Quantity</b></td>");
        sMilestoneDetails.Append("<td width = 100px " + smsfont_headerCenter + "><b>Rate</b></td>");
        sMilestoneDetails.Append("<td width = 70px " + smsfont_headerCenter + "><b>Per</b></td>");
        sMilestoneDetails.Append("<td width = 130px " + smsfont_headerCenter + "><b>Amount (" + Convert.ToString(ds.Tables[5].Rows[0]["CurSign"]).Trim() + ") </b></td>");
        sMilestoneDetails.Append("</tr>");
        for (int irow = 0; irow < ds.Tables[2].Rows.Count; irow++)
        {

            if (Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() == "Total without GST" || Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() == "Total with GST")
            {
                sMilestoneDetails_Rows.Append("<tr>");
                //sMilestoneDetails_Rows.Append("<td width = 60px colspan=2  style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:center'>" + Convert.ToString(ds.Tables[10].Rows[irow]["Srno"]) + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 240px colspan='2' style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt; text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Milestone_due_date"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 90px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Quantity"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Rate"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 70px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["stype"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 130px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;border-left:none;text-align:right;font-weight: bold;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["milestonebaseAmt"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("</tr>");
            }
            else
            {


                sMilestoneDetails_Rows.Append("<tr>");
                sMilestoneDetails_Rows.Append("<td width = 40px  style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:center'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Srno"]) + "</td>");
                if (Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() == "SGST" || Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() == "CGST" || Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() == "IGST")
                {
                    sMilestoneDetails_Rows.Append("<td width = 240px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                }
                else
                {
                    sMilestoneDetails_Rows.Append("<td width = 240px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["MilestoneName"]).Trim() + "</td>");
                }
                sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:left;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Milestone_due_date"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 90px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Quantity"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 100px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["Rate"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 70px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;mso-border-right-alt:solid windowtext .5pt;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["stype"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("<td width = 130px style='border:solid windowtext 1.0pt;border-top:none;mso-border-bottom-alt:solid windowtext .5pt;border-left:none;border-left:none;text-align:right;padding:0in 5.4pt 0in 5.4pt'>" + Convert.ToString(ds.Tables[2].Rows[irow]["milestonebaseAmt"]).Trim() + "</td>");
                sMilestoneDetails_Rows.Append("</tr>");

                sMilestoneDetails_Rows.Append("<tr>");

            }
        }
        sMilestoneDetails.Append(sMilestoneDetails_Rows + "</table>");


        #endregion


        #region Create Footer Approver details table
        Int32 ifooterRowCnt = ds.Tables[4].Rows.Count;

        StringBuilder sFooterHeaders = new StringBuilder();
        StringBuilder sFooterRows_ApprName = new StringBuilder();
        StringBuilder sFooterRows_ApprvedOn = new StringBuilder();
        StringBuilder sFooterRows_ApprDesignation = new StringBuilder();
        StringBuilder sFooterTable = new StringBuilder();

        sFooterHeaders.Append("<thead>");
        sFooterRows_ApprName.Append("<tr>");
        sFooterRows_ApprvedOn.Append("<tr>");
        sFooterRows_ApprDesignation.Append("<tr>");
        for (int irow = 0; irow < ds.Tables[4].Rows.Count; irow++)
        {
            sFooterHeaders.Append("<th style='font-size: 8pt; font-family:Trebuchet MS;text-align:left'>" + Convert.ToString(ds.Tables[4].Rows[irow]["ApprovedLable"]).Trim() + "</th>");
            sFooterRows_ApprName.Append("<td style='font-size:8pt;font-family:Trebuchet MS;width:20%'>" + Convert.ToString(ds.Tables[4].Rows[irow]["Emp_Name"]).Trim() + "</td>");
            sFooterRows_ApprvedOn.Append("<td style='font-size:8pt;font-family:Trebuchet MS;width:20%'>" + Convert.ToString(ds.Tables[4].Rows[irow]["ApprovedOn"]).Trim() + "</td>");
            sFooterRows_ApprDesignation.Append("<td style='font-size:8pt;font-family:Trebuchet MS;width:20%'>" + Convert.ToString(ds.Tables[4].Rows[irow]["Designation"]).Trim() + "</td>");
        }
        sFooterHeaders.Append("</thead><tbody>");
        sFooterRows_ApprName.Append("</tr>");
        sFooterRows_ApprvedOn.Append("</tr>");
        sFooterRows_ApprDesignation.Append("</tr>");

        sFooterTable.Append("<table style ='width: 100%' cellpadding ='1' cellspacing ='2'>");
        sFooterTable.Append(Convert.ToString(sFooterHeaders));
        sFooterTable.Append(Convert.ToString(sFooterRows_ApprName));
        sFooterTable.Append(Convert.ToString(sFooterRows_ApprvedOn));
        sFooterTable.Append(Convert.ToString(sFooterRows_ApprDesignation));
        sFooterTable.Append("<tr><td style ='font-size:8pt;font-family:Trebuchet MS;' colspan='" + ifooterRowCnt + "'> &nbsp;</td ></tr>");

        //Highbar Rasing the bar  
        //sFooterTable.Append("<tr><td style ='font-size:8pt;font-family:Trebuchet MS;'><img width=170 height=30 id =_x0000_i1025 src ='http://localhost/hrms/VendorBilling/ApprovedPO/hbtbar.jpg'></td>");
        sFooterTable.Append("<tr><td style ='font-size:8pt;font-family:Trebuchet MS;'><img width=170 height=30 id =_x0000_i1025 src ='http://localhost/hrms/VendorBilling/ApprovedPO/hbtbar.jpg'></td>");
        if (ifooterRowCnt == 2)
            sFooterTable.Append("<td style ='font-size: 8pt; font-family: Trebuchet MS;'> &nbsp;</td></tr>");
        else
            sFooterTable.Append("<td style ='font-size: 8pt; font-family: Trebuchet MS;' colspan='" + (ifooterRowCnt - 1) + "'> &nbsp;</td></tr>");


        //Highbar Address
        sFooterTable.Append("<tr><td style ='font-size:7pt;font-family:Trebuchet MS;' colspan='" + ifooterRowCnt + "'><br/><div style ='height:30px;color:#411954'><b>HIGHBAR TECHNOCRAT LIMITED<br/>D - Wing, 14th Floor, Empire Tower,<br/>Off.Thane - Belapur Road,Airoli, Navi Mumbai-40070 <br/>T: +91 22 6279 2000 | www.highbartech.com <br/>CIN Number: U72100MH2010PLC210078</b></div></td></tr>");

        // This is a Computer Generated Document hence dose not require physical signature
        sFooterTable.Append("<tr><td style ='font-size:7pt;font-family:Trebuchet MS;text-align:center' colspan='" + ifooterRowCnt + "'><br/><br/>This is a Computer Generated Document hence does not require physical signature</td></tr>");

        //Blank Row for to hide default shape
        sFooterTable.Append("<tr><td style ='font-size:8pt;font-family:Trebuchet MS;' colspan ='" + ifooterRowCnt + "'> &nbsp;</td></tr>");
        sFooterTable.Append("<tr><td style ='font-size:8pt;font-family:Trebuchet MS;' colspan ='" + ifooterRowCnt + "'> &nbsp;</td></tr>");

        sFooterTable.Append("</tbody></table>");
        #endregion

        string htmlFooterString = "<HTML><body>" + Convert.ToString(sFooterTable) + "</body></HTML>";

        string backfolder1 = System.Web.Hosting.HostingEnvironment.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());

        string documentPath = backfolder1 + "POWODownload.docx";

        var OutPutFile = "";
        string spo_number = Regex.Replace(Convert.ToString(sponumber), @"[^0-9a-zA-Z\._]", "_");
        OutPutFile = backfolder1 + spo_number + ".docx";
        File.Copy(documentPath, OutPutFile, true);


        #region ConvertHTML To Docs milestone and serviceorder

        string StrHTMLBodyAddressdetail = Convert.ToString(ds.Tables[3].Rows[0]["BodyAddressdetail"]).Trim();
        string StrHTMLAmountInWord = Convert.ToString(ds.Tables[3].Rows[0]["HTMLAmountInWord"]).Trim();
        string StrHTMLOtherTermsCondition = Convert.ToString(ds.Tables[3].Rows[0]["OtherTermsCondition"]).Trim();
        string StrHTMLpowocontent = Convert.ToString(ds.Tables[3].Rows[0]["powocontent"]).Trim();

        if (StrHTMLpowocontent == "")
        {
            StrHTMLOtherTermsCondition = "";
        }

        SautinSoft.HtmlToRtf h = new SautinSoft.HtmlToRtf();
        string htmlString = "<HTML><body></br>" + StrHTMLBodyAddressdetail + Convert.ToString(sMilestoneDetails) + StrHTMLAmountInWord + StrHTMLOtherTermsCondition + StrHTMLpowocontent + "</body></HTML>";

        string headerFromHtml = File.ReadAllText(backfolder1 + "header.html");
        h.PageStyle.PageHeader.Html(headerFromHtml);
        //string FooterFromHtml = File.ReadAllText(backfolder1 + "Footer.html");
        //h.PageStyle.PageFooter.Html(FooterFromHtml);

        h.PageStyle.PageFooter.Html(htmlFooterString);

        h.OpenHtml(htmlString);
        byte[] docxBytes = h.ToDocx();
        if (docxBytes != null)
        {
            File.WriteAllBytes(OutPutFile, docxBytes);
        }
        #endregion

        #region WordtoPDF File Convert

        string strFileName = spo_number + ".docx";
        string FromLocation = backfolder1 + strFileName;
        string outFile = backfolder1 + spo_number + ".pdf";
        byte[] inpData = File.ReadAllBytes(FromLocation);
        byte[] outData = null;
        using (MemoryStream msInp = new MemoryStream(inpData))
        {
            //Load a document.
            DocumentCore dc = DocumentCore.Load(msInp, new DocxLoadOptions());

            #region ContaindeleteFooter

            foreach (Paragraph par in dc.Sections[0].GetChildElements(true, ElementType.Paragraph))
            {
                var findText = par.Content.Find(".Net 5.7.11.28");
                var findText1 = par.Content.Find("HTML to RTF .Net 8.4.11.9 trial.");
                var findText2 = par.Content.Find("Have questions? Trial us:");
                var findText3 = par.Content.Find("Discussions, free help and custom examples for you.");
                var findText4 = par.Content.Find("support@sautinsoft.com");
                var findText5 = par.Content.Find("Have questions? Email us:");

                if (findText != null)
                {
                    foreach (ContentRange cr in findText)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;

                    }
                }
                if (findText1 != null)
                {
                    foreach (ContentRange cr in findText1)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                    }

                }
                if (findText2 != null)
                {
                    foreach (ContentRange cr in findText2)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                    }
                }
                if (findText3 != null)
                {
                    foreach (ContentRange cr in findText3)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                    }
                }
                if (findText4 != null)
                {
                    foreach (ContentRange cr in findText4)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                    }
                }
                if (findText5 != null)
                {
                    foreach (ContentRange cr in findText5)
                    {
                        cr.Delete();
                        par.ParagraphFormat.Alignment = HorizontalAlignment.Right;
                    }
                }
            }

            #endregion

            // Save the document to PDF format.
            using (MemoryStream outMs = new MemoryStream())
            {
                dc.Save(outMs, new PdfSaveOptions());
                outData = outMs.ToArray();
            }
            // Show the result for demonstration purposes.
            if (outData != null)
            {
                File.WriteAllBytes(outFile, outData);
            }
        }
        #endregion

        string sdeletedocxfile = backfolder1 + spo_number + ".docx";
        if (File.Exists(sdeletedocxfile))
        {
            File.Delete(sdeletedocxfile);
        }


    }
}