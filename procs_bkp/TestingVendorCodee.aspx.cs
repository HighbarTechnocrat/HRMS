using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AC = Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;


public partial class procs_TestingVendorCodee : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static bool fileIsOpen(string path)
    {
        System.IO.FileStream a = null;
        try
        {
            a = System.IO.File.Open(path,
            System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
            return false;
        }
        catch (System.IO.IOException ex)
        {
            return true;
        }



        finally
        {
            if (a != null)
            {
                a.Close();
                a.Dispose();
            }
        }
    }
    private  void FindReplace()
    {
        var app = new AC.Application();
        string documentLocation = @"D:\TestingDoc.docx";
        
       // string documentLocation = @"E:\reports\Word File Check\TEST4.docx";
        var isDocOpened = fileIsOpen(documentLocation);
        if (isDocOpened == false)
        {
            var doc = app.Documents.Open(documentLocation);
            try
            {
                string findText = "Harshad";
                string replaceText = "TEST 234";
                var range = doc.Range();
                range.Find.Execute(FindText: findText, Replace: AC.WdReplace.wdReplaceAll, ReplaceWith: replaceText);
                var shapes = doc.Shapes;
                foreach (AC.Shape shape in shapes)
                {
                    var initialText = shape.TextFrame.TextRange.Text;
                    var resultingText = initialText.Replace(findText, replaceText);
                    shape.TextFrame.TextRange.Text = resultingText;
                }
                Process process = new Process();
                process.StartInfo.FileName = documentLocation;
                process.Start();
                doc.Save();
                doc.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }
            catch (Exception ex)
            {
                //doc.Save();
                //doc.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }
        }
        else
        {
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //string documentLocation = @"D:\TestingDoc.docx";
        //FindReplace(documentLocation, "Harshad", "Dhande");
        //Process process = new Process();
        //process.StartInfo.FileName = documentLocation;
        //process.Start();

        FindReplace();

        //WordFileToRead.SaveAs(Server.MapPath(WordFileToRead.FileName));
        //object filename = Server.MapPath(WordFileToRead.FileName);
        //Microsoft.Office.Interop.Word.ApplicationClass AC = new Microsoft.Office.Interop.Word.ApplicationClass();
        //Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
        //object readOnly = false;
        //object isVisible = true;
        //object missing = System.Reflection.Missing.Value;
        //try
        //{
        //    doc = AC.Documents.Open(ref filename, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref isVisible, ref missing, ref missing, ref missing);
        //    WordFileText.Text = doc.Content.Text;

        //}
        //catch (Exception ex)
        //{

        //}


    }

    private void FindReplace(string documentLocation, string findText, string replaceText)
    {
        var app = new Microsoft.Office.Interop.Word.Application();
        var doc = app.Documents.Open(documentLocation);
        var range = doc.Range();

        range.Find.Execute(FindText: findText, Replace: AC.WdReplace.wdReplaceAll, ReplaceWith: replaceText);

        var shapes = doc.Shapes;
        foreach (AC.Shape shape in shapes)
        {
            var initialText = shape.TextFrame.TextRange.Text;
            var resultingText = initialText.Replace(findText, replaceText);
            shape.TextFrame.TextRange.Text = resultingText;
        }

        doc.Save();
        doc.Close();
        Marshal.ReleaseComObject(app);
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        FindReplace();
        // // Open the template
        // Application word = new Application();
        //// string documentLocation = @"D:\TestingDoc.docx";
        // const string TemplateFileName = @"D:\HRMS\hrms\VendorBilling\VSCB\TestingDoc.docx"; 
        // const string ResultFileName = @"D:\TestingDocc.docx";

        // var wordApp = new Application();
        // var doc = wordApp.Documents.Open(TemplateFileName, false, true);
        // var status = doc.Content.Find.Execute(FindText: "{NAME}",
        //                             MatchCase: false,
        //                             MatchWholeWord: false,
        //                             MatchWildcards: false,
        //                             MatchSoundsLike: false,
        //                             MatchAllWordForms: false,
        //                             Forward: true, //this may be the one
        //                             Wrap: false,
        //                             Format: false,
        //                             ReplaceWith: "Testing Purpose",
        //                             Replace: WdReplace.wdReplaceAll
        //                             );

        // doc.SaveAs(ResultFileName);
        // doc.Close();
    }
}