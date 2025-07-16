using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

/// <summary>
/// Summary description for clscheckout
/// </summary>
public class clscheckout
{
	public clscheckout()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable get_filldropdown_contry()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_filldropdown_contry";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable get_giftvoucher(string UserName, string gvcode, string gvpassword, int amt)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_proc_giftvoucher";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvcode";
        param.Value = gvcode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvpassword";
        param.Value = gvpassword;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@amt";
        param.Value = amt;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet get_shippingaddress_getuserdetails(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_shippingaddress_getuserdetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
      
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }

    public static DataSet get_shippingaddress_from_id(int indexid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_shippingaddress_from_indexid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }

    public static DataTable getorderdetails(int orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_order_getorderdetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool shippingaddress_insertupdateuserinfo(string user_emailid, string firstname, string lastname, string address, string mobileno, int countryid, int stateid, int cityid, string pincode, string citycode, string telno,string landmark)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_shippingaddress_insertupdateuserinfo";

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@landmark";
        param.Value = landmark;
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

    public static DataTable gettopshippingaddress()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_gettopshippingaddress";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool deleteshippingaddress(int indexid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteshippingaddress";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
        }
        return (result >= 1);

    }

    public static DataSet get_addressbook_getuserdetailsbyusername(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_addressbook_getuserdetailsbyusername";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }

    public static DataSet addressbook_from_indexid(int indexid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_addressbook_from_indexid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }



}