<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Yorumlarim.aspx.cs" Inherits="Yorumlarim"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="content">
    <div id="divArkaplan" style="background: url('App_Themes/Default/Images/defter/papertexture1.jpg') repeat 0 0 White; 
    padding-bottom:10px;">
        <table style="width:100%;">
            <tr style="vertical-align:top; height:15px;">
                <td colspan="3" style="padding:10px 10px 10px 10px;">
                    <asp:Label runat="server" ID="lblYorumOzeti"></asp:Label>
                </td>
            </tr>
            <tr style="width:100%;">                
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnOkulYorumlarim" OnClick="OkulYorumlariniGoster"
                        Text="Okul yorumlarim"></asp:LinkButton></h1>
                </td>
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnHocaYorumlarim" OnClick="HocaYorumlariniGoster"
                        Text="Hoca yorumlarim"></asp:LinkButton></h1>
                </td>
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnDersYorumlarim" OnClick="DersYorumlariniGoster"
                        Text="Ders yorumlarim"></asp:LinkButton></h1>
                </td>
            </tr>
            <tr style="height:20px;">
                <td colspan="3" style="text-align:right;">
                    <img src="App_Themes/Default/Images/diger/cizgi2.gif" />
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlDersYorumlarim">
            <asp:Repeater runat="server" ID="repeaterDersYorumlarim" OnItemDataBound="DersYorum_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td style="text-decoration:underline;">
                                Ders
                            </td>
                            <td style="text-decoration:underline;">
                                Hoca
                            </td>
                            <td style="text-decoration:underline;">
                                Okul
                            </td>
                            <td style="text-decoration:underline;">
                                Yorum
                            </td>
                            <td style="text-decoration:underline;">
                                Gonderilme tarihi
                            </td>
                            <td style="text-decoration:underline;">
                                Aldigi puan
                            </td>
                            <td style="text-decoration:underline;">
                                Durum
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>' />
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "DERS_KODU") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "HOCA_ISIM") %>
                            <%# DataBinder.Eval(Container.DataItem, "KAYITSIZ_HOCA_ISIM") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "OKUL_ISMI") %>
                        </td>
                        <td>
                            <%# YorumKisalt(DataBinder.Eval(Container.DataItem, "YORUM")) %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "TARIH") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %>
                        </td>
                        <td>
                            <%# YorumDurumunuDondur(DataBinder.Eval(Container.DataItem , "YORUM_DURUMU")) %>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltrHack" /><a href="DersYorumGuncelle.aspx?DersID=<%# DataBinder.Eval(Container.DataItem , "DERS_ID")%>&DersYorumID=<%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>&KeepThis=true&TB_iframe=true&modal=true&height=400&width=600"
                                class="thickbox" title="Yorumu guncelle"><img src="App_Themes/Default/Images/diger/kalem.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnDersYorumSil" AlternateText="Sil" ImageUrl="App_Themes/Default/Images/Diger/silgi.png"
                            OnClick="DersYorumSil" ToolTip="Yorumu sil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:ImageButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Goruntulenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildigimiz yorumlar</span>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlOkulYorumlarim">
            <asp:Repeater runat="server" ID="repeaterOkulYorumlarim" OnItemDataBound="OkulYorum_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr style="text-decoration:underline;">
                            <td style="text-decoration:underline;">
                                Okul
                            </td>
                            <td style="text-decoration:underline;">
                                Yorum
                            </td>
                            <td style="text-decoration:underline;">
                                Gonderilme tarihi
                            </td>
                            <td style="text-decoration:underline;">
                                Aldigi puan
                            </td>
                            <td style="text-decoration:underline;">
                                Durum
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "OKULYORUM_ID") %>' />
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "OKUL_ISMI") %>
                        </td>
                        <td>
                            <%# YorumKisalt(DataBinder.Eval(Container.DataItem, "YORUM")) %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "TARIH") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %>
                        </td>
                        <td>
                            <%# YorumDurumunuDondur(DataBinder.Eval(Container.DataItem , "YORUM_DURUMU")) %>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltrHack" /><a href="OkulYorumGuncelle.aspx?OkulID=<%# DataBinder.Eval(Container.DataItem , "OKUL_ID")%>&KeepThis=true&TB_iframe=true&modal=true&height=400&width=600"
                                class="thickbox" title="Yorumu guncelle"><img src="App_Themes/Default/Images/diger/kalem.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnOkulYorumSil" AlternateText="Sil" ImageUrl="App_Themes/Default/Images/Diger/silgi.png"
                            ToolTip="Yorumu sil" OnClick="OkulYorumSil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:ImageButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Goruntulenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildigimiz yorumlar</span>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHocaYorumlarim">
            <asp:Repeater runat="server" ID="repeaterHocaYorumlarim" OnItemDataBound="HocaYorum_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td style="text-decoration:underline;">
                                Hoca
                            </td>
                            <td style="text-decoration:underline;">
                                Yorum
                            </td>
                            <td style="text-decoration:underline;">
                                Gonderilme tarihi
                            </td>
                            <td style="text-decoration:underline;">
                                Aldigi puan
                            </td>
                            <td style="text-decoration:underline;">
                                Durum
                            </td>                            
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>                            
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "HOCAYORUM_ID") %>' />
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "HOCA_ISMI") %>
                        </td>
                        <td>
                            <%# YorumKisalt(DataBinder.Eval(Container.DataItem, "YORUM")) %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "TARIH") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %>
                        </td>
                        <td>
                            <%# YorumDurumunuDondur(DataBinder.Eval(Container.DataItem , "YORUM_DURUMU")) %>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltrHack" /><a href="HocaYorumGuncelle.aspx?HocaID=<%# DataBinder.Eval(Container.DataItem , "HOCA_ID")%>&KeepThis=true&TB_iframe=true&modal=true&height=620&width=620"
                                class="thickbox" title="Yorumu guncelle"><img src="App_Themes/Default/Images/diger/kalem.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnHocaYorumSil" AlternateText="Sil" ImageUrl="App_Themes/Default/Images/Diger/silgi.png"
                            OnClick="HocaYorumSil" ToolTip="Yorumu sil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:ImageButton>
                        </td>                                           
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Goruntulenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildigimiz yorumlar</span>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlYorumYok">
            <span style="padding:10px; text-align:center;">Yorumunuz bulunmamaktadir</span>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHata">
            Bir hata olustu, biz de anlamadik. Tekrar denerseniz belki duzelir.
        </asp:Panel>
        <asp:Literal runat="server" ID="ltrScript"></asp:Literal>
    </div>
</asp:Content>
