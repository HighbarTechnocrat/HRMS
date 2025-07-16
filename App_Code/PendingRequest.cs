using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PendingRequest
/// </summary>
public static class PendingRequest
{
    public static int get_Pending_LeaveReqstList_cnt(string emp_code)
    {
        SP_Methods spm = new SP_Methods();

        int Leave_Cnt = 0;
        try
        {
            DataSet dsTrDetails = new DataSet();
            System.Data.SqlClient.SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new System.Data.SqlClient.SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_inboxlst_cnt_HR";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Leave_Cnt = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["leave_reqst_pending"]);
            }

        }
        catch (Exception ex)
        {

        }
        return Leave_Cnt;

    }


    public static int get_KRA_NotAccepted_Count(string emp_code)
    {
        SP_Methods spm = new SP_Methods();

        int PendingCnt = 0;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_KRANotAccepted_Cnt";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                PendingCnt = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["KRANotAccept_Cnt"]);
            }

        }
        catch (Exception ex)
        {

        }
        return PendingCnt;

    }
}