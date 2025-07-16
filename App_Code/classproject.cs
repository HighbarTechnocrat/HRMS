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
/// Summary description for classproject
/// </summary>
public class classproject
{
	public classproject()
	{
		//
		// TODO: Add constructor logic here
		//

        

	}

    public static DataTable getsingleprojectuser(int projectid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_ProjectsSelectSingleItem";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //Excute store procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
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
    public static DataTable getuserimage1(int projectid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallprojectimages";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //Excute store procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    //Jayesh_Sagar below new code  for multiple images tobe displayed on projectdetail page 24nov2017
    public static DataTable getallprojectimages(int projectid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallprojectimages";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@projectid";
        param.Value = projectid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //Excute store procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    //Jayesh_Sagar Above new code  for multiple images tobe displayed on projectdetail page 24nov2017

    //Jayesh added and commented below code 28nov2017
    //public static DataTable getallProjectfullsearch( string title, out int RecordCount)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    // comm.CommandText = "usp_SearchProducts";
    //    comm.CommandText = "sp_proc_searchprojects";
    //    DbParameter param = comm.CreateParameter();

    //    //param = comm.CreateParameter();
    //    //param.ParameterName = "@pid";
    //    //param.Value = projectid;
    //    //param.DbType = DbType.String;
    //    //comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@p_title";
    //    param.Value = title;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
    //    return dt;
    //}
    public static DataTable search_clientproject_details(string projecttitle, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // comm.CommandText = "usp_SearchProducts";
        comm.CommandText = "sp_proc_search_client_projects";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@projecttitle";
        param.Value = projecttitle;
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
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        param.Direction = ParameterDirection.Output;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

    //Jayesh added and commented above code 28nov2017
    public static DataTable getallprojectcatlists()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallprojectcatlist";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    
    public static DataTable getallprojectstatuslists()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallprojectstatus";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }


    public static DataTable getmeetings()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "getpost_by_user";
        // create a new parameter
     
        //Excute store procedure
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        return dt;
    }
  
    public static DataTable task(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // comm.CommandText = "usp_SearchProducts";
        comm.CommandText = "sp_proc_getpost_by_user_task";
        DbParameter param = comm.CreateParameter();

        

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    //Jayesh try below code for project page search box 30nov2017
    //public static Datatable projectsearch(string projecttitle, int PageIndex, int PageSize, out int RecordCount)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_getallprojectfullsearch";
    //    // create a new parameter
    //    DbParameter param = comm.CreateParameter();
    //    param = comm.CreateParameter();
    //    param.ParameterName = "@PageIndex";
    //    param.Value = PageIndex;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = "@projecttitle";
    //    param.Value = projecttitle;
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

    //    //DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
    //    //RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
    //    //return dt;
    //}
    //public static DataSet projectdetails2(int PageIndex, int PageSize, out int RecordCount)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_projectdetails2";
    //    // create a new parameter
    //    DbParameter param = comm.CreateParameter();
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

    //    DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
    //    RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
    //    return dt;
    //}


    public static DataSet projectsearch(string projectstatusid,string projectcatid,string projecttitle, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallprojectfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projecttitle";
        param.Value = projecttitle;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectcatid";
        param.Value = projectcatid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectstatusid";
        param.Value = projectstatusid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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


//sagar added below code for ongoing sp 20dec2017

    public static DataSet ongoingprojectsearch( string projectcatid, string projecttitle, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallprojectongoingfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projecttitle";
        param.Value = projecttitle;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectcatid";
        param.Value = projectcatid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@projectstatusid";
        //param.Value = projectstatusid;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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




    //sagar added below code for completed sp 20dec2017

    public static DataSet completedprojectsearch(string projectcatid, string projecttitle, int PageIndex, int PageSize, out int RecordCount, string projectstatusid, string projectstate, string projectyear)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallprojectcompletedfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projecttitle";
        param.Value = projecttitle;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectcatid";
        param.Value = projectcatid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectstatusid";
        param.Value = projectstatusid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectstates";
        param.Value = Convert.ToString(projectstate).Trim();
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@projectyear";
        param.Value = Convert.ToString(projectyear).Trim();
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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



    //public static DataSet projectdetails3(int PageIndex, int PageSize, out int RecordCount)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_getallprojectfullsearch";
    //    // create a new parameter

    //    DbParameter param = comm.CreateParameter();
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

    //    DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
    //    RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
    //    return dt;
    //}
    //Jayesh try above code for project page search box 30nov2017


    public static DataTable getallprojectstateList()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallprojectStates";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
}

