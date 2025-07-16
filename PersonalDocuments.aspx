<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="PersonalDocuments.aspx.cs" Inherits="PersonalDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <!-- Custom Styles -->
    <style>
        .card-hover:hover {
            transform: scale(1.05);
            transition: transform 0.3s ease-in-out;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
        }

        .section-header {
            background-color: #a3b18a;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        .page-wrapper {
            padding: 20px 0;
        }

        img.card-img-top {
            width: 190px !important;
        }
        .mycorner
        {
            color: #0366ba !important;
            font-size: 22px !important;
            font-weight: 300;
            text-align: left;
             text-transform: capitalize;

        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-wrapper container">
        <section class="markets">
            <!-- Section Header -->
             <h2 class="mycorner">My Corner</h2>
         <%--   <div class="section-header text-center" >
                <h2 class="fw-bold">My Corner</h2>
                <p class="text-muted">Access your personal documents and services below.</p>
            </div>--%>

            <!-- Responsive Grid with Cards -->
            <div class="row row-cols-1 row-cols-md-3 row-cols-lg-4 g-4">
                <!-- Personal Information -->
                <div class="col">
                    <a href="<%=ReturnUrl("hccurlmain")%>/procs/Personal_Info.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/PERS_INFO.jpg" class="card-img-top" alt="Personal Information" />
                        </div>
                    </a>
                </div>

                <!-- Change Password -->
                <div class="col">
                    <a href="<%=ReturnUrl("hccurlmain")%>/procs/ChangePassword.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/PasswordChange.jpg" class="card-img-top" alt="Change Password" />
                        </div>
                    </a>
                </div>

                <!-- Nominations (Form 16) -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/Nominations.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/Nominations.jpg" class="card-img-top" alt="Nominations" />

                        </div>
                    </a>
                </div>

                <!-- Payslip -->
                <div class="col">
                    <a href="https://ess.hgsbs.com/Login/" target="_blank" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/payslip.jpg" class="card-img-top" alt="Payslip" />
                        </div>
                    </a>
                </div>

                <!-- Form 16 -->
                <div class="col">
                    <a href="https://ess.hgsbs.com/Login/" target="_blank" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/form16.jpg" class="card-img-top" alt="Form 16" />

                        </div>
                    </a>
                </div>

                <!-- EPFO -->
                <div class="col">
                    <a href="https://unifiedportal-mem.epfindia.gov.in/memberinterface/" target="_blank" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/EPFO.jpg" class="card-img-top" alt="EPFO" />
                        </div>
                    </a>
                </div>

                <!-- Reporting Structure -->
                <div class="col" id="REPO_STRU" runat="server" visible="false">
                    <a href="http://localhost/hrms/OrgStructure.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/REPO_STRU.jpg" class="card-img-top" alt="Reporting Structure" />
                        </div>
                    </a>
                </div>

                <!-- Employee Report -->
                <div class="col" id="EMP_RPT" runat="server">
                    <a href="http://localhost/hrms/procs/ReportsMenu.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/Reports.jpg" class="card-img-top" alt="Employee Report" />
                        </div>
                    </a>
                </div>

                <!-- CustomerFIRST -->
                <div class="col" id="CustomerFIRST" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/Custs_Service.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/CustomerFIRST.jpg" class="card-img-top" alt="CustomerFIRST" />
                        </div>
                    </a>
                </div>

                <!-- Resignation -->
                <div class="col" id="resg" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/ResignationMenu.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/RESIGNATION.jpg" class="card-img-top" alt="Resignation" />
                        </div>
                    </a>
                </div>

                <!-- Employee Transfer Request -->
                <div class="col" id="EMPTRequest" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/EmployeeTransfer.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/EMPTRA.jpg" class="card-img-top" alt="Employee Transfer Request" />
                        </div>
                    </a>
                </div>

                <!-- IT Asset Request -->
                <div class="col" id="ITASSETRequest" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/ITAssetService.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/ITAsset.jpg" class="card-img-top" alt="IT Inventory Request" />
                        </div>
                    </a>
                </div>

                <!-- Update CV -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/IndexEmployeeCV.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/CV_Update.jpg" class="card-img-top" alt="Update CV" />
                        </div>
                    </a>
                </div>

                <!-- Exit Process -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/ExitProcess_Index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/ExitProcess.jpg" class="card-img-top" alt="Exit Process" />
                        </div>
                    </a>
                </div>

                <!-- KRA -->
                <div class="col" id="Div1" runat="server">
                    <a href="http://localhost/hrms/procs/KRA_Index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/KRA.jpg" class="card-img-top" alt="KRA" />
                        </div>
                    </a>
                </div>

                <!-- Salary Status Update -->
                <div class="col" id="divSalStatUpdt" runat="server">
                    <a href="http://localhost/hrms/procs/SalaryApprovalHome.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/SalaryStatusUpdate.jpg" class="card-img-top" alt="Salary Status Update" />

                        </div>
                    </a>
                </div>

                <!-- Vendor Sub Con Billing -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/vscb_index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/PROCUREMENT.jpg" class="card-img-top" alt="VendorPay" />
                        </div>
                    </a>
                </div>

                <!-- PMS Appraisal -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/appraisalindex.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/PMS.jpg" class="card-img-top" alt="Performance Appraisal" />
                        </div>
                    </a>
                </div>

                <!-- Travel Request Form -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/TravelRequisition_Index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/TravelRequstForm.jpg" class="card-img-top" alt="Travel Form" />
                        </div>
                    </a>
                </div>

                <!-- Customer Feedback -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/customerFirst.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/CustomerFeedback.png" class="card-img-top" alt="Customer Feedback" />
                        </div>
                    </a>
                </div>

                <!-- Thank You Card -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/ThankyouCard_index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/thankyou.png" class="card-img-top" alt="Thank You Card" />
                        </div>
                    </a>
                </div>

                <!-- Update Photo -->
                <div class="col" id="updatephoto" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/Update_Photo_index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/uploademployeePhoto.jpg" class="card-img-top" alt="Update Photo" />
                        </div>
                    </a>
                </div>

                <!-- ABAP Object Tracker -->
                <div class="col">
                    <a href="http://localhost/hrms/procs/abap_object_tracker_index.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/ABAPTrackerSystem.png" class="card-img-top" alt="ABAP Object Tracker" />
                        </div>
                    </a>
                </div>

                <!-- Uploaded Gallery Images -->
                <div class="col" id="gallery_images" runat="server" visible="false">
                    <a href="http://localhost/hrms/procs/upload_gallery_images.aspx" class="text-decoration-none">
                        <div class="card h-100 card-hover">
                            <img src="Images/Gallery/Insidepages/UploadGalleryBannerImages.jpg" class="card-img-top" alt="Upload Gallery/Banner Images" />
                        </div>
                    </a>
                </div>

              <%--************* Appreciation  Letter ************--%>
            <div class="col-sm-3 team-box" id="Div2" runat="server" visible="true">
                <a href="http://localhost/hrms/procs/Appreciation_Letter_index.aspx">
                    <img src="Images/Gallery/Insidepages/AppreciationLetter.png" class="img-responsive" style="width: 197px !important;" title="Appreciation  Letter" alt="Appreciation  Letter" />
                </a>
                <p>&nbsp</p>
            </div>

            </div>
        </section>
    </div>

    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <!-- Bootstrap 5 JS (Optional, for interactive components) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</asp:Content>
