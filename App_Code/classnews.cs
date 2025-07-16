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
/// Summary description for classnews
/// </summary>
public class classnews
{
	public classnews()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable topNewsList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topNewsList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable topthoughtList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topthoughtList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable topbdayList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topbdayList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable topgrpList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topgrpList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable grpListbyuser(string user)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_grpListbyuser";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = user;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable newsdetails()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_newsdetails";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet newsdetails1(int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_newsdetails1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@RecordCount";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        param.Direction = ParameterDirection.Output;
        comm.Parameters.Add(param);

        DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

    public static DataTable newsdetailsbyid(int newsid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_newsdetailsbyid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@newsid ";
        param.Value = newsid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable gettopnewslist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topNewsList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getsinglenewsuser(int newsid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_NewsSelectSingleItem";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@newsid";
        param.Value = newsid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //Excute store procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
   

    /*survey */

    // Created by krishna
    public static bool addsurveyuser(int sid, string username, string fullname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_surveyuser_download";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
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
        param.ParameterName = "@fullname";
        param.Value = fullname;
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


    // Created By Krishna 
    public static bool updatesurveyuser(string id, string sid, string username, string fullname, string file)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatesurveyuser";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
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
        param.ParameterName = "@fullname";
        param.Value = fullname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@filename";
        param.Value = file;
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

    public static DataTable getalluserbyemailid(string emailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_byemail";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable updatedownloaduser(int sid,string emailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_checksurveyuser_download";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getdownloaduser(int sid, string emailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_checksurveyuser";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool updatesurveyuserupload(string id, string sid, string username, string fullname, string file)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatesurveyuser";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
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
        param.ParameterName = "@fullname";
        param.Value = fullname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@filename";
        param.Value = file;
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



    public static DataSet projectdetails1(int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_projectdetails1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@RecordCount";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        param.Direction = ParameterDirection.Output;
        comm.Parameters.Add(param);

        DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }
}