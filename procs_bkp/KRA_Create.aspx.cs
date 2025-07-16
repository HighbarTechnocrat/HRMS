using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using Microsoft.Reporting.WebForms;

public partial class KRA_Create : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    #endregion


    #region Page_Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblmessage.Text = ""; 
            lbl_goal_msg.Text = "";
            lbl_Measurement_msg.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                  //  txt_goal_description.Attributes.Add("maxlength", "2");
                    txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    txt_Weightage.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txt_goal_seq_no.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txt_measurement_seq_no.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    hdngoal_id.Value = "";
                    hdnKRA_id.Value = "";

                    setVisible_Cntrl(false);

                    if (Request.QueryString.Count > 0)
                    {
                        hdnKRA_id.Value = Request.QueryString[0];
                        hdnstype_Main.Value = "Update";

                    }

                    

                    getKRA_Units();
                    getEmployeeDetails();
                    if (Convert.ToString(hdnKRA_id.Value).Trim() == "")
                    {
                        Check_LoginEmp_IsSubmitCurrentPeriod_KRA(); 
                    }
                    get_goal_measurement_Details_New();
                    get_KRA_Approvers();

                    if (DgvApprover.Rows.Count <= 0)
                    {
                        trvl_btnSave.Visible = false;
                        btnCancel.Visible = false;
                        lblmessage.Text = "Since you are not assigned under any approver you can not create KRA, please contact HR";
                    }

                    Check_IsTemplateKRA_Created();


                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    #endregion

    #region Page_Events
    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {
             
            
            #region Validation
            if (Convert.ToString(txt_goal_title.Text).Trim() == "")
            {
                lbl_goal_msg.Text = "Please enter Goal Title";
                return;
            }
            if (Convert.ToString(txt_Weightage.Text).Trim() == "" || Convert.ToInt32(txt_Weightage.Text) <= 0)
            {
                lbl_goal_msg.Text = "Please enter Weightage";
                return;
            }

            if (dgMeasurementslist.Rows.Count == 0)
            {
                lbl_goal_msg.Text = "Please Add Measurement Details.";
                return;
            }

            if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
            {
                if (Convert.ToString(txt_goal_seq_no.Text).Trim() != Convert.ToString(hdnGoal_Seq_no_old.Value).Trim())
                {
                    DataSet dsGoalSeqNo = check_goal_Sequance_no_exist();
                    if (dsGoalSeqNo.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsGoalSeqNo.Tables[0].Rows[0]["goal_seq_no"]).Trim() != "")
                        {
                            lbl_goal_msg.Text = "Please check Goal Sequence Number already exist";
                            return;
                        }
                    }
                }
            }

            if (Convert.ToInt32(txt_Weightage.Text)>100)
            {
                lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
                return;
            }
                //check goal title already exist 
                DataSet tdsgoal = new DataSet();
            tdsgoal = check_goal_already_exist();
            if (tdsgoal.Tables.Count > 0)
            {
                if (Convert.ToString(hdnGoal_Title_old.Value).Trim().ToLower() != Convert.ToString(txt_goal_title.Text).Trim().ToLower())
                    if (tdsgoal.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(tdsgoal.Tables[0].Rows[0]["Goal_Title"]).Trim() != "")
                        {
                            lbl_goal_msg.Text = "Please check Goal Title already exist";
                            return;
                        }
                    }

                //// //check  Weightage not more than 100
                if (tdsgoal.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(tdsgoal.Tables[1].Rows[0]["Weightage"]).Trim() != "")
                    {
                        Int32 iWeightageCnt = Convert.ToInt32(tdsgoal.Tables[1].Rows[0]["Weightage"]) + Convert.ToInt32(txt_Weightage.Text);

                        if (iWeightageCnt > 100)
                        {
                            if (Convert.ToString(hdnWwighrage_old.Value).Trim() != "")
                            {
                                iWeightageCnt = iWeightageCnt - Convert.ToInt32(hdnWwighrage_old.Value);
                            }
                        }

                        if (iWeightageCnt > 100)
                        {
                            lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
                            return;
                        }
                    }
                }
            }

            #endregion

            #region insert goal into temp table

            DataTable dsgoal = new DataTable();
            SqlParameter[] spars = new SqlParameter[8];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(hdn_flg_goal.Value) == "update")
                spars[0].Value = "update_goal_temp";
            else
                spars[0].Value = "insert_goal_temp";

            spars[1] = new SqlParameter("@Goal_Id", SqlDbType.Int);
            if (Convert.ToString(hdngoal_id.Value).Trim() != "")
                spars[1].Value = Convert.ToInt32(hdngoal_id.Value);
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@Goal_Title", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txt_goal_title.Text).Trim();

            spars[4] = new SqlParameter("@Goal_Description", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(txt_goal_description.Text).Trim();

            spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txtemp_code.Text).Trim();

            spars[6] = new SqlParameter("@Weightage", SqlDbType.Int);
            spars[6].Value = Convert.ToInt32(txt_Weightage.Text);

            spars[7] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
            if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
                spars[7].Value = Convert.ToInt32(txt_goal_seq_no.Text);
            else
                spars[7].Value = DBNull.Value;

            dsgoal = spm.getDataList(spars, "SP_KRA_Insert_Update");

            hdngoal_id.Value = Convert.ToString(dsgoal.Rows[0]["maxgaolId"]).Trim();

           


            hdngoal_id.Value = "";
            txt_goal_title.Text = "";
            txt_goal_description.Text = "";
            txt_Weightage.Text = "";
            btnback_mng.Text = "Add Goal";
            hdn_flg_goal.Value = "";
            hdngoal_id.Value = "";
            txt_goal_seq_no.Text = "";
            hdnWwighrage_old.Value = "";
            hdnGoal_Title_old.Value = "";
            hdnGoal_Seq_no_old.Value = "";
            trvldeatils_delete_btn.Visible = false;
            dgMeasurementslist.DataSource = null;
            dgMeasurementslist.DataBind();
            get_goal_measurement_Details_New();
            setVisible_Cntrl(false);
            //txt_Measurement_dtls.Text = "+";
            lblAddGoalMsg.Visible = false;

            #endregion



        }
        catch (Exception ex)
        {

        }

    }

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation
            if (Convert.ToString(txt_Measurement_dtls.Text).Trim() == "")
            {
                lbl_Measurement_msg.Text = "Please enter Measurement Details.";
                return;
            }
            if (Convert.ToString(lstUnit.SelectedValue).Trim() == "0")
            {
                lbl_Measurement_msg.Text = "Please select Unit.";
                return;
            }
            if (Convert.ToString(txt_Mqty.Text).Trim() == "")
            {
                lbl_Measurement_msg.Text = "Please enter Quantity.";
                return;
            }

            if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
            {
                if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != Convert.ToString(hdnMeasurement_Seq_no_old.Value).Trim())
                {
                    DataSet dsGoalSeqNo = check_Measurement_Sequance_no_exist();
                    if (dsGoalSeqNo.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsGoalSeqNo.Tables[0].Rows[0]["measurement_seq_no"]).Trim() != "")
                        {
                            lbl_Measurement_msg.Text = "Please check Measurement Sequence Number already exist";
                            return;
                        }
                    }
                }
            }

            lblAddGoalMsg.Visible = true;
            #endregion

            //DataTable mytable = new DataTable();
            //mytable.Columns.Add("Measurement_id", typeof(int));
            //mytable.Columns.Add(new DataColumn("Measurement_Details", typeof(string)));
            //mytable.Columns.Add("unit_id", typeof(int));
            //mytable.Columns.Add("Unit", typeof(string));
            //mytable.Columns.Add("Quantity", typeof(string));
            //mytable.Columns.Add("remarks", typeof(string));
            //mytable.Columns.Add("measurement_seq_no", typeof(string));


            //DataRow dr1 = mytable.NewRow();
            //dr1 = mytable.NewRow();
            //dr1["Measurement_id"] = 1;
            //dr1["Measurement_Details"] = txt_Measurement_dtls.Text;
            //dr1["unit_id"] = lstUnit.SelectedValue;
            //dr1["Quantity"] = txt_Mqty.Text;
            //dr1["remarks"] = txt_remakrs.Text;
            //dr1["measurement_seq_no"] = txt_measurement_seq_no.Text;

            //mytable.Rows.Add(dr1);
            //ViewState["Row"] = mytable;
            //dgMeasurementslist.DataSource = ViewState["Row"];
            //dgMeasurementslist.DataBind();


            #region insert Measurement into temp table

            DataTable dsgoal = new DataTable();
            SqlParameter[] spars = new SqlParameter[10];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(hdn_flg_measurement.Value) == "update")
            {
                spars[0].Value = "update_Measurement_temp_temp";
                lblAddGoalMsg.Text = "Please click on Update Goal button to save the changes";
            }
            else
            {
                spars[0].Value = "insert_Measurement_temp_temp";
                lblAddGoalMsg.Text = "Please click on Add Goal button to save the changes";
            }

            spars[1] = new SqlParameter("@Goal_Id", SqlDbType.Int);
            if (Convert.ToString(hdngoal_id.Value).Trim() != "")
                spars[1].Value = Convert.ToInt32(hdngoal_id.Value);
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@Measurement_id", SqlDbType.Int);
            if (Convert.ToString(hdnMeasurement_id.Value).Trim() != "")
                spars[3].Value = Convert.ToInt32(hdnMeasurement_id.Value);
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@Measurement_Details", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(txt_Measurement_dtls.Text).Trim();

            spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txtemp_code.Text).Trim();

            spars[6] = new SqlParameter("@unit_id", SqlDbType.Int);
            spars[6].Value = Convert.ToInt32(lstUnit.SelectedValue);

            spars[7] = new SqlParameter("@Quantity", SqlDbType.VarChar);
            spars[7].Value = Convert.ToString(txt_Mqty.Text).Trim();

            spars[8] = new SqlParameter("@remarks", SqlDbType.VarChar);
            spars[8].Value = Convert.ToString(txt_remakrs.Text).Trim();

            spars[9] = new SqlParameter("@measurement_seq_no", SqlDbType.Int);
            if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
                spars[9].Value = Convert.ToInt32(txt_measurement_seq_no.Text);
            else
                spars[9].Value = DBNull.Value;



            dsgoal = spm.getDataList(spars, "SP_KRA_Insert_Update");

            #endregion

            //fill Measurement Gird from temp table

            //clear controls after add Measurement to temp table
            txt_Measurement_dtls.Text = "";
            lstUnit.SelectedValue = "0";
            txt_Mqty.Text = "";
            txt_remakrs.Text = "";
            txt_measurement_seq_no.Text = "";
            btnTra_Details.Text = "+";
            DivTrvl.Visible = false;
            trvldeatils_btnSave.Text = "Add Measurement";
            hdn_flg_measurement.Value = "";
            hdnMeasurement_Seq_no_old.Value = "";


            DataSet dsMeasuremnsts = getMeasurements_temp_List(Convert.ToString(hdngoal_id.Value));
            dgMeasurementslist.DataSource = dsMeasuremnsts.Tables[0];
            dgMeasurementslist.DataBind();



        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        #region validations
          
        if(dgKRA_Details.Rows.Count<=0)       
        {
            lblmessage.Text = "Please enter Goal and Measurements details";
            return;
        }

        #endregion



        SubmitKRA(true);



    }


    protected void rptKRA_Details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string goalId = (e.Item.FindControl("hdnGoal_Id_R") as HiddenField).Value;
            Repeater rptMeasurements = e.Item.FindControl("rptMeasurements") as Repeater;
            Repeater rptUnits = e.Item.FindControl("rptUnits") as Repeater;
            Repeater rptQuantity = e.Item.FindControl("rptQuantity") as Repeater;

            DataSet dsMeasuremnsts = getMeasurements_List(goalId);
            rptMeasurements.DataSource = dsMeasuremnsts.Tables[0];
            rptMeasurements.DataBind();

            rptUnits.DataSource = dsMeasuremnsts.Tables[0];
            rptUnits.DataBind();

            rptQuantity.DataSource = dsMeasuremnsts.Tables[0];
            rptQuantity.DataBind();


        }
    }

    protected void lnk_edit_Measurement_Click(object sender, EventArgs e)
    {
        try
        {
            // ImageButton btn = (ImageButton)sender;
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnMeasurement_id.Value = Convert.ToString(dgMeasurementslist.DataKeys[row.RowIndex].Values[0]).Trim();

            DataSet dsmeasurement_dtl = spm.KRA_get_Mesurements_detials_edit(txtemp_code.Text, Convert.ToInt32(hdnMeasurement_id.Value)); ;
            txt_Measurement_dtls.Text = "";
            lstUnit.SelectedValue = "0";
            hdnMeasurement_Seq_no_old.Value = "";
            txt_Mqty.Text = "";
            txt_remakrs.Text = "";

            if (dsmeasurement_dtl.Tables[0].Rows.Count > 0)
            {
                txt_Measurement_dtls.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["Measurement_Details"]).Trim();
                lstUnit.SelectedValue = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["unit_id"]).Trim();
                txt_Mqty.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["Quantity"]).Trim();
                txt_remakrs.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["remarks"]).Trim();
                txt_measurement_seq_no.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["measurement_seq_no"]).Trim();
                hdnMeasurement_Seq_no_old.Value = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["measurement_seq_no"]).Trim();

                hdn_flg_measurement.Value = "update";
                trvldeatils_btnSave.Text = "Update Measurement";
                DivTrvl.Visible = true;
                btnTra_Details.Text = "-";
            }
        }
        catch (Exception ex)
        {

        }

    }



    protected void trvldeatils_delete_btn_Click1(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        #region Delete Goal and Measurement from temp 
        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_temp_goal_measurement";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(hdngoal_id.Value).Trim() != "")
            spars[2].Value = Convert.ToInt32(hdngoal_id.Value);
        else
            spars[2].Value = DBNull.Value;

        dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        get_goal_measurement_Details_New();

        trvldeatils_delete_btn.Visible = false;



        #endregion

    }


    protected void lnkedit_goal_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdngoal_id.Value = Convert.ToString(dgKRA_Details.DataKeys[row.RowIndex].Values[1]).Trim();

       // DivTrvl.Visible = true;
        //btnTra_Details.Text = "-";



        DataSet dsMeasuremnsts = getMeasurements_list_temp_to_temmp(Convert.ToString(hdngoal_id.Value)); //getMeasurements_temp_List(Convert.ToString(hdngoal_id.Value));
        dgMeasurementslist.DataSource = dsMeasuremnsts.Tables[0];
        dgMeasurementslist.DataBind();

        trvldeatils_delete_btn.Visible = false;
        txt_goal_title.Text = "";
        txt_goal_description.Text = "";
        txt_Weightage.Text = "";
        txt_goal_seq_no.Text = "";
        if (dsMeasuremnsts.Tables.Count > 1)
        {
            if (dsMeasuremnsts.Tables[1].Rows.Count > 0)
            {
                txt_goal_title.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Title"]).Trim();
                hdnGoal_Title_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Title"]).Trim();
                txt_goal_description.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Description"]).Trim();
                txt_Weightage.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Weightage"]).Trim();
                hdnWwighrage_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Weightage"]).Trim();
                txt_goal_seq_no.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["goal_seq_no"]).Trim();
                hdnGoal_Seq_no_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["goal_seq_no"]).Trim();
                hdn_flg_goal.Value = "update";
                btnback_mng.Text = "Update Goal";
                // trvldeatils_delete_btn.Visible = true;
                trvldeatils_delete_btn.Visible = false;
                setVisible_Cntrl(true);
                DivTrvl.Visible = false;
                btnTra_Details.Text = "+";
            }
        }

    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        trvldeatils_btnSave.Text = "Add Measurement";
        if (DivTrvl.Visible)
        {
            DivTrvl.Visible = false;
            btnTra_Details.Text = "+";
        }
        else
        {
            DivTrvl.Visible = true;
            btnTra_Details.Text = "-";
        }




    }

    protected void lnk_download_kra_file_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRASupportedfiles"]).Trim()), lnk_download_kra_file.Text);
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

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (dgKRA_Details.Rows.Count <= 0)
        {
            lblmessage.Text = "Please enter Goal and Measurements details";
            return;
        }

        #region validations


        //check goal title already exist 
        DataSet tdsgoal = new DataSet();
        tdsgoal = check_goal_already_exist();
        if (tdsgoal.Tables.Count > 0)
        {
            //// //check  Weightage not less than 100
            if (tdsgoal.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(tdsgoal.Tables[1].Rows[0]["Weightage"]).Trim() != "")
                {
                    Decimal iWeightageCnt = Convert.ToDecimal(tdsgoal.Tables[1].Rows[0]["Weightage"]);

                    if (iWeightageCnt < 100)
                    {
                        lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
                        return;
                    }
                }
            }
        }



        //if(blnchk==false)
        //{
        //    lblmessage.Text = "Please enter Goal and Measurements details";
        //    return;
        //} 

        #endregion


       // System.Threading.Thread.Sleep(2147483647);
         System.Threading.Thread.Sleep(100);
        SubmitKRA(false);


    }

    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {

    }
    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {

        DivTrvl.Visible = false;
        txt_Measurement_dtls.Text = "";
        lstUnit.SelectedValue = "0";
        txt_Mqty.Text = "";
        txt_remakrs.Text = "";
        txt_measurement_seq_no.Text = "";
        btnTra_Details.Text = "+";
        DivTrvl.Visible = false;
        trvldeatils_btnSave.Text = "Add Measurement";
        hdn_flg_measurement.Value = "";
        hdnMeasurement_Seq_no_old.Value = "";

    }

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (Convert.ToString(hdnKRA_FilePath.Value).Trim() != "")
            {
                String strfilepath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]), hdnKRA_FilePath.Value);
               
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
                Response.WriteFile(strfilepath);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    #endregion


    #region PageMethods

    private void Check_IsTemplateKRA_Created()
    {
        DataSet dsgoal = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);             
            spars[0].Value = "check_IsCurrentPeriod_KRATemplate_Create";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
            if (Convert.ToString(hdnPeriod_id.Value).Trim() != "")
                spars[2].Value = Convert.ToInt32(hdnPeriod_id.Value);
            else
                spars[2].Value = DBNull.Value;

           dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
            if(dsgoal.Tables[0].Rows.Count>0)
            {
                txtTemplate_Role_Name.Text = Convert.ToString(dsgoal.Tables[0].Rows[0]["Role_Name"]).Trim();
                txtTempKRA_ID.Text = Convert.ToString(dsgoal.Tables[0].Rows[0]["Temp_KRA_ID"]).Trim();
                txtRole_ID.Text = Convert.ToString(dsgoal.Tables[0].Rows[0]["Role_Id"]).Trim();
            }
            else
            {
                lblmessage.Text = "KRA Template not assigned to you please contact to admin";
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                btnback_mng.Visible = false;
                litrvldetail.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
         
    }

    private void SubmitKRA(Boolean isDraft)
    {

        #region insert or update KRA
        string sType = "insert_KRA_main";

        if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
            sType = "update_KRA_main";

        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = sType;

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@IsDraft", SqlDbType.Bit);
        spars[2].Value = isDraft;

        spars[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
        if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
            spars[3].Value = Convert.ToDecimal(hdnKRA_id.Value);
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@Role_Id", SqlDbType.Int);
        spars[4].Value = Convert.ToInt32(txtRole_ID.Text);

        spars[5] = new SqlParameter("@Temp_KRA_ID", SqlDbType.Decimal);
        if (Convert.ToString(txtTempKRA_ID.Text).Trim() != "")
            spars[5].Value = Convert.ToDecimal(txtTempKRA_ID.Text);
        else
            spars[5].Value = DBNull.Value;


        dsgoal = spm.getDatasetList(spars, "SP_KRA_Insert_Update");
        double dMaxReqId = 0;
        dMaxReqId = Convert.ToDouble(dsgoal.Tables[0].Rows[0]["MaxKRAID"]);


        if (uploadFile_KRA.HasFile)
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





        }

        //insert approver detils
        if (isDraft == false)
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

                spm.KRA_insert_CurrentApprover_details(dMaxReqId, Convert.ToString(hdnAppr_empcode.Value).Trim(), Convert.ToInt32(hdnAppr_id.Value), "Approved","", true, FileName);

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
        }
        #endregion




        Response.Redirect("KRA_index.aspx");
    }

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }
    private void getEmployeeDetails()
    {
        try
        {
            DataSet dsEmpdtls = new DataSet();
            if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                dsEmpdtls = spm.KRA_getEmployeedetails_KRA(Convert.ToDecimal(hdnKRA_id.Value));
            else
                dsEmpdtls = spm.KRA_getEmployeedetails(txtEmpCode.Text);

            if (dsEmpdtls.Tables[0].Rows.Count > 0)
            {
                txtemp_code.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Emp_Code"]).Trim();
                txtEmployee_name.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txtPosition.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["designation"]).Trim();
                txtDepartment.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["department"]).Trim();
                txtLocation.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["emp_projectName"]).Trim();
                txtPeriod.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_Period"]).Trim();
                hdnPeriod_id.Value = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_Period_id"]).Trim();
                txtKRAFromDt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_from_dt"]).Trim();
                txtKTATodt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_to_dt"]).Trim();
                txtKRA_SubmitDt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["kra_submit_date"]).Trim();
                lnk_download_kra_file.Text = "";
                hdnKRA_FilePath.Value = "";

                dgKRA_Details.Visible = false;
                dgKRAView.Visible = true;

                if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                {
                    lnk_download_kra_file.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["FilePath"]).Trim();
                    lnk_download_kra_file.Visible = true;

                    if (Convert.ToBoolean(dsEmpdtls.Tables[0].Rows[0]["IsDraft"]) == false)
                    {
                        btnCancel.Visible = false;
                    }

                   

                    if (dsEmpdtls.Tables[1].Rows.Count > 0)
                    {

                        if(Convert.ToString(dsEmpdtls.Tables[1].Rows[0]["status_id"]) == "2")
                        {
                            ligoalmsg.Visible = false;
                            ligoalmsg1.Visible = false;
                            ligoalmsg2.Visible = false;
                            Span2.Visible = false;

                            accmo_delete_btn.Visible = true;
                            hdnKRA_FilePath.Value = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Approved_KRA_path"]).Trim();

                           

                        }
                         if ((Convert.ToString(dsEmpdtls.Tables[1].Rows[0]["Action"]) == "Approved" || Convert.ToString(dsEmpdtls.Tables[1].Rows[0]["Action"]) == "Pending") && Convert.ToString(dsEmpdtls.Tables[1].Rows[0]["status_id"]) != "3")
                         {
                         

                            liGoalTitle.Visible = false;
                            liWeightage.Visible = false;
                            liblank1.Visible = false;

                            ligoadexcription.Visible = false;
                            liblank2.Visible = false;
                            liblank3.Visible = false;

                            ligoalSeq.Visible = false;
                            liblank4.Visible = false;
                            liblank5.Visible = false;

                            litrvldetail.Visible = false;
                            liblank6.Visible = false;
                            liblank7.Visible = false;

                            DivTrvl.Visible = false;

                            libtngoal.Visible = false;
                            libtngoal_1.Visible = false;
                            libtngoal_2.Visible = false;

                            trvl_btnSave.Visible = false;

                            dgKRA_Details.Visible = false;
                            dgKRAView.Visible = true;



                            //trvldeatils_btnSave.Visible = false;
                            //btnback_mng.Visible = false;
                            //btnTra_Details.Visible = false;
                            //spntrvldtls.Visible = false;
                            //trvl_btnSave.Visible = false;

                            hdnKRA_IsApproved.Value = "Approved";
                        }

                        
                    }
                }

                

            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataSet check_goal_already_exist()
    {
        DataSet dsgoal = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_already_exist";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@Goal_Title", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txt_goal_title.Text).Trim();

            dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        }
        catch (Exception ex)
        {

        }
        return dsgoal;
    }


    private DataSet check_goal_Sequance_no_exist()
    {
        DataSet dsgoal = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_duplicate_goal_Sequance_No";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
            if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
                spars[2].Value = Convert.ToInt32(txt_goal_seq_no.Text);
            else
                spars[2].Value = DBNull.Value;


                dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        }
        catch (Exception ex)
        {

        }
        return dsgoal;
    }

    private DataSet check_Measurement_Sequance_no_exist()
    {
        DataSet dsgoal = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_duplicate_measurement_Sequance_No";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
            if (Convert.ToString(hdngoal_id.Value).Trim() != "")
                spars[2].Value = Convert.ToInt32(hdngoal_id.Value);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
            if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
                spars[3].Value = Convert.ToInt32(txt_measurement_seq_no.Text);
            else
                spars[3].Value = DBNull.Value;


            dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        }
        catch (Exception ex)
        {

        }
        return dsgoal;
    }

    private void getKRA_Units()
    {
        DataSet dsunits = spm.KRA_getunits();
        if (dsunits.Tables[0].Rows.Count > 0)
        {
            lstUnit.DataSource = dsunits.Tables[0];
            lstUnit.DataTextField = "unit_short_name";
            lstUnit.DataValueField = "unit_id";
            lstUnit.DataBind();
            lstUnit.Items.Insert(0, new ListItem("Select Unit", "0"));
        }
    }

    public void get_goal_measurement_Details()
    {
        DataSet dtgoal = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_goal_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

    }


    public void get_goal_measurement_Details_New()
    {
        DataSet dtgoal = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_goal_gridview";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");




        dgKRA_Details.DataSource = null;
        dgKRA_Details.DataBind();

        dgKRAView.DataSource = null;
        dgKRAView.DataBind();

        if (dtgoal.Tables[0].Rows.Count > 0)
        {


            dgKRA_Details.DataSource = dtgoal.Tables[0];
            dgKRA_Details.DataBind();

            
            dgKRAView.DataSource = dtgoal.Tables[0];
            dgKRAView.DataBind();

        }

    }


    private DataSet getMeasurements_temp_List(string igoalid)
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_temp_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }

    private DataSet getMeasurements_List(string igoalid)
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }

    private DataSet getMeasurements_list_temp_to_temmp(string igoalid)
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_list_temptotemp";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }


    private void Check_LoginEmp_IsSubmitCurrentPeriod_KRA()
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "check_IsCurrentPeriod_KRA_Submitted_ByEmployee";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();
         
        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

        if(dtmeasurement.Tables[0].Rows.Count>0)
        {
           
            lblmessage.Text = "KRA already submitted for this period";

            trvl_btnSave.Visible = false;
            btnCancel.Visible = false;
            btnback_mng.Visible = false;
            trvldeatils_btnSave.Visible = false;
            trvldeatils_cancel_btn.Visible = false;
            dgKRA_Details.Visible = false;

        }

    }

    public void get_KRA_Approvers()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();


        decimal dKRAID = 0;

        if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
            dKRAID = Convert.ToDecimal(hdnKRA_id.Value);

        DataSet dsMilestone = new DataSet();
        // dsMilestone = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtEmpCode.Text, "get_KRA_Approvers", dKRAID);

        dsMilestone = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", dKRAID);
        hdn_IsSelfApprover.Value = "no";
        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();

            for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
            {
                hdnAppr_empcode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Approver_emp_code"]).Trim();
                hdnAppr_empName.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim();
                hdnAppr_empEmail.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();

                hdnAppr_id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approver_id"]).Trim();
                break;
            }

            if(Convert.ToString(txtemp_code.Text).Trim()==Convert.ToString(hdnAppr_empcode.Value).Trim())
            {
                hdn_IsSelfApprover.Value = "yes";
            }
        }


    }

    public void setVisible_Cntrl(bool blnflg)
    {
        liGoalTitle.Visible = blnflg;
        liWeightage.Visible = blnflg;
        liblank1.Visible = blnflg;

        ligoadexcription.Visible = blnflg;
        liblank2.Visible = blnflg;
        liblank3.Visible = blnflg;

        ligoalSeq.Visible = blnflg;
        liblank4.Visible = blnflg;
        liblank5.Visible = blnflg;

         litrvldetail.Visible = blnflg;
         liblank6.Visible = blnflg;
         liblank7.Visible = blnflg;

         DivTrvl.Visible = blnflg;

        li1.Visible = blnflg;
        lilblgoalerrorMsg.Visible = blnflg;
        lilblgoalerrorMsg_2.Visible = blnflg;
        lilblgoalerrorMsg_3.Visible = blnflg;
          

        btnback_mng.Visible = blnflg;
    }

    #endregion


    private void CombineColumnCells_New(GridViewRow currentRow, int colIndex, string fieldName)
    {
        TableCell currentCell = currentRow.Cells[colIndex];

        if (currentCell.Visible)
        {
            Object currentValue = dgKRAView.DataKeys[currentRow.RowIndex].Values[fieldName];

            for (int nextRowIndex = currentRow.RowIndex + 1; nextRowIndex < dgKRAView.Rows.Count; nextRowIndex++)
            {
                Object nextValue = dgKRAView.DataKeys[nextRowIndex].Values[fieldName];

                if (nextValue.ToString() == currentValue.ToString())
                {
                    GridViewRow nextRow = dgKRAView.Rows[nextRowIndex];
                    TableCell nextCell = nextRow.Cells[colIndex];
                    currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
                    nextCell.Visible = false;
                }
                else
                {
                    break;
                }
            }
        }
    }


    private void CombineColumnCells(GridViewRow currentRow, int colIndex, string fieldName)
    {
        TableCell currentCell = currentRow.Cells[colIndex];

        if (currentCell.Visible)
        {
            Object currentValue = dgKRA_Details.DataKeys[currentRow.RowIndex].Values[fieldName];

            for (int nextRowIndex = currentRow.RowIndex + 1; nextRowIndex < dgKRA_Details.Rows.Count; nextRowIndex++)
            {
                Object nextValue = dgKRA_Details.DataKeys[nextRowIndex].Values[fieldName];

                if (nextValue.ToString() == currentValue.ToString())
                {
                    GridViewRow nextRow = dgKRA_Details.Rows[nextRowIndex];
                    TableCell nextCell = nextRow.Cells[colIndex];
                    currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
                    nextCell.Visible = false;
                }
                else
                {
                    break;
                }
            }
        }
    }



    protected void dgKRA_Details_DataBound(object sender, EventArgs e)
    {

        for (int currentRowIndex = 0; currentRowIndex < dgKRA_Details.Rows.Count; currentRowIndex++)
        {
            GridViewRow currentRow = dgKRA_Details.Rows[currentRowIndex];
            CombineColumnCells(currentRow, 0, "Goal_Id");
            CombineColumnCells(currentRow, 1, "Goal_Id");
            CombineColumnCells(currentRow, 2, "Goal_Id");
            CombineColumnCells(currentRow, 7, "Goal_Id");
           // CombineColumnCells(currentRow, 8, "Goal_Id");
        } 
    }




    protected void dgKRAView_DataBound(object sender, EventArgs e)
    {
        for (int currentRowIndex = 0; currentRowIndex < dgKRA_Details.Rows.Count; currentRowIndex++)
        {
            GridViewRow currentRow = dgKRAView.Rows[currentRowIndex];
            CombineColumnCells_New(currentRow, 0, "Goal_Id");
            CombineColumnCells_New(currentRow, 1, "Goal_Id");
            CombineColumnCells_New(currentRow, 2, "Goal_Id"); 
        }
    }




    protected void lstUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Mqty.Enabled = true;
        txt_Mqty.Text = "";
        if (Convert.ToString(lstUnit.SelectedItem.Text).Trim() == "Adherence to Completion")
        {
            txt_Mqty.Text = "As agreed";
            txt_Mqty.Enabled = false;
        }
    }


    protected void dgKRA_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int totalgoalid = Convert.ToInt32(e.Row.Cells[0].Text);

            if (totalgoalid == 99)
            {
                LinkButton lnkedit_goal = (LinkButton)e.Row.FindControl("lnkedit_goal");
                lnkedit_goal.Visible = false;
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.Cells[0].Text = "";
            }

        }
    }

    protected void dgKRAView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int totalgoalid = Convert.ToInt32(e.Row.Cells[0].Text);

            if (totalgoalid == 99)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.Cells[0].Text = "";
            }

        }
    }
}