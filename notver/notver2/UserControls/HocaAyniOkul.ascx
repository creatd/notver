<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaAyniOkul.ascx.cs" Inherits="UserControls_HocaAyniOkul" %>

<asp:Panel runat="server" ID="pnlKontrol">
<div class="HocaAyniOkul">
    <ol class="HocaAyniOkul">
        <asp:Repeater runat="server" ID="rptHocalar" OnItemDataBound="ItemDataBound">
            <ItemTemplate>
                <li class="HocaAyniOkul maviayrac">
                    <asp:Literal runat="server" ID="ltrHoca"></asp:Literal>
                </li>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <li class="HocaAyniOkul sariayrac">
                    <asp:Literal runat="server" ID="ltrHoca"></asp:Literal>
                </li>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <li class="HocaAyniOkul kirmiziayrac">
                    <a runat="server" href="">Tumu</a>
                </li>
            </FooterTemplate>
        </asp:Repeater>
    </ol>
</div>
</asp:Panel>