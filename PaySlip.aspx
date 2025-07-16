<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="PaySlip.aspx.cs" Inherits="PersonalDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!-- Stylesheets -->
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/bootstrap.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/revolution-slider.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/owl.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/style.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/responsive.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/jquery.mCustomScrollbar.css" />
    <link media="all" type="text/css" rel="stylesheet" href="MAP_IMG/css/morris.css" />
    <link href='MAP_IMG/css/font.css' rel='stylesheet' type='text/css' />
    <link href="<%=ReturnUrl("css") %>includes/mywall.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        body {
            background: #ddd;
            margin: 0;
            padding: 0;
        }

        #container {
            width: 950px;
            margin: 0 auto;
            background: #fff;
            padding: 200px 25px;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        td {
            border: 0px solid #eee;
            padding: 10px;
            width: 200px;
        }

        @media only screen and (max-width:600px) and (min-width:480px) {
            #container {
                width: auto;
                margin: 0;
                padding: 25px 0;
            }

            table {
                display: block;
            }

            td {
                display: block;
                padding: 5px 0;
                border: none;
            }

                td img {
                    display: block;
                    margin: 0;
                    width: 100%;
                    max-width: none;
                }
        }

        @media screen and (max-width: 480px) {
            #container {
                width: auto;
                margin: 0;
                padding: 25px 0;
            }

            table {
                display: block;
            }

            td {
                display: block;
                padding: 5px 0;
                border: none;
            }

                td img {
                    display: block;
                    margin: 0;
                    width: 100%;
                    max-width: none;
                }
        }

        img {
            max-width: 100%;
            height: auto;
            width: auto\\9; /* ie8 */
        }

        @media all and (min-width: 480px) {
            .deskContent {
                display: block;
            }

            .phoneContent {
                display: none;
            }
        }

        @media all and (max-width: 479px) {
            .deskContent {
                display: none;
            }

            .phoneContent {
                display: block;
            }
        }


        .mydropdownlist {
            color: #0c0808;
            font-size: 12px;
            padding: 5px 10px;
            border-radius: 5px;
            background-color: #fff; /*#cc2a41;*/
            font-weight: normal;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btncls {
            background-color: #FEBF39 !important;
            width: 125px;
            display: block !important;
        }

        .anch {
            background-color: #cfcfec;
            color: white;
            padding: 0.3em 1.6em;
            text-decoration: none;
            text-transform: uppercase;
        }

            .anch a:hover {
                background-color: #555;
            }

            .anch a:active {
                background-color: black;
            }

            .anch a:visited {
                background-color: #ccc;
            }


        .LockOn {
            display: block;
            visibility: visible;
            position: absolute;
            z-index: 999;
            top: 0px;
            left: 0px;
            width: 105%;
            height: 105%;
            background-color: white;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=75);
            opacity: 0.75;
            font-size: large;
            color: blue;
            font-style: italic;
            font-weight: 400;
            /*background-image: url("../images/Gallery/Insidepages/loader.gif");*/
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }
    </style>
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <asp:Panel ID="pnlsearch" runat="server" Visible="true" DefaultButton="lnksearch">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblcatname" runat="server"></asp:Label>
                    </span>
                    <span class="searchposts">
                        <asp:TextBox ID="txtsearch" Visible="false" placeholder="Search by Year" onkeydown="return ((event.keyCode>=48 && event.keyCode<=57 ) || event.keyCode==8 || ( event.keyCode == 46) || (event.keyCode>=96 && event.keyCode<=105 ));" runat="server" MaxLength="4" CssClass="txtbox" ToolTip="Enter year to search Document."></asp:TextBox>
                        <asp:TextBox ID="txtsdate" runat="server" ReadOnly="true" CssClass="txtbox1" placeholder="Start-Date" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtedate" runat="server" ReadOnly="true" CssClass="txtbox1" placeholder="End-Date" Visible="false"></asp:TextBox>
                        <asp:LinkButton ID="lnksearch" Visible="false" runat="server" OnClick="lnksearch_Click" CssClass="searchpostbtn" ToolTip="Search Post" ValidationGroup="valgrp"><i class="fa fa-search"></i></asp:LinkButton>
                        <asp:LinkButton ID="lnkreset" Visible="false" runat="server" OnClick="lnkreset_Click" CssClass="searchpostbtn" ToolTip="Reset Search"><i class="fa fa-undo" ></i></asp:LinkButton>
                        <asp:RegularExpressionValidator ID="regsearch" runat="server" ErrorMessage="Special characters are not allowed" CssClass="formerror" ControlToValidate="txtsearch" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special characters are not allowed. Except slash" CssClass="formerror" ControlToValidate="txtsdate" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 /]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special characters are not allowed. Except slash" CssClass="formerror" ControlToValidate="txtedate" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 /]+$" ValidationGroup="valgrp"></asp:RegularExpressionValidator>
                    </span>

                </div>


                <span>

                    <a id="gobackbtn" runat="server" class="aaa"></a>

                </span>
                  <div id="coverScreen" class="LockOn"  >
                      <asp:Image ID="img" runat="server" ImageUrl="~/images/Gallery/Insidepages/loader.gif" style="width:400px;height:400px;margin-left:350px;"/>
                </div>
                <div style="width: 10% ;">
                    <div id="type" runat="server" style="width: 10%!important;display: none; ">

                        <%--display: table-cell;padding-right:150px;--%>

                        <span style="padding: 20px 20px 15px 20px;">Select Year: </span>
                        <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true" CssClass="mydropdownlist" Style="width: 150px;">
                            <%--<asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>--%>
                        </asp:DropDownList>


                    </div>
                    <div id="dvmonth" runat="server" style="width: 10%!important; display: none; padding-right: 50px;">
                        <span style="padding: 20px 20px 15px 20px;">Select Month: </span>
                        <asp:DropDownList ID="ddlMonth" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" CssClass="mydropdownlist" Style="width: 150px;">
                        </asp:DropDownList>
                    </div>

                    <div runat="server" style="width: 10%; display:none ;" > <%-- table-cell--%>
                        <span style="padding: 20px 20px 15px 20px !important;"></span>
                        <asp:Button ID="btnSearch" runat="server"  OnClick="btnSearch_Click"
                            Style="text-align: center; margin-right: 650px; width: 150px; height: 30px!important;" class="btncls" Text="Search" ClientIDMode="Static" />
                        <%--OnClientClick="javascript: return ValidateSearch();"--%>

                    </div>
                </div>
               

                <div style="width: 10%; margin-top: 50px;margin-right:20px;">
                    <%-- <asp:Repeater ID="rptanchor" runat="server" OnItemDataBound="rptanchor_ItemDataBound">--%>
                    <asp:DataList ID="rptanchor" runat="server" RepeatColumns="4"   OnItemDataBound="rptanchor_ItemDataBound" RepeatDirection="Horizontal">
                        <ItemTemplate>

                            
                            <a id="anchrPF"  runat="server" class="anch" style="margin-top: 2px;width:150px;"  title="Click to view file" onclick="anchrPF_onclick" >
                                 <asp:Image ID="img" runat="server" ImageUrl="~/images/pdfIcon.jpg" Width="25px" Height="22px" style="padding-right:8px;align-self:center;" /><%# DataBinder.Eval(Container, "DataItem.yearname") %>
                               
                            </a> 
                            <asp:Label ID="lblmonthvalue" runat="server" Visible="false" Text='<%# Eval("MonthValue")%>'></asp:Label>
                            <asp:Label ID="lblyear" runat="server" Visible="false" Text='<%# Eval("year")%>'></asp:Label>
                            <asp:Label ID="lblyrname" runat="server" Visible="false" Text='<%# Eval("yrvalue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:DataList>

                    <%--  </asp:Repeater>--%>
                </div>

               <%--  <div class="userpostscats">
                    <asp:Label ID="lblmsgnew" Text="No files found" Style="margin: 67px 0 50px !important;" runat="server" Visible="false"></asp:Label>
                </div>--%>

                <div class="userpostscats">
                    <asp:Label ID="lblmsg"  Style="margin: 67px 0 50px !important;" runat="server" Visible="false"></asp:Label>
                </div>

                <div id="dvfrmame" runat="server" style="width: 100%; height: 500px;">
                    <iframe id="ifrmPDF" runat="server" name="ifrmPDF" width="100%" height="100%"></iframe>

                </div>

              

            </asp:Panel>
            <asp:Panel ID="pnlpst" runat="server" Visible="false">
                <div class="comments-summerytitle">
                    <div class="commentsdiv mywall">
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">--%>
                        <%-- <ContentTemplate>--%>
                        <div class="container">
                            <asp:DataList ID="rptwall" runat="server" RepeatLayout="flow" RepeatDirection="Horizontal" Visible="false" >
                                <ItemTemplate>
                                    <div class="deskContent  col-xs-6 col-sm-4 col-md-3 col-lg-2 ">
                                        <asp:LinkButton Style="background: #febf39; padding: 9px 7px;" ID="lnkView" runat="server" OnClick="View" CommandArgument='<%# Eval("DocPathNM") %>'><%--<img src="Images/Gallery/Insidepages/pdf.jpg" class="img-responsive">--%>
                                    <%--<p><%# DataBinder.Eval(Container, "DataItem.MonthNM") %>-<%# DataBinder.Eval(Container, "DataItem.year") %></p>--%>
                                    <p><%# DataBinder.Eval(Container, "DataItem.yearname") %></p></asp:LinkButton>
                                        <br />
                                    </div>
                                    <div class="phoneContent col-xs-6 col-sm-4 col-md-3 col-lg-2 ">
                                        <asp:LinkButton Style="background: #febf39; padding: 9px 7px;" ID="LinkButton1" runat="server" OnClick="ViewOne" CommandArgument='<%# Eval("DocPathNM") %>'><%--<img src="Images/Gallery/Insidepages/pdf.jpg" class="img-responsive">--%>
                                   <%--<p><%# DataBinder.Eval(Container, "DataItem.MonthNM") %>-<%# DataBinder.Eval(Container, "DataItem.year") %></p>--%>
                                     <p><%# DataBinder.Eval(Container, "DataItem.yearname") %></p></asp:LinkButton>
                                        <br />
                                    </div>

                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div>
                            <asp:Literal ID="ltEmbed" runat="server" />
                        </div>
                        <%-- </ContentTemplate>--%>
                        <%-- </asp:UpdatePanel>    --%>
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ValidateSearch() {


            var e = document.getElementById("ddlYear");
            var year = e.options[e.selectedIndex].value;

            if (year == '0') {

                alert("Please select year..!");
                
                return false;
            }
            return true;

        }

    </script>

    <script type="text/javascript">

        

        $(document).ready(function () {

            // hide loader initially
           
             $("#coverScreen").hide();
            // attach the handler to the button
          //  $('#btnSearch').click(function () {

                //var i = ValidateSearch();

                //if (i == false) {
                //    $("#coverScreen").hide();
                //}
                //else {
                //    $("#coverScreen").show();
                //}

                // provide enough information to the ajax function to make a call
                //$.ajax({
                //    type: "POST",
                //    url: "",
                //    success: function () {
                //        $(".loading-gif").hide();
                //    }
                //});
          //  });
        });


        $('#btnSearch').click(function () {

            var e = document.getElementById("ddlYear");
            var year = e.options[e.selectedIndex].value;

            if (year == '0') {

                alert("Please select year..!");
                $("#coverScreen").hide();
                return false;
            }

            $("#coverScreen").show();
            return true;
            
            
        });
    </script>


</asp:Content>



