<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hccgroup1.aspx.cs" Inherits="hccgroup1" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="newslettersnew.aspx.cs" Inherits="newslettersnew" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        #slideshow {
            margin: 50px auto;
            position: relative;
            box-shadow: 0 0 20px rgba(0,0,0,0.4);
        }

            #slideshow > div {
                position: absolute;
                left: 10px;
                right: 10px;
                bottom: 0;
            }

        #slideshow1 {
            margin: 50px auto;
            position: relative;
            box-shadow: 0 0 20px rgba(0,0,0,0.4);
        }

            #slideshow1 > div {
                position: absolute;
                left: 10px;
                right: 10px;
                bottom: 10px;
            }
            span#MainContent_lblheading {
    color: #F28820;
    font-size: 22px;
    font-weight: normal;
    text-align: left;
}
    </style>
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />

<!DOCTYPE html>
<html>

<head>
       
    <link href="https://fonts.googleapis.com/css2?family=Lobster&display=swap" rel="stylesheet">

    <style>
        body {
            /*background-color: #525252;*/         
            font-family: 'Trebuchet MS';
        }

        .div_main {
            background-color: #ffffff;
            width: 100%;
            max-width: 800px;
            margin: auto;       
            margin-top: 3%;
        }

        .div_outer {
            padding: 4%;
        }

        .div_inner {
            border: 3px solid gray;
            outline: 5px solid #461561;
        }

        .div_p1 {
            text-align: center;
        }

        .page_title {
            padding: 1% 2%;
            font-size: 46px;
            font-weight: 700;
            box-shadow: 0 4px 20px 0 rgba(0,0,0,0.2);
            border-radius: 15px;
            width: 42%;
            margin: auto;
            margin-top: 4%;
            margin-bottom: 4%;
            font-family: 'Lobster';
            color: #461561;
        }

        .title {
            color: #461561;
            font-size: 32px;
            font-style: inherit;
            font-weight: 800;
            margin-bottom: 1%;
        }

        .para {
            color: black;
            font-size: 22.5px;
            font-weight: 500;
        }

        ul {
            font-size: 26px;
        }

            ul li {
                margin-bottom: 3%;
                font-size: 22.5px;
            }

        p {
            margin: 0px;
        }

        .div_ul {
            text-align: justify;
            padding: 0% 2%;
        }

        .div_bottom {
            text-align: center;
        }
    </style>
</head>

<body>


    <div class="div_main">
        <div class="div_outer">

	  <div>
                <img src="images/Highbar Vision.png" style="width:750px" />
            </div>

            <div class="div_inner" style="display:none">
                <div class="div_p1">


                    <div class="page_title">Highbar Vision</div>
                    <!--<div class="page_title">Vison Card</div>-->


                    <div class="title">Core Purpose </div>
                    <%--<p class="para">Transforming businesses through path breaking solutions.</p>--%>
                    <ul>
                        <li>
                            Transforming businesses through path breaking solutions.
                        </li>
                    </ul>
                    <p><img src="design.jpg" /></p>

                    <div class="title">Core Value</div>
                    <%--<p class="para">Meritocracy, Result Oriented, Passionate.</p>--%>
                    <ul>
                        <li>
                            Meritocracy, Result Oriented, Passionate.
                        </li>
                    </ul>
                    <p><img src="design.jpg" /></p>

                    <div class="title">BHAG</div>
                    <ul>
                        <li>
                            To be the number one preferred IT solution company in whichever
                            market / segment we operate with a turnover of 2500 crore by 2028.
                        </li>
                    </ul>
<%--                    <p class="para">
                        To be the number one preferred IT solution company in whichever
                        market / segment we operate with a turnover of 2500 crore by 2028.
                    </p>--%>

                    <p><img src="design.jpg" /></p>

                    <div class="title">Vivid Description</div>
                </div>

                <div class="div_ul">
                    <ul>
                        <li>
                            By 2028, Highbar Technocrat Ltd would have 10-12 office’s in each
                            metro city & office’s in each continent.
                        </li>
                        <li>
                            By 2028, Highbar Technocrat Ltd would be the most admired
                            professionally run IT company in our market / segment. With an
                            environment to nurture new ideas, creativity & entrepreneurial
                            spirit.
                        </li>
                        <li>
                            By 2028, Highbar Technocrat Ltd would be the most sought after
                            work place for prospective employees. With 20 patented products
                            with sizable R & D lab & 3000 employees.
                        </li>
                        <li>
                            By 2028, Highbar Technocrat Ltd would be listed in NSE, BSE,
                            NASDAQ.
                        </li>
                        <li>
                            By 2028, Highbar Technocrat Ltd would drive meritocracy with
                            great opportunities to excel.
                        </li>
                    </ul>
                </div>
               



                <div class="div_bottom">                   
                    <img style="" src="logo.jpg" />
                </div>
            </div>
        </div>
    </div>

