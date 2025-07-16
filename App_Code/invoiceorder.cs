using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for invoice
/// </summary>
public class invoiceorder
{
    string custname = "";

    public string Custname
    {
        get { return custname; }
        set { custname = value; }
    }

    string pkgflag = "";

    public string Pkgflag
    {
        get { return pkgflag; }
        set { pkgflag = value; }
    }

    string strfullname = "";

    public string Strfullname
    {
        get { return strfullname; }
        set { strfullname = value; }
    }

    string straddress = "";

    public string Straddress
    {
        get { return straddress; }
        set { straddress = value; }
    }

    private string orderid;

    public string Orderid
    {
        get { return orderid; }
        set { orderid = value; }
    }
    private string orderdate;

    public string Orderdate
    {
        get { return orderdate; }
        set { orderdate = value; }
    }
    private string shipadrress;

    public string Shipadrress
    {
        get { return shipadrress; }
        set { shipadrress = value; }
    }

    private string total;

    public string Total
    {
        get { return total; }
        set { total = value; }
    }

    private string pkgname;

    public string Pkgname
    {
        get { return pkgname; }
        set { pkgname = value; }
    }

    private string datefrom;

    public string Datefrom
    {
        get { return datefrom; }
        set { datefrom = value; }
    }

    private string dateto;

    public string Dateto
    {
        get { return dateto; }
        set { dateto = value; }
    }

    private string gvfag;

    public string Gvfag
    {
        get { return gvfag; }
        set { gvfag = value; }
    }

    private string gvamt;

    public string Gvamt
    {
        get { return gvamt; }
        set { gvamt = value; }
    }

    private string promotype;

    public string Promotype
    {
        get { return promotype; }
        set { promotype = value; }
    }

    private string promoamt;

    public string Promoamt
    {
        get { return promoamt; }
        set { promoamt = value; }
    }
    DataTable pkgorder;
    string strvalidity;
    DateTime dtorder = new DateTime();
    private static string checkOutValidity = "";
    DateTime yesterday = new DateTime();

    public void getInvoice(int id)
    {
        DataTable ds1 = clspkgorder.getpackflag_orderid(Convert.ToDecimal(id));

        if (ds1.Rows.Count > 0)
        {
            pkgflag = Convert.ToString(ds1.Rows[0]["packageflag"].ToString());
            custname = Convert.ToString(ds1.Rows[0]["userName"]);
            DataTable dtinvoice = clspkgorder.getpackdetailinvoice_orderid(Convert.ToDecimal(id));
            pkgorder = classpkg.getpkgdetail_packageid(Convert.ToDecimal(dtinvoice.Rows[0]["packageid"].ToString()));
            if (pkgorder.Rows.Count > 0)
            {
                strvalidity = (pkgorder.Rows[0]["pkg_validity"].ToString()).Trim();
                int strval_no = Convert.ToInt32(pkgorder.Rows[0]["pkg_val_number"]);
                dtorder = DateTime.Now;
                if (strvalidity == "M")
                {
                    yesterday = DateTime.Today.AddMonths(+strval_no);
                    checkOutValidity = String.Format("{0} Month", strval_no);
                }
                else if (strvalidity == "Y")
                {
                    yesterday = DateTime.Today.AddYears(+strval_no);
                    checkOutValidity = String.Format("{0} Year", strval_no);
                }
                else if (strvalidity == "D")
                {
                    yesterday = DateTime.Today.AddDays(+strval_no);
                    checkOutValidity = String.Format("{0} Day", strval_no);
                }
            }
                if (pkgflag == "P")
                {
                    pkgname = "Intranet.com Subscription for " + checkOutValidity;
                }
                else if (pkgflag == "S")
                {
                    pkgname = "Intranet.com Pay Per View charges for Movie : " + dtinvoice.Rows[0]["productName"].ToString();
                }

                orderid = dtinvoice.Rows[0]["orderid"].ToString();
                orderdate = Convert.ToDateTime(dtinvoice.Rows[0]["pkgvalid_from"]).ToString("dd-MM-yyyy");
                total = string.Format("{0:F0}", Convert.ToDecimal(dtinvoice.Rows[0]["total"]));

                datefrom = Convert.ToDateTime(dtinvoice.Rows[0]["pkgvalid_from"]).ToString("dd-MM-yyyy");
                dateto = Convert.ToDateTime(dtinvoice.Rows[0]["pkgvalid_to"]).ToString("dd-MM-yyyy");

                DataTable dta = classaddress.getuserinfodetails(custname);
                if (dta.Rows.Count > 0)
                {
                    strfullname = Convert.ToString(dta.Rows[0]["firstname"]) + "  " + Convert.ToString(dta.Rows[0]["lastname"]);
                    straddress = Convert.ToString(dta.Rows[0]["address"]);
                }

                DataTable dt = classgiftvoucher.Getgvidbyorderid(id);
                int gvid = Convert.ToInt32(dt.Rows[0]["gvid"].ToString());
                if (gvid == 0)
                {
                    gvfag = "hidden";
                    gvamt = "0";
                }
                else
                {
                    gvfag = "visible";
                    DataTable dtgv = classgiftvoucher.Getamtbygvid_giftvoucher(Convert.ToDecimal(gvid));
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            gvamt = string.Format("{0:F0}", Convert.ToDecimal(dtgv.Rows[0]["gvamount"]));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                DataTable dtpromo = classpkgorder.getpromoamt(id);
                if (dtpromo.Rows.Count > 0)
                {

                    decimal bto1 = Convert.ToDecimal(dtpromo.Rows[0]["discount"]);
                    promoamt = string.Format("{0:F0}", bto1);
                    if (Convert.ToString(dtpromo.Rows[0]["type"]) == "amt")
                    {
                        promotype = "INR";
                    }
                    else
                    {
                        promotype = "%";
                    }

                }
                else
                {
                    promoamt = "0";
                }


            }
        }
    }