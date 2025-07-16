<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="procs_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../dist/jquery-3.2.1.min.js"></script>
    <script src="../dist/select2.min.js"></script>
    <link href="../dist/select2.min.css" rel="stylesheet" />
    
    
   
</head>
    <script type="text/javascript">
        // $(function() {
        $(document).ready(function () {
            //$("#MainContent_lstInterviewerOne").select2();
            $("#selUser").select2();
            // $("#MainContent_txtJobDescription").htmlarea(); // Initialize jHtmlArea's with all default values           
            //window.setTimeout(function() { $("form").submit(); }, 3000);                      
        });
    </script>
<body>
    <form id="form1" runat="server">
          <select id='selUser' style='width: 200px;'>
                                    <option value='0'>-- Select User --</option>
                                    <option value='1'>Yogesh singh</option>
                                    <option value='2'>Sonarika Bhadoria</option>
                                    <option value='3'>Anil Singh</option>
                                    <option value='4'>Vishal Sahu</option>
                                    <option value='5'>Mayank Patidar</option>
                                    <option value='6'>Vijay Mourya</option>
                                    <option value='7'>Rakesh sahu</option>
                             </select>
    </form>
</body>
</html>
