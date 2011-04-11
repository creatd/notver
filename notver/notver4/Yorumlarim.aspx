<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Yorumlarim.aspx.cs" Inherits="Yorumlarim"
    MasterPageFile="~/Masters/Giris.master"  MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorboxOkul").colorbox({iframe:true,width:'590px', height:'480px', close:''});    //Okul
        $("a.colorboxHoca").colorbox({iframe:true,width:'590px', height:'850px', close:''});    //Hoca
        $("a.colorboxDers").colorbox({iframe:true,width:'590px', height:'620px', close:''});    //Ders
    });
    </script>
</asp:Content>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="content">
    <div id="yorumlarim" style="background-color:White; margin-top:30px; display:block; padding:10px;">
        <table style="width:100%; border-bottom:solid 1pt #626262;">
            <tr style="vertical-align:top; height:15px;">
                <td colspan="3" style="padding:10px 10px 10px 10px;">
                    <asp:Label runat="server" ID="lblYorumOzeti"></asp:Label>
                </td>
            </tr>
            <tr style="width:100%; height:20px;">                
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnOkulYorumlarim" OnClick="OkulYorumlariniGoster"
                        Text="Okul yorumlarım"></asp:LinkButton></h1>
                </td>
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnHocaYorumlarim" OnClick="HocaYorumlariniGoster"
                        Text="Hoca yorumlarım"></asp:LinkButton></h1>
                </td>
                <td style="float:right; padding-right:20px;">
                    <h1><asp:LinkButton runat="server" ID="btnDersYorumlarim" OnClick="DersYorumlariniGoster"
                        Text="Ders yorumlarım"></asp:LinkButton></h1>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlDersYorumlarim">
            <asp:Repeater runat="server" ID="repeaterDersYorumlarim" OnItemDataBound="DersYorum_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th >
                                Ders
                            </th>
                            <th >
                                Hoca
                            </th>
                            <th >
                                Okul
                            </th>
                            <th >
                                Yorum
                            </th>
                            <th >
                                Gönderilme tarihi
                            </th>
                            <th >
                                Aldığı puan
                            </th>
                            <th >
                                Durum
                            </th>
                            <th>
                                &nbsp;
                            </th>
                            <th>
                                &nbsp;
                            </th>
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
                            <asp:Literal runat="server" ID="ltrHack" /><a class="colorboxDers" title="Guncelle"
                            href="DersYorumYap.aspx?DersID=<%# DataBinder.Eval(Container.DataItem , "DERS_ID")%>&DersYorumID=<%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>">
                                <img src="App_Themes/Default/Images/guncelle.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnDersYorumSil" AlternateText="Sil" ImageUrl="App_Themes/Default/Images/sil.png"
                            OnClick="DersYorumSil" ToolTip="Sil" OnClientClick="return confirm('İki kere düşündün mü?');"></asp:ImageButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Görüntülenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildiğimiz yorumlar</span>
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
                        <tr >
                            <th >
                                Okul
                            </th>
                            <th >
                                Yorum
                            </th>
                            <th >
                                Gönderilme tarihi
                            </th>
                            <th >
                                Aldığı puan
                            </th>
                            <th >
                                Durum
                            </th>
                            <th>
                                &nbsp;
                            </th>
                            <th>
                                &nbsp;
                            </th>
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
                            <asp:Literal runat="server" ID="ltrHack" /><a class="colorboxOkul" title="Guncelle"
                            href="OkulYorumYap.aspx?OkulID=<%# DataBinder.Eval(Container.DataItem , "OKUL_ID")%>">
                                <img src="App_Themes/Default/Images/guncelle.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnOkulYorumSil" AlternateText="Sil" 
                            ImageUrl="App_Themes/Default/Images/sil.png"
                            ToolTip="Sil" OnClick="OkulYorumSil" OnClientClick="return confirm('İki kere düşündün mü?');"></asp:ImageButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Görüntülenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildiğimiz yorumlar</span>
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
                            <th >
                                Hoca
                            </th>
                            <th >
                                Yorum
                            </th>
                            <th >
                                Gönderilme tarihi
                            </th>
                            <th >
                                Aldığı puan
                            </th>
                            <th >
                                Durum
                            </th>                            
                            <th>
                                &nbsp;
                            </th>
                            <th>
                                &nbsp;
                            </th>                            
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
                            <asp:Literal runat="server" ID="ltrHack" /><a class="colorboxHoca" title="Guncelle"
                            href="HocaYorumYap.aspx?HocaID=<%# DataBinder.Eval(Container.DataItem , "HOCA_ID")%>"
                                ><img src="App_Themes/Default/Images/guncelle.png" alt="Guncelle" /></a>
                                <asp:Literal runat="server" ID="ltrHack2" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnHocaYorumSil" AlternateText="Sil" 
                            ImageUrl="App_Themes/Default/Images/sil.png"
                            OnClick="HocaYorumSil" ToolTip="Sil" OnClientClick="return confirm('İki kere düşündün mü?');"></asp:ImageButton>
                        </td>                                           
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7" class="sessiz" style="text-align:right;">
                            <span style="Color:Green;">Görüntülenen yorumlar</span>
                            <br />
                            <span style="Color:Gray;">Onay bekleyen yorumlar</span>
                            <br />
                            <span style="Color:Red;">Sizin veya bizim sildiğimiz yorumlar</span>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlYorumYok">
            <span style="padding:10px; text-align:center; display:block;">- Yorumun yok -</span>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHata">
            Bir hata oluştu, biz de anlamadık. Tekrar deneyinz belki düzelir.
        </asp:Panel>
        <asp:Literal runat="server" ID="ltrScript"></asp:Literal>
    </div>
</asp:Content>
