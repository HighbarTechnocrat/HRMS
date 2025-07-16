using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_employeeTravelRequisitionForm : System.Web.UI.Page
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

            txtEmpcode.Text = Convert.ToString(Session["Empcode"]).Trim();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/TravelRequisition_Index");
            }
            else
            {
                Page.SmartNavigation = true;
                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim() + "/"));

                hdnAadharCardPath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()));

                if (!Page.IsPostBack)
                {
                    PopulateEmployeeData();
                    get_Locations_Timings();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnTravel_id.Value = Request.QueryString[0];
                        get_MyTravelRequisitions();
                        get_Project_Program_Mangers_details();
                    }


                }

            }


            //
           // txtprojectmanger_Email.Visible = true;

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
                txtEmpName.Text =Convert.ToString(dtEmp.Rows[0]["Emp_Name"]).Trim();
                txtDesignation.Text = Convert.ToString(dtEmp.Rows[0]["DesginationName"]).Trim();
                txtDepartment.Text = Convert.ToString(dtEmp.Rows[0]["Department_Name"]).Trim();
                txtband.Text = Convert.ToString(dtEmp.Rows[0]["grade"]).Trim();
                txtEmpName_Aadhar.Text = Convert.ToString(dtEmp.Rows[0]["Name_As_Per_Aadhaar"]).Trim();
                hdnNameAsPerAadhar.Value = Convert.ToString(dtEmp.Rows[0]["Name_As_Per_Aadhaar"]).Trim();
                txtMobile.Text = Convert.ToString(dtEmp.Rows[0]["mobile"]).Trim();
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

            //if (dsLocations.Tables[8].Rows.Count > 0)
            //{
            //    hdnAdmin_PrmEmailId.Value = Convert.ToString(dsLocations.Tables[8].Rows[0]["Prm_EmailId"]).Trim();
            //}

            //if (dsLocations.Tables[10].Rows.Count > 0)
            //{
            //    hdnAdmin_HODEmailId.Value = Convert.ToString(dsLocations.Tables[10].Rows[0]["Pm_EmailId"]).Trim();
            //}

            if (dsLocations.Tables[6].Rows.Count > 0)
            {
                foreach (DataRow dr in dsLocations.Tables[6].Rows)
                {
                    if (Convert.ToString(hdnAdmin_EmailsId.Value).Trim() == "")
                        hdnAdmin_EmailsId.Value = Convert.ToString(dr["Emp_Emailaddress"]).Trim();
                    else
                        hdnAdmin_EmailsId.Value = hdnAdmin_EmailsId.Value +  ";" + Convert.ToString(dr["Emp_Emailaddress"]).Trim();
                }
            }

            if (dsLocations.Tables[5].Rows.Count > 0)
            {
                divUploadAadhar.Visible = false;
                lnkfile_Vendor.Text = "Download Aadhar Card";
                hdnAadharCardFileName.Value = Convert.ToString(dsLocations.Tables[5].Rows[0]["FilePath"]).Trim();
                lnkfile_Vendor.Visible = true;
            }
            else
            {
                divUploadAadhar.Visible = true;

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
                lstTravelMode.Items.Insert(0, new ListItem("Select  Travel Mode", "0"));
                }

            if (dsLocations.Tables[8].Rows.Count > 0)
            {
                lstProjectlist.DataSource = dsLocations.Tables[8];
                lstProjectlist.DataTextField = "Location_name";
                lstProjectlist.DataValueField = "comp_code";
                lstProjectlist.DataBind();
                lstProjectlist.Items.Insert(0, new ListItem("Select Travel Project ", "0"));
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
        if (Convert.ToString(lstProjectlist.SelectedValue).Contains("HO"))
            spars[1].Value = Convert.ToString(txtDepartment.Text);
        else
            spars[1].Value = Convert.ToString(lstProjectlist.SelectedValue);
         
        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
         
        if (dsLocations != null)
        {
            if (Convert.ToString(lstProjectlist.SelectedValue).Contains("HO"))
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

    }

    public void Validate_acc_fromdate()
    {
        string[] strdate;
        string strAfDate = "";
        string strFrmDate = "";


        if (Convert.ToString(txtFromDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromDate.Text).Trim().Split('-');
            strFrmDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtaccdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtaccdate.Text).Trim().Split('-');
            strAfDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_accFrom_Date";


        spars[1] = new SqlParameter("@acc_from_date", SqlDbType.VarChar);
        spars[1].Value = strAfDate;

        spars[3] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[3].Value = strFrmDate;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpcode.Text).Trim(); ;

        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtaccdate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtFromDate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }

            }
        }

    }

    public void Validate_acc_todate()
    {
        string[] strdate;
        
        string strAtDate = "";
        string strAfDate = "";

        if (Convert.ToString(txtaccdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtaccdate.Text).Trim().Split('-');
            strAfDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtacctodate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtacctodate.Text).Trim().Split('-');
            strAtDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_acc_to_Date";

        

        spars[1] = new SqlParameter("@acc_to_date", SqlDbType.VarChar);
        spars[1].Value = strAtDate;

        
        spars[2] = new SqlParameter("@acc_from_date", SqlDbType.VarChar);
        spars[2].Value = strAfDate;





        dsLocations = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtacctodate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }

                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtaccdate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }

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

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_From_Date";


        spars[1] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[1].Value = strFrmDate;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpcode.Text).Trim(); ;

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
        string strAtDate = "";
        string strAfDate = "";



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

        if (Convert.ToString(txtaccdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtaccdate.Text).Trim().Split('-');
            strAfDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtacctodate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtacctodate.Text).Trim().Split('-');
            strAtDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "validate_To_Date";
         
        spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
        spars[1].Value = strToDate;

        spars[3] = new SqlParameter("@acc_to_date", SqlDbType.VarChar);
        spars[3].Value = strAtDate;

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

                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim() != "")
                {
                    txtacctodate.Text = "";
                    lblmessage.Text = Convert.ToString(dsLocations.Tables[0].Rows[0]["msg"]).Trim();
                }

            }
        }

    }


    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            if (rdoNo.Checked)
            {
                txtaccomodationl.Text = "";
                txtaccdate.Text = "";
                txtacctodate.Text = "";
            }

            #region Check All Parameters Selected
            lblmessage.Text = "";
            if (Convert.ToString(lstProjectlist.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Travel Project.";
                return;
            }

            if (Convert.ToString(txtEmpName_Aadhar.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Name As Aadhar.";
                return;
            }

            if (Convert.ToString(txtMobile.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Mobile Number.";
                return;
            }

            if (Convert.ToString(lstFromLocation.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Location.";
                return;
            }

            if (Convert.ToString(txtFromDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date.";
                return;
            }
            if (Convert.ToString(lstToLocation.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select To Location.";
                return;
            }

            //  if (Convert.ToString(txttodate.Text).Trim() == "")
            // {
            //   lblmessage.Text = "Please select To Date.";
            // return;
            // }

            if (Convert.ToString(txtbaselocation.Text).Trim() == "")
            {
                lblmessage.Text = "Please select Boarding Point (Station/Airport).";
                return;
            }

            if (Convert.ToString(lstPreferTime.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Prefer Time.";
                return;
            }

            if (Convert.ToString(lstTravelMode.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Travel Mode.";
                return;
            }

            //if (Convert.ToString(txtAccomodation.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please select Near Accomodation.";
            //    return;
            //}

            //if (Convert.ToString(txtremarks.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please Enter Remarks.";
            //    return;
            //}

            if (lnkfile_Vendor.Visible == false)
            {
                if (Convert.ToString(uploadAaadharCard.FileName).Trim() == "")
                {
                    lblmessage.Text = "Please Upload Aaadhar Card.";
                    return;
                }
            }

            if (Convert.ToString(lstFromLocation.SelectedItem.Text).Trim() == Convert.ToString(lstToLocation.SelectedItem.Text).Trim())
            {
                lblmessage.Text = "Please Check From and To Location.";
                return;
            }
            
            

            submit_travelreqiuistion_details();


            #endregion 

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }
    }


    private void submit_travelreqiuistion_details()
    {

        string filename = "";
        //Upload Aadhar Card file
        if (uploadAaadharCard.HasFile)
        {
             DateTime loadedDate = DateTime.Now;
            var strfromDate = loadedDate.ToString("ddMMyyyyHHmm");
            filename = uploadAaadharCard.FileName;
            var strfileName = "";
            strfileName = txtEmpcode.Text + "_DT_ID_11_" + strfromDate + Path.GetExtension(uploadAaadharCard.FileName);
            uploadAaadharCard.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()), strfileName));
            var getStatus = spm.InsertUpdateDocumentDetails(0, txtEmpcode.Text, filename, strfileName,11, true);
             
        } 

        string[] strdate;
        string strFrmDate = "";
        string strToDate = "";
        string strAfDate = "";
        string strAtDate = "";


        if (Convert.ToString(txtFromDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromDate.Text).Trim().Split('-');
            strFrmDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txttodate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txttodate.Text).Trim().Split('-');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtaccdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtaccdate.Text).Trim().Split('-');
            strAfDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtacctodate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtacctodate.Text).Trim().Split('-');
            strAtDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }


        #region insert or update
        string sType = "insert_empTravelRequisition_form";

        if (Convert.ToString(hdnTravel_id.Value).Trim() != "")
            sType = "update_empTravelRequisition_form";

        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[20];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = sType;

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpcode.Text).Trim();

        spars[2] = new SqlParameter("@emp_name_asPer_aadhar", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpName_Aadhar.Text).Trim();

        spars[3] = new SqlParameter("@mobile_no", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(txtMobile.Text).Trim();
         
        spars[4] = new SqlParameter("@from_location", SqlDbType.VarChar);
        spars[4].Value = Convert.ToString(lstFromLocation.SelectedItem.Text).Trim();

        spars[5] = new SqlParameter("@to_location", SqlDbType.VarChar);
        spars[5].Value = Convert.ToString(lstToLocation.SelectedItem.Text).Trim();

        spars[6] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[6].Value = Convert.ToString(strFrmDate).Trim();

        spars[7] = new SqlParameter("@to_date", SqlDbType.VarChar);
        if (Convert.ToString(strToDate).Trim() != "")
            spars[7].Value = Convert.ToString(strToDate).Trim();
        else
            spars[7].Value = DBNull.Value;

        spars[8] = new SqlParameter("@base_location", SqlDbType.VarChar);
        spars[8].Value = Convert.ToString(txtbaselocation.Text).Trim();

        spars[9] = new SqlParameter("@prefer_time", SqlDbType.Int);
        spars[9].Value = Convert.ToInt32(lstPreferTime.SelectedValue);

        spars[10] = new SqlParameter("@remarks", SqlDbType.VarChar);
        spars[10].Value = Convert.ToString(txtremarks.Text).Trim();

        spars[11] = new SqlParameter("@Trvl_Req_ID", SqlDbType.BigInt);
        if (Convert.ToString(hdnTravel_id.Value).Trim() == "")
            spars[11].Value = DBNull.Value;
        else
            spars[11].Value = Convert.ToDecimal(hdnTravel_id.Value);

        spars[12] = new SqlParameter("@booked_reqested", SqlDbType.Int);
        spars[12].Value = Convert.ToInt32(lstTravelMode.SelectedValue);

        spars[13] = new SqlParameter("@yes_no", SqlDbType.Int);
        if (rdoYes.Checked)
            spars[13].Value = 1;
        else
            spars[13].Value = 0;

        spars[14] = new SqlParameter("@Accomo_dation", SqlDbType.VarChar);
        if (Convert.ToString(txtaccomodationl.Text).Trim() != "")
            spars[14].Value = Convert.ToString(txtaccomodationl.Text).Trim();
        else
            spars[14].Value = DBNull.Value;

        spars[15] = new SqlParameter("@travel_project", SqlDbType.VarChar);
        spars[15].Value = Convert.ToString(lstProjectlist.Text).Trim();

        spars[16] = new SqlParameter("@brand", SqlDbType.VarChar);
        spars[16].Value = Convert.ToString(txtband.Text).Trim();

         
         spars[17] = new SqlParameter("@NameAs_perAadhar_employee", SqlDbType.VarChar);
        if (Convert.ToString(txtEmpName_Aadhar.Text).Trim() != Convert.ToString(hdnNameAsPerAadhar.Value).Trim())
            spars[17].Value = Convert.ToString(txtEmpName_Aadhar.Text).Trim();
        else
            spars[17].Value = DBNull.Value;

        spars[18] = new SqlParameter("@acc_from_date", SqlDbType.VarChar);
        if (Convert.ToString(strAfDate).Trim() != "")
            spars[18].Value = Convert.ToString(strAfDate).Trim();
        else
            spars[18].Value = DBNull.Value;

        spars[19] = new SqlParameter("@acc_to_date", SqlDbType.VarChar);
        if (Convert.ToString(strAtDate).Trim() != "")
            spars[19].Value = Convert.ToString(strAtDate).Trim();
        else
            spars[19].Value = DBNull.Value;

 
        try
        {
            dsgoal = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
            double dMaxReqId = 0;
            dMaxReqId = Convert.ToDouble(dsgoal.Tables[0].Rows[0]["MaxTravelID"]);
            if (dMaxReqId != 0)
            {
                //Send mail to admin

                string strsubject = "Travel Requisition Request: " + txtEmpName.Text;
                StringBuilder strbuild = new StringBuilder();
                 
                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
                strbuild.Append("</td>Dear All,</tr>");
                strbuild.Append("<tr><td colspan='2' style='height:20px'></td></tr>");               
                strbuild.Append("<tr><td colspan='2'>" + txtEmpName.Text +" has create travel requisition request in OneHr,Please check the below details and take action.</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");

                strbuild.Append("<tr><td>From Date:-</td><td> " + txtFromDate.Text + "</td></tr>");
                if(Convert.ToString(txttodate.Text).Trim()!="")
                strbuild.Append("<tr><td>Return Date:-</td><td> " + txttodate.Text + "</td></tr>");

                strbuild.Append("<tr><td style='width:5%;'>From Location :-</td><td style='width:20%;'> " + lstFromLocation.SelectedItem.Text +"</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>To Location :-</td><td style='width:20%;'> " + lstToLocation.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Prefer Time :-</td><td style='width:20%;'> " + lstPreferTime.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Travel Mode :-</td><td style='width:20%;'> " + lstTravelMode.SelectedItem.Text + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                string strReuisitionURL = "";
                strReuisitionURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approveTravelRequisitionReq"]).Trim() + "?TrvlReqID=" + dMaxReqId).Trim();
                 
                strbuild.Append("<tr><td colspan='2'><a href='" + strReuisitionURL + "'> Please Click here for your action </a></td></tr>");
                strbuild.Append("</table>");
                strbuild.Append("<table>");
                strbuild.Append("<tr><td style='height:20px'>This is an auto generated email, please do not reply!<td></tr>");
                strbuild.Append("</table>");


                string sccmails = Convert.ToString(hdnAdmin_HODEmailId.Value).Trim();

                if (Convert.ToString(sccmails).Trim() != "")
                {
                    if (Convert.ToString(txtprogrammanger_Email.Text).Trim() != "")
                    {
                        sccmails = sccmails + ";" + Convert.ToString(txtprogrammanger_Email.Text).Trim();
                    }
                }

                if (Convert.ToString(sccmails).Trim() != "")
                {
                    if (Convert.ToString(txtprojectmanger_Email.Text).Trim() != "")
                    {
                        if (Convert.ToString(txtprogrammanger_Email.Text).Trim() != Convert.ToString(txtprojectmanger_Email.Text).Trim())
                            sccmails = sccmails + ";" + Convert.ToString(txtprojectmanger_Email.Text).Trim();
                    }
                }
                 spm.sendMail(Convert.ToString(hdnAdmin_EmailsId.Value), strsubject, Convert.ToString(strbuild),"", Convert.ToString(sccmails).Trim());
                 

                Response.Redirect("myTRavelRequisitions.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }

        /*  if (uploadFile_KRA.HasFile)
          {
              string filename = uploadFile_KRA.FileName;

              if (Convert.ToString(filename).Trim() != "")
              {

                  string KRAsupportedFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRASupportedfiles"]).Trim() + "/");
                  bool folderExists = Directory.Exists(KRAsupportedFilePath);
                  if (!folderExists)
                  {
                      Directory.CreateDirectory(KRAsupportedFilePath);
                  }



                  HttpFileCollection fileCollection = Request.Files;
                  for (int i = 0; i < fileCollection.Count; i++)
                  {
                      string strfileName = "";

                      HttpPostedFile uploadfileName = fileCollection[i];
                      string fileName = ReplaceInvalidChars(Path.GetFileName(uploadfileName.FileName));
                      if (uploadfileName.ContentLength > 0)
                      {
                          String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                          strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_KRAsupported" + InputFile;
                          filename = strfileName;
                          uploadfileName.SaveAs(Path.Combine(KRAsupportedFilePath, strfileName));


                          DataSet dsFile = new DataSet();
                          SqlParameter[] spar = new SqlParameter[3];

                          spar[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                          spar[0].Value = "insert_KRA_main_file";

                          spar[1] = new SqlParameter("@FilePath", SqlDbType.VarChar);
                          spar[1].Value = Convert.ToString(strfileName).Trim();

                          spar[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                          spar[2].Value = dMaxReqId;


                          dsFile = spm.getDatasetList(spar, "SP_KRA_Insert_Update");



                      }
                  }

              }





          } */

        //insert approver detils
        /* if (isDraft == false)
         {

             if (Convert.ToString(hdn_IsSelfApprover.Value).Trim() == "yes")
             {
                 string sApproverEmail_CC = "";
                 DateTime dsysdate = DateTime.Now.Date;
                 var year = dsysdate.Year;
                 var month = dsysdate.Month;
                 var day = dsysdate.Day;
                 //KRA File Name Period_Empcode_KRA.Final.Approved.date.pdf
                 string FileName = Convert.ToString(txtPeriod.Text).Trim() + "_" + Convert.ToString(txtemp_code.Text).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";
                 spm.KRA_insert_NextApprover_details(dMaxReqId, Convert.ToString(hdnAppr_empcode.Value).Trim(), Convert.ToInt32(hdnAppr_id.Value), "Pending", "");

                 spm.KRA_insert_CurrentApprover_details(dMaxReqId, Convert.ToString(hdnAppr_empcode.Value).Trim(), Convert.ToInt32(hdnAppr_id.Value), "Approved", "", true, FileName);

                 #region export KRA pdf if it's final approver

                 DataSet dtKRA = new DataSet();

                 #region get KRA

                 var my_date = DateTime.Now;

                 SqlParameter[] spar_R = new SqlParameter[2];

                 spar_R[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                 spar_R[0].Value = "Rpt_get_employee_KRA_details";

                 spar_R[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                 spar_R[1].Value = dMaxReqId;

                 dtKRA = spm.getDatasetList(spar_R, "SP_KRA_GETALL_DETAILS");

                 #endregion

                 string strpath = Server.MapPath("~/procs/KRAprt.rdlc");

                 LocalReport ReportViewer2 = new LocalReport();
                 ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
                 ReportDataSource rds = new ReportDataSource("dsKRA", dtKRA.Tables[0]);
                 ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", dtKRA.Tables[1]);
                 ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", dtKRA.Tables[2]);
                 ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", dtKRA.Tables[3]);
                 ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", dtKRA.Tables[4]);

                 ReportViewer2.DataSources.Clear();
                 ReportViewer2.DataSources.Add(rds);
                 ReportViewer2.DataSources.Add(rds_goal);
                 ReportViewer2.DataSources.Add(rds_Reviewee);
                 ReportViewer2.DataSources.Add(rds_Reviewer);
                 ReportViewer2.DataSources.Add(rds_Final_Reviewer);

                 //Export the RDLC Report to Byte Array.
                 // byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings); 

                 byte[] Bytes = ReportViewer2.Render(format: "PDF", deviceInfo: "");

                 using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + FileName, FileMode.Create))
                 {
                     stream.Write(Bytes, 0, Bytes.Length);
                 }


                 #endregion


                 #region send mail to Reviewee for approved KRA

                 DataSet dsKRAApprover = new DataSet();
                 dsKRAApprover = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", Convert.ToDecimal(dMaxReqId));

                 for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                 {
                     //if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                     //    sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                     //else
                     //    sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;

                 }

                 StringBuilder strbuild = new StringBuilder();
                 strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
                 strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
                 strbuild.Append("<tr><td style='height:20px'></td></tr>");
                 strbuild.Append("<tr><td colspan='2'>This is to inform you that your KRA for the period " + txtPeriod.Text + " is approved. Please find attached digitally approved KRA copy for your reference.</td></tr>");
                 strbuild.Append("<tr><td style='height:20px'></td></tr>");


                 StringBuilder strbuild_Approvers = new StringBuilder();
                 strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                 strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewed On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Remarks</th></tr>");
                 for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                 {
                     strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                     strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                     strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                     strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                 }

                 strbuild_Approvers.Append("</table>");

                 // FileName = "2022-2023_00630902_12.4.2022.pdf";
                 string sattchedfileName = Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + FileName;

                 string strsubject = "KRA approved for the period " + txtPeriod.Text + " of " + Convert.ToString(txtEmployee_name.Text).Trim();
                 spm.sendMail_KRA(hdnAppr_empEmail.Value, strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), sattchedfileName, sApproverEmail_CC);

                 #endregion

             }
             else
             {
                 spm.KRA_insert_NextApprover_details(dMaxReqId, Convert.ToString(hdnAppr_empcode.Value).Trim(), Convert.ToInt32(hdnAppr_id.Value), "Pending", "");
                 #region send mail to Next Approver

                 #region send mail to Reviewee for approved KRA

                 string strsubject = "Request for KRA Approval :-" + Convert.ToString(txtEmployee_name.Text);
                 string sApproverEmail_CC = "";

                 DataSet dsKRAApprover = new DataSet();
                 dsKRAApprover = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", Convert.ToDecimal(dMaxReqId));

                 string strKRAURL = "";
                 strKRAURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_KRA"]).Trim() + "?kraid=" + dMaxReqId).Trim();

                 StringBuilder strbuild = new StringBuilder();
                 strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
                 strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
                 strbuild.Append("<tr><td style='height:20px'></td></tr>");
                 strbuild.Append("<tr><td colspan='2'> " + txtEmployee_name.Text + "  has submitted KRA for the period " + Convert.ToString(txtPeriod.Text).Trim() + ". Please Review & Approve.</td></tr>");
                 strbuild.Append("<tr><td style='height:20px'></td></tr>");

                 strbuild.Append("<tr><td style='height:20px'><a href='" + strKRAURL + "'> Click Here to review & approve. </a></td></tr>");

                 strbuild.Append("<tr><td style='height:20px'></td></tr>");

                 StringBuilder strbuild_Approvers = new StringBuilder();
                 strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                 strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewed On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Remarks</th></tr>");
                 for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                 {
                     strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                     strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                     strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                     strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                 }

                 strbuild_Approvers.Append("</table>");

                 spm.sendMail_KRA(hdnAppr_empEmail.Value, strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), "", sApproverEmail_CC);

                 #endregion
                 #endregion
             }
         } */
        #endregion
         
            
        //Response.Redirect("employeeTravelRequisitionForm.aspx");
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
            txtEmpName_Aadhar.Text = dsVendorEdit.Tables[0].Rows[0]["emp_name_asPer_aadhar"].ToString();
            txtMobile.Text = dsVendorEdit.Tables[0].Rows[0]["mobile_no"].ToString();
            lstFromLocation.SelectedValue = dsVendorEdit.Tables[0].Rows[0]["from_location"].ToString();
            txtFromDate.Text = dsVendorEdit.Tables[0].Rows[0]["from_date"].ToString();
            if(dsVendorEdit.Tables[0].Rows[0]["to_location"].ToString()!="")
            lstToLocation.SelectedValue = dsVendorEdit.Tables[0].Rows[0]["to_location"].ToString();
            txttodate.Text = dsVendorEdit.Tables[0].Rows[0]["to_date"].ToString();
            txtbaselocation.Text = dsVendorEdit.Tables[0].Rows[0]["base_location"].ToString();
            lstPreferTime.Text = dsVendorEdit.Tables[0].Rows[0]["prefer_time"].ToString();
            lstTravelMode.SelectedValue = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["booked_reqested"]);
           // txtband.Text = dsVendorEdit.Tables[0].Rows[0]["brand"].ToString();
            lstProjectlist.SelectedValue = dsVendorEdit.Tables[0].Rows[0]["travel_project"].ToString();
            txtaccomodationl.Text= dsVendorEdit.Tables[0].Rows[0]["Accomo_dation"].ToString();
            //  txtprogrammanger.Text = dsVendorEdit.Tables[0].Rows[0]["program_manger"].ToString();
            // txtprojectmanger.Text = dsVendorEdit.Tables[0].Rows[0]["project_manger"].ToString();
            txtaccdate.Text = dsVendorEdit.Tables[0].Rows[0]["acc_from_date"].ToString();
            txtacctodate.Text = dsVendorEdit.Tables[0].Rows[0]["acc_to_date"].ToString();

            txtaccomodationl.Text = dsVendorEdit.Tables[0].Rows[0]["Accomo_dation"].ToString();
           
            txtremarks.Text = dsVendorEdit.Tables[0].Rows[0]["remarks"].ToString();

            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() == "1")
            {
                btnCorrection.Visible = true;
            }
            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["trvl_Req_status"]).Trim() !="1")
            {
                txtEmpName_Aadhar.Enabled = false;
                txtMobile.Enabled = false;
                trvl_btnSave.Visible = false;
                lstFromLocation.Enabled = false;
                txtFromDate.Enabled = false;
                lstToLocation.Enabled = false;
                txttodate.Enabled = false;
                txtbaselocation.Enabled = false;
                lstPreferTime.Enabled = false;
                lstTravelMode.Enabled = false;
                txtaccomodationl.Enabled = false;
                txtremarks.Enabled = false;
                lstProjectlist.Enabled = false;
                txtaccdate.Enabled = false;
                txtacctodate.Enabled = false;
                rdoNo.Enabled = false;
                rdoYes.Enabled = false;


            }

            if (Convert.ToString(lstProjectlist.SelectedValue).Contains("HO"))
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

            if (dsVendorEdit.Tables[0].Rows[0]["yes_no"].ToString() == "1")
            {
                rdoYes.Checked = true;
                rdoNo.Checked = false;
                divAcc.Visible = true;
                divdate.Visible = true;
                divtodate.Visible = true;
            }
            else
            {
                rdoNo.Checked = true;
                rdoYes.Checked = false;
                divAcc.Visible = false;
                divdate.Visible = false;
                divtodate.Visible = false;

            }
        }


    }


        protected void lnkfile_Vendor_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim()), lnkfile_Vendor.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/TravelRequisition_Index.aspx");

    }



    protected void TxtVendorAddress_TextChanged(object sender, EventArgs e)
    {

    }

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
        if (Convert.ToString(txtaccdate.Text).Trim() != "")
            Validate_acc_fromdate();
    }

    protected void txtAccomodationtodate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtacctodate.Text).Trim() != "")
            Validate_acc_todate();
    }

    protected void rdoYes_CheckedChanged(object sender, EventArgs e)
    {
        divdate.Visible = false;
        divtodate.Visible = false;
        divAcc.Visible = false;
        if (rdoYes.Checked)
            divAcc.Visible = true;
        if (rdoYes.Checked)
            divdate.Visible = true;
        if (rdoYes.Checked)
            divtodate.Visible = true;
    }


   




