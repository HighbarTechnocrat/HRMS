using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Leave_Request_Parameters
/// </summary>
public class Leave_Request_Parameters
{
		  public string Emp_Code { get; set; }
        public string Emp_Status { get; set; }
        public string Emp_Name { get; set; }
        public string Designation_name { get; set; }
        public string department_name { get; set; }
        public string Grade { get; set; }
        public string EmailAddress { get; set; }
        public string Appl_Type { get; set; }
        public int Leave_Type_id { get; set; }
      //  public DateTime   Leave_FromDate { get; set; }
       // public DateTime  Leave_ToDate { get; set; }
	   public string   Leave_FromDate { get; set; }
       public string  Leave_ToDate { get; set; }
        public string Leave_From_for { get; set; }

        public string Leave_To_For { get; set; }
        public int LeaveDays { get; set; }
        public string Reason { get; set; }
        public string FilePath { get; set; }
        public string Approvers_code { get; set; }
        public int appr_id { get; set; }
        public string Intermediate_Levels { get; set; }
        public int Req_id {get;set;}
        public double leave_balance { get; set; }
	
}