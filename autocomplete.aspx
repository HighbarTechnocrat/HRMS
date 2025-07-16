<%@ Page Language="C#" AutoEventWireup="true" CodeFile="autocomplete.aspx.cs" Inherits="autocomplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="<%=ReturnUrl("css") %>autocomplete/bootstrap.min.css"/>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>autocomplete/app.css"/>
    <script src="~/themes/creative1.0/js/autocomplete/jquery.min.js"></script>
    <script src="~/themes/creative1.0/js/autocomplete/typeahead.bundle.min.js"></script>
    <script src="~/themes/creative1.0/js/autocomplete/bootstrap-tagsinput.min.js"></script>
    <script src="~/themes/creative1.0/js/autocomplete/app.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<div class="example example_tagclass">
        <div class="bs-example">
            <input id="txtsearch" class="txtsearch" type="text" />
            <asp:HiddenField ID="hdvalue" runat="server" />
            <asp:HiddenField ID="hdarray" runat="server" />
        </div>
        <div class="accordion">
            <div class="accordion-group">
                <div id="accordion_example_tagclass" class="accordion-body collapse">
                    <div class="accordion-inner">
                        <pre>
<code data-language="html">
                        <input type="text" />
                            </code></pre>
                    </div>
                </div>
            </div>
        </div>
        <table class="table table-bordered table-condensed">
            <thead>
                <tr>
                    <th>statement</th>
                    <th>returns</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><code>$("input").val()</code></td>
                    <td>
<pre class="val"><code data-language="javascript"></code></pre>
                    </td>
                </tr>
                <tr>
                    <td><code>$("input").tagsinput('items')</code></td>
                    <td>
<pre class="items"><code data-language="javascript"></code></pre>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
