/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* FIXED NAVIGATION
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
(function ($) {
    "use strict";
    // EXISTS ALSO IN SCRIPT.JS
    function UserSidebar() {
        var topbarHeight =jQuery("#topbar").height(),
		 menuHeight =jQuery("#navbar").height(),
		 sidebarTop = 0;
        if ($("topbar").hasClass("topbar-closed")) {
            sidebarTop = menuHeight + topbarHeight;
        }
        else {
            sidebarTop = menuHeight;
        }
       jQuery("#user-sidebar").css("padding-top", sidebarTop);
    }

   jQuery(window).bind('load', function () {
        fixedMenuPosition();
    });

   jQuery(window).bind('scroll', function () {
        fixedMenuPosition();
    });

    function fixedMenuPosition() {
        var scroll =jQuery(window).scrollTop();
        var menu_is_horizontal =jQuery('body').hasClass('menu-is-horizontal');

        var height =jQuery('#main-header').height();
        if (height == 0)
            height =jQuery('#navbar').height();
        if (menu_is_horizontal)
            height +=jQuery('#navigation').height() + 50;

        var animation_classes = 'animate-me animated slideInDown';

        if ($(window).width() <= 600) {
            animation_classes = '';
            var adminbar_height = parseInt($("html").css("margin-top").replace("px", ""));

            var top_scroll = (adminbar_height - scroll > 0) ? (adminbar_height - scroll) : 0;
           jQuery("#navbar").css('top', top_scroll);

            var navbar_plus = 0;
            if ($(window).width() <= 450)
                navbar_plus =jQuery('#navbar').height();

            if (scroll > adminbar_height) {
                addFixedMenu(menu_is_horizontal, animation_classes);

                if (menu_is_horizontal)
                   jQuery('#navigation').css('top',jQuery('#navbar').height());
                else
                   jQuery('#navigation').css('top', parseInt(navbar_plus + top_scroll));
            } else {
                removeFixedMenu(menu_is_horizontal);

                if (menu_is_horizontal)
                   jQuery('#navigation').css('top', parseInt($('#navbar').height() + adminbar_height - scroll));
                else
                   jQuery('#navigation').css('top', parseInt(navbar_plus + adminbar_height - scroll));
            }
        } else {
            if (scroll >= height + 50) {
                addFixedMenu(menu_is_horizontal, animation_classes);
            } else if (scroll <= 0) {
                removeFixedMenu(menu_is_horizontal);
            }
        }
    }

    function addFixedMenu(is_horizontal, animation) {
       jQuery("#navbar").addClass('navigation-fixed ' + animation);
        if (is_horizontal)
           jQuery("#navigation").addClass('navigation-fixed ' + animation);
       jQuery("body").addClass('has-navigation-fixed');
       jQuery("#user-sidebar").addClass('onscroll');
        if ($("#user-sidebar").length > 0) {
            UserSidebar();
        }
    }

    function removeFixedMenu(is_horizontal) {
       jQuery("#navbar").removeClass('navigation-fixed animated slideInDown');
        if (is_horizontal)
           jQuery("#navigation").removeClass('navigation-fixed animated slideInDown');
       jQuery("body").removeClass('has-navigation-fixed');
       jQuery("#user-sidebar").removeClass('onscroll');
        if ($("#user-sidebar").length > 0) {
            UserSidebar();
        }
    }
})(jQuery);