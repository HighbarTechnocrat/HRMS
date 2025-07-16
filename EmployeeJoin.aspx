<%@ Page Language="C#"  MasterPageFile="~//InnerMaster.master" AutoEventWireup="true" CodeFile="EmployeeJoin.aspx.cs" Inherits="EmployeeJoin" Title=" OneHR Admin :Employees" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder10" Runat="Server">
    <div class="breadcrumb">
<asp:Label ID="lblName" runat="server"></asp:Label>
        </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<a class="selected" rel="menu1">Employee</a>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<a href='<%=ConfigurationManager.AppSettings["sitepath"]%>EmployeeJoinadd.aspx'>Insert / Update Employee</a>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>

<asp:Content ID="menu1" ContentPlaceHolderID="ContentPlaceHolder5" Runat="Server">
    <script language="javascript" type="text/javascript">
        function jsFunction_Employment() {
            var ddlEmployment = document.getElementById('ContentPlaceHolder5_ddlEmployment');
            var a = ddlEmployment.options[ddlEmployment.selectedIndex].value;
            var b = ddlEmployment.options[ddlEmployment.selectedIndex].text;

            var textbox = document.getElementById('ContentPlaceHolder5_ddlEmploymentId');
            var txt = document.getElementById('ContentPlaceHolder5_ddlEmploymenttexts');
            textbox.value = a;
            txt.value = b;
        }
        function jsFunction_Location() {
            var ddl_Location = document.getElementById('ContentPlaceHolder5_ddl_Location');
            var a = ddl_Location.options[ddl_Location.selectedIndex].value;
            var b = ddl_Location.options[ddl_Location.selectedIndex].text;

            var textbox = document.getElementById('ContentPlaceHolder5_ddl_LocationId');
            var txt = document.getElementById('ContentPlaceHolder5_ddl_Locationtexts');
            textbox.value = a;
            txt.value = b;
        }
        function jsFunction_Department() {
            var ddl_Department = document.getElementById('ContentPlaceHolder5_ddl_Department');
            var a = ddl_Department.options[ddl_Department.selectedIndex].value;
            var b = ddl_Department.options[ddl_Department.selectedIndex].text;

            var textbox = document.getElementById('ContentPlaceHolder5_ddl_DepartmentId');
            var txt = document.getElementById('ContentPlaceHolder5_ddl_Departmenttexts');
            textbox.value = a;
            txt.value = b;
        }
    </script>
<div style="text-align:center">
             <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true">
    </ajaxToolkit:ToolkitScriptManager>

<%--    <div style="border-bottom: 1px solid #cccccc; padding-bottom: 5px; float: left; width: 100%;">
        <div class="bannersearch" id="attrgr" runat="server">--%>
            <table width="100%">
                <tr>
                    <td> Search By Employee name: </td>
                    <td>
                        <asp:TextBox ID="txttitle" autoComplete="Off" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special characters are not allowed" CssClass="error_msg" ControlToValidate="txttitle" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 ]+$" ValidationGroup="valgrp" ForeColor="Red"></asp:RegularExpressionValidator>
                    </td>
                    <td> Employment Type: </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlEmployment" onchange="jsFunction_Employment();" /><br/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Employment"
                            ControlToValidate="ddlEmploymenttexts" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
			            <asp:TextBox ID="ddlEmploymentId" runat="server" type="hidden"></asp:TextBox>
                        <asp:TextBox ID="ddlEmploymenttexts"  runat="server" type="hidden"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td> Location: </td>
                    <td>
                        <asp:DropDownList ID="ddl_Location"   runat="server" Width="220px" onchange="jsFunction_Location();"> 
                        </asp:DropDownList><br/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Location"
                            ControlToValidate="ddl_Locationtexts" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
			            <asp:TextBox ID="ddl_LocationId" runat="server" type="hidden" ></asp:TextBox>
                        <asp:TextBox ID="ddl_Locationtexts"  runat="server" type="hidden" ></asp:TextBox>
			        </td>
                    <td> Department: </td>
                    <td>
                        <asp:DropDownList ID="ddl_Department"   runat="server" Width="220px" onchange="jsFunction_Department();"> 
                        </asp:DropDownList><br/>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Select Department"
                            ControlToValidate="ddl_Departmenttexts" Display="Dynamic" ForeColor="Red" ValidationGroup="EditNews"></asp:RequiredFieldValidator>
			            <asp:TextBox ID="ddl_DepartmentId" runat="server" type="hidden" ></asp:TextBox>
                        <asp:TextBox ID="ddl_Departmenttexts"  runat="server" type="hidden" ></asp:TextBox>
			        </td>
                </tr>
                <tr>
                    <td colspan="4">      
                    <div class="searchbtn" style=" margin-top: -5px;">
                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                    ToolTip="Search" /></div>
                    </td>
                </tr>
            </table>
