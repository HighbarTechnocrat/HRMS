<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestingVendorCode.aspx.cs" Inherits="procs_TestingVendorCode" 
    EnableEventValidation = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="Div1Render" style=" font-size:8px">
      <div>
                    <div style="width:50%">
                        <div style="border-style: inset none none inset; border-color: #000000; width:30%; float:left; border-top-width: thin; border-right-width: inherit; border-bottom-width: inherit; border-left-width: thin;">Invoice To
                         <br />
                            <span>HIGHBAR TECHNOCRAT LIMITED <br />
                                Unit No 1409, Empire Tower,14 th Floor

                            </span>
                        </div>
                        <div style="border-style: inset none inset inset; border-width: thin; border-color:black; width:30%; float:left; ">Serial No.
                            <br /><br />
                            <span>
                                <asp:Label ID="Label1" runat="server" Text="HBT/PO/21-22/AC/1000"></asp:Label>


                            </span>
                        </div>
                        <div style="width:20%;float:left;border-style: inset; border-width: thin; border-color:black;">Dated
                            <br /><br />
                            <span>01-09-2022</span>
                        </div>
                    </div>

                     <div style="width:50%">
                        <div style="width:30%;float:left; border-left-style: inset; border-left-width: thin; border-left-color: #000000;">
                            D Wing, Gut No 31,Unit SB-1402,<br />
                            Airoli, Navi Mumbai-400708.<br />
                            GSTIN/UIN: 27AABCO4311L1ZI<br />
                        </div>
                        <div style="width:30%;float:left;border-style: inset none inset inset; border-width: thin; border-color:black">
                            <br /><br /><br />
                           
                        </div>
                        <div style="width:20%;float:left;border-style: inset; border-width: thin; border-color:black">Mode/Terms of Payment
                            <br /><br />
                            <span>As per Annexure A</span>
                        </div>
                    </div>

                    <div style="width:50%">
                        <div style="width:30%;float:left; border-bottom-style: inset; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: none; border-left-style: inset; border-left-width: thin;">
                            State Name: Maharashtra, Code:27<br />
                            CIN: U72100MH2010PLC210078
                            <br /> <br />
                        </div>
                        <div style="width:30%;float:left;border-style: inset none inset inset; border-width: thin; border-color:black">Reference No. & Date
                            <br /><br />
                            <span>HBT/PO/21-22/AC/1000</span>
                        </div>
                        <div style="width:20%;float:left;border-style: inset; border-width: thin; border-color:black">Other References
                            <br /><br /><br />
                           
                        </div>
                    </div>

                    </div>

                    

                    <div class="trvl_Savebtndiv">
                    <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Approve" ToolTip="Approve" CssClass="Savebtnsve"  
                      OnClick="trvl_btnSave_Click">DownLoad Print</asp:LinkButton>
           </div>
                        

            </div>
    </form>
</body>
</html>
