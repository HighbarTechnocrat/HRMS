using System;
using System.Data;
using System.Data.Common;
using System.Web.Security;



/// <summary>
/// Summary description for classcity
/// </summary>
public class classrolemodule
{
    public classrolemodule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet getallmodules()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallmodules";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataSet getparentmodules()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentmodules";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataTable getchildmodules(string moduleid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getchildmodules";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@module_id";
        paramp.Value = moduleid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getchildmodulesbyparentid(string moduleid,string parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getchildmodulesbyparentid";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@module_id";
        paramp.Value = moduleid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        // create a new parameter
        paramp = comm.CreateParameter();
        paramp.ParameterName = "@parentid";
        paramp.Value = parentid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getrolemodulesnamebyid(int moduleid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrolemodulesnamebyid";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@module_id";
        paramp.Value = moduleid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);

      
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getrolemodulesname(string role, int parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrolemodulesname";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@role_id";
        paramp.Value = role;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        // create a new parameter
        paramp = comm.CreateParameter();
        paramp.ParameterName = "@parent_id";
        paramp.Value = parentid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getrolemodulesnamebyrole1(string role)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrolemodulesnamebyrole1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@role_id";
        paramp.Value = role;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getrolemodulesnamebyrole(string role)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrolemodulesnamebyrole";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@role_id";
        paramp.Value = role;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
      
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static string getUserPriorityRole(string username)
    {
        string[] UserRoles = Roles.GetRolesForUser(username);
        string UserPriorityRole = "";

        foreach (string Role in UserRoles)
        {


            if (UserPriorityRole == "")
            {
                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

                if (Role == "GeneralAdmin")
                {
                    UserPriorityRole = "GeneralAdmin";

                }


                if (Role == "Vendor")
                {
                    UserPriorityRole = "Vendor";

                }

                if (Role == "Operation")
                {
                    UserPriorityRole = "Operation";

                }
                if (Role == "BannerManager")
                {
                    UserPriorityRole = "BannerManager";

                }
                if (Role == "CatlogManager")
                {
                    UserPriorityRole = "CatlogManager";

                }
                if (Role == "OrderSupport")
                {
                    UserPriorityRole = "OrderSupport";

                }
                if (Role == "Customer")
                {
                    UserPriorityRole = "Customer";

                }
               
                if (Role == "SEO")
                {
                    UserPriorityRole = "SEO";

                }
                if (Role == "Sales")
                {
                    UserPriorityRole = "Sales";

                }

            }
            if (UserPriorityRole == "Administrator")
            {
                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

            }
            if (UserPriorityRole == "Supervisor")
            {
                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

            }

            if (UserPriorityRole == "GeneralAdmin")
            {
                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

                if (Role == "Supervisor")
                {
                    UserPriorityRole = "Supervisor";

                }

            }

            if (UserPriorityRole == "Vendor")
            {

                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }
            }
            if (UserPriorityRole == "SEO")
            {

                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }
            }
            if (Role == "BannerManager")
            {

                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

            }
            if (Role == "CatlogManager")
            {

                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

            }
            if (Role == "OrderSupport")
            {

                if (Role == "Administrator")
                {
                    UserPriorityRole = "Administrator";

                }

            }



        }//End of  foreach (string Role in UserRoles)

        return UserPriorityRole;
    }
    public static DataTable getrolepagename(string role, string pgname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrolepagename";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@role_id";
        paramp.Value = role;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        // create a new parameter
        paramp = comm.CreateParameter();
        paramp.ParameterName = "@pagename";
        paramp.Value = pgname;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static bool deleteRolemodule(string module_id)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteRolemodule";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@module_id";
        param.Value = module_id;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


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
        return (result != -1);
    }
    public static bool deleteRolemodulebyroleid(string Role_id)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteRolemodulebyroleid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@roleid";
        param.Value = Role_id;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        int result = -1;
       // try
       // {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
       // catch
       // {
            // any errors are logged in GenericDataAccess, we ingore them here
      //  }
        // result will be 1 in case of success 
        return (result != -1);
    }
    public static bool createrolemodule(int moduleid, string roleid, bool condition)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdrolemodule";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@module_id";
        param.Value = moduleid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@role_id";
        param.Value = roleid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@condition";
        param.Value = condition;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

      
        int result = -1;
       // try
       // {
            result = GenericDataAccess.ExecuteNonQuery(comm);
       // }
      //  catch
       // {
       // }
        return (result >= 1);
    }

    public static DataTable getmember(string emailid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectmember";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@email";
        paramp.Value = emailid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getusername(string useremail)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectuser";
        // create a new parameter
        DbParameter paramp1 = comm.CreateParameter();
        paramp1.ParameterName = "@username_emailid";
        paramp1.Value = useremail;
        paramp1.DbType = DbType.String;
        comm.Parameters.Add(paramp1);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getmemberuser(string emailid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "Sp_proc_Getuseradditionalinfouser  ";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@email";
        paramp.Value = emailid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
}
