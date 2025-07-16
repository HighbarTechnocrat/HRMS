using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;

/// <summary>
/// Summary description for Invoice
/// </summary>
public class Invoice
{
    private string _customerComments = "";
    public string CustomerComments
    {
        get
        {
            return _customerComments;
        }
        set
        {
            _customerComments = value.Trim();
        }
    }

    private ArrayList _invoiceItems = new ArrayList();
    public ArrayList InvoiceItems
    {
        get
        {
            return _invoiceItems;
        }
        set
        {
            _invoiceItems = value;
        }
    }

    private string _currency = "USD"; //currency to display in
    public string Currency
    {
        get
        {
            return _currency;
        }
        set
        {
            if (value == null || value.Trim() == "")
            {
                _currency = "USD";
            }
            else
            {
                _currency = value.ToUpper();
            }
        }
    }

    
    private string _invoiceId = "";
    /// <summary>
    /// Same as session Id (unless pulling from database)
    /// </summary>
    public string InvoiceId
    {
        get
        {
            return _invoiceId;
        }
        set
        {
            _invoiceId = value;
        }
    }

    private string _orderid = "";
    /// <summary>
    /// Same as session Id (unless pulling from database)
    /// </summary>
    public string Orderid
    {
        get
        {
            return _orderid;
        }
        set
        {
            _orderid = value;
        }
    }

    public string ProductNames
    {
        get
        {
            DataTable dtpkg = classpkgorder.getpackagetypebyemail(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));
            string pkgflag = "";
            if (dtpkg.Rows.Count > 0)
            {

                pkgflag = dtpkg.Rows[0]["packageflag"].ToString().Trim();
           
            }

            string productNames = "";
            DataTable dtorderdetails = classorder.getorderdetailsbyorderid(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));

