using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Data;
using System.Data.Common;
/// <summary>
/// Summary description for settingmanager
/// </summary>
public class settingmanager
{
	public settingmanager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable GetSettingValue(string name,string m_flag)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_setting_manager";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@m_flag";
        param.Value = m_flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}