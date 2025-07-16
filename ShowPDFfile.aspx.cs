using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel.Channels;
using System.Security.Cryptography;
using System.Text;

public partial class ShowPDFfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cat"] != null)
            {
                string str = UrlRewritingVM.Decrypt(Request.QueryString["cat"].ToString());
                string year = string.Empty;
                string EmpCode = string.Empty;
                string Month = string.Empty;
                string[] StrGrp = str.Split('&');
                if (StrGrp.Length > 1)
                {

                    year = StrGrp[1].Substring(StrGrp[1].IndexOf('=') + 1);
                    EmpCode = StrGrp[2].Substring(StrGrp[2].IndexOf('=') + 1);

                    if (StrGrp.Length == 4)
                    {
                        Month = StrGrp[3].Substring(StrGrp[3].IndexOf('=') + 1);
                    }

                    LoadPDF(StrGrp[0].ToString(), year, EmpCode, Month);
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Something went wrong...!";
                }
            }

             //if (Request.QueryString["yr"] != null && Request.QueryString["id"] != null && Request.QueryString["cat"] != null)
            //{
            //    LoadPDF();
            //}
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Something went wrong...!";
            }
        }
    }

    private void LoadPDF(string category, string year, string empid, string month)
    {
        string filename = string.Empty;
        string base64String = string.Empty;
        string filetype = string.Empty;
        string monthn = string.Empty;
        //string category = UrlRewritingVM.Decrypt(Request.QueryString["cat"].ToString());
        //string yearn = UrlRewritingVM.Decrypt( Request.QueryString["yr"].ToString());
        //Int64 empid =Convert.ToInt64(UrlRewritingVM.Decrypt( Request.QueryString["id"].ToString()));



        //"99912438";
        string yrname = string.Empty;
        string status = string.Empty;
        string foldername = string.Empty;



        switch (category)
        {
            case "P":
                filetype = "PaySlip";
                break;
            case "PF":
                filetype = "PF";
                break;
            case "F":
                filetype = "Form16";
                break;
            case "S":
                filetype = "SuperannuationFund";
                break;

            case "CT":
                filetype = "MyCTC";
                break;

            default:
                break;
        }



        PDFService.Service1Client service = new PDFService.Service1Client();
        // string base64String = service.GetPDF("~/PDF/Mr. User E1_99912438_1.pdf"); ~/MyFiles/PF/2018/99912438.pdf

        if (category != "P")
        {
            //if (category == "PF")
            //{
            //    if (Request.QueryString["yrname"] != null)
            //    {
            //        yrname = UrlRewritingVM.Decrypt(Request.QueryString["yrname"].ToString().Trim());
            //        status = service.CheckFileExists("~/MyFiles/" + filetype + "/" + year.Trim() + "/" + empid + "_" + yrname.Trim() + ".pdf");
            //    }
            //}
            //else
            //{
            //status = service.CheckFileExists("~/MyFiles/" + filetype + "/" + year.Trim() + "/" + empid + ".pdf");
            if (category == "PF")
            {
                string yr = year.Replace('-', '_');
                status = service.CheckFileExists("/MyFiles/" + filetype + "/" + year.Trim() + "/" + Convert.ToInt64(empid) + "_" + yr + ".pdf");
            }
            else if (category == "S")
            {
                status = service.CheckFileExists("/MyFiles/" + filetype + "/" + year.Trim() + "/" + empid + ".pdf");
            }

            else if(category == "CT")
            {
                status = service.CheckFileExists("/MyFiles/" + filetype + "/" + year.Trim() + "/" + empid + ".pdf");
            }

            else
            {

                status = service.CheckFileExists("/MyFiles/" + filetype + "/" + year.Trim() + "/" + Convert.ToInt64(empid) + ".pdf");
            }
            //status = service.CheckFileExistsOutside(@"\MyFiles\" + filetype + @"\" + year.Trim() + @"\" + empid + "_" + yr + ".pdf");
            // lblmsg.Text = @"\MyFiles\" + filetype + @"\" + year.Trim() + @"\" + empid + "_" + yr + ".pdf";

            //}
        }

        else
        {
            //month = UrlRewritingVM.Decrypt(Request.QueryString["mon"].ToString().Trim());
            //status = service.CheckFileExists("~/MyFiles/" + filetype + "/" + year + "/" + month + "/" + empid + ".pdf");
            status = service.CheckFileExists("/MyFiles/" + filetype + "/" + year + "/" + month + "/" + Convert.ToInt64(empid) + ".pdf");
        }


        if (status == "0")
        {
            lblmsg.Text = "No File Found";
            lblmsg.Visible = true;
        }



        else
        {


            if (filetype != "PaySlip")
            {

                //if (category == "PF")
                //{
                //    if (Request.QueryString["yrname"] != null)
                //    {
                //        yrname = UrlRewritingVM.Decrypt(Request.QueryString["yrname"].ToString().Trim());

                //        base64String = service.GetPDF("~/MyFiles/" + filetype + "/" + year + "/" + empid + "_" + yrname + ".pdf");
                //        filename = empid + "_" + filetype + year;
                //    }
                //}
                //else
                //{
                //base64String = service.GetPDF("~/MyFiles/" + filetype + "/" + year + "/" + empid + ".pdf");

                if (filetype == "PF")
                {
                    string yr = year.Replace('-', '_');
                    base64String = service.GetPDF("/MyFiles/" + filetype + "/" + year + "/" +Convert.ToInt64(empid) + "_" + yr + ".pdf");

                }

                else if (filetype == "SuperannuationFund")
                {
                    base64String = service.GetPDF("/MyFiles/" + filetype + "/" + year + "/" + empid + ".pdf"); ;

                }

                else if (filetype == "MyCTC")
                {
                    base64String = service.GetPDF("/MyFiles/" + filetype + "/" + year + "/" + empid + ".pdf"); ;
                }

                else
                {

                    base64String = service.GetPDF("/MyFiles/" + filetype + "/" + year + "/" +Convert.ToInt64(empid) + ".pdf");
                }

                //base64String = service.GetPDFOutside(@"\MyFiles\" + filetype + @"\" + year + @"\" + empid + "_" + yr + ".pdf");
                filename = empid + "_" + filetype + year;
                //  }
            }
            else
            {


                // month = UrlRewritingVM.Decrypt(Request.QueryString["mon"].ToString().Trim());

                //base64String = service.GetPDF("~/MyFiles/" + filetype + "/" + year + "/" + month + "/" + empid + ".pdf");
                base64String = service.GetPDF("/MyFiles/" + filetype + "/" + year + "/" + month + "/" +Convert.ToInt64( empid) + ".pdf");
                filename = empid + "_" + filetype + year + "_" + month;
            }
            if (base64String == "404")
            {
                lblmsg.Text = "No File Found";
                lblmsg.Visible = true;
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "File Not Found");
            }
            else
            {
                lblmsg.Visible = false;

                byte[] bytes = Convert.FromBase64String(base64String);

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline;filename=" + filename + ".pdf");
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
        }
    }

    public static string Decrypt(string input, string key)
    {
        byte[] inputArray = Convert.FromBase64String(input);
        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}