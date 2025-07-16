<%@ WebHandler Language="C#" Class="MyJsonRes" CodeBehind="MyJsonRes.ashx.cs" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Security;

public class MyJsonRes : IHttpHandler,IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string username = context.Request.QueryString["id"].ToString();
        List<int> idList1 = new List<int>();
        List<ImproperCalendarEvent> taskList = new List<ImproperCalendarEvent>();
        foreach(CalendarEvent cevent in EventDAOS.getMyEvents(username))
        {
            taskList.Add(new ImproperCalendarEvent
            {
                id = cevent.id,
                title=cevent.title,
                
                //SAGAR commented below line for stop adding 1 day when update the event 4jan2018
                //start=Convert.ToDateTime(cevent.start).AddDays(1),
                
                //SAGAR added below line for stop adding 1 day when update the event 4jan2018
                start = Convert.ToDateTime(cevent.start),
                
                end=Convert.ToDateTime(cevent.end),
                description=cevent.description,
                allDay=cevent.allDay,
                taguser=cevent.taguseremail,
                rtime=cevent.rtime,
                ddlrtime = cevent.ddlrtime
            });
            idList1.Add(cevent.id);
        }
        context.Session["idList1"] = idList1;

        System.Web.Script.Serialization.JavaScriptSerializer os = new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJ = os.Serialize(taskList);

            context.Response.Write(sJ);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}