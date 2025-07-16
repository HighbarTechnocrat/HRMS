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
using System.Data.Common;


/// <summary>
/// Summary description for classinvite
/// </summary>
public class classinvite
{
	public classinvite()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static DataTable getinvitepoint()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getinvitepts";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable getgiftvoucherstatus_username(string username)
     {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        //  comm.CommandText = "sp_get_assignpoints";
        comm.CommandText = "get_giftvoucherstatusbyusername";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
      }


    public static DataTable getuserdetailsbyemail(string emailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_getalluserbyemail";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}