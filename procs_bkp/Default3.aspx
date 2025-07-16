<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
    CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
    
      <select id="city" style="width:400px;">
      <option value='0'>-- Select User --</option>
                                    <option value='1'>Yogesh singh</option>
                                    <option value='2'>Sonarika Bhadoria</option>
                                    <option value='3'>Anil Singh</option>
                                    <option value='4'>Vishal Sahu</option>
                                    <option value='5'>Mayank Patidar</option>
                                    <option value='6'>Vijay Mourya</option>
                                    <option value='7'>Rakesh sahu</option>
      </select>
 
     <script src="https://code.jquery.com/jquery-2.1.1.min.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.1/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.1/js/select2.min.js"></script>
    <script type="text/javascript">
      $(document).ready(function() {
           $("#city").select2({          
        });
      });
    </script>
</asp:Content>

