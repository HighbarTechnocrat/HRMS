using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Components_Common_homeglobal : System.Web.UI.UserControl
{
    SqlConnection source;
    SP_Methods spm = new SP_Methods();
    public Int32 Claims_Cnt = 0;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    public void PopulateControl()
    {
        //SAGAR ADDED BELOW LINE FOR ADDING USERNAME TO CONTENT PAGE 23OCT2017
        DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);

        //Control userbirth = LoadControl(String.Format("../../Themes/{0}/LayoutControls/birthdays.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        //uxbirth.ID = "m_uxuserbirth";
        //uxbirth.Controls.Add(userbirth);

//  commented by sony to stop display of GROUPS widget on home page
        //Control usergroups = LoadControl(String.Format("../../Themes/{0}/LayoutControls/groups.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        //uxgroups.ID = "m_uxusergroups";
        //uxgroups.Controls.Add(usergroups);

//  commented by sony to stop display of WEATHER widget on home page     
        //Control userwhether = LoadControl(String.Format("../../Themes/{0}/LayoutControls/whether.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        //uxwhether.ID = "m_uxuserwhether";
        //uxwhether.Controls.Add(userwhether);
     
        Control usercatbanner = LoadControl(String.Format("../../Themes/{0}/LayoutControls/catproduct2.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        usercatbanner.ID = "m_uxcategory";
        string proimg = "";
        if (user.Rows.Count > 0)
        {
            //SAGAR ADDED BELOW LINE FOR ADDING USERNAME TO CONTENT PAGE 23OCT2017 
            //Comment lblfname.Text = "Welcome " + user.Rows[0]["firstname"].ToString().Trim() + " " + user.Rows[0]["lastname"].ToString().Trim();
            // Comment lblfname.Text = "Welcome to hrms";
			//lblfname.Text = user.Rows[0]["firstname"].ToString().Trim() + " " + user.Rows[0]["lastname"].ToString().Trim() + " : hrms" ;
            proimg = user.Rows[0]["profilephoto"].ToString().Trim();
            lblfname.Text = user.Rows[0]["firstname"].ToString().Trim() + " " + user.Rows[0]["lastname"].ToString().Trim()  + " - Welcome to oneHR" ;
             //comment by sanjay on 18.12.2024
	   // user_profile_image.Src = ReturnUrl("sitepath") + "images/profile110x110/" + proimg;
 		string version = DateTime.Now.Ticks.ToString(); // Unique value
 		user_profile_image.Src = ResolveUrl(ReturnUrl("sitepath") + "images/profile110x110/" + proimg) + "?v=" + version;
        }
        uxcategorypanel.Controls.Add(usercatbanner);
        ////30-10-2020
        ////string emp_code = "";
        ////if (Convert.ToString(Session["Empcode"]).Trim() != "" && Session["Empcode"] != null)
        ////{
        ////    emp_code = Convert.ToString(Session["Empcode"]).Trim();
        ////    Mobile_Approver_Count(emp_code);
        ////    check_COS_ACC("RCOS", emp_code);
        ////    check_COS_ACC("RACC", emp_code);
        ////    check_COS_ACC("RCFO", emp_code);

        ////    Fuel_Approver_Count(emp_code);
        ////    check_COS_ACC_Fuel("RCOS", emp_code);
        ////    check_COS_ACC_Fuel("RACC", emp_code);
        ////    check_COS_ACC_Fuel("RCFO", emp_code);

        ////    Payment_Approver_Count(emp_code);
        ////    check_COS_ACC_Payment("RCOS", emp_code);
        ////    check_COS_ACC_Payment("RACC", emp_code);
        ////    check_COS_ACC_Payment("RCFO", emp_code);
        ////    if (Claims_Cnt > 0)
        ////        ClaimsHead.InnerText = "Claims (" + Convert.ToString(Claims_Cnt) + ")";
        ////    else
        ////        ClaimsHead.InnerText = "Claims";
        ////}
        ////30-10-2020

        //  commented by sony to stop display of MYWALL widget on home page   
        //Control userwall = LoadControl(String.Format("../../Themes/{0}/LayoutControls/mywall.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        //uxwall.ID = "m_uxmywall";
        //uxwall.Controls.Add(userwall);
    }

    public void Mobile_Approver_Count(string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Mobile_ACC_COS_Count(strtype, emp_code);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Mobile_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");



            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
                if (strtype == "RCFO")
                {
                    Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }

            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Fuel_Approver_Count(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[1].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC_Fuel(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Fuel_ACC_COS_Count(strtype, emp_code);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Fuel_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Fuel_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {

                if (strtype == "RCOS")
                {
                    Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
                if (strtype == "RACC")
                {
                    Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Payment_Approver_Count(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            //Mobile Claim Request Count
            if (dsTrDetails.Tables[2].Rows.Count > 0)
            {
                Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[2].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC_Payment(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Payment_ACC_COS_Count(strtype, emp_code);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Payment_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Payment_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");



            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    Claims_Cnt = Claims_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void Page_init(object sender, EventArgs e)
    {
        DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
      
       // DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
        //try
        //{
       
      PopulateControl();
        //}
        //catch (Exception ex)
        //{
        //    //ErrorLog.WriteError(ex.ToString());
        //}
    }
}