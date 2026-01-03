<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="TalentosConfirma.aspx.vb" Inherits="SiteTalentos_TalentosConfirma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="bg-white">
    <div class="container text-black" style="min-height:400px;">
            <div class="text-center">
                <asp:Literal ID="LitImagem" runat="server"></asp:Literal>
            </div>

            <div class="bg-white pt-5">
                <h3 class="text-center pt-2">Parabéns, sua inscrição foi realizada com sucesso</h3>
                <br />
                <asp:Literal ID="LitLink" runat="server"></asp:Literal>
            </div>
        </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" Runat="Server">
</asp:Content>

