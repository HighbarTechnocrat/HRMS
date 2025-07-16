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
using System.Xml;
using System.Collections.Generic;

public struct attribute
{
    public int attributegroupid;
    public string name;
    public int productid;
    public int AttributeId;
    public string smaillImage;
    
    //public string code;
}
/// <summary>
/// Summary description for classattribute
/// </summary>
public class classattribute
{
	public classattribute()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet Getattributes()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributes";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataSet Getattributeproduct()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributeProduct";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static int insertAttributeIdinPro_Att(int productid, int AttributeId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_insertAttributeId";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeId";
        param.Value = AttributeId;
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
    public static DataTable selectAttNamefromAttributes(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText ="sp_proc_selectAttNamefromAttributes";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //usp_SearchProducts
  

    public static DataTable getattributeId(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getattributeid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable selectAttIdfromProduct_Attribute(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectAttIdfromProduct_Attribute";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static int deleteAttidfromProduct_Attribute(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteAttidfromProduct_Attribute";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
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


    public static bool deleteAttidfromProduct_AttributeByPID(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteAttidfromProduct_Attribute";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
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
    public static bool deleteattributes(int attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AttributesDelete";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value =attributeid;
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
    public static attribute GetattributeDetails(int attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributeDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
      attribute details = new attribute();
     
        if (table.Rows.Count > 0)
        {
            details.attributegroupid = Convert.ToInt32(table.Rows[0]["AttributeGroupId"]);
            details.name = table.Rows[0]["Name"].ToString();
            if (table.Rows[0]["smallimage"] != DBNull.Value)
            {
                details.smaillImage = table.Rows[0]["smallimage"].ToString();
            }
            else
            {
                details.smaillImage = "noimage2.gif";
             }
            
        }
        // return shop details
        return details;
    }
    public static bool createattributes(int attributegroupid, string name, string smallimage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddAttributes";

        DbParameter param = comm.CreateParameter();
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@attributegroupid";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@name";
        param.Value = name;       
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         param = comm.CreateParameter();
         param.ParameterName = "@smallimage";
         param.Value = smallimage;
         param.DbType = DbType.String;
         comm.Parameters.Add(param);

        
         //// create a new parameter
         //param = comm.CreateParameter();
         //param.ParameterName = "@addedby";
         //param.Value = addedby;
         //param.DbType = DbType.String;
         //comm.Parameters.Add(param);

        

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
    public static bool updateattribute(int attributeid, int attributegroupid, string name, string smallimage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddAttributes";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@attributegroupid ";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
      
        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@smallimage";
        param.Value = smallimage;
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
    public static DataSet Getnewattributes(int attributegroupid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetNewAttributes";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributegroupid";
        param.Value = attributegroupid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataTable relatedattributelist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_relatedattributelist";
        // create a new parameter
        //DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@DeviceModelId";
        //param.Value = DeviceModelId;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable selectattributgroupid()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectattributegroupid";
        return GenericDataAccess.ExecuteSelectCommand(comm);
 
    }

    public static DataTable getattributegroupid(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getattributegroupid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);


    }

    public static DataTable getattributegroupStatus(int AttributeGroupID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectattributegroupstatus";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AttributeId";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);


    }

    public static DataTable selectattributegroupname(int AttributeGroupID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectattributegroupname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static bool insertAttributesInCategoryAttribute(int CategoryId, int AttributeId)
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
        param.Value = DBNull.Value;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@AttributeId";
        param.Value = AttributeId;
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

    public static bool insertAttributesInCategoryAttributes(int CategoryId,int AttributeId,int attributegrid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_insertAttributesInCategoryAttribute";

        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param );

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = AttributeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@AttributeId";
        param.Value = attributegrid;
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
    public static int deleteAttributefromCategoryAttribute(int CategoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText="sp_proc_deleteAttributefromCategoryAttribute";
        DbParameter param =comm.CreateParameter();
        param.ParameterName= "@CategoryId";
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
        return result ;

    }

    

    public static DataTable selectAttributetoEditFromCategoryAttribute(int CategoryId )
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText= "sp_proc_selectAttributetoEditFromCategoryAttribute";
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable selectAttributeIdPareGroupIdfromAttributes(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectAttributeIdPareGroupIdfromAttributes";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool updateAttribute(int CategoryId, int AttributeGroupId,int AttributeId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_updateAttribute";

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
        param.Value = AttributeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result=-1;
        try
        {
            result=GenericDataAccess.ExecuteNonQuery(comm );

        }
        catch 
        {
        }
        return (result != -1);
    }
    public static DataTable selectattributename()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectAttriduteName";
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static int  deleteAttribute(int Category_AttributeId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_proc_deleteAttribute";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Category_AttributeId";
        param.Value = Category_AttributeId;
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
    public static DataTable selectattribute()
    {
         DbCommand comm = GenericDataAccess.CreateCommand();
         comm.CommandText = "proc_SelectAttributes";
         return GenericDataAccess.ExecuteSelectCommand(comm);

    }




    public static DataTable getAttributegrpdetailCatAttributeByCatID(int CatID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getAttributegrpdetailCatAttributeByCatID";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = CatID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getAttributeDetailByProductID(int productid)
    {
        DbCommand comm= GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllAttributeByproductId1";

        // create a new parameter
        DbParameter paramz = comm.CreateParameter();
        paramz.ParameterName = "@productid";
        paramz.Value = productid;
        paramz.DbType = DbType.Int32;
        comm.Parameters.Add(paramz);

        // execute the stored procedure
   return GenericDataAccess.ExecuteSelectCommand(comm);

       // return dtc;
    }
    public static DataSet Getattributesearch(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_Getattributesearch";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }

    public static DataTable getattributes(int CategoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getattributesbycategoryid ";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getattributesid(int CategoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getattributesbycategoryids";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getattributegroup(int CategoryId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getatrgrBycategoryid ";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getattribute(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getattrigr";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
    
        return table;

    }

    public static DataTable getcatattribute(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetCatattrigr";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getAttributeByAttributeGroupIdandcatid(int categoryid, int AttributeGroupId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAttributeByAttributeGroupIdandcatid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = AttributeGroupId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable getcolorattributes()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getcolorattributes";
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static string getAttrColorName(string HexCode)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_colorNamefromHex";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@hexCode";
        param.Value = HexCode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        return GenericDataAccess.ExecuteScalar(comm);
        // return the page of products

    }
    
    public static string[] GetProductList(String prefixText)
    {
        XmlDocument doc = new XmlDocument();
        string path = HttpContext.Current.Server.
        MapPath("~/xml/tagcloud.xml");
        doc.Load(path);
        XmlNodeList footballerData = doc.GetElementsByTagName("NewDataSet");

        List<string> footballers = new List<string>();
        foreach (XmlNode footballer in footballerData)
        {
            string footballerName = footballer.Attributes["keywordname"].Value;

            if (footballerName.ToLower().StartsWith(prefixText.ToLower()))
                footballers.Add(footballerName);

            // if (footballers.Count >= count)
            //   break;
        }

        return footballers.ToArray();
    }

    public static DataTable getattributerelatedtoproductbygAttrid(int productid, int AttributeGroupId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getattributerelatedtoproductbygAttrid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = AttributeGroupId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

      

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable getattributeFromColorHexTable()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getattributeFromColorHexTable";
        // create a new parameter
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable  countForOrderSpCust(string  UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_countForOrderSpCust";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable selectattributenameformulticolor(int AttributeGroupID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectattributenameformulticolor";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);


    }

    public static DataTable selectattributenameformulticolor1()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectattributenameformulticolor1";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getgroupattributeid(string Name, string attgpid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getgroupattributeid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = attgpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable get_sp_proc_recordfindinMapAttr(string StrAttribute)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_recordfindinMapAttr";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AttributeID";
        param.Value = StrAttribute;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable Getallattributes()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetAttributes";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //method on 15th may for variant
    public static DataTable Getattributesidbyprdid_variant(string strprdid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getmultiattributeid";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pProductId";
        param.Value = strprdid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //method on 23sept 
    public static DataTable getattributenamebyid(string strattrid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getattributenamebyid";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attridId";
        param.Value = strattrid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable getmetattributedetails(string attrid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmetaattributesdetail";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attrid";
        param.Value = attrid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    //method on sept 23 2014 for display menu url
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

}



