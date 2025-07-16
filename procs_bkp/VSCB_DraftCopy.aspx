<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VSCB_DraftCopy.aspx.cs" Inherits="procs_VSCB_DraftCopy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
       
        .tr-table {

            font-family:'Trebuchet MS';
            font-size:9.0pt;
        }
        .td-table {

            padding:0in 5.4pt 0in 5.4pt;
           
        }

        .th-table {

            padding:0in 5.4pt 0in 5.4pt;font-family:'Trebuchet MS';font-size:10.0pt;
            text-align:center;
        }
        .div_style {

            border-style: solid; 
            border-width: thin; 
            width:700px; 
            margin-left:30px; 
            margin-top:30px; 
            border-top-color: #000000;

        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div  class="div_style">
           
       <table cellpadding="0" cellspacing="0" border="1">
           <tr>
         <td style="width:100.0pt;text-align:right; padding-bottom:10px; padding-top:20px" colspan="3"><img src="../images/HBTLogo.png"  style="width:190px;height:45px"/></td>
         </tr>
         <tr>
         <td style="width:100.0pt;height:30px" colspan="3" class="th-table"><b> <asp:Label ID="lblHPOType" runat="server" Text=""></asp:Label> </b></td>
         </tr>
         <tr class="tr-table">                       
         <td rowspan="3" class="td-table">Invoice To <b> <br/><br/>HIGHBAR TECHNOCRAT LIMITED </b> <br/> Unit No 1409, Empire Tower,14 th Floor <br/>D Wing, Gut No 31,Unit SB-1402,<br/>Airoli, Navi Mumbai-400708.<br/>GSTIN/UIN: 27AABCO4311L1ZI<br/>State Name: Maharashtra, Code:27<br/>CIN: U72100MH2010PLC210078<br/>
         </td>
         <td class="td-table">Serial No. <br/><br/> <asp:Label ID="lblSerialno" runat="server" Text=""></asp:Label> <br/></td>
         <td class="td-table">Dated <br/><br/><asp:Label ID="lblDated" runat="server" Text=""></asp:Label></td>
         </tr>
         <tr class="tr-table">                    
         <td>  </td>
         <td class="td-table"> Mode/Terms of Payment 
             <br />
             <br/><b><asp:Label ID="lblModeofpayment" runat="server" Text=""></asp:Label> </b></td>
         </tr>
         <tr class="tr-table">
         <td class="td-table">Reference No. & Date 
             <br />
             <br/><b><asp:Label ID="lblReferenceNo" runat="server" Text=""></asp:Label> </b><br/></td>
         <td class="td-table"> Other References <br/><br/><b></b><br/></td>
          </tr>
         <tr class="tr-table">
         <td style="width:180.0pt;vertical-align:top;" class="td-table">Consignee (Ship to) 
             <br />
             <br/><asp:Label ID="lblshippingAddress" runat="server" Text=""></asp:Label></td>
         <td colspan="2" style="width:330.0pt;vertical-align:top;"class="td-table">Supplier (Bill From) 
             <%--<br />--%>
             <br />
             <br/>
         <b><asp:Label ID="lblVendorname" runat="server" Text=""></asp:Label> </b><br/> <br/><asp:Label ID="lblvendorAddress" runat="server" Text=""></asp:Label><br/><br/><br/>
          </td>
         </tr>
       </table> 
            <span id="SpanIDmilestonedetail" runat="server" style="width:700px"> </span>
            <span id="SpanIDHTMLContainDetail" runat="server" style="width:700px"> </span>

        </div>  
    </form>
</body>
</html>
