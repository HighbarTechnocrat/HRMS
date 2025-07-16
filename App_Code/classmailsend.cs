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
/// Summary description for sendmail
/// </summary>
/// 

public struct maildetails
{
 
    public string username;
    public string usernameto;
    public string title;
}

public class classmailsend
{
	public classmailsend()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool createmessagelog(string username, string usernameto,string title, DateTime expirydate,string description, string fname, string field1,string field2,string field3)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemessagelog";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@usernameto";
        param.Value = usernameto;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@expirydate";
        param.Value = expirydate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

       
        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@field1";
        param.Value = field1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@field2";
        param.Value = field2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@field3";
        param.Value = field3;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
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

    public static bool createmessagelog1(string username, string usernameto, string title, DateTime expirydate, string description, string fname, string field1, string field2,string cfname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemessagelog1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@usernameto";
        param.Value = usernameto;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@expirydate";
        param.Value = expirydate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@field1";
        param.Value = field1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@field2";
        param.Value = field2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@cfname";
        param.Value = cfname;
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




    public static bool UpdateUnreadStatus(int mailID, string Status)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_UpdateUnreadStatus";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mailID";
        param.Value = mailID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        //param = comm.CreateParameter();
        //param.ParameterName = "@Status";
        //param.Value = Status;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Status";
        param.Value = Status;
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







    public static DataTable messageloglist(string usernameto)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
         // set the stored procedure name
        comm.CommandText = "sp_proc_messageloglist";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@usernameto";
        paramp.Value = usernameto;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable messageloglist1(string usernameto)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_messageloglist1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@usernameto";
        paramp.Value = usernameto;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

       
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable msgloglistreceiver(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_msgloglistreceiver";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@username";
        paramp.Value = username;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable msgloglistreceiver1(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_msgloglistreceiver1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@username";
        paramp.Value = username;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static maildetails getmessagelogdetails(int mailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmessagelogdetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mailid";
        param.Value = mailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        maildetails details = new maildetails();
        if (table.Rows.Count > 0)
        {
           
            details.username = table.Rows[0]["username"].ToString();
            details.usernameto = table.Rows[0]["usernameto"].ToString();
          details.title=table.Rows[0]["title"].ToString();
        }
        // return department details
        return details;
    }

 /*   public static DataTable searchmessageloginbox(string search)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessageloginbox ";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
  */ 

    public static DataTable searchmessageloginbox1(string search,string fromdate,string todate,string usernameto)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessageloginbox1";
       // comm.CommandText = "sp_proc_searchmessageloginbox12";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@fromdate";
        paramp.Value = fromdate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@todate";
        paramp.Value = todate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@usernameto";
        paramp.Value = usernameto;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable searchmessageloginbox12(string search, string fromdate, string todate, string usernameto)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessageloginbox12";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@fromdate";
        paramp.Value = fromdate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@todate";
        paramp.Value = todate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@usernameto";
        paramp.Value = usernameto;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable searchmessagelogoutbox1(string search, string fromdate, string todate,string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessagelogoutbox1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@fromdate";
        paramp.Value = fromdate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@todate";
        paramp.Value = todate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@username";
        paramp.Value = username;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable searchmessagelogoutbox12(string search, string fromdate, string todate, string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessagelogoutbox12";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@fromdate";
        paramp.Value = fromdate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@todate";
        paramp.Value = todate;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@username";
        paramp.Value = username;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

 /*   public static DataTable searchmessagelogoutbox(string search)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchmessagelogoutbox ";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@search";
        paramp.Value = search;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
  */

    public static bool deletemessagelog(int mailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletemessagelog";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mailid";
        param.Value = mailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;     
        result = GenericDataAccess.ExecuteNonQuery(comm);   
        return (result != -1);
    }


    public static bool deletemessagelog1(int mailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletemessagelog1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@mailid";
        param.Value = mailid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        result = GenericDataAccess.ExecuteNonQuery(comm);
        return (result != -1);
    }

    public static DataTable getmessagelogdetails1(int mailid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmessagelogdetails ";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@mailid";
        paramp.Value = mailid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
 

}
