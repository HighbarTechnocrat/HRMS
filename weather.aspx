<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weather.aspx.cs" Inherits="test" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/login.ascx" TagName="login"
    TagPrefix="uclogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=ReturnUrl("css") %>fontawesome/font-awesome.css" rel="stylesheet" type="text/css"  />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>weather/normalize.css"/>
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>weather/weather.css"/>
   
</head>
    
<body id="weather-background" class="default-weather">
    <form id="form1" runat="server">
 <canvas id="rain-canvas"></canvas>
 <canvas id="cloud-canvas"></canvas>
 <canvas id="weather-canvas"></canvas>
 <canvas id="time-canvas"></canvas>
 <canvas id="lightning-canvas"></canvas>
          <div class="page-wrap">
        <header class="search-bar">
            <p class="search-text"><span class="search-location-text"><input id="search-location-input" class="search-location-input" type="text" placeholder="City"> <button id="search-location-button" class="fa fa-search search-location-button search-button" onclick="return false;"></button><button id="geo-button" class="geo-button fa fa-location-arrow search-button" onclick="return false;"></button></span></p>
            <div class="search-location-button-group">
               
            </div>
        </header>

        <div id="geo-error-message" class="geo-error-message hide"><button id='close-error' class='fa fa-times close-error'></button>Uh oh! It looks like we can't find your location. Please type your city into the search box above!</div>

        <div id="front-page-description" class="front-page-description middle">
            <h1>Blank Canvas Weather</h1>
            <h2>An Obligatory Weather App</h2>
        </div>
<%--    <div class="attribution-links hide" id="attribution-links"><button id='close-attribution' class='fa fa-times close-attribution'></button>
            <h3>Icons from <a href="https://thenounproject.com/">Noun Project</a></h3>
            <ul>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/cloud/6852/">Cloud</a> by Pieter J. Smits</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/snow/64/">Snow</a> from National Park Service Collection</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/drop/11449/">Drop</a> Alex Fuller</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/smoke/27750/">Smoke</a> by Gerardo Martín Martínez</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/moon/13554/">Moon</a> by Jory Raphael</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/warning/18974/">Warning</a> by Icomatic</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/sun/13545/">Sun</a> by Jory Raphael</li>
                <li class="icon-attribution"><a href="https://thenounproject.com/term/windsock/135621/">Windsock</a> by Golden Roof</li>
        </div>--%>
        <div id="weather" class="weather middle hide">
            <div class="location" id="location"></div>
            <div class="weather-container">
                <div id="temperature-info" class="temperature-info">
                    <div class="temperature" id="temperature"></div>
                    <div class="weather-description" id="weather-description"></div>
                </div>
                <div class="weather-box">
                    <ul class="weather-info" id="weather-info">
                        <li class="weather-item humidity">Humidity: <span id="humidity"></span>%</li><!--
                     --><li class="weather-item wind">Wind: <span id="wind-direction"></span> <span id="wind"></span> <span id="speed-unit"></span></li>
                    </ul>
                </div>
                <div class="temp-change">
                    <button id="celsius" class="temp-change-button celsius" onclick="return false;">&deg;C</button><button id="fahrenheit" onclick="return false;" class="temp-change-button fahrenheit">&deg;F</button>
                </div>
            </div>
        </div> 
    </div>
    </form>
     <script src="<%=ReturnUrl("sitepath") %>js/jquery.min.js" type="text/javascript"></script>
   <%-- SAGAR COMMENTED THIS FOR REMOVING WEATHER.JS 22SEPT2017 --%>
    <%--<script src="<%=ReturnUrl("sitepath") %>js/weather/weather.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#search-location-input").val('Ahmedabad');
            $("#search-location-button").click();
        });
    </script>--%>
</body>
</html>
