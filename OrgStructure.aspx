<%@ Page Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="OrgStructure.aspx.cs" Inherits="OrgStructure" %>

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

        <style>
        /*.myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }


        .Dropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

         .grayDropdown {
            border-bottom: 2px solid #cccccc;
            background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;            
            background-color:#ebebe4;
        }

         .grayDropdownTxt
         {            
            background-color:#ebebe4;
            }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;
        }

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;
        }

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;
        }*/

table.google-visualization-orgchart-table {
    width: auto !important;
    vertical-align: middle !important;
    text-align: center !important;
    align-content: center !important;
    /*margin: 0 0 0 44% !important;*/
}
        .google-visualization-orgchart-node-medium {
    font-size: 0.9em;
}

        .google-visualization-orgchart-lineleft {
    border-left: 1px solid #FF0000 !important;
}
.google-visualization-orgchart-linebottom {
    border-bottom: 1px solid #FF0000 !important;
}
.google-visualization-orgchart-lineright {
    border-right: 1px solid #FF0000 !important;
}
.google-visualization-orgchart-linetop {
    border-right: 1px solid #FF0000 !important;
}
        td.google-visualization-orgchart-node.google-visualization-orgchart-node-medium {
    text-align: center;
    vertical-align: middle;
    font-family: Roboto,helvetica;
    cursor: default;
    /*border: 2px solid #b5d9ea;*/
    border: 2px solid #3D1956;
    -moz-border-radius: 5px;
    -webkit-border-radius: 5px;
    -webkit-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
    -moz-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
    background-color: #edf7ff;
    
    /*background: -webkit-gradient(linear, left top, left bottom, from(#edf7ff), to(#3D1956));*/
    background: -webkit-gradient(linear, left top, left bottom, from(#edf7ff), to(#FFFFFF));
}

     /*img{
             border-color: black;
    border-width: 0pt;
    border-style: solid;
    vertical-align: top;
     }*/

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    <%--<script src="http://www.google.com/jsapi"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>

    
    <script>
   
    
    
    </script>
    <script>
        //google.load('visualization', '1', { packages: ['table', 'orgchart'] });
        //google.charts.setOnLoadCallback(draw);
    google.load("visualization", "1", { packages: ["orgchart"] });
    google.setOnLoadCallback(drawChart);
    function drawChart() {
        $.ajax({
            type: "POST",
            url: "OrgStructure.aspx/GetChartData",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Entity');
                data.addColumn('string', 'ParentEntity');
                data.addColumn('string', 'ToolTip');
                for (var i = 0; i < r.d.length; i++) {
                    var employeeId = r.d[i][0].toString();
                    var employeeName = r.d[i][1];
                    var designation = r.d[i][2];
                    var Cnt = r.d[i][4];
                    var EMP_CODE = r.d[i][5];
                    var A_EMP_CODE = r.d[i][6];
                    var zURL;
                    if (Cnt.toString() == "0") {
                        zURL = "http://localhost/hrms/OrgStructure.aspx?reqid=" + A_EMP_CODE;
                    }
                    else
                    {
                        zURL = "http://localhost/hrms/OrgStructure.aspx?reqid=" + EMP_CODE;
                    }

                    var reportingManager = r.d[i][3] != null ? r.d[i][3].toString() : '';
                    if (employeeId == reportingManager)
                    {
                        zURL = "http://localhost/hrms/OrgStructure.aspx?reqid=" + A_EMP_CODE;
                    }
                    data.addRows([[{ v: employeeId,
                        f: '<div><span style="color:#3D1956; font-weight:bold;">' + employeeName + '</span></div> <div><a href="' + zURL + '"><img width=80px height=80px ve style="border-color:black; border-width:0pt; border-style:solid;vertical-align:top;" src = "themes/creative1.0/images/profile55x55/' + employeeId + '.jpg"/></a></div><div><span style="color:#F28820;">(' + designation + ')</span></div> <div><span style="color:Green;">[Team Count: ' + Cnt + ']</span></div>'
                    }, reportingManager, designation]]);
                }
                var chart = new google.visualization.OrgChart($("#chart")[0]);
                chart.draw(data, { allowHtml: true });
            },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
        });
    }

    function draw() {
        $.ajax({
            type: "POST",
            url: "OrgService.svc/",
            data: "{}",
            dataType: "json",
            contentType: "application/json",
            success: function (x, textStatus) {
                var data = JSON.parse(x);

                if ((emp_count = data.length) > 0) {
                    var l_data_table = new google.visualization.DataTable();
                    l_data_table.addColumn('string', 'Name');
                    l_data_table.addColumn('string', 'Manager');
                    l_data_table.addRows(emp_count);

                    for (i = 0; i < emp_count; i++) {
                        l_data_table.setCell(i, 0, data[i].name);
                        l_data_table.setCell(i, 1, data[i].Manger);
                    }

                    var chart = new google.visualization.OrgChart(document.getElementById('org_div'));
                    chart.draw(l_data_table, { allowHtml: true });


                }
            }
        });
    }
        $(document).ready(function () {
            draw();

        });
    </script>
</head>
<body>
    <div id="org_div"></div>
<div id="chart" style="overflow:scroll;"></div>
</body>
</html>
    <asp:HiddenField ID="hdnReqid" runat="server" />
</asp:Content>
