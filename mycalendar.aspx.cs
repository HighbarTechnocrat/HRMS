using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Data.SqlClient;

public partial class eventcalendar : System.Web.UI.Page
{

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;

    }

    public static string username="";
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString();
            hduser.Value = username;
			
        }
        else
        {
            Response.Redirect("login");
        }
    }

    [System.Web.Services.WebMethod(true)]
    public static string UpdateEvent(CalendarEvent cevent)
    {
        username = HttpContext.Current.User.Identity.Name.ToString().Trim();
        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(cevent.id))
        {
            //if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
            //{
            EventDAO.updateEvent(username, cevent.id, cevent.title, cevent.description, Convert.ToDateTime(cevent.start.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss")), Convert.ToDateTime(cevent.end.ToShortDateString() + " " + DateTime.MaxValue.ToString("HH:mm:ss")), cevent.rtime, cevent.ddlrtime, cevent.taguser);

                return "updated event with id:" + cevent.id + " update title to: " + cevent.title +
                " update description to: " + cevent.description;
            //}
        }
        else
        {
            return "unable to update event with id:" + cevent.id + " title : " + cevent.title + " description : " + cevent.description;
        }
    }
    [System.Web.Services.WebMethod(true)]
    public static string UpdateEventTime(ImproperCalendarEvent improperEvent)
    {
        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(improperEvent.id))
        {
            // FullCalendar 1.x
            //EventDAO.updateEventTime(improperEvent.id,
            //    DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
            //    DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
            // FullCalendar 2.x

            EventDAO.updateEventTime(improperEvent.id,
                                     Convert.ToDateTime(improperEvent.start.ToShortDateString()+ " " + DateTime.Now.ToString("HH:mm:ss")),
                                     Convert.ToDateTime(improperEvent.end.ToShortDateString() + " " + DateTime.MaxValue.ToString("HH:mm:ss")),
                                     improperEvent.allDay);  //allDay parameter added for FullCalendar 2.x

            return "updated event with id:" + improperEvent.id + " update start to: " + improperEvent.start +
                " update end to: " + improperEvent.end;
        }
        return "unable to update event with id: " + improperEvent.id;
    }
    //called when delete button is pressed
    [System.Web.Services.WebMethod(true)]
    public static String deleteEvent(int id)
    {
        //idList is stored in Session by JsonResponse.ashx for security reasons
        //whenever any event is update or deleted, the event id is checked
        //whether it is present in the idList, if it is not present in the idList
        //then it may be a malicious user trying to delete someone elses events
        //thus this checking prevents misuse
        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(id))
        {
            EventDAO.deleteEvent(id);
            return "deleted event with id:" + id;
        }

        return "unable to delete event with id: " + id;
    }
    //called when Add button is clicked
    //this is called when a mouse is clicked on open space of any day or dragged 
    //over mutliple days
    [System.Web.Services.WebMethod]
    public static int addEvent(ImproperCalendarEvent improperEvent)
    {
        // FullCalendar 1.x
        //CalendarEvent cevent = new CalendarEvent()
        //{
        //    title = improperEvent.title,
        //    description = improperEvent.description,
        //    start = DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
        //    end = DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
        //};

        // FullCalendar 2.x
        username = HttpContext.Current.User.Identity.Name.ToString().Trim();
        CalendarEvent cevent = new CalendarEvent()
        {
            title = improperEvent.title,
            description = improperEvent.description,
            start = Convert.ToDateTime(improperEvent.start.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss")),
            end = Convert.ToDateTime(improperEvent.end.ToShortDateString() + " " + DateTime.MaxValue.ToString("HH:mm:ss")),
            //start = Convert.ToDateTime(improperEvent.start),
            //end = Convert.ToDateTime(improperEvent.end),
            allDay = improperEvent.allDay,
            // New code for reminder
            rtime = improperEvent.rtime,
            ddlrtime = improperEvent.ddlrtime,
            //repeat = improperEvent.repeat,
            rfrom = improperEvent.rfrom,
            ddlrfrom = improperEvent.ddlrfrom,
            rto = improperEvent.rto,
            ddlrto = improperEvent.ddlrto,
            inttype = improperEvent.inttype,
            inttime = improperEvent.inttime,
            taguser = improperEvent.taguser,
            hidden = improperEvent.hidden,
            ltscript = improperEvent.ltscript
        };

        //if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
        //{
        //int key = 0;
        int key = EventDAO.addEvent(cevent, username);

        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

        if (idList != null)
        {
            idList.Add(key);
        }

            return key; //return the primary key of the added cevent object
            
        //}
        
       // return 1; //return a negative number just to signify nothing has been added
    }
    private static bool CheckAlphaNumeric(string str)
    {
        return Regex.IsMatch(str, @"^[a-zA-Z0-9-,:';.?()]*$");
    }
    public static bool CheckNumeric(string str)
    {
        return Regex.IsMatch(str, @"^[0-9]*\d{10}(-\d{9})?");
    }
    [WebMethod]
    public static List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select indexid, firstname+' '+ lastname as fullname from addressbook where firstname LIKE @SearchText+'%'", sqlcon1))
            {
                sqlcon1.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["fullname"].ToString());
                }
                return result;
            }
        }
    }
}