<%--        </div>
    </div>--%>

<asp:Label ID="lblmsg" runat="server"></asp:Label></div>
<table align="center" width="80%">
<tr>
    <td width="70%">
        <asp:Label ID="lblmainmsg" runat="server" ></asp:Label>
        <asp:Label ID="lblmainmsg1" Text="You searched for" runat="server" Style="text-align: center" Visible="false"></asp:Label>
        &nbsp;
        <asp:Label ID="lblmessage1" ForeColor="red" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblerror" runat="server"></asp:Label>
    </td>
    <td width="30%" align="right">
        <asp:Button id="btnadd" runat="server" Width="137px" Text="Create New Entry" OnClick="btnadd_Click"></asp:Button>
    </td>
    <td width="30%" align="right">
        <asp:Button id="Btn_Excel" runat="server" Width="137px" Text="EMP Details to Excel" OnClick="Btn_Excel_Click"></asp:Button>
    </td>
</tr>
</table>
<div class="productsearch-gridview" align="center">


<asp:GridView id="gvproject" runat="server"  CellPadding="4" BorderColor="white" DataKeyNames="Emp_id" 
    AutoGenerateColumns="False" Width="100%" OnRowCommand="gvproject_RowCommand" OnPageIndexChanging="gvproject_PageIndexChanging" 
    AllowPaging="True" AllowSorting="true" OnSorting="gvproject_Sorting">
        <Columns>
            <asp:BoundField DataField="Location_Code" HeaderText="Loc Code" SortExpression="Location_Code">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Location_name" HeaderText="Location Name" SortExpression="Location_name">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>            
            <asp:BoundField DataField="Emp_type" HeaderText="Employment Type" SortExpression="Emp_type">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Emp_Code" HeaderText="Emp Code"  SortExpression="Emp_Code">
                <ItemStyle HorizontalAlign="Center"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" SortExpression="Emp_Name">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Joining Date" SortExpression="emp_doj">
                <ItemTemplate>
                 <asp:Label ID="lblsdate" runat="server" Text='<%# (Eval("emp_doj","{0:dd/MMM/yyyy}")) %>'></asp:Label>
                </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="mobile" HeaderText="Mobile no.">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="emp_status" HeaderText="Status" SortExpression="emp_status">
                <ItemStyle HorizontalAlign="Left"  />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>                        
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                     <asp:LinkButton ID="Button1" runat="server" CommandArgument='<% #Eval("Emp_id") %>' CommandName="editItem" ToolTip="Edit" >
                      <asp:Image ID="imgeditmapt" runat="server" ToolTip="Edit" ImageUrl="~/images/icon/edit.png" /></asp:LinkButton>
                </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Delete" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="Button2" runat="server" CommandArgument='<% #Eval("Emp_id") %>' CommandName="DeleteItem" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                                        <asp:Image ID="imgedeletemap" runat="server" ToolTip="Delete" ImageUrl="~/images/icon/delete.png" /></asp:LinkButton>
                </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings NextPageText="&amp;lt;" Mode="NumericFirstLast"  />
    <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="pagination" />
    </asp:GridView>   
    
    <br />
    </div>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" Runat="Server">
</asp:Content>

<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" Runat="Server">
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="ContentPlaceHolder8" Runat="Server">
</asp:Content>

<asp:Content ID="Content12" ContentPlaceHolderID="ContentPlaceHolder12" Runat="Server">


* Click on "Add New Entry" button to add new Employee entry.<br/>
* Fill the form & click on "Save" button to save the entry.<br/>
* Click on the <b>Search</b> Button to search the Employee on the basis of Name.<br>
<br />


</asp:Content>
