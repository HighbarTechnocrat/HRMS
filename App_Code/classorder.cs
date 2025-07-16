using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public struct order
{
    //public string username;
    //public int orderid;
    //public DateTime orderdate;
    //public decimal total;
    //public decimal gfwrap_amt;
    //public string gflag;
    //public string paymode;

}
public struct customer
{
    public string username;
}
public struct sumorderamt
{
    public string total;
}

public struct giftamt
{
    public string amount;
}
/// <summary>
/// Summary description for classorder
/// </summary>
public class classorder
{
	public classorder()
	{

		//
		// TODO: Add constructor logic here
		//
	}

    public static int insertorder(string username, string firstname, string lastname, decimal subTotalamount, decimal shippingamount, decimal total, string paymode,string orderstatus, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateorder";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
      
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@firstname";
        param.Value = firstname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lastname";
        param.Value = lastname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@subTotalamount";
        param.Value = subTotalamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shippingamount";
        param.Value = shippingamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
               
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@total";
        param.Value = total;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
       
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@paymode";
        param.Value = paymode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@outorderid";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        
        int outorderid = 0;
        int result = -1;
        try
        {           
            result = GenericDataAccess.ExecuteNonQuery(comm);
             outorderid = Int32.Parse(comm.Parameters["@outorderid"].Value.ToString());  
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        return outorderid;

    }
    public static DataTable getadminorder(string orderstatus)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getorders";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getadminorderdatewise(string orderstatus, DateTime fromdate, DateTime todate)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getordersfromdate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderfrom";
        param.Value = fromdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderto";
        param.Value = todate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getadminorderbyId(int orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getordersbyidnew";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@orderstatus";
        //param.Value = orderstatus;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int16;
        comm.Parameters.Add(param);
        
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getvendororderdatewise(string orderstatus, DateTime fromdate, DateTime todate, string vendorname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getvendorordersfromdate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderfrom";
        param.Value = fromdate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderto";
        param.Value = todate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable orderSearch(string status, string fromdate, string todate, string orderid, string orderidto, string paymode, string vendorname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_orderSearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        // create a new parameter      
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fromdate";
        param.Value = fromdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@todate";
        param.Value = todate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@orderidto";
        param.Value = orderidto;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@paymode";
        param.Value = paymode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getvendororderbyId( int orderid, string vendorname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getvendorordersbyid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
       // param.ParameterName = "@orderstatus";
       // param.Value = orderstatus;
       // param.DbType = DbType.String;
      //  comm.Parameters.Add(param);

        // create a new parameter
      //  param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int16;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getvendororderitems(int orderid, string vendorname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getvorderitemdetail";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getorderitems(int orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getorderitemdetail";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getvendororder(string orderstatus, string vendorname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getvendororders";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool deleteorder(int orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteorder";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }
    //Added on 31st July 2009 for campaign wise Orders search
    public static DataTable getadminorderbycampaign(string orderstatus, string campaignname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getordersbycampaign";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderstatus";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@campaignname";
        param.Value = campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getbalancepoints()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbalancepoint";
      // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getorderhistory(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getorderhistory";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    // new created by Sony Surana as on 11 Nov 2012
    public static DataTable getcustomerorderhistory(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcustomerorderhistory";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getorderstatus(string orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getorderstatus";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static string updatecustomer_orderstatus(int orderid, string username, string status,string orderstatus)
    {
       
        DbCommand comm = GenericDataAccess.CreateCommand();    
        comm.CommandText = "usp_updatecustomer_orderstatus";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         param = comm.CreateParameter();
         param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@orderstatus ";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        string str =  GenericDataAccess.ExecuteScalar(comm);
        return str;
    }

    public static string updatecustomer_orderstatus1(int orderid,  string orderstatus)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_updatecustomer_orderstatus1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@orderstatus ";
        param.Value = orderstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        string str = GenericDataAccess.ExecuteScalar(comm);
        return str;
    }

    public static DataTable getorderitemdetailbyid(int orderid)
    {
       
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getorderitemdetailbyid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderitem";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    public static int insertorderitem( int orderid ,  int Productid , 
          string productname , string productnumber ,int quantity ,
          decimal originalprice, decimal shiprate,
          decimal itemtotal,
          decimal total,
          string createdby, int shipaddressid 
          )
    {
        
        DbCommand comm = GenericDataAccess.CreateCommand();       
        comm.CommandText = "usp_insertorderitem";     
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Productid";
        param.Value = Productid;
        param.DbType = DbType.Int16;
        comm.Parameters.Add(param);
        
        param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@quantity";
        param.Value = quantity;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@originalprice";
        param.Value = originalprice;
        param.DbType = DbType.Double;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@shiprate";
        param.Value = shiprate;
        param.DbType = DbType.Double;
        comm.Parameters.Add(param);
     
        param = comm.CreateParameter();
        param.ParameterName = "@itemtotal";
        param.Value = itemtotal;
        param.DbType = DbType.Double;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@total";
        param.Value = total;
        param.DbType = DbType.Double;
        comm.Parameters.Add(param);
       
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@shipaddressid";
        param.Value = shipaddressid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
      
       int result = GenericDataAccess.ExecuteNonQuery(comm);

        // execute the stored procedure
       return result;
    }

      public static sumorderamt getsumorderamt(string orderid)
      {
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_sumorderamt";

          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // execute the stored procedure and save the results in a DataTable
          DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
          // return the page of products

          sumorderamt details = new sumorderamt();

          //if (table.Rows.Count > 0)
          //{
          //    DataRow dr = table.Rows[0];

          //    if ((dr["sumorderamt"]) != DBNull.Value)
          //    {
          //        details.total = Convert.ToString(dr["sumorderamt"].ToString());
          //    }
          //    else
          //    {
          //        details.total = "";
          //    }
          //}
          return details;

      }
      public static giftamt getgiftamt(string orderid)
      {
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_getgiftamt";

          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // execute the stored procedure and save the results in a DataTable
          DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
          // return the page of products

          giftamt details = new giftamt();

          if (table.Rows.Count > 0)
          {
              DataRow dr = table.Rows[0];

              if ((dr["amount"]) != DBNull.Value)
              {
                  details.amount = Convert.ToString(dr["amount"].ToString());
              }
              else
              {
                  details.amount = "";
              }


          }
          return details;

      }
      public static DataTable Getcustomerorderdtl()
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_Getcustomerorderdtl";
          // execute the stored procedure
          return GenericDataAccess.ExecuteSelectCommand(comm);
      }

      //public static order getcustorder(string orderid)
      //{
      //    DbCommand comm = GenericDataAccess.CreateCommand();
      //    // set the stored procedure name
      //    comm.CommandText = "sp_proc_getcustdetails";

      //    DbParameter param = comm.CreateParameter();
      //    param.ParameterName = " @orderid";
      //    param.Value = orderid;
      //    param.DbType = DbType.String;
      //    comm.Parameters.Add(param);
      //    // execute the stored procedure and save the results in a DataTable
      //    DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
      //    // return the page of products

      //    order details = new order();

      //    if (table.Rows.Count > 0)
      //    {
      //        DataRow dr = table.Rows[0];

             
      //            details.username = Convert.ToString(dr["username"]);
      //            details.orderid = Convert.ToInt32(dr["orderid"]);
      //            details.orderdate = Convert.ToDateTime(dr["orderdate"]);
      //            details.total = Convert.ToDecimal(dr["total"]);


      //    }
      //    return details;

      //}

      public static bool customer(string username)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_deletecustpt";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@username";
          param.Value = username;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          int result = -1;

          result = GenericDataAccess.ExecuteNonQuery(comm);


          return (result != -1);
      }

      public static bool updatecustorder(int orderid,string paymode)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_updatecustorderitem1";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;

          param.ParameterName = "@paysubmode";
          param.Value = paymode;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          // create a new parameter

        
          //  param.Size = 1;


          // result will represent the number of changed rows
          int result = -1;
          try
          {
              // execute the stored procedure
              result = GenericDataAccess.ExecuteNonQuery(comm);
          }
          catch
          {
              // any errors are logged in GenericDataAccess, we ingore them here
          }
          // result will be 1 in case of success 
          return (result != -1);
      }
      public static bool updatecustorderitem(int orderid, string orderstatus)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_updatecustorderitem";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;

          param.ParameterName = "@orderstatus";
          param.Value = orderstatus;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          // create a new parameter


          //  param.Size = 1;


          // result will represent the number of changed rows
          int result = -1;
          try
          {
              // execute the stored procedure
              result = GenericDataAccess.ExecuteNonQuery(comm);
          }
          catch
          {
              // any errors are logged in GenericDataAccess, we ingore them here
          }
          // result will be 1 in case of success 
          return (result != -1);
      }
      public static bool usp_UpdateOrderIntblorditemcustord(int orderid, string itemStatus,DateTime dateTime_Main)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "usp_upordstusintblorditemcustord";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@itemStatus";
          param.Value = itemStatus;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@dateTime";
          param.Value = dateTime_Main;
          param.DbType = DbType.DateTime;
          comm.Parameters.Add(param);
          // result will represent the number of changed rows
          int result = -1;
          try
          {
              // execute the stored procedure
              result = GenericDataAccess.ExecuteNonQuery(comm);
          }
          catch
          {
              // any errors are logged in GenericDataAccess, we ingore them here
          }
          // result will be 1 in case of success 
          return (result != -1);
      }
      public static DataTable getstatusorderCusttbl(int orderid)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "usp_getordstatusfromcustordtbl";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }
      public static DataTable getAllEmailFormatstoredProc(int orderid)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "usp_AllEmailFormatstoredProc";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