            for (int i = 0; i < dtorderdetails.Rows.Count; i++)
            {
                if (pkgflag == "S")
                {
               productNames += dtorderdetails.Rows[i]["packagename"].ToString().Trim() + ", ";
                }
                if (pkgflag == "R")
                {
              productNames += dtorderdetails.Rows[i]["pname"].ToString().Trim() + ", ";
                }
                
            }
            if (productNames.Length > 2)
            {
                productNames = productNames.Substring(0, productNames.Length - 2);
            }
            return productNames;
        }
    }

    /// <summary>
    /// RETURNS the HTML Encoded version of the Invoice data
    ///   May be useful in emails or displays of invoice
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        decimal LastTotalAmount = 0;
        string amtgift = "";

        decimal usdprice = 0;
        decimal totalconvprice = 0;
        StringBuilder invoiceHtml = new StringBuilder();
        LastTotalAmount = Convert.ToDecimal(this.Total.ToString());

        DataTable dtusd = classcurrency.GetUSDrates();
        if (dtusd.Rows.Count > 0)
        {

            usdprice = Convert.ToDecimal(dtusd.Rows[0]["ConversionRate"]);
            totalconvprice = Convert.ToDecimal(LastTotalAmount * usdprice);
            amtgift = string.Format("{0:F2}", totalconvprice);

        }
        invoiceHtml.Append("<b>INVOICE : ").Append(this.Orderid.ToString()).Append("</b><br />");
        invoiceHtml.Append("<b>DATE : </b>").Append(DateTime.Now.ToShortDateString()).Append("<br />");
        invoiceHtml.Append("<b>Invoice Amt :</b> $").Append(totalconvprice.ToString("#.00")).Append("<br />");

        invoiceHtml.Append("<br /><b>CUSTOMER CONTACT INFO:</b><br />");
        invoiceHtml.Append("<b>Name : </b>").Append(this.ContactName).Append("<br />");
        invoiceHtml.Append("<b>Phone : </b>").Append(this.ContactPhone).Append("<br />");
        invoiceHtml.Append("<b>Email : </b>").Append(this.ContactEmail).Append("<br />");
        invoiceHtml.Append("<b>Address : </b><br />").Append(this.ContactAddress1).Append("<br />");
        invoiceHtml.Append(this.ContactAddress2).Append("<br />");
        invoiceHtml.Append(this.ContactCity).Append(", ").Append(this.ContactStateProvince).Append(" ").Append(this.ContactZip).Append("<br />");
        invoiceHtml.Append("<br /><b>SHIP TO:</b><br />");
        invoiceHtml.Append("<b>Name : </b>").Append(this.ShipToName).Append("<br />");
        invoiceHtml.Append("<b>Address : </b><br />").Append(this.ShipToAddress1).Append("<br />");
        invoiceHtml.Append(this.ShipToAddress2).Append("<br />");
        invoiceHtml.Append(this.ShipToCity).Append(", ").Append(this.ShipToStateProvince).Append(" ").Append(this.ShipToZip).Append("<br />");

        DataTable dtpkg = classpkgorder.getpackagetypebyemail(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));
        string pkgflag = "";
        DataTable dtorderdetails = new DataTable();
        if (dtpkg.Rows.Count > 0)
        {
            pkgflag = dtpkg.Rows[0]["packageflag"].ToString().Trim();
        }

        if (pkgflag == "S")
        {
            invoiceHtml.Append("<br /><b>MOVIE:</b><br /><table><tr><th>MovieName</th></tr>");
             dtorderdetails = classorder.getorderdetailsbyorderid(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));

            for (int i = 0; i < dtorderdetails.Rows.Count; i++)
            {
                invoiceHtml.Append("<tr><td>").Append(dtorderdetails.Rows[i]["pname"].ToString()).Append("</td></tr>");
            }
        }

        if (pkgflag == "P")
        {
            invoiceHtml.Append("<br /><b>MOVIE:</b><br /><table><tr><th>PackageName</th></tr>");
            dtorderdetails = classorder.getorderdetailsbyorderid(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));

            for (int i = 0; i < dtorderdetails.Rows.Count; i++)
            {
                invoiceHtml.Append("<tr><td>").Append(dtorderdetails.Rows[i]["packagename"].ToString()).Append("</td></tr>");
            }
        }
        invoiceHtml.Append("</table>");
        return invoiceHtml.ToString();
    }


    /// <summary>
    /// PURPOSE: to write PayPay item list for invoice
    /// </summary>
    public string PaypalItemList
    {
        get
        {
            StringBuilder payPalItems = new StringBuilder();
            int counter = 0;
            decimal LastTotalAmount = 0;
            string amtgift = "";
            decimal usdprice = 0;
            decimal totalconvprice = 0;
            DataTable dtorderdetails = classorder.getorderdetailsbyorderid(this.ContactEmail, Convert.ToInt32(this.Orderid.ToString()));
            //foreach (InvoiceItem x in _invoiceItems)
            //{
               for (int i = 0; i < dtorderdetails.Rows.Count; i++)
                {
                counter++;
                decimal amount =Convert.ToDecimal(dtorderdetails.Rows[i]["total"].ToString());
                decimal shiprating = Convert.ToDecimal(dtorderdetails.Rows[i]["shiprate"].ToString()) * Convert.ToDecimal(dtorderdetails.Rows[i]["quantity"].ToString());
                decimal handling = 0M;

                string itemNameTemplate = "<input type=\"hidden\" name=\"item_name_$count$\" value=\"$itemName$\" />\n";
                string amountTemplate = "<input type=\"hidden\" name=\"amount_$count$\" value=\"$amount$\" />\n";
                string qtyTemplate = "<input type=\"hidden\" name=\"quantity_$count$\" value=\"$quantity$\" />\n";
                string shippingTemplate = "<input type=\"hidden\" name=\"shipping_$count$\" value=\"$shipping$\" />\n";
                string handlingTemplate = "<input type=\"hidden\" name=\"handling_$count$\" value=\"$handling$\" />\n\n";
                
                itemNameTemplate = itemNameTemplate.Replace("$itemName$", dtorderdetails.Rows[i]["pname"].ToString()).Replace("$count$", counter.ToString());
                LastTotalAmount = Convert.ToDecimal(amount.ToString());
                DataTable dtusd = classcurrency.GetUSDrates();
                if (dtusd.Rows.Count > 0)
                {

                    usdprice = Convert.ToDecimal(dtusd.Rows[0]["ConversionRate"]);
                    totalconvprice = Convert.ToDecimal(LastTotalAmount * usdprice);
                    amtgift = string.Format("{0:F2}", totalconvprice);

                }
                amountTemplate = amountTemplate.Replace("$amount$", totalconvprice.ToString("#.00")).Replace("$count$", counter.ToString());
                qtyTemplate = qtyTemplate.Replace("$quantity$", dtorderdetails.Rows[i]["quantity"].ToString()).Replace("$count$", counter.ToString());
                shippingTemplate = shippingTemplate.Replace("$shipping$", shiprating.ToString("#.00")).Replace("$count$", counter.ToString());
                handlingTemplate = handlingTemplate.Replace("$handling$", handling.ToString("#.00")).Replace("$count$", counter.ToString());

                payPalItems.Append(itemNameTemplate).Append(amountTemplate).Append(qtyTemplate).Append(shippingTemplate).Append(handlingTemplate);
            }
            return payPalItems.ToString();
        }
    }

    /// <summary>
    /// Cost of Unit Prices * qty
    /// </summary>
    public decimal SubTotal
    {
        get
        {
            decimal subtotal = 0.0M;
            //DataTable dtorderdetails = classorder.getcustomerorderbyorderid(Convert.ToInt32(this.Orderid.ToString()));
            //if (dtorderdetails.Rows.Count > 0)
            //{
            //    subtotal = Convert.ToDecimal(dtorderdetails.Rows[0]["subTotalamount"].ToString());
            //}
            //else
            //{
            //    return 0;
            //}
            return subtotal;
        }
    }

    /// <summary>
    /// Retrieve Shipping cost for Invoice
    /// </summary>
    public decimal ShippingCost
    {
        get
        {
            decimal shipping = 0.0M;
           
            return shipping;
        }
    }

    /// <summary>
    /// Any Handling costs
    /// </summary>
    public decimal HandlingCost
    {
        get
        {
            decimal handling = 0.0M;
            handling = 0M;
            return handling;
        }
    }

   

    public decimal Total
    {
        get
        {
            return this.SubTotal + this.HandlingCost + this.Taxes + this.ShippingCost;
        }
    }
    public decimal Taxes
    {
        get
        {
            decimal taxes = 0.0M;
            taxes = 0M;
            return taxes;
        }
    }
   

    #region Ship To Properties

    private string _shipToName = "";
    public string ShipToName
    {
        get
        {
            return _shipToName;
        }
        set
        {
            _shipToName = value.Trim();
        }
    }

    private string _shipToAddress1 = "";
    public string ShipToAddress1
    {
        get
        {
            return _shipToAddress1;
        }
        set
        {
            _shipToAddress1 = value.Trim();
        }
    }

    private string _shipToAddress2 = "";
    public string ShipToAddress2
    {
        get
        {
            return _shipToAddress2;
        }
        set
        {
            _shipToAddress2 = value.Trim();
        }
    }

    private string _shipToCity = "";
    public string ShipToCity
    {
        get
        {
            return _shipToCity;
        }
        set
        {
            _shipToCity = value.Trim();
        }
    }

    private string _shipToStateProvince = "";
    public string ShipToStateProvince
    {
        get
        {
            return _shipToStateProvince;
        }
        set
        {
            _shipToStateProvince = value.Trim();
        }
    }

    private string _shipToZip = "";
    public string ShipToZip
    {
        get
        {
            return _shipToZip;
        }
        set
        {
            _shipToZip = value.Trim();
        }
    }

    private string _shipToCountry ="";
    public string ShipToCountry
    {
        get
        {
            return _shipToCountry;
        }
        set
        {
            _shipToCountry = value.Trim();
        }
    }
    #endregion

    #region Contact Properties

    private string _contactName = "";
    public string ContactName
    {
        get
        {
            return _contactName;
        }
        set
        {
            _contactName = value.Trim();
        }
    }

    private string _contactPhone = "";
    public string ContactPhone
    {
        get
        {
            return _contactPhone;
        }
        set
        {
            _contactPhone = value.Trim();
        }
    }

    private string _contactEmail = "";
    public string ContactEmail
    {
        get
        {
            return _contactEmail;
        }
        set
        {
            _contactEmail = value.Trim();
        }
    }

    private string _contactAddress1 = "";
    public string ContactAddress1
    {
        get
        {
            return _contactAddress1;
        }
        set
        {
            _contactAddress1 = value.Trim();
        }
    }

    private string _contactAddress2 = "";
    public string ContactAddress2
    {
        get
        {
            return _contactAddress2;
        }
        set
        {
            _contactAddress2 = value.Trim();
        }
    }

    private string _contactCity = "";
    public string ContactCity
    {
        get
        {
            return _contactCity;
        }
        set
        {
            _contactCity = value.Trim();
        }
    }

    private string _contactStateProvince = "";
    public string ContactStateProvince
    {
        get
        {
            return _contactStateProvince;
        }
        set
        {
            _contactStateProvince = value.Trim();
        }
    }

    private string _contactZip = "";
    public string ContactZip
    {
        get
        {
            return _contactZip;
        }
        set
        {
            _contactZip = value.Trim();
        }
    }

    private string _contactCountry = "US";
    public string ContactCountry
    {
        get
        {
            return _contactCountry;
        }
        set
        {
            _contactCountry = value.Trim();
        }
    }

    #endregion


    public Invoice()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Invoice(string invoiceId)
    {
        this.InvoiceId = invoiceId;
    }

    public void EmptyCart()
    {
        _invoiceItems = new ArrayList();
    }

    

}
