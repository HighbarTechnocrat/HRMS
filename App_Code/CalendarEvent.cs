using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalendarEvent
/// </summary>
public class CalendarEvent
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public bool allDay { get; set; }

    // new code for reminder
    public string rtime { get; set; }
    public string ddlrtime { get; set; }
    public string repeat { get; set; }
    public string rfrom { get; set; }
    public string ddlrfrom { get; set; }
    public string rto { get; set; }
    public string ddlrto { get; set; }
    public string inttype { get; set; }
    public string inttime { get; set; }
    public string taguser { get; set; }
    public string tagusername { get; set; }
    public string taguseremail { get; set; }
    public string hidden { get; set; }
    public string ltscript { get; set; }

    public string aadhdval { get; set; }
    public string addhdarray { get; set; }
}
