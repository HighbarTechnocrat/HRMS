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
/// Summary description for classfollower
/// </summary>
public class classfollower
{
	public classfollower()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static DataTable insertfollowdetailsbyusername(string followusername, string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText ="sp_proc_insert_delete_followers";
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@followerid";
        param.Value = followusername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getcountfollowbyusername(string followusername, string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_followercount";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@followeremail";
        param.Value = followusername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable followeduserdetail(int fid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_get_followedDeatils";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@fid";
        param.Value = fid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

}