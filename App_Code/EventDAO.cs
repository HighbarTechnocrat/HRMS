using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventDAO
{
	//change the connection string as per your database connection.
    private static string connectionString = ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString.ToString();

	//this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents()
    {   
        List<CalendarEvent> events = new List<CalendarEvent>();
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, all_day FROM ECICalendarEvent_Test where event_start>=@start AND event_end<=@end", con);
        //cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
        //cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
        DataTable dtp = classcategory.geteventsbycatidsdisplayhome(Convert.ToString(ConfigurationManager.AppSettings["EventCatID"].ToString().Trim()));

       // DataTable dtp = classcategory.geteventsbycatidsdisplayhome(Convert.ToString(28));
        //using (con)
        //{
        //    con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        CalendarEvent cevent = new CalendarEvent();
        //        cevent.id = (int)reader["event_id"];
        //        cevent.title = (string)reader["title"];
        //        cevent.description = (string)reader["description"];
        //        cevent.start = (DateTime)reader["event_start"];
        //        cevent.end = (DateTime)reader["event_end"];
        //        cevent.allDay = (bool)reader["all_day"];
        //        events.Add(cevent);
        //    }
        //}
        if(dtp.Rows.Count>0)
        {
            for(int i=0;i<dtp.Rows.Count;i++)
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = Convert.ToInt32(dtp.Rows[i]["productid"].ToString());
                cevent.title = Convert.ToString(dtp.Rows[i]["productname"].ToString());
                cevent.description = (string)dtp.Rows[i]["shortdescription"].ToString();
                cevent.start = Convert.ToDateTime(dtp.Rows[i]["startdate"].ToString());
                cevent.end = Convert.ToDateTime(dtp.Rows[i]["enddate"].ToString());
                cevent.allDay = (bool)true;
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains an extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }
    public static List<CalendarEvent> getMyEvents(string username)
    {
        List<CalendarEvent> events = new List<CalendarEvent>();
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, all_day FROM ECICalendarEvent_Test where event_start>=@start AND event_end<=@end", con);
        //cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
        //cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;

        //Remove for offline
        //DataTable dtp = classcategory.getmyevents(Convert.ToString(28), username);

        //Remove for online
        //DataTable dtp = classcategory.getmyevents(Convert.ToString(ConfigurationManager.AppSettings["EventCatID"].ToString().Trim()), username);
        
        DataTable dtp = classcategory.getmyevents(Convert.ToString(ConfigurationManager.AppSettings["EventIdeaCentral"].ToString().Trim()), username);
        
        //using (con)
        //{
        //    con.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        CalendarEvent cevent = new CalendarEvent();
        //        cevent.id = (int)reader["event_id"];
        //        cevent.title = (string)reader["title"];
        //        cevent.description = (string)reader["description"];
        //        cevent.start = (DateTime)reader["event_start"];
        //        cevent.end = (DateTime)reader["event_end"];
        //        cevent.allDay = (bool)reader["all_day"];
        //        events.Add(cevent);
        //    }
        //}
        if (dtp.Rows.Count > 0)
        {
            for (int i = 0; i < dtp.Rows.Count; i++)
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = Convert.ToInt32(dtp.Rows[i]["productid"].ToString());
                cevent.title = Convert.ToString(dtp.Rows[i]["productname"].ToString());
                cevent.description = (string)dtp.Rows[i]["shortdescription"].ToString();
                cevent.start = Convert.ToDateTime(dtp.Rows[i]["startdate"].ToString());
                cevent.end = Convert.ToDateTime(dtp.Rows[i]["enddate"].ToString());
                cevent.allDay = (bool)true;

                DataTable dt1 = classreminder.getReminderByProductid(Convert.ToInt32(dtp.Rows[i]["productid"].ToString()));
                if(dt1.Rows.Count > 0)
                {
                    string remtime = "";
                    remtime = dt1.Rows[0]["reminder_time"].ToString().Trim();
                    if(remtime.Contains("PM"))
                    {
                        cevent.ddlrtime = "PM";
                        cevent.rtime = remtime.Replace("PM", "").Trim();
                    }
                    else
                    {
                        cevent.ddlrtime = "AM";
                        cevent.rtime = remtime.Replace("AM", "").Trim();
                    }
                    // DataTable dt2 = classreminder.getTaguserForReminderUsingProductid(Convert.ToInt32(dtp.Rows[i]["productid"].ToString()));
                    // if (dt2.Rows.Count > 0)
                    // {
                        // int indexid = 0;
                        // string ltfullname = "";
                        // string lttext = "";
                        // for (int j = 0; j < dt2.Rows.Count; j++)
                        // {
                            // if(!string.IsNullOrEmpty(dt2.Rows[j]["indexid"].ToString()))
                            // {
                                // indexid = Convert.ToInt32(dt2.Rows[j]["indexid"]);
                                // DataTable dt = classaddress.getuserbyindexid(indexid);
                                // if (dt.Rows.Count > 0)
                                // {
                                    // lttext = lttext + dt.Rows[0]["username"].ToString().Trim() + ",";
                                    // ltfullname = ltfullname + dt.Rows[0]["fullname"].ToString().Trim() + ",";
                                // }
                            // }
                            
                        // }
                        // if(lttext.Contains(","))
                        // {
                            // lttext = lttext.Substring(0, lttext.Length - 2);
                            // ltfullname = ltfullname.Substring(0, ltfullname.Length - 2);
                        // }
                        // else
                        // {
                            // lttext = lttext.Substring(0, lttext.Length - 1);
                            // ltfullname = ltfullname.Substring(0, ltfullname.Length - 1);
                        // }
                        // cevent.tagusername = ltfullname;
                        // cevent.taguseremail = lttext;

                        // //txtTo.Text = dt.Rows[0]["username"].ToString();
                        // //txtTo1.Text = dt.Rows[0]["fullname"].ToString();
                        // //hdvalue.Value = dt.Rows[0]["username"].ToString();
                        // //ltmsg.Visible = true;
                        // //ltmsg.Text = "<script type='text/javascript' defer='defer'>$(document).ready(function(){$(\"<span class='tag label label-primary'>" + dt.Rows[0]["fullname"].ToString() + "<span data-role='remove'></span></span>\").insertBefore($('.twitter-typeahead'));});</script>";
                    // }
                }
                
                
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains an extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }
	//this method updates the event title and description
    public static void updateEvent(String username,int id, String title, String description, DateTime start, DateTime end,String rtime,String ddlrtime,String taguser)
    {
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("UPDATE ECICalendarEvent_Test SET title=@title, description=@description WHERE event_id=@event_id", con);
        //cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
        //cmd.Parameters.Add("@description", SqlDbType.VarChar).Value= description;
        //cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

        //using (con)
        //{
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //}
        int key = 0;
        key = Convert.ToInt32(classproduct.updateevent(id, title, description,start.ToString(),end.ToString()));
        string to = "";
        if (key > 0)
        {
            if (rtime != "" && rtime != "null")
            {
                if (ddlrtime.Trim() == "AM" || ddlrtime.Trim() == "am")
                {
                    rtime = rtime.Trim() + " " + "AM";
                }
                else
                {
                    rtime = rtime.Trim() + " " + "PM";
                }
            }
            if (rtime.ToString() != "" && rtime.ToString() != "null" && rtime.Length > 0)
            {
                bool rem=classreminder.updatereminder(username,id, rtime.ToString(), "U", "N", "", "", 0, "");
                if(rem)
                {
                    classreminder.deleteTaguserForReminder(id);
                    classreminder.insertTaguser(id, username.ToString().Trim(), "U");
                    to = taguser.ToString().Trim();
                    string[] tomail = to.Split(',');
                    for (int i = 0; i < tomail.Length; i++)
                    {
                        string tomail1 = "";
                        tomail1 = (tomail[i]).ToString();
                        if (tomail1 != "")
                        {
                            classreminder.insertTaguser(id, tomail1.ToString().Trim(), "U");
                        }
                    }
                }              
                
            }
        }
    }

	//this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
    public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE ECICalendarEvent_Test SET event_start=@event_start, event_end=@event_end, all_day=@all_day WHERE event_id=@event_id", con);
        cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = start;
        cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = end;
        cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
        cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = allDay;

        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        classproduct.deleteproduct(id);
    }

	//this method adds events to the database
    public static int addEvent(CalendarEvent cevent,string username)
    {
        //add event to the database and return the primary key of the added event row

        //insert
        //SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("INSERT INTO ECICalendarEvent_Test(title, description, event_start, event_end, all_day) VALUES(@title, @description, @event_start, @event_end, @all_day)", con);
        //cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
        //cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
        //cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
        //cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
        //cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

        int key = 0;

        //using (con)
        //{
        //    con.Open();
        //    cmd.ExecuteNonQuery();

        //    //get primary key of inserted row
        //    cmd = new SqlCommand("SELECT max(event_id) FROM ECICalendarEvent_Test where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end AND all_day=@all_day", con);
        //    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
        //    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
        //    cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
        //    cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
        //    cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

        //    key = (int)cmd.ExecuteScalar();
        //}
        DataTable ds = classproduct.gettopidproduct();
        int topid = Convert.ToInt32(ds.Rows[0][0]);
        string rfrom="", rto="", rtime="", to="";
        cevent.title = stopword(cevent.title.ToString());
        //cevent.title = commonclass.GetSafeSearchString(cevent.title);
        //key = Convert.ToInt32(classproduct.inserttextpost(cevent.title, (Convert.ToInt32(topid) + 1).ToString(), cevent.description, 'S', 'Y', username, (cevent.start).ToString(), (cevent.end).ToString()));
		 key = Convert.ToInt32(classproduct.inserttextpost(cevent.title, (Convert.ToInt32(topid) + 1).ToString(), cevent.description, 'H', 'N', username, (cevent.start).ToString(), (cevent.end).ToString()));
		
        if(key>0)
        {
           
            DataTable dstxt = classproduct.gettopidproduct();
            int topid1 = Convert.ToInt32(dstxt.Rows[0][0]);
            //classproduct.addcategorytoproduct((Convert.ToInt32(topid1)),28);
			classproduct.addcategorytoproduct((Convert.ToInt32(topid1)),60);
            //classproduct.addcategorytoproduct((Convert.ToInt32(topid1)), Convert.ToInt32(ConfigurationManager.AppSettings["EventCatID"].ToString().Trim()));
            if (cevent.rtime != "" && cevent.rtime != "null")
            {
                if (cevent.ddlrtime.Trim() == "AM" || cevent.ddlrtime.Trim() == "am")
                {
                    rtime = cevent.rtime.Trim()  + " " + "AM";
                }
                else
                {
                    rtime = cevent.rtime.Trim() + " " + "PM";
                }
            }
            //if(cevent.rfrom!="" && cevent.rfrom!="null")
            //{
            //    if (cevent.ddlrfrom.Trim() == "A" || cevent.ddlrfrom.Trim() == "a")
            //    {
            //        rfrom = cevent.rfrom.Trim()+ " " + "AM";
            //    }
            //    else
            //    {
            //        rfrom = cevent.rfrom.Trim()+ " " + "PM";
            //    }
            //}
            //if (cevent.rto != "" && cevent.rto != "null")
            //{
            //    if (cevent.ddlrto.Trim() == "A" || cevent.ddlrto.Trim() == "a")
            //    {
            //        rto = cevent.rto.Trim()+ " " + "AM";
            //    }
            //    else
            //    {
            //        rto = cevent.rto.Trim() + " " + "PM";
            //    }
            //}
            string repeat = "N";
            //repeat = cevent.repeat.Trim();
            //if (repeat.Trim() =="Y")
            //{
            //    classreminder.insertreminder((Convert.ToInt32(topid1)), rtime.ToString(), "U", cevent.repeat, rfrom.ToString(), rto.ToString(), Convert.ToInt32(cevent.inttime), cevent.inttype);
            //    classreminder.insertTaguser((Convert.ToInt32(topid1)), username.ToString().Trim(), "U");
            //}
            //else
            //{
                if (cevent.rtime.ToString() != "" && cevent.rtime.ToString() != "null" && cevent.rtime.Length>0)
                {
                    classreminder.insertreminder(username.ToString().Trim(),(Convert.ToInt32(topid1)), rtime.ToString(), "U", "N", "", "", 0, "");
                    classreminder.insertTaguser((Convert.ToInt32(topid1)), username.ToString().Trim(), "U");
                    to = cevent.hidden.ToString().Trim();
                    string[] tomail = to.Split(',');
                    for (int i = 0; i < tomail.Length; i++)
                    {
                        string tomail1 = "";
                        tomail1 = (tomail[i]).ToString();
                        if (tomail1 != "")
                        {
                            classreminder.insertTaguser((Convert.ToInt32(topid1)), tomail1.ToString().Trim(), "U");
                        }
                    }
                }
            //}
            //classproduct.addcategorytoproduct((Convert.ToInt32(topid1)),31); // online id 31
        }
        return key;
    }
    public static string stopword(string rword)
    {
        DataTable sw = classproduct.replacestopword();
        string desc2 = rword;
        for (int j = 0; j < sw.Rows.Count; j++)
        {
            desc2 = desc2.ToString();
            string rword1;
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            rword1 = textInfo.ToTitleCase(sw.Rows[j]["stopword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(rword1, sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString().Trim(), sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ',', sw.Rows[j]["replaceword"].ToString() + ',');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ';', sw.Rows[j]["replaceword"].ToString() + ';');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '.', sw.Rows[j]["replaceword"].ToString() + '.');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ':', sw.Rows[j]["replaceword"].ToString() + ':');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "&nbsp;", sw.Rows[j]["replaceword"].ToString() + "&nbsp;");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "!", sw.Rows[j]["replaceword"].ToString() + "!");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "@", sw.Rows[j]["replaceword"].ToString() + "@");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "#", sw.Rows[j]["replaceword"].ToString() + "#");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '*', sw.Rows[j]["replaceword"].ToString() + '*');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '_', sw.Rows[j]["replaceword"].ToString() + '_');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '&', sw.Rows[j]["replaceword"].ToString() + '&');
            desc2 = desc2.ToString().Replace('(' + sw.Rows[j]["stopword"].ToString() + ')', '(' + sw.Rows[j]["replaceword"].ToString() + ')');
            desc2 = desc2.ToString().Replace('{' + sw.Rows[j]["stopword"].ToString() + '}', '{' + sw.Rows[j]["replaceword"].ToString() + '}');
            desc2 = desc2.ToString().Replace('[' + sw.Rows[j]["stopword"].ToString() + ']', '[' + sw.Rows[j]["replaceword"].ToString() + ']');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '$', sw.Rows[j]["replaceword"].ToString() + '$');
        }
        return desc2;
    }
}
