<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dirjs.ascx.cs" Inherits="themes_creative1_0_LayoutControls_dirjs" %>
<!--Start js for menu-->
<script type="text/javascript" src="ReturnUrl("sitepath")js/jsframework.js"></script>
<script type="text/javascript" src="ReturnUrl("sitepath")js/logindropdown/logindropdown.js" defer="defer">
</script>
<script type="text/javascript" src="ReturnUrl("sitepath")js/navigation/mobile-navi-dd.js" defer="defer">
</script>
<!--End js for menu-->
<!--Start js for related carousel-->
<script type="text/javascript" src="ReturnUrl("sitepath")js/relatedproduct/carousel-related.js" defer="defer">
</script>
<!--End js for related carousel-->
<script type="text/javascript" src="ReturnUrl("sitepath")js/productfilter/dd.js" defer="defer">
</script>
<script type="text/javascript" src="ReturnUrl("sitepath")js/backtotop.js" defer="defer"></script>
<script type="text/javascript" src="ReturnUrl("sitepath")js/carousel/carousel.js" defer="defer"></script>
<script type="text/javascript" src="ReturnUrl("sitepath")js/scrollbar/scrollbar.js" defer="defer"></script>
<!--End js -->
<script type="text/javascript" src="ReturnUrl("sitepath")js/autocomplete/autocomplete.js" defer="defer"> 
</script>
<script type="text/javascript" defer="defer">
    $(function () {
        $('.IM-carousel').owlCarousel({
            loop: false,
            lazyload: true,
            margin: 50,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 2,
                    nav: false,
                    loop: false
                },
                320: {
                    items: 3,
                    nav: false,
                    loop: false
                },
                480: {
                    items: 4,
                    nav: false,
                    loop: false
                },
                640: {
                    items: 5,
                    nav: false,
                    loop: false
                },
                720: {
                    items: 5,
                    nav: false,
                    loop: false
                },
                1000: {
                    items: 4,
                    nav: true,
                    loop: false
                },
                1200: {
                    items: 5,
                    nav: true,
                    loop: false
                },
                1400: {
                    items: 5,
                    nav: true,
                    loop: false
                },
                1500: {
                    items: 6,
                    nav: true,
                    loop: false
                },
                1600: {
                    items: 6,
                    nav: true,
                    loop: false
                },
                1800: {
                    items: 7,
                    nav: true,
                    loop: false
                },
                2200: {
                    items: 9,
                    nav: true,
                    loop: false
                },
                2400: {
                    items: 10,
                    nav: true,
                    loop: false
                },
                2800: {
                    items: 14,
                    nav: true,
                    loop: false
                }
            }
        })
    });

    jQuery("document").ready(function (e) {
        var t = e(".header");
        e(window).scroll(function () {
            if (e(this).scrollTop() > 0) {
                t.addClass("fixed-navigation")
            } else {
                t.removeClass("fixed-navigation")
            }
        })
    })
    $(document).ready(function () {
        $("#topseach_m_uxlogopanel_txtsearch").autocomplete("http://testim.com.iis1101.shared-servers.com/search_cs.ashx", {
            inputClass: "ac_input",
            resultsClass: "ac_results",
            loadingClass: "ac_loading",
            minChars: 3,
            delay: 10,
            matchCase: false,

            matchSubset: true,
            matchContains: false,
            cacheLength: 0,
            max: 100,
            mustMatch: false,
            extraParams: {},
            selectFirst: true,
            formatItem: function (row) { return row[0]; },
            formatMatch: null,
            autoFill: false,
            width: 0,
            multiple: false,
            multipleSeparator: ", ",
            scroll: true,
            scrollHeight: 180
        });
        $("#topseach_m_uxlogopanel_txtsearch").bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB &&
$(this).data("autocomplete").menu.active) {
                event.preventDefault();
            }
        })
    });
    </script>
