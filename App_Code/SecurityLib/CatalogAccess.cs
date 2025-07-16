using System;
using System.Data;
using System.Data.Common;


/// Wraps category details data
/// </summary>
public struct CategoryDetails
{
  public string Name;
  public string Description;
}

/// <summary>
/// Wraps product details data
/// </summary>
public struct ProductDetails
{
    public int productid;
    public string productName;
    public string producttitle;
    public string productnumber;
   
    public string shortDescription;
    public string longDescription;
    public string attributexml;
    public decimal ourPrice;
    public decimal retailprice;
    public string smallimage;
    public string bigimage;
    public char viewstatus;
    public char productstatus;
    public int visitcount;
    public decimal shippingrate;
    public decimal CODRate;
    public char CODRateFlag;
    public string shipday;
    public DateTime createdon;
    public string createdby;
    public DateTime modifiedon;
    public string modifiedby;
    public int stockvalue;
    public string inventorytype;

    public char auctionflag;
    public decimal StartBid;
    public DateTime OpenFrom;
    public DateTime Endon;
    public string Status;
    public Double weight;
    public decimal gfwraprate;
    public char gfwrapFlag;
    public decimal gfdiscount;

    public int count;
    //public string videoembed;
    //public string producttype;
}

/// <summary>
/// Product catalog business tier component
/// </summary>
public static class CatalogAccess
{
  static CatalogAccess()
  {
    //
    // TODO: Add constructor logic here
    //
  }
 // to get maximum frequency keywords to jion in cloud container 
    public static DataSet getkeywordfrequencydetails()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_Selectkeywordfrequency";
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    public static DataTable UserGetRelatedProducts(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_usergetrelatedproducts";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProductID";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }
    
    ////////////////Get FEATURED PRODUCT
 public static DataTable GetFeaturedProducts()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getfeaturedproduct";
       
       // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
               // return the page of products
        return table;
    }
    public static DataSet GetDealOfDayProducts()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetDealOfDayProducts";
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }


    public static ProductDetails GetAuctionProductDetails(int productId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAuctionProductDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a ProductDetails object
        ProductDetails details = new ProductDetails();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.productName = dr["productName"].ToString();
            details.producttitle = dr["productName"].ToString();
            details.productnumber = Convert.ToString(dr["productnumber"]);
            details.shortDescription = dr["shortDescription"].ToString();
            details.longDescription = dr["longDescription"].ToString();
            details.ourPrice = Decimal.Parse(dr["ourPrice"].ToString());
            details.retailprice = Decimal.Parse(dr["retailprice"].ToString());
            details.smallimage = dr["smallimage"].ToString();
            details.bigimage = dr["bigimage"].ToString();
            details.viewstatus = Convert.ToChar(dr["viewstatus"]);
            details.productstatus = Convert.ToChar(dr["productstatus"]);
            //  details.digitallink = Convert.ToString(dr["digitallink"]);
            //  details.digitalflag = Convert.ToChar(dr["digitalflag"]);
            details.visitcount = Convert.ToInt32(dr["visitcount"]);
            details.shippingrate = Convert.ToDecimal(dr["shippingrate"]);
            details.shipday = Convert.ToString(dr["shipday"]);
            details.attributexml = Convert.ToString(dr["attributexml"]);
            //   details.weight = Convert.ToDecimal(dr["weight"]);
            details.OpenFrom = Convert.ToDateTime(dr["OpenFrom"]);
            details.Endon = Convert.ToDateTime(dr["Endon"]);
            details.StartBid = Convert.ToDecimal(dr["startbid"]);
            details.Status = Convert.ToString(dr["Status"]);
            details.auctionflag = Convert.ToChar(dr["auctionflag"]);
            if (dr["stockValue"] != DBNull.Value)
            {
                details.stockvalue = Convert.ToInt32(dr["stockValue"]);
            }
            else
            {
                details.stockvalue = 0;
            }
            details.count = 1;
        }
        else
        {
            details.count = 0;
        }
      
        // return department details
        return details;
    }


    public static DataTable getproductbigimageauction(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetAuctionProductDetails";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
}