using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class projectdetail : System.Web.UI.Page
{
    int projectid;
    string projectimages;

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["projectid"] != null && Request.QueryString["projectid"] != "" && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["projectid"].ToString().Trim()), out projectid))
                {
                    projectid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["projectid"]));
                    if (projectid != 0)
                    {
                        DataTable dt = classproject.getsingleprojectuser(projectid);
                        bind(projectid); 
                        //sagar added this code for trying pm photo 
                        bind1(projectid); 
                        //Jayesh_Sagar below  new code  for multiple images tobe displayed on projectdetail page 24nov2017
                        display();
                        //Jayesh_Sagar Above  new code  for multiple images tobe displayed on projectdetail page 24nov2017
                        this.Title = creativeconfiguration.SiteName + ": Projects: " + (dt.Rows[0]["projecttitle"]).ToString();

                    }
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
    }

    public void bind(int projectid)
    {
        try
        {

            DataTable dt = classproject.getsingleprojectuser(projectid);
            if(dt.Rows.Count>0)
            {
               // string projectimages;
                rptrprojectdetail.DataSource = dt;
                rptrprojectdetail.DataBind();
                projectimages = dt.Rows[0]["projectimage"].ToString();
				rptrprojectdetail2.DataSource = dt;
				 rptrprojectdetail2.DataBind();
				
                    /*Label Label2111 = (Label)rptrprojectdetail2.FindControl("lblyr");					
					Panel pnlyr = (Panel)rptrprojectdetail2.FindControl("pnlyr");
					Label spnyr = (Label)rptrprojectdetail2.FindControl("spnyr");
                    //Label2111.Visible = tr;
					pnlyr.Visible=false;
					spnyr.Visible=false;
					if (Convert.ToString(Label2111.Text).Trim()=="")
					{
						pnlyr.Visible=false;
						spnyr.Visible=false;
					}
					else
					{
						pnlyr.Visible=false;
						spnyr.Visible=false;
					}*/
					
					foreach(RepeaterItem rpt in rptrprojectdetail2.Items)
				{
						//HtmlAnchor anchor = (HtmlAnchor)rpt.FindControl("id of anchor tag");
						Label Label2111 = (Label)rpt.FindControl("lblyr");
							Panel pnlyr = (Panel)rpt.FindControl("pnlyr");
							Label spnyr = (Label)rpt.FindControl("spnyr");
							
					if (Convert.ToString(Label2111.Text).Trim()=="9999")
					{
							pnlyr.Visible=false;
							spnyr.Visible=false;
					}
					else
					{
						pnlyr.Visible=true;
						spnyr.Visible=true;
					}
				}
                
               // Response.Write(projectimages);
              //  Response.End();
                //if (projectimages == null || projectimages == "")
                //{
                //    Label Label2111 = (Label)rptrprojectdetail.FindControl("Label2");
                //    Label2111.Visible = false;
                //}
                //else
                //{
                //    Label Label2111 = (Label)rptrprojectdetail.FindControl("Label2");
                //    Label2111.Visible = true;
                //}


            }

        }
        catch (Exception ex)
        {

        }


    }

    public void bind1(int projectid)
    {
        try
        {

            DataTable dt = classproject.getsingleprojectuser(projectid);
            if (dt.Rows.Count > 0)
            {
               //string projectimages1;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                //projectimages1 = dt.Rows[0]["projectimage"].ToString();
                //if (projectimages1 != "" || projectimages1!=null)
                //{
                //    Repeater1.Visible = true;
                    
                //}
                //else
                //{
                //    //Repeater1.Visible = false;
                //}
               // Response.Write(projectimages1);
               // Response.End();
                //if (projectimages == null || projectimages == "")
                //{
                //    Label Label2111 = (Label)rptrprojectdetail.FindControl("Label2");
                //    Label2111.Visible = false;
                //}
                //else
                //{
                //    Label Label2111 = (Label)rptrprojectdetail.FindControl("Label2");
                //    Label2111.Visible = true;
                //}


            }

        }
        catch (Exception ex)
        {

        }


    }
   


    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("project1.aspx");
    }

    //Jayesh_Sagar added below code to display project manager profile picture on projectdetail.aspx page 21nov2017
    public string getuserimage(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0)
            {
                strurl = ReturnUrl("sitepathadmin") + "images/project/" + imagepath.ToString();
                Repeater1.Visible = true;
            }
            else
            {
                Repeater1.Visible = false;
                //strurl = ReturnUrl("sitepathadmin") + "images/project/noimage.png";
                //imgprofiles1.Visible = true;

            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    //Jayesh_Sagar added above code to display project manager profile picture on projectdetail.aspx page 21nov2017

    //Jayesh_Sagar below  new code  for multiple images to be displayed on projectdetail page 24nov2017
    public void display()
    {
        DataTable dt=classproject.getallprojectimages(projectid);
        if(dt.Rows.Count>0)
        {
        //rptprojectimages.DataSource = dt;
        //rptprojectimages.DataBind();
		rptprojectimages1.DataSource = dt;
        rptprojectimages1.DataBind();
		
        
        }
       // if (rptprojectimages.Items.Count >= 1)
       // {
       //     pnlimg.Visible = true;
       //     rptprojectimages.Visible = true;				
       // }
       //else
       // {
       //     pnlimg.Visible = false;
       //     rptprojectimages.Visible = false;
       // }
		
		  if (rptprojectimages1.Items.Count >= 1)
        {          
			pnlimg1.Visible = true;
            rptprojectimages1.Visible = true;			
        }
        else
        {
            pnlimg1.Visible = false;
            rptprojectimages1.Visible = false;
        }
    }
    //Jayesh_Sagar Above new code  for multiple images to be displayed on projectdetail page 24nov2017
    //Jayesh_sagar try below code 8Jan2018
    //protected void Viewall_Click(object sender, EventArgs e)
    //{
    //int currentdate = Convert.ToInt32(DateTime.Now.ToString());
    //int completedate;
    //DataTable dt = classproject.getsingleprojectuser(projectid);
    //if (dt.Rows.Count > 0)
    //{
    //    rptrprojectdetail.DataSource = dt;
    //    rptrprojectdetail.DataBind();

    //    completedate = Convert.ToInt32(dt.Rows[0]["projectcompletedate"].ToString());
    //    string strurl = "";
    //   // strurl = UrlRewritingVM.getUrlRewritingInfo(catname, UrlRewritingVM.Encrypt(catid.ToString().Trim()), "PS");
    //    Response.Redirect(strurl);
    //    //Response.End();
    //if (currentdate >= completedate)
    //{
    //    Response.Redirect("http://localhost:80/hrms/completed.aspx");
    //}
    //else
    //{
    //    Response.Redirect("http://localhost:80/hrms/ongoing.aspx");
    //}
    //   Response.Redirect("http://localhost:80/hrms/completed.aspx");

    // }
    // }
    //Jayesh_sagar try above code 8Jan2018


}