protected void btnCorrection_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }


        //
        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Cancel_Travel_Request_employee_Pending";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpcode.Text).Trim(); 

        spars[2] = new SqlParameter("@remarks", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtremarks.Text).Trim();

        spars[3] = new SqlParameter("@Trvl_Req_ID", SqlDbType.BigInt);
        if (Convert.ToString(hdnTravel_id.Value).Trim() == "")
            spars[3].Value = DBNull.Value;
        else
            spars[3].Value = Convert.ToDecimal(hdnTravel_id.Value);

        spars[4] = new SqlParameter("@cancel_Req_status", SqlDbType.Int);
        spars[4].Value = 4;

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
                strbuild.Append("</td>Dear All,</tr>");
                strbuild.Append("<tr><td colspan='2' style='height:20px'></td></tr>");
                strbuild.Append("<tr><td colspan='2'>" + txtEmpName.Text + " has cancel  travel requisition request in OneHr,for your information.</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");

                strbuild.Append("<tr><td>From Date:-</td><td> " + txtFromDate.Text + "</td></tr>");
                if (Convert.ToString(txttodate.Text).Trim() != "")
                    strbuild.Append("<tr><td>Return Date:-</td><td> " + txttodate.Text + "</td></tr>");

                strbuild.Append("<tr><td style='width:5%;'>From Location :-</td><td style='width:20%;'> " + lstFromLocation.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>To Location :-</td><td style='width:20%;'> " + lstToLocation.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Prefer Time :-</td><td style='width:20%;'> " + lstPreferTime.SelectedItem.Text + "</td></tr>");
                strbuild.Append("<tr><td style='width:5%;'>Travel Mode :-</td><td style='width:20%;'> " + lstTravelMode.SelectedItem.Text + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");

                /*string strReuisitionURL = "";
                strReuisitionURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approveTravelRequisitionReq"]).Trim() + "?TrvlReqID=" + dMaxReqId).Trim();

                strbuild.Append("<tr><td colspan='2'><a href='" + strReuisitionURL + "'> Please Click here for your action </a></td></tr>");
                */ 
                strbuild.Append("</table><table><tr><td style='height:20px;colspan=2;'>This is an auto generated email, please do not reply!<td></tr></table>");

                string sccmails = Convert.ToString(hdnAdmin_HODEmailId.Value).Trim();

                if (Convert.ToString(sccmails).Trim() != "")
                {
                    if (Convert.ToString(txtprogrammanger_Email.Text).Trim() != "")
                    {
                        sccmails = sccmails + ";" + Convert.ToString(txtprogrammanger_Email.Text).Trim();
                    }
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
        catch(Exception ex)
        {

        }
}

    protected void txttravelproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtprogrammanger_Name.Text = "";
        txtprojectmanger_Name.Text = "";
        txtprogrammanger_Email.Text = "";
        txtprojectmanger_Email.Text = "";


        if (Convert.ToString(lstProjectlist.SelectedValue).Trim()!="0")
        {
            if (Convert.ToString(lstProjectlist.SelectedValue).Contains("HO"))
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
            get_Project_Program_Mangers_details();
        }
    }
}