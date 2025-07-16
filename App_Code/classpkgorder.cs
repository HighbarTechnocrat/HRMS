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



/// <summary>
/// Summary description for classpkgorder
/// </summary>
public class classpkgorder
{
	public classpkgorder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

public static int insertorder(string username, string firstname, string lastname, decimal subTotalamount, decimal shippingamount, decimal total, string paymode, string orderstatus, string createdby,string pkgflag,int gvid)
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

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pkgflag";
        param.Value = pkgflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@gvid";
        param.Value = gvid;
        param.DbType = DbType.String;
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
public static DataTable getorderdetailsbyorderid(string emailid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_customer_order_orderid";
        //command.CommandText = "sp_proc_customer_order_orderemailid";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        command.Parameters.Add(param);

        //param = command.CreateParameter();
        //param.ParameterName = "@orderid";
        //param.Value = orderid;
        //param.DbType = DbType.Decimal;
        //command.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(command);
    }
public static int insertorderitem(int orderid, int Productid,string productname, string productnumber, int quantity,decimal originalprice, decimal shiprate,decimal itemtotal, decimal total, string createdby, int shipaddressid,decimal pkgid,string pkgname,string pkg_from,string pkg_to,string pkg_status,string pkg_oprname,string pkg_mobno)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_insertorderitem_pkg";
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

        param = comm.CreateParameter();
        param.ParameterName = "@packageid";
        param.Value = pkgid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@packagename";
        param.Value = pkgname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pkgvalid_from";
        param.Value = pkg_from;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pkgvalid_to";
        param.Value = pkg_to;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@pkg_status";
        param.Value = pkg_status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@operatot_name";
        param.Value = pkg_oprname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@operatot_mobno";
        param.Value = pkg_mobno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        int result = GenericDataAccess.ExecuteNonQuery(comm);

        // execute the stored procedure
        return result;
    }
public  static bool packagevalidity_status(string emailid,decimal orderid)
 {
     DbCommand command = GenericDataAccess.CreateCommand();
     command.CommandText = "sp_proc_updateuserpakgvalidity";
     DbParameter param = command.CreateParameter();
     param.ParameterName = "@useremail";
     param.Value = emailid;
     param.DbType = DbType.String;
     command.Parameters.Add(param);

     param = command.CreateParameter();
     param.ParameterName = "@orderid";
     param.Value = orderid;
     param.DbType = DbType.Decimal;
     command.Parameters.Add(param);

     int result = -1;
     try
     {
         result = GenericDataAccess.ExecuteNonQuery(command);

     }
     catch
     {
     }
     return (result >= 1);

 }
public static DataTable getpackagetypebyemail(string emailid, decimal orderid)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_getpkgtype_customer_order";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@useremail";
    param.Value = emailid;
    param.DbType = DbType.String;
    command.Parameters.Add(param);

    param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.Decimal;
    command.Parameters.Add(param);
    return GenericDataAccess.ExecuteSelectCommand(command);
}

public static DataSet getorderdetailsbyorderid(string emailid, int orderid)
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

    return GenericDataAccess.ExecuteSelectCmd1(command);

}

public static DataTable getcustorderdetailsbyorderid(decimal orderid)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_customer_orderdetails_orderid";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.Decimal;
    command.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(command);
}

public static DataTable updateorderstatus(decimal orderid,string orderstatus)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_updateorderstatus";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.Decimal;
    command.Parameters.Add(param);

    param = command.CreateParameter();
    param.ParameterName = "@orderstatus";
    param.Value = orderstatus;
    param.DbType = DbType.String;
    command.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(command);
}



public static DataTable getgiftamtorderid(decimal orderid)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_giftamt_order_orderid";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.Decimal;
    command.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(command);
}

    //method on 3 nov 
public static DataTable getuserorderdetailsbyorderid_user(decimal orderid)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_customer_order_orderid";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.String;
    command.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(command);
}

    //method on 4th nov
public static DataSet getcustdetails_oid(string emailid, int orderid)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_orderitem_getcustdetails_oidemail";
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

    return GenericDataAccess.ExecuteSelectCmd1(command);

}

public static DataTable getorderitems_package(int orderid,string username)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "sp_proc_getorderitemdetail_pkg_username";
    // create a new parameter
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
    // execute the stored procedure
    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static DataSet orderSearch(string status, string fromdate, string todate, string orderid, string paymode, string pkgtypes,string username,string compname)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "sp_proc_orderSearch_username_orderid";
    //comm.CommandText = "sp_proc_orderSearch_username";
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
    param.ParameterName = "@paymode";
    param.Value = paymode;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);
    // create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@pkftype";
    param.Value = pkgtypes;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);
    // create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@username";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);
    // create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@compname";
    param.Value = compname;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);
    // execute the stored procedure
   // return GenericDataAccess.ExecuteSelectCommand(comm);
    return GenericDataAccess.ExecuteSelectCmd1(comm);
}

public static DataTable getorderitems_package_user(int orderid,string username)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "sp_proc_getorderitemdetail_pkg_email";
    // create a new parameter
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
    // execute the stored procedure
    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static DataTable Getamtbygvid_giftvoucher(decimal Id)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_proc_getgvamtbygvid";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@gvid";
    param.Value = Id;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);
    return GenericDataAccess.ExecuteSelectCommand(comm);

}

public static DataTable bindrepeater(int page, int pagesize)
	{
	DbCommand comm = GenericDataAccess.CreateCommand();
	
	comm.CommandText="subscri_pagination";
	DbParameter param = comm.CreateParameter();
	param.ParameterName="@pg";
	param.Value=page;
	param.DbType = DbType.Int32;
	comm.Parameters.Add(param);

	param = comm.CreateParameter();
	param.ParameterName = "@pgSize";
	param.Value = pagesize;
	param.DbType = DbType.Int32;
	comm.Parameters.Add(param);
	return GenericDataAccess.ExecuteSelectCommand(comm);
	}

public static DataTable orderdetailsbyorderid(decimal orderid, string username)
{
    DbCommand command = GenericDataAccess.CreateCommand();
    command.CommandText = "sp_proc_customer_orderdetailsbyorderid";
    DbParameter param = command.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = orderid;
    param.DbType = DbType.Decimal;
    command.Parameters.Add(param);

    param = command.CreateParameter();
    param.ParameterName = "@username";
    param.Value = username;
    param.DbType = DbType.String;
    command.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(command);
}
public static DataTable Getcompanion()
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "sp_proc_Getcompanion";
    // execute the stored procedure and return the results
    // return GenericDataAccess.ExecuteSelectCommand(comm);
    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static DataTable getpromoamt(int Id)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_proc_getpromoamt";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@orderid";
    param.Value = Id;
    param.DbType = DbType.Int32;
    comm.Parameters.Add(param);
    return GenericDataAccess.ExecuteSelectCommand(comm);

}

}