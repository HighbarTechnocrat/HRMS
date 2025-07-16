<%@ Page Title="Gallery" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="PhotoGalleryHome.aspx.cs" Inherits="GalleryHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!-- Stylesheets -->
    <link rel="stylesheet" href="MAP_IMG/css/bootstrap.css" />
    <link rel="stylesheet" href="MAP_IMG/css/style.css" />
    <link rel="stylesheet" href="MAP_IMG/css/responsive.css" />
    <link rel="stylesheet" href="MAP_IMG/css/gallery.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .gallery-container {
            padding: 20px;
        }
        .gallery-header-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }
        .gallery-header {
            font-size: 24px;
            font-weight: bold;
        }
        .gallery-grid {
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
            gap: 15px;
        }
        .gallery-item {
            width: calc(25% - 15px);
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background: #fff;
            padding: 10px;
            text-align: center;
            position: relative;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        .gallery-item img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            border-radius: 8px;
            transition: opacity 0.5s ease-in-out;
        }
        .gallery-item:hover img {
            opacity: 0.6;
        }
        .gallery-item a {
            color: #3D1956;
            text-decoration: none;
            font-weight: bold;
            display: block;
            margin-top: 5px;
        }
        .back-btn {
            padding: 10px 15px;
            background: #3D1956;
            color: #F28820 !important;
            border-radius: 5px;
            text-decoration: none;
        }
        
        /* Responsive Design */
        @media (max-width: 1024px) {
            .gallery-item {
                width: calc(50% - 15px); /* 2 items per row on tablets */
            }
        }
        @media (max-width: 768px) {
            .gallery-header-container {
                flex-direction: column;
                align-items: flex-start;
            }
            .gallery-item {
                width: 100%; /* 1 item per row on mobile */
            }
        }
    </style>

    <div class="page-wrapper animated fadeInDown">
        <section class="markets">
            <div class="auto-container">
                <div class="col-sm-12">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="Label1" runat="server" Text="Gallery"></asp:Label>
                        </span>
                    </div>
    <div class="gallery-container">
        <div class="gallery-header-container">
            <div class="gallery-header">
                <asp:Label ID="lblheading" runat="server" Text="Gallery"></asp:Label>
            </div>
            <a href="GalleryHome.aspx" class="back-btn">Gallery</a>
        </div>
        
        <div class="gallery-grid">
            <asp:Repeater ID="rptSlider" runat="server">
                <ItemTemplate>
                    <div class="gallery-item">
                        <%# !string.IsNullOrEmpty(Eval("Image_URL").ToString()) ? 
                            "<a href='" + Eval("Image_URL") + "' target='_blank'>" : "" %>
                        <img src='<%# ResolveUrl("~/images/projectimages/") + Eval("Image_Name") %>' alt="Gallery Image">
                        <%# !string.IsNullOrEmpty(Eval("Image_URL").ToString()) ? "</a>" : "" %>
                        <p><%# Eval("Image_Des") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

                    
            </div>
        </section>

    </div>


    <asp:HiddenField ID="hdngallryid" runat="server" Value="" />
</asp:Content>