      public static DataTable getShipaddressforEmail(int shipid)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "getShipaddressforEmail";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@shippingid";
          param.Value = shipid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

      public static DataTable get_UserNameforProfileInfo(int orderid)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "get_UserNameforProfileInfo";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

      public static DataTable get_StateandcityInfoforEmail(string cityname)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "get_StateandcityInfoforEmail";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@CityName";
          param.Value = cityname;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

      public static DataTable newGetShipAddressforEmailAddressFormat(int shipid,string SbChar)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "usp_newGetShipAddressforEmailAddressFormat";

          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@shippingid";
          param.Value = shipid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@SBChar";
          param.Value = shipid;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

      public static DataTable getCountandProductidInfo(int orderid,string FlagM,int Productid)
      {

          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "usp_getCountandProductidInfo";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@flagM";
          param.Value = FlagM;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@productid";
          param.Value = Productid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
          return dt;
      }

//      public static int getstatusorderCusttbl(int orderid)
//      {
//          // get a configured DbCommand object
//          DbCommand comm = GenericDataAccess.CreateCommand();
//          // set the stored procedure name
//          comm.CommandText = "usp_getordstatusfromcustordtbl";
//          // create a new parameter
//          DbParameter param = comm.CreateParameter();
//          param.ParameterName = "@orderid";
//          param.Value = orderid;
//          param.DbType = DbType.Int32;
//          comm.Parameters.Add(param);

//          // create a new parameter
//          param = comm.CreateParameter();
//          param.ParameterName = "@outorderid";
//          param.Direction = ParameterDirection.Output;
//          param.DbType = DbType.Int32;
//          comm.Parameters.Add(param);

//          int outorderid = 0;
//          int result = -1;
//          try
//          {
//              result = GenericDataAccess.ExecuteNonQuery(comm);
//              outorderid = Int32.Parse(comm.Parameters["@outorderid"].Value.ToString());
//          }
//          catch
//          {
//              // any errors are logged in GenericDataAccess, we ingore them here
//          }
//          return outorderid;

//      }


