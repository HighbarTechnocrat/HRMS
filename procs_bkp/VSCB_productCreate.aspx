<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="VSCB_productCreate.aspx.cs" 
    Inherits="procs_VSCB_productCreate" EnableViewState="true" ValidateRequest="false"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
            /*overflow: unset;*/
        }

        .Dropdown {
            border-bottom: 2px solid #cccccc;
            /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .textboxBackColor {
            background-color: cadetblue;
            color: aliceblue;
            text-align:right;
        }

        .textboxAlignAmount {
           
            text-align:right;
        }


        .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }

        .POWOContentTextArea {
            width: 783px !important;
            height: 400px !important;
            overflow: auto;
        }

        .LableName {
            color: #F28820;
            font-size: 16px;
            font-weight: normal;
            text-align: left;
        }

        .boxTest {
            BORDER-RIGHT: black 1px solid;
            BORDER-TOP: black 1px solid;
            BORDER-LEFT: black 1px solid;
            BORDER-BOTTOM: black 1px solid;
            BACKGROUND-COLOR: White;
        }

        .GoalDecriptionTextArea {
            width: 722px !important;
            height: 250px !important;
        }
        .txtRemarks {
            width: 623px;
            height: 77px;
        }

         .BtnShow {
            color: blue !important;
            background-color: transparent;
            text-decoration: none;
            font-size: 13px !important;
        }

            .BtnShow:visited {
                color: blue !important;
                background-color: transparent;
                text-decoration: none;
            }

            .BtnShow:hover {
                color: red !important;
                background-color: transparent;
                text-decoration: none !important;
            }


    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />




    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="Product Create"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <div>
                    <span>
                          <a href="vscb_index.aspx" class="aaaa"><%=System.Configuration.ConfigurationManager.AppSettings["VSCBPageTitle"]%></a>
                    </span>
                </div>
                <div class="edit-contact">

                    <ul id="editform" runat="server" visible="true">
                        <li class="trvl_type">
                              <span>Product List</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="lstproductName" CssClass="DropdownListSearch" Width="255px" AutoPostBack="true" OnSelectedIndexChanged="lstproductName_SelectedIndexChanged">
                            </asp:DropDownList>
                           
                        </li>
                        <li class="trvl_date">
                            <span runat="server" id="SpanProductName" visible="false">Product Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtproductName" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                            
                        </li>
                        <li class="trvl_date">
                        </li>

                        <li class="trvl_date">
                            <br />
                            <span>Unit of Measurement</span>&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:DropDownList runat="server" ID="LStUNitOfMeasurement" AutoPostBack="true" CssClass="DropdownListSearch" Width="255px" OnSelectedIndexChanged="LStUNitOfMeasurement_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                         <li class="trvl_date">
                              <span runat="server" id="SpanUnitofMeasurement" visible="false">Unit of Measurement Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="Txt_UnitofMeasurement" runat="server" MaxLength="40" Visible="false"></asp:TextBox>
                        </li>
                         <li class="trvl_date">
                        </li>
                        <li class="trvl_date">
                            <br />
                               <span>HSN SAC Code</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtHSNSACCode" runat="server" MaxLength="20"></asp:TextBox>
                            </li>
                            <li class="trvl_date">
                            <span>GST Percentage</span> &nbsp;&nbsp;
                            <asp:TextBox AutoComplete="off" ID="txtGST" runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li class="trvl_date">
                            <span>IGST Percentage</span>&nbsp;&nbsp;<br />
                            <asp:TextBox AutoComplete="off" ID="TxtIGST" runat="server" MaxLength="2"></asp:TextBox>
                        </li>

                         <li class="trvl_date">
                          <span>Applicable from Date</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: red">*</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtDate" runat="server"  MaxLength="100"></asp:TextBox>
                             <asp:HiddenField ID="HDDate" runat="server" />
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" Format="dd-MM-yyyy" TargetControlID="txtDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>

                             

                         </li>
                        
                        
                        <li class="trvl_date">
                             
                         </li>
                        <li class="trvl_date">
                        
                        </li>

                        <li class="trvl_date">
                       </li>
                        <li class="trvl_date"></li>
                        <li class="trvl_date"></li>

                            </ul>
                           
                        </div>

                </div>
            </div>
        </div>
   
    <div class="trvl_Savebtndiv">
        <asp:LinkButton ID="trvl_btnSave" runat="server" Text="Submit" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveMultiClick();" OnClick="trvl_btnSave_Click">Submit</asp:LinkButton>
       
        <asp:LinkButton ID="btnback_mng" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" OnClick="btnback_mng_Click">Back</asp:LinkButton>
        
      
        </div>
    <asp:Label runat="server" ID="RecordCount" Style="color: red; font-size: 14px;" Visible="false">Product History</asp:Label>
    <asp:GridView ID="gvMngTravelRqstList" Visible="false" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px"
                         CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false" >
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                        <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <PagerStyle HorizontalAlign="Right" CssClass="paging" />

                        <Columns>
                            <asp:BoundField HeaderText="Product Name"
                                DataField="Item_detail"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                             <asp:BoundField HeaderText="Unit of Measurement"
                                DataField="UOM"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="18%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="HSN SAC Code"
                                DataField="HSN_SAC_Code"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                              <asp:BoundField HeaderText="GST Percentage"
                                DataField="GST_Per"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                            <asp:BoundField HeaderText="IGST Percentage"
                                DataField="IGST_Per"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                              <asp:BoundField HeaderText="Start Date"
                                DataField="Start_Datee"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />  

                            <asp:BoundField HeaderText="End Date"
                                DataField="End_Datee"
                                ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />   
                            
                        </Columns>
                    </asp:GridView>

    
    <asp:HiddenField ID="hdnCondintionId" runat="server" />
    <asp:HiddenField ID="hdVendorID" runat="server" />
    <asp:HiddenField ID="HDVendorCode" runat="server" />
    <asp:HiddenField ID="hdnShortClose_Cancelled" runat="server" />
    <asp:HiddenField ID="hdnIsShMilestoneClick" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="FilePath" runat="server" />

    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {
            $(".DropdownListSearch").select2();
            $("#MainContent_txtPOWO_Content").htmlarea();
            $("#MainContent_txt_POWOContent_description").htmlarea();

            $('#MainContent_gvMngTravelRqstList').gridviewScroll({
                width: 1070,
                height: 600,
                freezesize: 4, // Freeze Number of Columns.
                headerrowcount: 1, //Freeze Number of Rows with Header.
			});
        });


        function SaveMultiClick() {
            try {
                var retunboolean = true;
                var ele = document.getElementById('<%=trvl_btnSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm();
                        
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        

         function Confirm() {
          
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            //alert(confirm_value.value);
            //document.forms[0].appendChild(confirm_value);
            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;

        }

        


         function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }

       function checkDate(sender,args)
         {
           if (sender._selectedDate < new Date())
            {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date(); 
                // set the date back to the current date
               sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
          }


    </script>
</asp:Content>



