using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TwoCheckoutData
/// </summary>
public class TwoCheckoutData
{

    //
    // TODO: Add constructor logic here
    //

    // Seller Account Id
    public string SId { get; private set; }

    public string Mode { get; private set; }

    public string Type { get; private set; }

    public string Name { get; set; }

    public string Price { get; set; }

    /// <summary>
    /// Set Your Custome Order ID
    /// </summary>
    public string MerchantOrderId { get; set; }
    /// <summary>
    /// Set true for subscription Or False
    /// </summary>
    public bool Recurrence { get; set; }

    /// <summary>
    /// Sets billing frequency. Ex. ‘1 Week’ to bill order once a week. (Can use # Week, # Month, or # Year)
    /// </summary>
    public string RecurrenceDuration { get; set; }

    /// <summary>
    /// Card holder’s name (128 characters max). 
    /// </summary>
    public string CardHolderName { get; set; }
    /// <summary>
    /// Card holder’s street address(64 characters max)
    /// </summary>
    public string StreetAddress { get; set; }
    /// <summary>
    /// The second line for the street address
    /// </summary>
    public string StreetAddress2 { get; set; }
    /// <summary>
    /// Card holder’s city (64 characters max)
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Card holder’s state (64 characters max). Required if “country” value is ARG, AUS, BGR, CAN, CHN, CYP, EGY, FRA, IND, IDN, ITA, JPN, MYS, MEX, NLD, PAN, PHL, POL, ROU, RUS, SRB, SGP, ZAF, ESP, SWE, THA, TUR, GBR, USA - Optional for all other “country” values.
    /// </summary>
    public string State { get; set; }
    /// <summary>
    /// Card holder’s zip code/post code. Required if “country” value is ARG, AUS, BGR, CAN, CHN, CYP, EGY, FRA, IND, IDN, ITA, JPN, MYS, MEX, NLD, PAN, PHL, POL, ROU, RUS, SRB, SGP, ZAF, ESP, SWE, THA, TUR, GBR, USA - Optional for all other “country” values. (16 characters max)
    /// </summary>
    public string Zip { get; set; }

    public string Country { get; set; }

    public string Email { get; set; }
    /// <summary>
    /// Card holder’s phone (16 characters max)
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// ARS, AUD, BRL, GBP, CAD, DKK, EUR, HKD, INR, ILS, JPY, MYR, MXN, NZD, NOK, PHP, RON, RUB, SGD, ZAR, SEK, CHF, TRY, AED, USD.
    /// </summary>
    public string CurrencyCode { get; set; }

    public TwoCheckoutData()
    {
        //SId = "901274358";
        SId = "102510880";
        Mode = "2CO";
        Type = "product";
    }
}
/// <summary>
/// You will receive back all of the parameters that were passed in as well as the following parameters:
/// </summary>
public class TwoCheckoutResponce
{

    #region Parameters Returned After Process
    /// <summary>
    /// 2Checkout order number
    /// </summary>
    public string OrderNumber { get; set; }
    /// <summary>
    /// 2Checkout invoice id
    /// </summary>
    public string InvoiceId { get; set; }
    /// <summary>
    /// Y if successful (Approved).
    /// </summary>
    public string CreditCardProcessed { get; set; }
    /// <summary>
    /// the total amount of the purchase
    /// </summary>
    public string Total { get; set; }
    /// <summary>
    /// the MD5 hash used to verify that the sale came from one of our servers
    /// </summary>
    public string Key { get; set; }
    #endregion


    #region Parameter Send By Us
    
    public string Mode { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public string Price { get; set; }

    /// <summary>
    /// Set True for Subscription Or Set False for one time payment 
    /// </summary>
    public bool Recurrence { get; set; }

    /// <summary>
    /// Sets billing frequency. Ex. ‘1 Week’ to bill order once a week. (Can use # Week, # Month, or # Year)
    /// </summary>
    public string RecurrenceDuration { get; set; }

    /// <summary>
    /// Set Your Custome Order ID
    /// </summary>
    public string MerchantOrderId { get; set; }

    /// <summary>
    /// Card holder’s name (128 characters max). 
    /// </summary>
    public string CardHolderName { get; set; }
    /// <summary>
    /// Card holder’s street address(64 characters max)
    /// </summary>
    public string StreetAddress { get; set; }
    /// <summary>
    /// The second line for the street address
    /// </summary>
    public string StreetAddress2 { get; set; }
    /// <summary>
    /// Card holder’s city (64 characters max)
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Card holder’s state (64 characters max). Required if “country” value is ARG, AUS, BGR, CAN, CHN, CYP, EGY, FRA, IND, IDN, ITA, JPN, MYS, MEX, NLD, PAN, PHL, POL, ROU, RUS, SRB, SGP, ZAF, ESP, SWE, THA, TUR, GBR, USA - Optional for all other “country” values.
    /// </summary>
    public string State { get; set; }
    /// <summary>
    /// Card holder’s zip code/post code. Required if “country” value is ARG, AUS, BGR, CAN, CHN, CYP, EGY, FRA, IND, IDN, ITA, JPN, MYS, MEX, NLD, PAN, PHL, POL, ROU, RUS, SRB, SGP, ZAF, ESP, SWE, THA, TUR, GBR, USA - Optional for all other “country” values. (16 characters max)
    /// </summary>
    public string Zip { get; set; }

    public string Country { get; set; }

    public string Email { get; set; }
    /// <summary>
    /// Card holder’s phone (16 characters max)
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// ARS, AUD, BRL, GBP, CAD, DKK, EUR, HKD, INR, ILS, JPY, MYR, MXN, NZD, NOK, PHP, RON, RUB, SGD, ZAR, SEK, CHF, TRY, AED, USD.
    /// </summary>
    public string CurrencyCode { get; set; } 
    #endregion

}