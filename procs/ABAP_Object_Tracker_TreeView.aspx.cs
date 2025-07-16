using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_TreeView : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    string selectedProject = "";
    String ddlselected = "";

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getProjectLocation(sender, e);
        }
    }
    private void getProjectLocation(object sender, EventArgs e)
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAllLocationsReportAccess";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));

                if (DS.Tables[0].Rows.Count == 1)
                {
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    //DDLProjectLocation_SelectedIndexChanged(sender, e);
                    selectedProject = HDProjectLocation.Value.Trim();
                }
                else
                {
                    HDProjectLocation.Value = Convert.ToString(DDLProjectLocation.SelectedValue).Trim();
                    selectedProject = HDProjectLocation.Value.Trim();

                }
            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void SearchABAPObjectDataList_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (!string.IsNullOrEmpty(txtSearchedABAPObject.Text) && DDLProjectLocation.SelectedValue == "0")
            {
                lblmessage.Text = "Please  select the Project";
                return;
            }

            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAllLocationsReportAccess";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(!string.IsNullOrEmpty(selectedProject) ? selectedProject : DDLProjectLocation.SelectedValue).Trim();

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                spnprojectlocation.Visible = false;
                spnobjectdesc.Visible = false;
                errormsg.Text = "";
                lidata.Visible = false;
                txt_locationname.Text = "";
                txt_objectDesc.Text = "";

                gvRGSuploadedFiles.Visible = false;
                gvFSuploadedFiles.Visible = false;
                gvABAPuploadedFiles.Visible = false;
                gvHBTuploadedFiles.Visible = false;
                gvCTMuploadedFiles.Visible = false;
                gvUATuploadedFiles.Visible = false;

                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbt.Visible = false;
                spnctm.Visible = false;
                spnuat.Visible = false;


                ulABAPObjects.Visible = true;
                ulABAPObjects.Nodes.Clear();
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    string LocationCode = row[0].ToString();
                    string LocationName = row[1].ToString();
                    TreeNode parent = new TreeNode
                    {
                        Text = LocationCode + " - " + LocationName,
                        Value = LocationCode,
                        ToolTip = LocationCode + " - " + LocationName,
                    };

                    ulABAPObjects.Nodes.Add(parent);
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }

    }

    protected void ulABAPObjects_SelectedNodeChanged(object sender, EventArgs e)
    {
        errormsg.Text = "";
        lblmessage.Text = "";
       TreeNode selectedNode = ulABAPObjects.SelectedNode;
        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

        string parentId = selectedNode.Value;
        gvRGSuploadedFiles.Visible = false;
        gvFSuploadedFiles.Visible = false;
        gvABAPuploadedFiles.Visible = false;
        gvHBTuploadedFiles.Visible = false;
        gvCTMuploadedFiles.Visible = false;
        gvUATuploadedFiles.Visible = false;

        spnrgs.Visible = false;
        spnfs.Visible = false;
        spnabap.Visible = false;
        spnhbt.Visible = false;
        spnctm.Visible = false;
        spnuat.Visible = false;

        spnprojectlocation.Visible = false;
        spnobjectdesc.Visible = false;

        txt_locationname.Visible = false;
        txt_objectDesc.Visible = false;
        int result;
        if (int.TryParse(parentId, out result))
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAllStageDocumentsBasedOnId";

            spars[1] = new SqlParameter("@ABAPODId", SqlDbType.VarChar);
            spars[1].Value = parentId;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0)
            {
                spnprojectlocation.Visible = true;
                spnobjectdesc.Visible = true;

                Div1.Visible = true;
                txt_locationname.Visible = true;
                txt_objectDesc.Visible = true;
                lidata.Visible = true;
                txt_locationname.Text = Convert.ToString(selectedNode.Parent.Text);
                txt_objectDesc.Text = Convert.ToString(selectedNode.Text);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    spnrgs.Visible = true;

                    gvRGSuploadedFiles.DataSource = null;
                    gvRGSuploadedFiles.DataBind();

                    gvRGSuploadedFiles.Visible = true;

                    gvRGSuploadedFiles.DataSource = DS.Tables[0];
                    gvRGSuploadedFiles.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    spnfs.Visible = true;

                    gvFSuploadedFiles.DataSource = null;
                    gvFSuploadedFiles.DataBind();

                    gvFSuploadedFiles.Visible = true;

                    gvFSuploadedFiles.DataSource = DS.Tables[1];
                    gvFSuploadedFiles.DataBind();
                }
                if (DS.Tables[2].Rows.Count > 0)
                {
                    spnabap.Visible = true;

                    gvABAPuploadedFiles.DataSource = null;
                    gvABAPuploadedFiles.DataBind();

                    gvABAPuploadedFiles.Visible = true;

                    gvABAPuploadedFiles.DataSource = DS.Tables[2];
                    gvABAPuploadedFiles.DataBind();
                }
                if (DS.Tables[3].Rows.Count > 0)
                {
                    spnhbt.Visible = true;

                    gvHBTuploadedFiles.DataSource = null;
                    gvHBTuploadedFiles.DataBind();

                    gvHBTuploadedFiles.Visible = true;

                    gvHBTuploadedFiles.DataSource = DS.Tables[3];
                    gvHBTuploadedFiles.DataBind();
                }
                if (DS.Tables[4].Rows.Count > 0)
                {
                    spnctm.Visible = true;

                    gvCTMuploadedFiles.DataSource = null;
                    gvCTMuploadedFiles.DataBind();

                    gvCTMuploadedFiles.Visible = true;

                    gvCTMuploadedFiles.DataSource = DS.Tables[4];
                    gvCTMuploadedFiles.DataBind();
                }
                if (DS.Tables[5].Rows.Count > 0)
                {
                    spnuat.Visible = true;

                    gvUATuploadedFiles.DataSource = null;
                    gvUATuploadedFiles.DataBind();

                    gvUATuploadedFiles.Visible = true;

                    gvUATuploadedFiles.DataSource = DS.Tables[5];
                    gvUATuploadedFiles.DataBind();
                }
                else if (DS.Tables[0].Rows.Count == 0 && DS.Tables[1].Rows.Count == 0 && DS.Tables[2].Rows.Count == 0 && DS.Tables[3].Rows.Count == 0 && DS.Tables[4].Rows.Count == 0 && DS.Tables[5].Rows.Count == 0)
                {
                    errormsg.Text = "No documents available.";
                }
            }
            else
            {
                spnprojectlocation.Visible = false;
                spnobjectdesc.Visible = false;
                txt_locationname.Text = "";
                txt_objectDesc.Text = "";
                lidata.Visible = false;
                gvRGSuploadedFiles.Visible = false;
                gvFSuploadedFiles.Visible = false;
                gvABAPuploadedFiles.Visible = false;
                gvHBTuploadedFiles.Visible = false;
                gvCTMuploadedFiles.Visible = false;
                gvUATuploadedFiles.Visible = false;
            }
        }
        else
        {
            selectedNode.ChildNodes.Clear();
            spnprojectlocation.Visible = false;
            spnobjectdesc.Visible = false;

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetABAPObjectDataListwithSearch";

            spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[1].Value = parentId;

            spars[2] = new SqlParameter("@SearchText", SqlDbType.VarChar);
            spars[2].Value = txtSearchedABAPObject.Text;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    string childId = row[0].ToString();
                    string childName = row[1].ToString();

                    TreeNode childNode = new TreeNode
                    {
                        Text = childName,
                        Value = childId,
                        ToolTip = childName,
                    };

                    selectedNode.ChildNodes.Add(childNode);
                }
                selectedNode.Expand();
            }
            else
            {
                lblmessage.Text = "No results found for the search text in this project.";
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            errormsg.Text = "";
            DDLProjectLocation.SelectedIndex = 0;
            ulABAPObjects.Nodes.Clear();
            txtSearchedABAPObject.Text = "";
            Div1.Visible = false;
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btnABAPObjectList_Click(object sender, EventArgs e)
    {
        SearchABAPObjectDataList_Click(sender, e);
       
    }
    
    public void ibdownloadbtn_Click(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        if (button == null)
        {
            lblmessage.Text = "Invalid control.";
            return;
        }

        string commandArgument = button.CommandArgument;
        if (string.IsNullOrWhiteSpace(commandArgument))
        {
            lblmessage.Text = "File reference not found.";
            return;
        }

        var pathConfigurations = new Dictionary<string, string>
        {
            { "RGS_Attachment", ConfigurationManager.AppSettings["ABAPObjectRGS"].Trim() },
            { "FS_Attachment", ConfigurationManager.AppSettings["ABAPObjectFS"].Trim() },
            { "ABAP_SourceCode", ConfigurationManager.AppSettings["ABAPObjectABAPSoruceCode"].Trim() },
            { "HBT_PassedTestCase", ConfigurationManager.AppSettings["ABAPObjectPassedHBTTestCase"].Trim() },
            { "HBT_FailedTestCase", ConfigurationManager.AppSettings["ABAPObjectFailedHBTTestCase"].Trim() },
            { "CTMTestCase", ConfigurationManager.AppSettings["ABAPObjectCTMTestCase"].Trim() },
            { "UATSingOff", ConfigurationManager.AppSettings["ABAPObjectUATSingOff"].Trim() }
        };

        string configKey = pathConfigurations.Keys.FirstOrDefault(key => commandArgument.Contains(key));
        if (configKey == null)
        {
            lblmessage.Text = "Invalid file type.";
            return;
        }

        string relativePath = Path.Combine(pathConfigurations[configKey], commandArgument);
        string filePath = Server.MapPath(relativePath);
        if (!File.Exists(filePath))
        {
            lblmessage.Text = "File Not Found.";
            return;
        }
        try
        {
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + commandArgument);
            Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            lblmessage.Text = "Error downloading file: " + ex.Message;
        }
    }

    public static List<string> GetSuggestions(string prefix)
    {
        List<string> suggestions = new List<string>();

        
        // Simulate DB fetch. Replace with DB call.
        string[] data = { "Apple", "ABanana", "ACherry", "Date", "Elderberry", "Fig", "Grape" };
        suggestions = data.Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();

        return suggestions;
    }
    
    

}
