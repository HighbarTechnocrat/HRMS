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

public struct attributegroup
{
    public string attributegroupname;
    public char status;
    public string Condition;
}
/// <summary>
/// Summary description for classattributegroup
/// </summary>
public class classattributegroup
{
	public classattributegroup()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet GetAttributegroup()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributeGroup";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        DataSet Ds = GenericDataAccess.ExecuteSelectCmd(comm);
        
        //return table;

        int intcount = Ds.Tables[0].Rows.Count;
        return Ds;
    }
    
    public static bool deleteattributegroup(int attrigroupid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AttributeGroupdDelete";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributgroupeid";
        param.Value = attrigroupid;
        param.DbType = DbType.Int32;
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
        return (result != -1);
    }
    public static attributegroup GetattributegroupDetails(int attributegroupid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributeGroupDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributegroupid";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
       attributegroup details = new attributegroup();
        if (table.Rows.Count > 0)
        {
            details.attributegroupname = table.Rows[0]["Name"].ToString();
            details.status =Convert.ToChar(table.Rows[0]["status"]);
            details.Condition = Convert.ToString(table.Rows[0]["Condition"]);
        }
        // return department details
        return details;
    }
    public static bool  createattributegroup(string attributegroupname,char status,string AndOr)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddAttributeGroup";
        DbParameter param = comm.CreateParameter();
        // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@status";
          param.Value = status;       
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@attributegroupname";
        param.Value = attributegroupname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@AndOr";
        param.Value = AndOr;
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
    public static bool updateattributegroup(int attributegroupid, string attributename,char status,string AndOr)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddAttributegroup";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributgroupeid";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
       
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@attributegroupname";
        param.Value = attributename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@AndOr";
        param.Value = AndOr;
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
        return (result != -1);
    }
    public static DataTable selectattributegroup()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "Sp_proc_SelectAttributeGroup";
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static bool insertattrgrpInAttributeGroup(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "Sp_proc_InsertAttributeGroup";

        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
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
        return (result != -1);
    }
    public static int deleteattrgrpInAttributeGroup(int AttributeGroupID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "Sp_proc_DeleteAttributegroup";


        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = 0;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
        }
        return result;

    }

    public static int deleteAttributefromCategoryAttribute(int CategoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteAttributefromCategoryAttribute";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        int result = 0;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);

        }
        catch
        {
        }
        return result;
    }

    public static bool insertAttributeGrpInCategoryAttribute(int CategoryId, int AttributeGroupId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_insertAttributesInCategoryAttribute";

        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = AttributeGroupId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@AttributeId";
        param.Value = DBNull.Value;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
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
    
     public static DataSet GetAttributeGroupChecker(string Name,int attributegroupid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetAttributeGroupChecker";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributegroupId";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    public static DataSet GetAttributeGroupSearch(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetAttributeGroupSearch";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    public static DataTable getcaregoryattributegroups(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getcaregoryattributegroups";      
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getcaregoryattributegroupsimges(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getcaregoryattributegroupsimges";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getcaregoryattributegroupsBox(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getcaregoryattributegroupsBox";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getcaregoryattributegroupsProduct(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        //comm.CommandText = "usp_getAttributeGroup";
        comm.CommandText = "usp_getAttributeGroups";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getattributecolorgroupsProduct(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getAttributeGroupColor";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable usp_getAttributeMappingCat(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getAttributeMappingCat";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    //method on 23 sept
    public static DataTable getattributegrbyattrid(string attributeid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getattrigrbyattributeid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable getop2attributegroup()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_gettop2attrgr";
        //  comm.CommandText = "sp_proc_gettopattrgr";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getattributeidbyattrgrid(string prdid, string attrgpid ,string strpath)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "getattributecommabyattrgrid";
       // comm.CommandText = "getattributecommabygrid";
       
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = prdid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributegrid";
        param.Value = attrgpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@strpath";
        param.Value = strpath;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable geattributegroup_prodid(decimal productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_geattrgr_productid";
        
        // create a new parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

}
