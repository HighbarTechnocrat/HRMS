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


public struct Testimonial
{
    public int indexid;
    public string clientname;
    public string emailaddress;
    public string comments;
    public string senddate;
    public string attachment;
    public char status;
    public string createdby;
    public string modifiedby;


}

/// <summary>
/// Summary description for classtestimonial
/// </summary>
public class classtestimonial
{
	public classtestimonial()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool createtestimonial(string clientname, string emailaddress, string comments, DateTime senddate, string attachment, string status, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_testimonialinsert";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@clientname";
        param.Value = clientname;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailaddress";
        param.Value = emailaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@comments";
        param.Value = comments;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@senddate";
        param.Value = senddate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@attachment";
        param.Value = attachment;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
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

    public static DataTable testimonialdetails()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_testimonialdetails";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable toptestimonialdetails()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_toptestimonialdetails";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable testimonialdetailsbyid(int indexid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_testimonialdetailsbyid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

}