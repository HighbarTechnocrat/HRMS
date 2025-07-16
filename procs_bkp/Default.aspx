<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" Inherits="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   
    
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     
    <select id='selUser' style='width: 200px;'>
                                    <option value='0'>-- Select User --</option>
                                    <option value='1'>bharat mainkar</option>
                                    <option value='2'>Sonarika Bhadoria</option>
                                    <option value='3'>Anil Singh</option>
                                    <option value='4'>Vishal Sahu</option>
                                    <option value='5'>Mayank Patidar</option>
                                    <option value='6'>Vijay Mourya</option>
                                    <option value='7'>Rakesh sahu</option>
                             </select>
     <script src="../dist/jquery-3.2.1.min.js"></script>       
    <script src="../dist/select2.min.js"></script>
    <link href="../dist/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
       
        $(document).ready(function () {
            //$("#MainContent_lstInterviewerOne").select2();
            $("#selUser").select2();
            
        });
    </script>
    
   
</asp:Content>

