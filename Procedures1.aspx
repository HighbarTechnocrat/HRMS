<%@ Page Title="" Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Procedures.aspx.cs" Inherits="GalleryHome" %>

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
                <h2>Policy & Procedure : Procedures</h2>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/informationsystemdepartmentprocedures"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px; vertical-align:middle" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                      <div class="cshero-fancybox-inner">                        
                        <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="font-size: 14px !important;"> <span class="cshero-title-main">Information Systems</span> </h4>                                                
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/informationsystemdepartmentprocedures">
                            <div class="right-arrow">+</div>
                            <h2>Information Systems</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <a href="http://localhost/hrms/engineeringmanagement"><div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                       <div class="cshero-fancybox-inner">                      
                        <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Engineering Management</span> </h4>                                          
                    </div>
                </div>
            </div></a>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/engineeringmanagement">
                            <div class="right-arrow">+</div>
                            <h2>Engineering Management</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
               <a href="IMSCommonProcedure.aspx">  <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                      <div class="cshero-fancybox-inner">                       
                        
                          <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">IMS Common Procedures</span> </h4>
                         
                         </div> 
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="IMSCommonProcedure.aspx">
                            <div class="right-arrow">+</div>
                            <h2>IMS Common Procedures</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
              <a href="http://localhost/hrms/procurementandsubcontract">   <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                     <div class="cshero-fancybox-inner">
                                      <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Procurement And Sub-Contract</span> </h4>                                           
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/procurementandsubcontract">
                            <div class="right-arrow">+</div>
                            <h2>Procurement And Sub-Contract</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                 <a href="http://localhost/hrms/humanresource"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                   <div class="cshero-fancybox-inner">
                           <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Human Resource</span> </h4>                                            
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/humanresource">
                            <div class="right-arrow">+</div>
                            <h2>Human Resource</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>             
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/equipment"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                    <div class="cshero-fancybox-inner">                        
                         <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main"> Equipment</span> </h4>                                               
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/equipment">
                            <div class="right-arrow">+</div>
                            <h2> Equipment</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>       
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/contracts"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                    <div class="cshero-fancybox-inner">                       
                         <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Contracts</span> </h4>                                              
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/contracts">
                            <div class="right-arrow">+</div>
                            <h2>Contracts</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>  
            
            
            
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                <a href="http://localhost/hrms/accountsfinanceandtaxation"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px; vertical-align:middle" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                      <div class="cshero-fancybox-inner">                        
                        <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="font-size: 14px !important;"> <span class="cshero-title-main">Accounts, Finance & Taxation</span> </h4>                                                
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" ref="http://localhost/hrms/accountsfinanceandtaxation">
                            <div class="right-arrow">+</div>
                            <h2>Accounts, Finance & Taxation</h2>
                        </a>
                       
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
               <a href="http://localhost/hrms/centralprojectsplanningandmonitoring"> <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                       <div class="cshero-fancybox-inner">                      
                        <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Central Planning & Monitoring</span> </h4>                                          
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/centralprojectsplanningandmonitoring">
                            <div class="right-arrow">+</div>
                            <h2>Central Planning & Monitoring</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                 <a href="http://localhost/hrms/cc"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                   <div class="cshero-fancybox-inner">
                          <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Corporate Communication</span> </h4>
                         </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/cc">
                            <div class="right-arrow">+</div>
                            <h2>Corporate Communication</h2>
                        </a>                       
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                 <a href="http://localhost/hrms/cos"> <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                    <div class="cshero-fancybox-inner">
                                      <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Corporate Office Services</span> </h4>                                           
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/cos">
                            <div class="right-arrow">+</div>
                            <h2>Corporate Office Services</h2>
                        </a>
                        
                    </li>
                </ul>
            </div>
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                 <a href="http://localhost/hrms/csr"> <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                   <div class="cshero-fancybox-inner">
                           <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Corporate Social Responsibility</span> </h4>                                               
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/csr">
                            <div class="right-arrow">+</div>
                            <h2>Corporate Social Responsibility</h2>
                        </a>
                      
                    </li>
                </ul>
            </div>             
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
               <a href="http://localhost/hrms/tara"> <div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                    <div class="cshero-fancybox-inner">                        
                         <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main"> TARA</span> </h4>                                               
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/tara">
                            <div class="right-arrow">+</div>
                            <h2> TARA</h2>
                        </a>
                       
                    </li>
                </ul>
            </div>       
            <div class="PolicyProcedureIMS wow fadeInUp hidden-xs" data-wow-delay="600ms" data-wow-duration="1000ms">
                 <a href="http://localhost/hrms/siteprocedure"><div style="border-color: transparent; padding: 85px 0px 30px; background: #9b9b9b; min-height: 150px;" class="cshero-fancybox-wrap  clearfix fancybox-layout5 text-center">
                   <div class="cshero-fancybox-inner">                       
                         <h4 class="cshero-fancybox-title-wrap-imsprocedure" style="top: 10%;font-size: 14px !important;"> <span class="cshero-title-main">Site Procedure</span> </h4>                                              
                    </div>
                </div></a>
            </div>
            <div id="integration-list" class="hidden-sm hidden-md hidden-lg">
                <ul>
                    <li>
                        <a class="expand" href="http://localhost/hrms/siteprocedure">
                            <div class="right-arrow">+</div>
                            <h2>Site Procedure</h2>
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

