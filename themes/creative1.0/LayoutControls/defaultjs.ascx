<%@ Control Language="C#" AutoEventWireup="true" CodeFile="defaultjs.ascx.cs" Inherits="Themes_FirstTheme_LayoutControls_defaultjs" %>
<script type="text/javascript" defer="defer">//$(document).ready(function () { $("#topseach_m_uxlogopanel_txtsearch").autocomplete("search_cs.ashx", { inputClass: "ac_input", resultsClass: "ac_results ", loadingClass: "ac_loading", minChars: 3, delay: 10, matchCase: false, matchSubset: true, matchContains: false, cacheLength: 0, max: 100, mustMatch: false, extraParams: {}, selectFirst: true, formatItem: function (e) { return e[0] }, formatMatch: null, autoFill: false, width: 0, multiple: false, multipleSeparator: ", ", scroll: true, scrollHeight: 180 }); $("#topseach_m_uxlogopanel_txtsearch").bind("keydown", function (e) { if (e.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) { e.preventDefault() } }) });
                                                                                                                                                                               $(document).ready(function () {
var count = 1;
function lastAddedLiveFunc() {
    if (count < 50)
    {
        $('#lastPostsLoader').html('<img src="http://intranet.com.iis1100.shared-servers.com/themes/creative1.0/images/loading.gif"/>');
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'default.aspx/GetMyWall',
            data: "{'page':'" + count + "'}",
            dataType: 'text',
            async: false,
            success: function (response) {
                var json = JSON.parse(response);
                $('.mywall').append(json["d"]);
            },
            error: function (xhr, status, error) {
                // alert(xhr.responseText);
            }
        });
        $('#lastPostsLoader').empty();
        count = count + 1;
    }
    
};
$(window).scroll(function () {
    var wintop = $(window).scrollTop(), docheight = $(document).height(), winheight = $(window).height();
    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        lastAddedLiveFunc(); }
});
});
                                                                                                                                                                               
</script>



