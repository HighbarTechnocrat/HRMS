<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homepopup.ascx.cs" Inherits="themes_creative1_0_LayoutControls_homepopup" %>


            <div class="productratinguser">
                    <asp:UpdatePanel runat="server" ID="up2">
                        <ContentTemplate>
                            <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                            </ajaxToolkit:Rating>
                            <asp:Label ID="lblcount" runat="server" ForeColor="green"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
      
          
              