</body>
</html>


    <script type="text/javascript">

        $(document).ready(function () {
            $('#myModal').on('show.bs.modal', function (e) {
                var vid_id = e.relatedTarget.getAttribute('data-video');
                $(this).find('iframe').attr('src', 'https://www.youtube.com/embed/' + vid_id);
            });
        });

        $(document).ready(function () {
            $("#close_box1").click(function () {
                //$(".cart_over1").fadeOut();
                $("#clearmySound").attr('src', '');
                $('#myModal').modal('hide');

            })
        });

        var li_list = $('#thumb-slider').find('li');
        var li_list_length = li_list.length;


        function photoSliderTop() {
            var top = parseInt($('#thumb-slider').css('top'));
            if (top < 0) {
                var inc = top + 150;
                $('#thumb-slider').animate({ top: inc + 'px' });
            }
        }



        function photoSliderBottom() {
            var top = parseInt($('#thumb-slider').css('top'));

            if (li_list_length > 3) {
                li_list_length = li_list_length - 3;
                var final_count_pix = 150 * parseInt(li_list_length) + 100;
            } else {
                final_count_pix = 100;
            }


            if (top > -final_count_pix) {
                var dec = top - 150;
                $('#thumb-slider').animate({ top: dec + 'px' });
            }
        }

        $(document).ready(function () {
            // Photo Landing page Slider
            $('#photoLandSlider').carousel();
            // handles the carousel thumbnails click
            $('[id^=carousel-selector-]').click(function () {
                var id_selector = $(this).attr("id");
                var id = id_selector.substr(id_selector.length - 1);
                id = parseInt(id);
                $('#photoLandSlider').carousel(id);
                $('[id^=carousel-selector-]').removeClass('selected');
                $(this).addClass('selected');
            });
            // when the carousel slides, auto update thumbnail
            $('#photoLandSlider').on('slid.bs.carousel', function (e) {
                var id = $('.item.active').data('slide-number');
                id = parseInt(id);
                $('[id^=carousel-selector-]').removeClass('selected');
                $('[id=carousel-selector-' + id + ']').addClass('selected');
            });
            // Map Main slider and thumbnail slider
            $('#phl-ms-l').click(function () {
                photoSliderTop();
            });
            $('#thumb-slider img').click(function () {
                photoSliderBottom(li_list_length);
            });
        });


        $(document).ready(function () {

            $('')

            $("#slideshow > div:gt(0)").hide();
            setInterval(function () {
                $('#slideshow > div:first')
                .fadeOut(1000)
                .next()
                .fadeIn(1000)
                .end()
                .appendTo('#slideshow');
            }, 3000);

            $("#slideshow1 > div:gt(0)").hide();
            setInterval(function () {
                $('#slideshow1 > div:first')
                .fadeOut(1000)
                .next()
                .fadeIn(1000)
                .end()
                .appendTo('#slideshow1');
            }, 3000);


        });

        /*  var ctx = $("#myChart").get(0).getContext("2d");
        var data = {
        labels: ,
        datasets: [
        {
        label: "Intra Day",
        fillColor: "rgba(151,187,205,0.2)",
        strokeColor: "rgba(151,187,205,1)",
        pointColor: "rgba(151,187,205,1)",
        pointStrokeColor: "#fff",
        pointHighlightFill: "#fff",
        pointHighlightStroke: "rgba(151,187,205,1)",
        data:         }
        ]
        };
        var myLineChart = new Chart(ctx).Line(data); */

    </script>
    <script type="text/javascript">
        $(function () {
            $('#add_button').click(function (e, ele) {
                $('#letter_submit').ajaxSubmit({
                    url: "http://hccconstruction.co.in/~highbar/public",
                    type: 'post',
                    dataType: 'json',
                    beforeSend: function () {
                        $('[id$=_error]').text('');
                    },
                    complete: function () {
                    },
                    success: function (respObj) {
                        $("#success_news").fadeTo(2000, 500).slideUp(500, function () {
                            $("#success_news").hide();
                        });
                    },
                    error: function (respObj) {
                        $.each(respObj.responseJSON, function (k, v) {
                            $('#email1_error').text(v);
                        });
                    }
                });
                return false;
            });
        });
    </script>
    <script src="MAP_IMG/js/revolution.min.js"></script>
    <script src="MAP_IMG/js/bxslider.js"></script>
    <script src="MAP_IMG/js/owl.carousel.min.js"></script>
    <script src="MAP_IMG/js/jquery.mixitup.min.js"></script>
    <script src="MAP_IMG/js/jquery.fancybox.pack.js"></script>
    <script src="MAP_IMG/js/wow.js"></script>
    <script src="MAP_IMG/js/script.js"></script>
    <script src="MAP_IMG/js/smoothscroll.js"></script>
    <script src="MAP_IMG/Binary/jquery.min.js"></script>
    <script src="MAP_IMG/Binary/jquery.js"></script>
    <script src="MAP_IMG/Binary/bootstrap.js"></script>
    <script src="MAP_IMG/Binary/jquery.form.min.js"></script>
    <script src="MAP_IMG/Binary/raphael-min.js"></script>
    <script src="MAP_IMG/Binary/morris.min.js"></script>
    <script src="MAP_IMG/Binary/chart.min.js"></script>
    <script src="MAP_IMG/Binary/binary.js"></script>

</asp:Content>



