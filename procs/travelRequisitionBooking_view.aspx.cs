 
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class procs_travelRequisitionBooking_view : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string StrEmpCode = "";
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    #endregion


    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            hdnLoginEmployee_Code.Value = Convert.ToString(Session["Empcode"]).Trim();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/TravelRequisition_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadBookedTravelTicket"]).Trim()));
                hdnAadharCardPath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()));

                if (!Page.IsPostBack)
                {
                    //
                    get_Locations_Timings(); 

                    if (Request.QueryString.Count > 0)
                    { 
                        hdnTravel_id.Value = Request.QueryString[0];
                        get_MyTravelRequisitions();
                        PopulateEmployeeData();
                        get_Project_Program_Mangers_details();
                         

                    }

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }






    public void PopulateEmployeeData()
    {
        try
        {
            DataTable dtEmp;
            dtEmp = spm.GetEmployeeData(Convert.ToString(txtEmpcode.Text));
           
            if (dtEmp.Rows.Count > 0)
            {
                txtEmpName.Text = Convert.ToString(dtEmp.Rows[0]["Emp_Name"]).Trim();
                txtDesignation.Text = Convert.ToString(dtEmp.Rows[0]["DesginationName"]).Trim();
                txtDepartment.Text = Convert.ToString(dtEmp.Rows[0]["Department_Name"]).Trim();
                band.Text = Convert.ToString(dtEmp.Rows[0]["grade"]).Trim();
                txtEmpName_Aadhar.Text = Convert.ToString(dtEmp.Rows[0]["Name_As_Per_Aadhaar"]).Trim(); 
                txtMobile.Text = Convert.ToString(dtEmp.Rows[0]["mobile"]).Trim();
                hdnEmployee_Email.Value= Convert.ToString(dtEmp.Rows[0]["Emp_EmailAddress"]).Trim();

                // txttravelproject.Text = (string)dtEmp.Rows[0]["emp_projectName"];
                // txtprojectmanger.Text = (string)dtEmp.Rows[0]["Project_Manager"];

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void get_Locations_Timings()
    {
        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getLocation_Times";


        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpcode.Text);


        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
         
        if (dsLocations != null)
        {
            if (dsLocations.Tables[7].Rows.Count > 0)
            {
                hdnAdmin_HODEmailId.Value = Convert.ToString(dsLocations.Tables[7].Rows[0]["HOD_EmailId"]).Trim();

            }
            if (dsLocations.Tables[6].Rows.Count > 0)
            {
                foreach (DataRow dr in dsLocations.Tables[6].Rows)
                {
                    if (Convert.ToString(hdnAdmin_EmailsId.Value).Trim() == "")
                        hdnAdmin_EmailsId.Value = Convert.ToString(dr["Emp_Emailaddress"]).Trim();
                    else
                        hdnAdmin_EmailsId.Value = hdnAdmin_EmailsId.Value + ";" + Convert.ToString(dr["Emp_Emailaddress"]).Trim();
                }
            }

            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                lstPreferTime.DataSource = dsLocations.Tables[0];
                lstPreferTime.DataTextField = "time_name";
                lstPreferTime.DataValueField = "time_id";
                lstPreferTime.DataBind();
                lstPreferTime.Items.Insert(0, new ListItem("Select Prefer Time", "0"));
            }

            if (dsLocations.Tables[1].Rows.Count > 0)
            {
                lstFromLocation.DataSource = dsLocations.Tables[1];
                lstFromLocation.DataTextField = "CITYNAME";
                lstFromLocation.DataValueField = "CITYNAME";
                lstFromLocation.DataBind();

                lstToLocation.DataSource = dsLocations.Tables[1];
                lstToLocation.DataTextField = "CITYNAME";
                lstToLocation.DataValueField = "CITYNAME";
                lstToLocation.DataBind();


                lstFromLocation.Items.Insert(0, new ListItem("Select From Location", "0"));

                lstToLocation.Items.Insert(0, new ListItem("Select To Location", "0"));
            }
            
                if (dsLocations.Tables[2].Rows.Count > 0)
                {
                lstTravelMode.DataSource = dsLocations.Tables[2];
                lstTravelMode.DataTextField = "trip_mode";
                lstTravelMode.DataValueField = "trip_mode_id";
                lstTravelMode.DataBind();

                lsttravelbook.DataSource = dsLocations.Tables[2];
                lsttravelbook.DataTextField = "trip_mode";
                lsttravelbook.DataValueField = "trip_mode_id";
                lsttravelbook.DataBind();

                lstTravelMode.Items.Insert(0, new ListItem("Select  Travel Mode", "0"));

                lsttravelbook.Items.Insert(0, new ListItem("Select Travel Mode", "0"));
                }

            if (dsLocations.Tables[4].Rows.Count > 0)
            {
                lstTravelStatus.DataSource = dsLocations.Tables[4];
                lstTravelStatus.DataTextField = "status_name";
                lstTravelStatus.DataValueField = "status_id";
                lstTravelStatus.DataBind();
                lstTravelStatus.Items.Insert(0, new ListItem("Select Travel Status", "0"));
            } 

        }

    }

    

    public void Validate_from_date()
    {
        string[] strdate;
        string strFrmDate = "";

        if (Convert.ToString(txtFromDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromDate.Text).Trim().Split('-');
            strFrmDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_From_Date";


        spars[1] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[1].Value = strFrmDate;

        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtFromDate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }
            }
        }

    }

    public void validate_To_Date()
    {
        string[] strdate;
        string strToDate = "";
        string strFromDate = "";

        if (Convert.ToString(txttodate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txttodate.Text).Trim().Split('-');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtFromDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromDate.Text).Trim().Split('-');
            strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_To_Date";
         
        spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
        spars[1].Value = strToDate;

        spars[2] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[2].Value = strFromDate;


        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txttodate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }
            }
        }

    }
     
    public void get_Project_Program_Mangers_details()
    {
        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getProject_PM_PRM_details";

        spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
        if (Convert.ToString(txt_travel_project.Text).Contains("HO"))
            spars[1].Value = Convert.ToString(txtDepartment.Text);
        else
            spars[1].Value = Convert.ToString(txt_travel_project.Text);

        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");

        if (dsLocations != null)
        {
            if (Convert.ToString(txt_travel_project.Text).Contains("HO"))
            {
                if (dsLocations.Tables[2].Rows.Count > 0)
                {
                    txtprogrammanger_Name.Text = Convert.ToString(dsLocations.Tables[2].Rows[0]["HOD_Name"]).Trim();
                    txtprogrammanger_Email.Text = Convert.ToString(dsLocations.Tables[2].Rows[0]["HOD_EmailId"]).Trim();
                }
            }
            else
            {
                if (dsLocations.Tables[0].Rows.Count > 0)
                {
                    txtprogrammanger_Name.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["Prm_Name"]).Trim();
                    txtprogrammanger_Email.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["Prm_EmailId"]).Trim();

                }
                if (dsLocations.Tables[1].Rows.Count > 0)
                {
                    txtprojectmanger_Name.Text = Convert.ToString(dsLocations.Tables[1].Rows[0]["Pm_Name"]).Trim();
                    txtprojectmanger_Email.Text = Convert.ToString(dsLocations.Tables[1].Rows[0]["Pm_EmailId"]).Trim();
                }
            } 
        }

        if (Convert.ToString(txt_travel_project.Text).Contains("HO"))
        {
            spn_pm.Visible = false;
            txtprojectmanger_Name.Visible = false;
            spn_prm.InnerText = "HOD Name";
        }
        else
        {
            spn_pm.Visible = true;
            txtprojectmanger_Name.Visible = true;
            spn_prm.InnerText = "Program Manger";
        }

    }

    public void get_MyTravelRequisitions()
    {
        DataSet dsVendorEdit = new DataSet();
        SqlParameter[] sparss = new SqlParameter[2];

        sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparss[0].Value = "get_myTravelRequisition_dtls";

        sparss[1] = new SqlParameter("@Trvl_Req_ID", SqlDbType.Decimal);
        sparss[1].Value = Convert.ToDecimal(hdnTravel_id.Value);

        dsVendorEdit = spm.getDatasetList(sparss, "SP_Employee_TravelRequisitons_Dtls");
        if (dsVendorEdit.Tables[0].Rows.Count > 0)
        {
            txtEmpcode.Text= dsVendorEdit.Tables[0].Rows[0]["emp_code"].ToString();
            txtEmpName_Aadhar.Text = dsVendorEdit.Tables[0].Rows[0]["emp_name_asPer_aadhar"].ToString();
            txtMobile.Text = dsVendorEdit.Tables[0].Rows[0]["mobile_no"].ToString();
            lstFromLocation.SelectedValue = dsVendorEdit.Tables[0].Rows[0]["from_location"].ToString();
            txtFromDate.Text = dsVendorEdit.Tables[0].Rows[0]["from_date"].ToString();
            lstToLocation.SelectedValue = dsVendorEdit.Tables[0].Rows[0]["to_location"].ToString();
            txttodate.Text = dsVendorEdit.Tables[0].Rows[0]["to_date"].ToString();
            txtbaselocation.Text = dsVendorEdit.Tables[0].Rows[0]["base_location"].ToString();
            lstPreferTime.Text = dsVendorEdit.Tables[0].Rows[0]["prefer_time"].ToString();
            lstTravelMode.SelectedValue = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["booked_reqested"]);
            txtaccomodationl.Text = dsVendorEdit.Tables[0].Rows[0]["Accomo_dation"].ToString();
            txtremarks.Text = dsVendorEdit.Tables[0].Rows[0]["remarks"].ToString();
            lsttravelbook.SelectedValue = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["booked_for"]);
            lstTravelStatus.SelectedValue = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]);
            //lstTravelStatus.SelectedValue = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["emp_Req_status"]);
            txtremarksby.Text = dsVendorEdit.Tables[0].Rows[0]["admin_Remarks"].ToString();
            txtremarkscancel.Text = dsVendorEdit.Tables[0].Rows[0]["cancle_remarks"].ToString();
            txtconfirmAccomodation.Text = dsVendorEdit.Tables[0].Rows[0]["confirm_accomodation"].ToString();
            //lnkfile_Vendor.Text = dsVendorEdit.Tables[0].Rows[0]["upload_booked_ticket"].ToString();
            if (dsVendorEdit.Tables[0].Rows[0]["upload_booked_ticket"].ToString() != "")
                lnkfile_Vendor.Text = dsVendorEdit.Tables[0].Rows[0]["upload_booked_ticket"].ToString();
            else
                lnkfile_Vendor.Text = "";
            band.Text = dsVendorEdit.Tables[0].Rows[0]["brand"].ToString();
            txt_travel_project.Text = dsVendorEdit.Tables[0].Rows[0]["travel_project"].ToString();
            // lnk_downloadAadhar.Text = dsVendorEdit.Tables[0].Rows[0]["aadhar_fileName"].ToString();
            hdnAadharCardFileName.Value= dsVendorEdit.Tables[0].Rows[0]["aadhar_filePath"].ToString();
            txtaccfromdate.Text = dsVendorEdit.Tables[0].Rows[0]["acc_from_date"].ToString();
            txtacctodate.Text = dsVendorEdit.Tables[0].Rows[0]["acc_to_date"].ToString();
            txtbookamt.Text = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_booked_amt"]);
            txtaccamt.Text = dsVendorEdit.Tables[0].Rows[0]["acc_booked_amt"].ToString();
            txtcancleamt.Text = dsVendorEdit.Tables[0].Rows[0]["cancel_ticket_amt"].ToString();

            //if (Convert.ToString(lnkfile_Vendor.Text).Trim() !="")
            //{   

            //    uplodedticket.InnerText = "Download Travel Ticket";
            //    travelticket.Visible = false;
            //}

            if (dsVendorEdit.Tables[4].Rows.Count > 0)
            {
                dg_bookedTickets.DataSource = dsVendorEdit.Tables[4];
                dg_bookedTickets.DataBind();

                uplodedticket.InnerText = "Download Travel Ticket Files";
                travelticket.Visible = false;
                uploadtravelticket.Visible = false;
            }

             
            if (dsVendorEdit.Tables[1].Rows.Count > 0)
            {
                gvuploadedFiles.DataSource = dsVendorEdit.Tables[1];
                gvuploadedFiles.DataBind();

                spnsportingfiles.InnerText = "Download Spporting Files";
                spnsportingfiles_1.Visible = false;
                uplodmultiple.Visible = false;
            }
             
            if (dsVendorEdit.Tables[1].Rows.Count > 0)
            {
                gvuploadedFiles.DataSource = dsVendorEdit.Tables[1];
                gvuploadedFiles.DataBind();

                
            }

            
            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() == "2" && Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["cancel_Req_status"]).Trim() =="")
            {
                btnCorrection.Visible = true;
            } 
            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["cancel_Req_status"]).Trim() == "1" )
            {
                divcancleremarks.Visible = true;
            }
             
            

            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["cancel_Req_status"]).Trim() == "1")
            {
                txtcancleamt.Enabled = true;
            }

            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["cancel_Req_status"]).Trim() == "4")
            {
                divcancleremarks.Visible = true;
            }
            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["cancel_Req_status"]).Trim() == "4")
            {
                txtremarkscancel.Enabled = false;
            }
 

            if (dsVendorEdit.Tables[0].Rows[0]["yes_no"].ToString() == "1")
            {
                rdoYes.Checked = true;
                rdoNo.Checked = false;
                divAcc.Visible = true;
                div3.Visible = true;
                div2.Visible = true; 
                li_Admin_Acc_1.Visible = true;
                li_Admin_Acc_2.Visible = true;
                li_Admin_Acc_3.Visible = true;
            }

            else
            {
                rdoYes.Checked = false;
                rdoNo.Checked = true;
            }
            
            txtaccomodationl.Enabled = false;
            rdoYes.Enabled = false;
            rdoNo.Enabled = false;
            txtEmpName_Aadhar.Enabled = false;
            txtMobile.Enabled = false;
            lstFromLocation.Enabled = false;
            txtFromDate.Enabled = false;
            lstToLocation.Enabled = false;
            txttodate.Enabled = false;
            txtbaselocation.Enabled = false;
            lstPreferTime.Enabled = false;
            lstTravelMode.Enabled = false;
            txtremarks.Enabled = false;
            txtaccomodationl.Enabled = false;
            txtconfirmAccomodation.Enabled = false;
            txtconfirmAccomodation.Enabled = false;
            txtaccamt.Enabled = false;
            txtaccamt.Enabled = false;
            txtaccfromdate.Enabled = false;
            txtacctodate.Enabled = false;




            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() != "1")
            {
                
                trvl_btnSave.Visible = false;
                lnkfile_Vendor.Visible = true;

                lstTravelStatus.Enabled = false;
                lsttravelbook.Enabled = false;
                txtbookamt.Enabled = false;

                if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() != "1")
                {
                    
                    trvl_btnSave.Visible = false;
                    lnkfile_Vendor.Visible = true;

                    lstTravelStatus.Enabled = false;
                    txtconfirmAccomodation.Enabled = false;
                    txtaccamt.Enabled = false;

                    //if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() == "2")
                    //{
                    //    li1.Visible = true;

                    //}


                    if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() == "2")
                {
                    uploadtravelticket.Visible = false;
                    
                }
                txtremarksby.Enabled = false;
                   
                           }
                }
        }


    }


    //    protected void lnkfile_Vendor_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadBookedTravelTicket"]).Trim()), lnkfile_Vendor.Text);
    //        Response.ContentType = ContentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
    //        Response.WriteFile(strfilepath);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }
    //} 
     

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    { 
        if(Convert.ToString(txtFromDate.Text).Trim()!="")
        Validate_from_date();

       // txtFromDate.Text = "";

        // lblmessage.Text = "";
        //  var getDate = txtFromDate.Text.ToString();
        //var getDate1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        // DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        // DateTime Today = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //  DateTime Today = DateTime.Now;
        // if (Today > SelectedDate)
        {
            //lblmessage.Text = "Please select date only today or greater than today";
            //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
          //  txtFromDate.Text = "";
            //return;
        }
    }


    protected void txttodate_TextChanged(object sender, EventArgs e)
    {

        if (Convert.ToString(txttodate.Text).Trim() != "")
            validate_To_Date();
        //    lblmessage.Text = "";
        //    if (Convert.ToString(txttodate.Text).Trim() != "")
        //    {
        //        var getDate = txttodate.Text.ToString();
        //        var getDate1 = txttodate.Text.ToString();
        //       // DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //       // DateTime Today = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        // DateTime Today = DateTime.Now;
        //        if (Today < SelectedDate)
        //        {
        //            lblmessage.Text = "Please select to date greater than from date";
        //            //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //            txttodate.Text = "";
        //            return;
        //        }

        //  }
    }

    protected void txtAccomodationFromDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtaccfromdate.Text).Trim() != "")
            Validate_from_date();
    }

    protected void txtAccomodationtodate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtacctodate.Text).Trim() != "")
            validate_To_Date();
    }
    protected void lstTravelStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        lsttravelbook.Enabled = false;
        lsttravelbook.SelectedValue = "0";
        txtbookamt.Enabled = false;
        
        
        if (Convert.ToString(lstTravelStatus.SelectedValue).Trim()=="2")
        {
            lsttravelbook.Enabled = true;
            txtbookamt.Enabled = true;
        }



        txtaccamt.Enabled = false;
        txtconfirmAccomodation.Enabled = false;
      //  txtconfirm.SelectedValue = "0";
        if (Convert.ToString(lstTravelStatus.SelectedValue).Trim() == "2")
        {
            txtconfirmAccomodation.Enabled = true;
            txtaccamt.Enabled = true;
        }

        uploadtravelticket.Enabled = false;
        //uploadtravelticket.FileName = "0";
        if(Convert.ToString(lstTravelStatus.SelectedValue).Trim()=="2")
        { uploadtravelticket.Enabled = true; }

        uplodmultiple.Enabled = false;
        //uploadtravelticket.FileName = "0";
        if (Convert.ToString(lstTravelStatus.SelectedValue).Trim() == "2")
        {
            uplodmultiple.Enabled = true;
        }

        

    }

    protected void rdoYes_CheckedChanged(object sender, EventArgs e)
    {
         

    }




    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if(Convert.ToString(txtremarkscancel.Text).Trim()=="")
        {
            divcancleremarks.Visible = true;
            lblmessage.Text = "Please Enter Travel Requisition Cancellation Remakrs.";
            return;
        }
         
       
        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Cancel_Travel_Request_employee";
          
        spars[1] = new SqlParameter("@Trvl_Req_ID", SqlDbType.BigInt);
        if (Convert.ToString(hdnTravel_id.Value).Trim() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = Convert.ToDecimal(hdnTravel_id.Value);

        spars[2] = new SqlParameter("@cancle_remarks", SqlDbType.VarChar);
        if (Convert.ToString(txtremarkscancel.Text).Trim() != "")
            spars[2].Value = Convert.ToString(txtremarkscancel.Text).Trim();
        else
            spars[2].Value = DBNull.Value;

        spars[3] = new SqlParameter("@cancel_Req_status", SqlDbType.Int);
 
        spars[3].Value = 1;

        spars[4] = new SqlParameter("@cancel_ticket_amt", SqlDbType.Decimal);
        if (Convert.ToString(txtcancleamt.Text).Trim() != "")
            spars[4].Value = Convert.ToString(txtcancleamt.Text).Trim();
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@booked_by_empcode", SqlDbType.VarChar);
        spars[5].Value = Convert.ToString(hdnLoginEmployee_Code.Value).Trim();

        try
        {
            dsgoal = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
            double dMaxReqId = 0;
            dMaxReqId = Convert.ToDouble(dsgoal.Tables[0].Rows[0]["MaxTravelID"]);
            if (dMaxReqId != 0)
            {
                //Send Mail

                string strsubject = "Cancelled Travel Requisition Request : " + txtEmpName.Text;
                 
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
                 
                strbuild.Append("</td colspan='2'>Dear All,</tr>");
                strbuild.Append("<tr><td colspan='2' style='height:20px'></td></tr>");
                 strbuild.Append("<tr><td colspan='2'>" + txtEmpName.Text + " has create cancel  travel requisition request in OneHr,please take a action or check with employee.</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");

                strbuild.Append("<tr><td>From Date:-</td><td> " + txtFromDate.Text + "</td></tr>");
                if (Convert.ToString(txttodate.Text).Trim() != "")
                    strbuild.Append("<tr><td>Return Date:-</td><td> " + txttodate.Text + "</td></tr>");

                strbuild.Append("<tr><td style='width:5%;'>From Location :-</td><td style='width:20%;'> " + lstFromLocation.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>To Location :-</td><td style='width:20%;'> " + lstToLocation.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Prefer Time :-</td><td style='width:20%;'> " + lstPreferTime.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Travel Mode :-</td><td style='width:20%;'> " + lstTravelMode.SelectedItem.Text + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");


                strbuild.Append("</table><table><tr><td style='height:20px;colspan=2;'>This is an auto generated email, please do not reply!<td></tr></table>");

                string sccmails = Convert.ToString(hdnAdmin_HODEmailId.Value).Trim();

                if (Convert.ToString(sccmails).Trim() != "")
                {
                    if (Convert.ToString(txtprogrammanger_Email.Text).Trim() != "")
                    {
                        sccmails = sccmails + ";" + Convert.ToString(txtprogrammanger_Email.Text).Trim();
                    }
                }
                else
                {
                    sccmails = Convert.ToString(txtprogrammanger_Email.Text).Trim();
                }

                if (Convert.ToString(sccmails).Trim() != "")
                {
                    if (Convert.ToString(txtprojectmanger_Email.Text).Trim() != "")
                    {
                        if (Convert.ToString(txtprogrammanger_Email.Text).Trim() != Convert.ToString(txtprojectmanger_Email.Text).Trim())
                            sccmails = sccmails + ";" + Convert.ToString(txtprojectmanger_Email.Text).Trim();
                    }
                }

                    spm.sendMail(Convert.ToString(hdnAdmin_EmailsId.Value), strsubject, Convert.ToString(strbuild), "", Convert.ToString(sccmails).Trim());
                 
                    Response.Redirect("MyTravelRequisitions.aspx");
            }
        }
        catch (Exception ex)
        {
                                                                                                             
        }
    }


    protected void btnback_mng_Click(object sender, EventArgs e)
    { 
            Response.Redirect("MyTravelRequisitions.aspx");
    }
}