      public static bool deletecartdetails(string email)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_cart_deletecartdetails";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@emailid";
          param.Value = email;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          int result = -1;

          result = GenericDataAccess.ExecuteNonQuery(comm);


          return (result != -1);
      }

      public static bool updateorderstatus(int orderid, string email, string orderstatus)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_customerorder_updateorderstatus";

          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@email";
          param.Value = email;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@orderstatus";
          param.Value = orderstatus;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // result will represent the number of changed rows
          int result = -1;
          try
          {
              // execute the stored procedure
              result = GenericDataAccess.ExecuteNonQuery(comm);
          }
          catch
          {
              // any errors are logged in GenericDataAccess, we ingore them here
          }
          // result will be 1 in case of success 
          return (result >= 1);
      }

      public static DataTable getorderdetailsbyorderid(string emailid, int orderid)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "sp_proc_orderitem_getorderdetailsbyorderid";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@emailid";
          param.Value = emailid;
          param.DbType = DbType.String;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          command.Parameters.Add(param);

          return GenericDataAccess.ExecuteSelectCommand(command);

      }
      public static DataTable getcustomerorderflag_orderid(string emailid, int orderid)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "sp_proc_customerorder_getorderpackageflagbyorderid";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@emailid";
          param.Value = emailid;
          param.DbType = DbType.String;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          command.Parameters.Add(param);

          return GenericDataAccess.ExecuteSelectCommand(command);

      }

    //method added on 19th jan 2015

      public static DataTable getpackageorder_orderdetails(decimal orderid)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "sp_proc_packageorder_orderdetails_orderid";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Decimal;
          command.Parameters.Add(param);

          return GenericDataAccess.ExecuteSelectCommand(command);

      }




      //method on 4th march for insert profileid

      public static DataTable insertprofileid_customerorder(decimal orderid, string profileid, string flag)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "insertupdateprofileid_customer";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Decimal;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "@user_profileid";
          param.Value = profileid;
          param.DbType = DbType.String;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "@user_activeflag";
          param.Value = flag;
          param.DbType = DbType.String;
          command.Parameters.Add(param);
          return GenericDataAccess.ExecuteSelectCommand(command);

      }

      public static DataTable getprofileid_customerorder(decimal orderid)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "getprofileidbyorderid_customer_order";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Decimal;
          command.Parameters.Add(param);


          return GenericDataAccess.ExecuteSelectCommand(command);

      }

      public static DataTable updateprofileflag_customerorder(decimal orderid, string profileflag)
      {
          DbCommand command = GenericDataAccess.CreateCommand();
          command.CommandText = "updateprofileflagbyorderid_customer_order";
          DbParameter param = command.CreateParameter();
          param.ParameterName = "@orderid";
          param.Value = orderid;
          param.DbType = DbType.Decimal;
          command.Parameters.Add(param);

          param = command.CreateParameter();
          param.ParameterName = "@user_activeflag";
          param.Value = profileflag;
          param.DbType = DbType.String;
          command.Parameters.Add(param);
          return GenericDataAccess.ExecuteSelectCommand(command);

      }
      public static DataSet getshipaddressdetails(int indexid, int orderid)
      {
          DbCommand comm = GenericDataAccess.CreateCommand();
          comm.CommandText = "sp_proc_shipaddress_getshipaddressdetails";
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@indexid";
          param.Value = indexid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@Orderid";
          param.Value = orderid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          DataSet ds = GenericDataAccess.ExecuteSelectCmd(comm);
          return ds;
      }
}
