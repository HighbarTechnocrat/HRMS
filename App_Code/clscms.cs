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
/// Summary description for clscms
/// </summary>
public class clscms
{
	public clscms()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable searchnewsbyname(string name)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_CMS_searchcmsbyname";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@search";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable searchPSPDbyname(string name, string flag)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_PSPD_searchcmsbyname";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@search";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getparentlinkdetails(int parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_link_getparentdetails";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@parentid";
        paramp.Value = parentid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


}