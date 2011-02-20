<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="SearchResults"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="headContent">
    <script type="text/javascript">
    // Create the tooltips only on document load
    $(document).ready(function() 
    {
       // Notice the use of the each() method to acquire access to each elements attributes
       $('#divDersler a[tooltip]').each(function()
       {
          $(this).qtip({
             content: $(this).attr('tooltip'), // Use the tooltip attribute of the element for the content
             style: 'light' // Give it a crea mstyle to make it stand out
          });
       });
    });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <a name='tepe'></a>
    <asp:Panel ID="pnlHocalar" runat="server" Visible="false">
    <div id="div1" style="margin-top:30px; clear:both;">
        <span style="background-color:#191919; padding:10px 20px 10px 20px; color:#f6f6f6; 
        display:block; margin-bottom:10px; width:160px; font-weight:bold;">Hoca Arama Sonucu</span>
            <br />
    <asp:Repeater runat="server" ID="repeaterHocalar">
        <HeaderTemplate><table style="font-weight:bold;"></HeaderTemplate>
        <ItemTemplate>
        <tr id="item" style="margin-bottom:20px; background-color:#f6f6f6; width:960px;">
            <td id="hocaIsim" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                <%# HocaLinkiniDondur(DataBinder.Eval(Container.DataItem, "ISIM").ToString(), DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString())%>
            </td>
            <td id="hocaOkullar" style="width:640px; padding-left:20px; color:#626262; padding-top:30px; padding-bottom:30px;">
                <%# HocaOkullariniDondur( DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString() )%>
            </td>
            <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top;">
                <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
            </td>
        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr id="item" style="margin-bottom:20px; background-color:#ffffff; width:960px;">
            <td id="hocaIsim" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                <%# HocaLinkiniDondur(DataBinder.Eval(Container.DataItem, "ISIM").ToString(), DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString())%>
            </td>
            <td id="hocaOkullar" style="width:640px; padding-left:20px; color:#626262; padding-top:30px; padding-bottom:30px;">
                <%# HocaOkullariniDondur( DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString() )%>
            </td>
            <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top;">
                <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
            </td>
        </tr>
        </AlternatingItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
    </div> 
                   

    </asp:Panel>
    <asp:Panel ID="pnlDersler" runat="server" Visible="false" CssClass="pnlDersler">
    <div id="divDersler" style="margin-top:30px; clear:both;">
        <span style="background-color:#191919; padding:10px 20px 10px 20px; color:#f6f6f6; 
        display:block; margin-bottom:10px; width:160px; font-weight:bold;">Ders Arama Sonucu</span>
            <br />
        <asp:Repeater runat="server" ID="repeaterDersler">
            <HeaderTemplate><table style="font-weight:bold;"></HeaderTemplate>
            <ItemTemplate>
                <tr id="item" style="margin-bottom:20px; background-color:#f6f6f6; width:960px;">
                    <td id="dersBaslik" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                        <%# DersLinkiniDondur(DataBinder.Eval(Container.DataItem, "KOD").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ISIM").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ID").ToString())%>
                    </td>
                    <td id="dersOkul" style="width:640px; padding-left:20px; color:#626262; padding-top:30px; padding-bottom:30px;">
                        <%# OkulLinkiniDondur( DataBinder.Eval(Container.DataItem, "OKUL_ISIM").ToString() , DataBinder.Eval(Container.DataItem, "OKUL_ID").ToString() )%>
                    </td>
                    <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top;">
                        <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr id="item" style="margin-bottom:20px; background-color:#ffffff; width:960px;">
                <td id="dersBaslik" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                    <%# DersLinkiniDondur(DataBinder.Eval(Container.DataItem, "KOD").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ISIM").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ID").ToString())%>
                </td>
                <td id="dersOkul" style="width:640px; padding-left:20px; color:#626262; padding-top:30px; padding-bottom:30px;">
                    <%# OkulLinkiniDondur( DataBinder.Eval(Container.DataItem, "OKUL_ISIM").ToString() , DataBinder.Eval(Container.DataItem, "OKUL_ID").ToString() )%>
                </td>
                <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top;">
                    <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
                </td>
            </tr>            
            </AlternatingItemTemplate>
            <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>
    </div>                   
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSonucYok" Visible="false" CssClass="pnlSonucYok">
        <div style="margin-top:30px; clear:both;">
                <span style="background-color:#191919; padding:10px 20px 10px 20px; color:#f6f6f6; 
        display:block; margin-bottom:10px; width:160px; font-weight:bold;"><asp:Label runat="server" ID="lblBaslik"></asp:Label></span>
            <br />
            <asp:Label runat="server" ID="lblSonucYok"></asp:Label>
        </div>
    </asp:Panel>
</asp:Content>
