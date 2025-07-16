using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Configuration;

/// <summary>
/// Summary description for classcontact
/// </summary>
public class classcontact
{
	public classcontact()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static bool createcontact(string username, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemycontact";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        //result will represent the number of changed rows
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

    public static bool updatecontact(int id,string username, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemycontact";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        //result will represent the number of changed rows
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

    public static DataTable getstarcontact(string user)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcontactforuser";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = user;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getstarcontact1(string username, string fname, string lname, string email, string mob, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcontactforuser1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

    public static DataTable getstarcontactFullsearch(string username, string fname, string lname, string email, string mob, string loc, string dept, string subdept, string desg, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getstarcontactforfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@loc";
        param.Value = loc;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@subdept";
        param.Value = subdept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }



    //public static DataTable getstarcontactFullsearch(string username, int PageIndex, int PageSize, out int RecordCount)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_getstarcontactforfullsearch";
    //    // create a new parameter
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@username";
    //    param.Value = username;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);
    //    param = comm.CreateParameter();
    //    param.ParameterName = "@PageIndex";
    //    param.Value = PageIndex;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@PageSize";
    //    param.Value = PageSize;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@RecordCount";
    //    param.Value = PageSize;
    //    param.DbType = DbType.Int32;
    //    param.Direction = ParameterDirection.Output;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
    //    return dt;

    //}

        // create a new parameter
    

    public static DataTable getothercontact(string user)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcontactforuser";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = user;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getothercontact1(string username, string fname, string lname, string email, string mob, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallcontactforuser1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param); 
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param); 
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

    public static DataTable getAllcontactFullsearch(string username, string fname, string lname, string email, string mob, string loc, string dept, string subdept , string desg, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallcontactforfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@loc";
        param.Value = loc;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@subdept";
        param.Value = subdept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

    //Jayesh try below code to bind new SP to contacts.aspx page 9dec2017
    public static DataTable getAllcontactFullsearch1(string fname, string lname, string emailid, string dept, string desg, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallcontacts";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@username";
        //param.Value = username;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fname";
        param.Value = fname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lname";
        param.Value = lname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@mobileno";
        //param.Value = mob;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        //// create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@loc";
        //param.Value = loc;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@department";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@subdept";
        //param.Value = subdept;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@designation";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }
    //Jayesh try above code to bind new Sp to contacts.aspx page 9dec2017
    public static DataTable deletestarcontact(string user)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_deletestarcontact";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = user;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable deletestarcontact1(string user)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcontactforuser";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = user;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable seacrhcontact(string user)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_searchcontact";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = user;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    // addition contact classes

    public static DataTable allcontactlabel()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_allcontactlbl";
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static bool createmycontact(int clid,string username,string contactdetails,string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertcontact_user";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@clid";
        param.Value = clid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@contacts";
        param.Value = contactdetails;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        //result will represent the number of changed rows
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

}