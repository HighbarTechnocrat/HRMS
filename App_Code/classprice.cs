using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for classprice
/// </summary>
public class classprice
{
    string packageprice = "";

    public string Packageprice
    {
        get { return packageprice; }
        set { packageprice = value; }
    }

    string specialprice = "";

    public string Specialprice
    {
        get { return specialprice; }
        set { specialprice = value; }
    }


    string premiumcount = "";

    public string Premiumcount
    {
        get { return premiumcount; }
        set { premiumcount = value; }
    }

    string premiummonth = "";

    public string Premiummonth
    {
        get { return premiummonth; }
        set { premiummonth = value; }
    }
    string dolloersign = "";

    public string dolloer_sign
    {
        get { return dolloersign; }
        set { dolloersign = value; }
    }

    string spdolloersign = "";


    public string specialdolleramt
    {
        get { return spdolloersign; }
        set { spdolloersign = value; }
    }

    public void getpackage()
    {

        //DataTable dtp = classpkg.getpkgprice();
        //if (dtp.Rows.Count > 0)
        //{
        //    packageprice = dtp.Rows[0]["pkg_price"].ToString();
        //}
        decimal usdprice = 0;
        decimal totalconvprice = 0;
        DataTable dts = classpkg.getspecialprice();
        if (dts.Rows.Count > 0)
        {
            decimal specprice = Convert.ToDecimal(dts.Rows[0]["special_price"]);
            specialprice = string.Format("{0:F0}", specprice);
            DataTable dtusd = classcurrency.GetUSDrates();
            if (dtusd.Rows.Count > 0)
            {

                usdprice = Convert.ToDecimal(dtusd.Rows[0]["ConversionRate"]);
                totalconvprice = Convert.ToDecimal(Convert.ToInt32(specialprice) * usdprice);
                specialdolleramt = string.Format("{0:F2}", totalconvprice);

            }
           
        }

        DataTable dtprem = classpkg.getpremiumpkgdetails();
        if (dtprem.Rows.Count > 0)
        {
            string premcnt = Convert.ToString(dtprem.Rows[0]["pkg_val_number"]);
            string premium= Convert.ToString(dtprem.Rows[0]["pkg_validity"]);
            packageprice = Convert.ToString(dtprem.Rows[0]["pkg_price"]);
            premiumcount = Convert.ToString(premcnt);
            premiummonth = Convert.ToString(premium);
            DataTable dtusd = classcurrency.GetUSDrates();
            if (dtusd.Rows.Count > 0)
            {

                usdprice = Convert.ToDecimal(dtusd.Rows[0]["ConversionRate"]);
                totalconvprice = Convert.ToDecimal(Convert.ToInt32(packageprice) * usdprice);
                dolloer_sign = string.Format("{0:F2}", totalconvprice);

            }
        
        }


    }
}