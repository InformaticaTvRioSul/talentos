<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="SiteTalentos_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jsAgenda.js"></script>
    <style>
        .error {
            color: #ff0000;
        }

        input.error {
            border-bottom-color: #ff0000 !important;
        }

        input {
            text-transform: uppercase;
        }
        .inscricao {
            width: 100%; max-width: 600px; text-align: center; border: 1px solid #000; padding: 24px; color:#000;
        }
        .inscricao p {
            font-size: 1.6rem;
        }
        .inscricao h4 {
            font-size: 2rem;
            font-weight: bold;
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="modal fade" id="ModEsqueceu" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-dark" id="exampleModalLabel">Reenviar senha</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label class="text-dark">CNPJ</label>
                    <asp:TextBox ID="TxtCNPJ" CssClass="form-control" placeholder="CNPJ" onkeyup="mascara(this, mcnpj);" AutoCompleteType="Disabled" MaxLength="18" minlength="13" required type="tel" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="CmdReenviaSenha" CssClass="btn btn-dark" runat="server">Enviar Senha</asp:LinkButton>
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>


    <asp:Literal ID="LitResposta" runat="server"></asp:Literal>
    <div class="container-fluid bg-white">
        <div class="container pt-4" style="min-height:450px;">
            <asp:Panel ID="PanLogin" Visible="false" runat="server">
                <h1 class="titulo-dark mb-5">Login</h1>
                <div class="row no-gutters" style="color: #000">
                    <div class="col-md-6 col-lg-4 border border-dark">
                        <div class="p-3">
                            <h3>Olá!</h3>
                            <h5>Caso já tenha um cadastro, faça o login:</h5>

                            <div class="form-row">
                                <div class="col-12">
                                    <label>Usuário</label>
                                    <asp:TextBox ID="TxtLogin" CssClass="form-control" placeholder="CNPJ" onkeyup="mascara(this, mcnpj);" AutoCompleteType="Disabled" MaxLength="18" minlength="13" required type="tel" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <label>Senha</label>
                                    <asp:TextBox ID="TxtSenha" CssClass="form-control" TextMode="Password" MaxLength="10" minlength="6" placeholder="Senha" required runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <asp:Button ID="CmdLogin" CssClass="btn btn-dark btn-block mb-1 rounded-0" runat="server" Text="Login" />

                            <a href="javascript:void(0)" data-toggle="modal" data-target="#ModEsqueceu" class="mr-auto small" style="color: #000; text-decoration: underline">Esqueci a senha</a>
                            <br />
                            <a href="TalentosAgencia" class="ml-auto small" style="color: #000; text-decoration: underline">Quero me cadastrar</a>

                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanMensagemInscricao" runat="server">
                <h1 class="titulo-dark mb-5">Inscrição</h1>
                <div class="d-flex justify-content-center align-content-center">
                <div class="inscricao">
                    <asp:Literal ID="LitMensagemInscricao" runat="server"></asp:Literal>                 
                </div>
                    </div>

            </asp:Panel>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    <script src="../js/validate/jquery.validate.min.js"></script>
    <script src="../js/validate/localization/messages_pt_BR.min.js"></script>
    <script>
        $().ready(function () {
            $("#form1").validate({});
        });
    </script>
</asp:Content>

