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
/// Summary description for classuserdetails
/// </summary>
public class classuserdetails
{
	public classuserdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet get_wishlist_from_username(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_wish_list_username";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    public static bool updatecount()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updatecounts";
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
    public static DataTable get_counts()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcounts";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable getalluserdetails(string ostatus, string fname, string lname, string email, string approved, int PageIndex, int PageSize, out int RecordCount)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getalluserbymembership";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ostatus";
        param.Value = ostatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@approved";
        param.Value = approved;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

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
        param.Value = 0;
        param.DbType = DbType.Int32;
        param.Direction = ParameterDirection.Output;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);

        return dt;
    }
}
