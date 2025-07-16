using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classdonate
/// </summary>
public struct donate
{
}
public class classdonate
{
	public classdonate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int adddonation(decimal userid, decimal directorid, string status, string description, decimal amount, string useremail)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_insertdonation";
       // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        //create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@directorid";
        param.Value = directorid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@amount";
        param.Value = amount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@outorderid";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int outorderid = 0;
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
            outorderid = Int32.Parse(comm.Parameters["@outorderid"].Value.ToString());
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return outorderid;
    }



    public static int adddonationtracking(decimal donateid,decimal userid, string status, string donation_paymode, decimal amount,string donationdate, string useremail,decimal directid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_insertdonationtracking";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@donationid";
        param.Value = donateid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        //create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@donation_paymode";
        param.Value = donation_paymode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@amount";
        param.Value = amount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@donationdate";
        param.Value = donationdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@directorid";
        param.Value = directid;
        param.DbType = DbType.Decimal;
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
        return result;
    }


    public static DataSet getcustdetails_donationid(int donationorderid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_orderitem_getcustdetails_donations";
        //command.CommandText = "sp_proc_orderitem_getcustdetails_donationorder";
        DbParameter param = command.CreateParameter();
        param.ParameterName ="@donationorderid";
        param.Value = donationorderid;
        param.DbType = DbType.Int32;
        command.Parameters.Add(param);

       
        return GenericDataAccess.ExecuteSelectCmd1(command);

    }

    public static DataTable getamountdetailsbydonationorderid(string emailid, int orderid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText ="sp_proc_orderitem_getamountbydonatiomorderid";
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


    public static DataTable getuserdetails_donationid(int donationorderid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_orderitem_getcustdetails_donationorder";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@donationorderid";
        param.Value = donationorderid;
        param.DbType = DbType.Int32;
        command.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(command);

    }

    public static DataTable getdonationorderdetails_donationid(string emailid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_donation_order_orderid";
        //command.CommandText = "sp_proc_donation_order_orderemail";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        command.Parameters.Add(param);

        //param = command.CreateParameter();
        //param.ParameterName = "@donationoid";
        //param.Value = orderid;
        //param.DbType = DbType.Decimal;
        //command.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(command);
    }

    public static DataTable updatedonationorderstatus(decimal orderid, string orderstatus)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_updatedonationorderstatus";
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

    public static DataTable getuserdetails_donationid_director(int donationorderid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
       // command.CommandText = "sp_proc_orderitem_getcustdetails_donationorder_director";
        command.CommandText = "sp_proc_orderitem_getcustdetails_donation";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@donationorderid";
        param.Value = donationorderid;
        param.DbType = DbType.Int32;
        command.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(command);

    }
}