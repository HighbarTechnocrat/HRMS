<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="thankyou_card_report_View.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="procs_thankyou_card_report_View" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }

        .edit-contact input:focus {
            border-bottom: 2px solid rgb(51, 142, 201) !important;
        }

        .edit-contact input {
            padding-left: 30px !important;
            width: 83%;
        }

        .edit-contact > ul {
            padding: 0;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
            /*overflow:initial;*/
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

         .dgSupervisor {
			color: White !important;
			background-color: #669999;
			font-weight: bold;
		}
        .dgSupervisorRemove {
			color: White !important;
			background-color: none;
			font-weight: normal;
		}
         textarea#MainContent_txt_remakrs {
            width: 597px !important;
            height: 50px !important;
        }

           .paging a {
            background-color: #C7D3D4;
            padding: 5px 7px;
            text-decoration: none;
            border: 1px solid #C7D3D4;
        }

            .paging a:hover {
                background-color: #E1FFEF;
                color: #00C157;
                border: 1px solid #C7D3D4;
            }

        .paging span {
            background-color: #E1FFEF;
            padding: 5px 7px;
            color: #00C157;
            border: 1px solid #C7D3D4;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="userposts">
        <span>
            <br />
            <asp:Label ID="lblheading" runat="server" Text="Thank You Card Report"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="http://localhost//hrms/procs/ThankyouCard_Index.aspx" class="aaa">Thankyou</a>
    </div>

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
        </div>
    </div>
    <div class="edit-contact">

        <asp:GridView ID="gv_MyProcessedTaskExecuterList" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid"
            BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="50%" EditRowStyle-Wrap="false"
            PageSize="20" AllowPaging="True">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
            <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
            <Columns>

                <asp:TemplateField HeaderText="Send Card Count" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSendThankYouCardCnt" runat="server"  CssClass="BtnShow" CommandArgument='<%# Eval("SendThankYouCard") %>'
                            OnClick="lnkSendThankYouCardCnt_Click"><%# Eval("SendThankYouCard") %>
                    </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Received Card Count" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkReceivedThankYouCardCnt" CssClass="BtnShow" runat="server" CommandArgument='<%# Eval("ReceivedThankYouCard") %>'
                            OnClick="lnkReceivedThankYouCardCnt_Click"><%# Eval("ReceivedThankYouCard") %></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
     </div>

    <br /><br />
    <div class="edit-contact">
        <label id="lblheading_Thankyou" runat="server"></label>
        <asp:GridView ID="gv_ThankyouCard_Send"  DataKeyNames="photo,thankyouid" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid"
    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="80%" EditRowStyle-Wrap="false"
    OnSelectedIndexChanged="gv_ThankyouCard_Send_SelectedIndexChanged" OnRowDataBound="gv_ThankyouCard_Send_RowDataBound" OnPageIndexChanging="gv_ThankyouCard_Send_PageIndexChanging"
    PageSize="7" AllowPaging="True">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
    <RowStyle ForeColor="Black" />
     <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
     
    <Columns>
        <asp:BoundField HeaderText="Thank You Card" DataField="card_sub" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Thank You Card Send To" DataField="send_to" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Send Date" DataField="createdon" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />       
    </Columns>
</asp:GridView>


           <asp:GridView ID="gv_ThankyouCard_Received"  DataKeyNames="photo,thankyouid" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid"
                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="80%" EditRowStyle-Wrap="false"
                OnSelectedIndexChanged="gv_ThankyouCard_Received_SelectedIndexChanged" OnRowDataBound="gv_ThankyouCard_Received_RowDataBound"
                PageSize="7" AllowPaging="True"  OnPageIndexChanging="gv_ThankyouCard_Received_PageIndexChanging">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                <RowStyle ForeColor="Black" />
                <PagerStyle HorizontalAlign = "Right" CssClass = "paging" />
                <Columns>
                    <asp:BoundField HeaderText="Thank You Card" DataField="card_sub" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Thank You Card Received From" DataField="createdby" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Received Date" DataField="createdon" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />                    

                </Columns>
            </asp:GridView>

     

                <br />
               <asp:Image ID="SelectedImage" runat="server" Visible="False" />
              <br /><br />
                 <span id="spnThankYouNote" runat="server" visible="false">Thank You Note</span>
         <br />
                  <asp:TextBox AutoComplete="off" ID="txt_remakrs" runat="server" TextMode="MultiLine" MaxLength="200" Height="200px" Visible="false"></asp:TextBox>
    </div>

    <br />
    <br />



    <asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="HiddenFieldid" runat="server" />

    <asp:HiddenField ID="hflEmpDepartmentID" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflCEO" runat="server" />

    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />


  


</asp:Content>

