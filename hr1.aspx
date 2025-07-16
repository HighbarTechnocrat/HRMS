<%@ Page Title="" Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="hr.aspx.cs" Inherits="GalleryHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
</style>

<div class="page-wrapper animated fadeInDown"> 
        <section class="markets">
    <div class="auto-container">
        <div class="col-sm-12">
      <div class="sec-title wow fadeInLeft" data-wow-delay="300ms" data-wow-duration="1000ms">
         <br />
                  <h2>Policy & Procedure : Human Resource</h2>
            </div>
            <div class="knowledageCenter wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/HR_Policies.aspx">   <div style="border-color: transparent; padding: 110px 0px 30px; background: #9b9b9b; min-height: 300px; " class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                  <div class="cshero-fancybox-inner">
                        <div class="cshero-fancybox-image"> <img alt="" src="Images/Gallery/hrmsgroup.png" height="100px"> </div>
                       <h4 class="cshero-fancybox-title-wrap"> <span class="cshero-title-main">Policies</span> </h4>
                                           
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/HR_Policies.aspx">
                            <div class="right-arrow">+</div>
                            <h2>Policies</h2>
                        </a>
                       
                    </li>
                </ul>
            </div>
            <div class="knowledageCenter wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/HR_HolidayList.aspx"><div style="border-color: transparent; padding: 113px 0px 30px; background: #9b9b9b; min-height: 300px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                     <div class="cshero-fancybox-inner">
                        <div class="cshero-fancybox-image"> <img alt="" src="Images/Gallery/Headoffice.png" width="100px"> </div>
                        <h4 class="cshero-fancybox-title-wrap"> <span class="cshero-title-main">Holiday List</span> </h4>                     
                      
                    </div>
                </div></a>   
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/HR_HolidayList.aspx">
                            <div class="right-arrow">+</div>
                            <h2>Holiday List</h2>
                        </a>
                      
                    </li>
                </ul>
            </div>          
        </div>
    </div>
</section>

</div>




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

