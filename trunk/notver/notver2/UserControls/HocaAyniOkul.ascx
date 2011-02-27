<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaAyniOkul.ascx.cs" Inherits="UserControls_HocaAyniOkul" %>

<asp:Panel runat="server" ID="pnlKontrol">
<div style="padding:10px 10px 10px 20px; display:block; color:#191919; width:250px;">
        <asp:Repeater runat="server" ID="rptHocalar">
            <ItemTemplate>
                <%# HocaLinkBaslangic(DataBinder.Eval(Container.DataItem, "HOCA_ID")) %>
                    <p style="background-image:url('./App_Themes/Default/Images/hocabuton.png');
                         margin-bottom:15px; padding:10px 10px 10px 15px; width:225px; height:10px;
                    font-weight:bold;">
                        <%# DataBinder.Eval(Container.DataItem, "ISIM") %>
                    </p>
                </a>
            </ItemTemplate>
        </asp:Repeater>
</div>
</asp:Panel>