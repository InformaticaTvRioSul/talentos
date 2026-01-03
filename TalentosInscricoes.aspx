<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="TalentosInscricoes.aspx.vb" Inherits="SiteTalentos_TalentosInscricoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Literal ID="LitResposta" runat="server"></asp:Literal>
    <div class="container-fluid bg-white" style="min-height: 300px; color: #000;">
        <div class="container">
            <br />
            <h1 class="titulo-dark mb-5">Minhas inscrições</h1>
            <asp:Literal ID="LitInscricao" runat="server"></asp:Literal>
            <br />
            <br />
            <div class="container">
                <asp:Literal ID="LitInscricoes" runat="server"></asp:Literal>
                <asp:Label ID="LblGrdInscricoes" runat="server" Text=""></asp:Label>
                <div class="table-responsive">


                    <asp:GridView ID="GrdInscricoes" CssClass="table table-hover table-sm grid-inscricao my-4" runat="server" GridLines="None" CellSpacing="-1" AllowPaging="False"
                        AutoGenerateColumns="False" DataKeyNames="InsCodigo" HeaderStyle-Wrap="True" ShowHeader="true">
                        <Columns>
                            <asp:BoundField DataField="InsCodigo" HeaderText="Inscrição">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TalTitulo" HeaderText="Edição" />
                            <asp:BoundField DataField="InsTitulo" HeaderText="Título" />
                            <asp:BoundField DataField="InsCategoria" HeaderText="Categoria" />
                            <asp:BoundField DataField="CliNomeFantasia" HeaderText="Cliente" />
                            <asp:BoundField DataField="InsDataCadastro" HeaderText="Data Cad." DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InsDataFinalizacao" HeaderText="Finalização" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InsFinalizado" HeaderText="Status" />

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <a href='TalentosInscricao?Codigo=<%# DataBinder.Eval(Container.DataItem, "InsCodigo")%>' class="btn btn-outline-dark btn-sm float-right">Ver</a>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Size="Small" CssClass="bg-dark text-white" />
                        <RowStyle Font-Size="Small" />
                    </asp:GridView>
                </div>
                <br />
            </div>
        </div>
        <asp:HiddenField ID="HidAgeCodigo" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
</asp:Content>

