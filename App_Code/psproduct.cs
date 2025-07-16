using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for psproduct
/// </summary>
public class psproduct
{
	public psproduct()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string productname { get; set; }
    public string pname { get; set; }
    public string retailprice { get; set; }
    public string ourprice { get; set; }
    public string productid { get; set; }
    public string producturl { get; set; }
    public string smallimage { get; set; }
    public string smallimage2{ get;set; }
    public string productnumbar { get; set; }
    public string parentflag { get; set; }
    public string parentflag2{ get; set;  }
    public string flagcolor { get; set; }
    public string caturl { get; set; }
    public string shortdescription { get; set; }
    public string moviefav { get; set; }
    public string display  { get; set; }
    public string star1  { get; set; }
    public string star2 { get; set; }
    public string star3 { get; set; }
    public string star4 {get; set;  }
    public string star5 { get;set;  }
    public string count {get;set; }
    public string heartclass{get;set;}
    public string star { get; set; }

    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, productid, "PD");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

   
}