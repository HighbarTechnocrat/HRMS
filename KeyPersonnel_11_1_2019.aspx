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
</style>
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
   
        <div class="page-wrapper animated fadeInDown"> 
        <section class="markets">
		<div class="auto-container">
        <div class="col-sm-12">
           <div class="userposts"><span>Key Personnel</span> </div>		             
           <div class="row">
  			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/7VxLjT_DIlDiIB5xqsb-uQ=="><img src="Images/Gallery/Ajit_Gulabchand.jpg" class="img-responsive"/></a>
				 <a href="https://www.hccnet.in/hccuser/profile/7VxLjT_DIlDiIB5xqsb-uQ=="><b>Ajit Gulabchand</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/7VxLjT_DIlDiIB5xqsb-uQ==">Chairman & Managing Director</a></p>
               
            </div>
			   <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/tXA5Lic_Ero5Es_yqZPBeA=="><img src="Images/Gallery/arjun.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/tXA5Lic_Ero5Es_yqZPBeA=="><b>Arjun Dhawan</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/tXA5Lic_Ero5Es_yqZPBeA==">Director & Group CEO</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/gR0Jl71JBxc39ULpXH_dEA=="> <img src="Images/Gallery/Shalaka.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/gR0Jl71JBxc39ULpXH_dEA=="><b> Shalaka Gulabchand  Dhawan </b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/gR0Jl71JBxc39ULpXH_dEA==">Whole-time Director</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
          
            
         </div> 
		</div>

		<div class="col-sm-12">
           <h4>HCC Group</h4> 
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/0kibAO_7YEnakwO4-Rh0PA=="><img src="Images/Gallery/Praveen Sood.jpg" class="img-responsive"/></a><a href="https://www.hccnet.in/hccuser/profile/0kibAO_7YEnakwO4-Rh0PA=="><b>Praveen Sood</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/0kibAO_7YEnakwO4-Rh0PA==">Group CFO</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/tF13aqf9xeM4PxOoFQwZcA=="> <img src="Images/Gallery/Shailesh Sawa.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/tF13aqf9xeM4PxOoFQwZcA=="><b> Shailesh Sawa </b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/tF13aqf9xeM4PxOoFQwZcA==">EVP - Group Finance</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/56YTbeUZfgdCbUp40qAIUg=="><img src="Images/Gallery/Aditya Jain.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/56YTbeUZfgdCbUp40qAIUg=="><b>Aditya Jain</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/56YTbeUZfgdCbUp40qAIUg==">Group EVP - Human Resources</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
            <div class="col-sm-3 team-box">
				   <a href="https://www.hccnet.in/hccuser/profile/jwNrFbjkGEwnKoEAmaea1Q=="><img src="Images/Gallery/Arun Bodupali.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/jwNrFbjkGEwnKoEAmaea1Q=="> <b>Arun Bodupali</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/jwNrFbjkGEwnKoEAmaea1Q==">Senior VP - Group Legal And <br> General Counsel</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>
		<div class="col-sm-12">
            
           <div class="row">
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/IZfsBOVONVei83946Y4xKQ=="> <img src="Images/Gallery/Amod Tupkari.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/IZfsBOVONVei83946Y4xKQ=="><b> Amod Tupkari </b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/IZfsBOVONVei83946Y4xKQ==">Chief Internal Auditor</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
            
          
          
		</div>
		   
        </div>
		<!-- HCC Group -->
		<!-- HCC E&C -->
		<div class="col-sm-12">
		   <h4>HCC E&C</h4>  
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/cDMJFlNaRDMeww94dLRbCw=="><img src="Images/Gallery/Amit Uplenchwar.jpg" class="img-responsive"/></a><a href="https://www.hccnet.in/hccuser/profile/cDMJFlNaRDMeww94dLRbCw=="><b>Amit Uplenchwar</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/cDMJFlNaRDMeww94dLRbCw==">CEO - HCC E&C</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/oxpck9Y5QDitpRr_bDrQMQ=="> <img src="Images/Gallery/S D Jeur.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/oxpck9Y5QDitpRr_bDrQMQ=="><b>S D Jeur</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/oxpck9Y5QDitpRr_bDrQMQ==">Senior VP - Projects</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/2zcIOPoHeGluJe2YW1pLkg=="><img src="Images/Gallery/V Seshu Babu.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/2zcIOPoHeGluJe2YW1pLkg=="><b>V Seshu Babu</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/2zcIOPoHeGluJe2YW1pLkg==">Project Director</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
            <div class="col-sm-3 team-box">
				   <a href="https://www.hccnet.in/hccuser/profile/7p8bS_n3c0Di222hXNQoiA=="><img src="Images/Gallery/K C Mahalik.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/7p8bS_n3c0Di222hXNQoiA=="> <b>K C Mahalik</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/7p8bS_n3c0Di222hXNQoiA==">Project Director</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>	
		<div class="col-sm-12">
            
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/vUXwS11AxZTK-krhKez_Sw=="><img src="Images/Gallery/RVR Kishore.jpg" class="img-responsive"/></a><a href="https://www.hccnet.in/hccuser/profile/vUXwS11AxZTK-krhKez_Sw=="><b>RVR Kishore</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/vUXwS11AxZTK-krhKez_Sw==">Project Director</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/pQzcEVCJZ8PzGSfh1_55Tw=="> <img src="Images/Gallery/Isaac Joseph.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/pQzcEVCJZ8PzGSfh1_55Tw=="><b>Isaac Joseph</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/pQzcEVCJZ8PzGSfh1_55Tw==">Project Director</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/-HIBxku0TK5gmtxAPisOEA=="><img src="Images/Gallery/Santosh Rai.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/-HIBxku0TK5gmtxAPisOEA=="><b>Santosh Rai</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/-HIBxku0TK5gmtxAPisOEA==">Head - Business Development</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
            <div class="col-sm-3 team-box">
				   <a href="https://www.hccnet.in/hccuser/profile/MPijXxxfJwyp9P1iawbgsA=="><img src="Images/Gallery/Suresh Karki.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/MPijXxxfJwyp9P1iawbgsA=="> <b>Suresh Karki</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/MPijXxxfJwyp9P1iawbgsA==">Senior GM - Tendering</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>	
		<div class="col-sm-12">
            
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/jcbpgALJKAVCzZE2KJVOQA=="><img src="Images/Gallery/Girish Gangal.jpg" class="img-responsive"/></a><a href="https://www.hccnet.in/hccuser/profile/jcbpgALJKAVCzZE2KJVOQA=="><b>Girish Gangal</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/jcbpgALJKAVCzZE2KJVOQA==">CFO - HCC E&C and Senior VP -<br> Group Taxation</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/rm296Rhxsf-ueGW29Mh73w=="> <img src="Images/Gallery/Satish Kumar Sharma.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/rm296Rhxsf-ueGW29Mh73w=="><b>Satish Kumar Sharma</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/rm296Rhxsf-ueGW29Mh73w==">Chief Technology Officer</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/DY9jBJVHAoqivfV81dSaUA=="><img src="Images/Gallery/Gurudas Naik.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/DY9jBJVHAoqivfV81dSaUA=="><b>Gurudas Naik</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/DY9jBJVHAoqivfV81dSaUA==">Senior VP - Contracts & Claims</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
            <div class="col-sm-3 team-box">
				   <a href="https://www.hccnet.in/hccuser/profile/5lsLger58tNFQQHFDcJHxQ=="><img src="Images/Gallery/Badrinath Durvasula.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/5lsLger58tNFQQHFDcJHxQ=="> <b>Badrinath Durvasula</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/5lsLger58tNFQQHFDcJHxQ==">VP - Legal</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>	
		<div class="col-sm-12">
            
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/4ZNPJ03yQVunDDSQNqrKeA=="><img src="Images/Gallery/Vivek Shenoy.jpg" class="img-responsive"/></a><a href="https://www.hccnet.in/hccuser/profile/4ZNPJ03yQVunDDSQNqrKeA=="><b>Vivek Shenoy</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/4ZNPJ03yQVunDDSQNqrKeA==">Senior GM - Central Planning & Monitoring</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/6R0PvYGjUcyjyCTtQBhpfg=="> <img src="Images/Gallery/Ajit Shenoy.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/6R0PvYGjUcyjyCTtQBhpfg=="><b>Ajit Shenoy</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/6R0PvYGjUcyjyCTtQBhpfg==">GM - Procurement Services</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/pSF4MVQ85WA5JCAT3ipZDg=="><img src="Images/Gallery/Anuran Ghatak.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/pSF4MVQ85WA5JCAT3ipZDg=="><b>Anuran Ghatak</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/pSF4MVQ85WA5JCAT3ipZDg==">Senior GM - Equipment</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
            <div class="col-sm-3 team-box">
				   <a href="https://www.hccnet.in/hccuser/profile/PQzemBw1RzDvJ3I9p0Z_-Q=="><img src="Images/Gallery/Avinash Harde.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/PQzemBw1RzDvJ3I9p0Z_-Q=="> <b>Avinash Harde</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/PQzemBw1RzDvJ3I9p0Z_-Q==">VP - IMS</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>			
		<!-- HCC E&C -->

		<!-- Other Group Companies -->
		<div class="col-sm-12">
		   <h4>Other Group Companies</h4>  
           <div class="row">
			<div class="col-sm-3 team-box">
				 <a href="https://www.hccnet.in/hccuser/profile/EZ5YAXDD3aapS1wIxpvn4Q=="><img src="Images/Gallery/Arun Kumar Singh.jpg" class="img-responsive"/></a>
				 <a href="https://www.hccnet.in/hccuser/profile/EZ5YAXDD3aapS1wIxpvn4Q=="><b>Arun Kumar Singh</b></a>
                 <p><a href="https://www.hccnet.in/hccuser/profile/EZ5YAXDD3aapS1wIxpvn4Q==">COO - Steiner India</a></p>
                  <p align="left" style="text-align:justify">
                  

                  </p>
            </div>
           <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/hckf616DdqJeS6b3sCSVZg=="> <img src="Images/Gallery/Ravindra Singh.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/hckf616DdqJeS6b3sCSVZg=="><b>Ravindra Singh</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/hckf616DdqJeS6b3sCSVZg==">COO - HCC Concessions</a></p>
                 <p align="left" style="text-align:justify">
                 
                
                 </p>
			</div>
             <div class="col-sm-3 team-box">
				  <a href="https://www.hccnet.in/hccuser/profile/NzKvEgkQ4qaMZ4WCCs8JaQ=="><img src="Images/Gallery/Devendra Manchekar.jpg" class="img-responsive"></a>
				  <a href="https://www.hccnet.in/hccuser/profile/NzKvEgkQ4qaMZ4WCCs8JaQ=="><b>Devendra Manchekar</b></a>
				 <p><a href="https://www.hccnet.in/hccuser/profile/NzKvEgkQ4qaMZ4WCCs8JaQ==">COO - HCC Real Estate</a></p>
				 <p align="left" style="text-align:justify">
                  

                  </p>
			</div>
          
		</div>
		   
        </div>	
		<!-- Other Group Companies -->
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
                url: "http://hccconstruction.co.in/~hcc/public",
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



