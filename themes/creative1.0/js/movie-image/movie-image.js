function backgroundResize() {
    var a = $(window).height();
    $(".backgroundimage").each(function () {
        var i = $(this),
            e = i.width(),
            t = i.height(),
            d = i.attr("data-img-width"),
            r = i.attr("data-img-height"),
            s = d / r,
            g = parseFloat(i.attr("data-diff"));
        g = g ? g : 0;
        var n = 0;
        if (i.hasClass("parallax")) {
            n = a - t
        }

//sony
//a = (r/d)*d;
        a = (9 / 16) * e;
//alert(a);

        r = t + n + g, d = r * s, e > d && (d = e, r = d / s), i.data("resized-imgW", d), i.data("resized-imgH", r), i.css("background-size", d + "px " + a + "px")



       $('#MainContent_pdnewglobal_uxpdzommer').css({ 'height': a + 'px' });

    })
}
$(window).resize(backgroundResize), $(window).focus(backgroundResize), backgroundResize();
