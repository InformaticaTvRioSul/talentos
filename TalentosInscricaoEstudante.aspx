<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="TalentosInscricaoEstudante.aspx.vb" Inherits="SiteTalentos_TalentosInscricaoEstudante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="modal fade" id="ClienteCadastro" tabindex="-1" role="dialog" aria-labelledby="ClienteCadastroLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Novo Cliente</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body p-0">
                    <iframe name="FrameCadCliente" id="FrameCadCliente" style="border-width: 0; width: 100%; overflow-y: scroll; min-height: 650px;"></iframe>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ProdutoraCadastro" tabindex="-1" role="dialog" aria-labelledby="ProdutoraCadastroLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Nova Produtora</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body p-0">
                    <iframe name="FrameCadProdutora" id="FrameCadProdutora" style="border-width: 0; width: 100%; overflow-y: scroll; min-height: 650px;"></iframe>
                </div>
            </div>
        </div>
    </div>


    <div class="bg-white text-black">
        <div class="container py-3">
            <asp:Literal ID="LitResposta" runat="server"></asp:Literal>
            <h1 class="titulo-dark mb-5"><a href="TalentosInscricoesEstudante" class="text-reset" style="font-size: 1.8rem">Minhas inscrições</a></h1>

            <h2>Inscrição</h2>
            <br />
            <div class="container">
                <asp:Literal ID="LitTitulo" runat="server"></asp:Literal>
                <asp:Literal ID="LitDados" runat="server"></asp:Literal>
                <asp:Panel ID="PanCliente" runat="server">
                    <div class="form-row">
                        <div class="form-group col-sm-12 col-md-6">
                            <label for="CboDuracao">Duracao</label>
                            <asp:DropDownList ID="CboDuracao" CssClass="form-control form-control-sm" required Style="max-width: 300px;" runat="server">
                                <asp:ListItem Value="30">30"</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-12">
                            <label for="TxtTitulo">Título do Comercial</label>
                            <asp:TextBox ID="TxtTitulo" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="3" required type="text"></asp:TextBox>
                        </div>
                        <div class="form-group col-6">
                            <label for="TxtTitulo">Nome da Equipe</label>
                            <asp:TextBox ID="TxtNomeEquipe" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="3" required type="text"></asp:TextBox>
                        </div>
                        <div class="form-group col-6">
                            <label for="TxtTitulo">Slogan da Equipe</label>
                            <asp:TextBox ID="TxtSloganEquipe" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="3" required type="text"></asp:TextBox>
                        </div>
                        <div class="form-group col-12">
                            <label for="TxtIntregrantes">Integrantes e Matrículas (Máx. 5 pessoas por grupo)</label>
                            <asp:TextBox ID="TxtIntregrantes" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="500" minlength="5" required TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <div class="form-group col-12">
                            <label for="TxtCoordenador">Coordenador</label>
                            <asp:TextBox ID="TxtCoordenador" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="3" required type="text"></asp:TextBox>
                        </div>
                        <div class="form-group col-12">
                            <label for="TxtResponsavel">Responsável</label>
                            <asp:TextBox ID="TxtResponsavel" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="3" required type="text"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12">
                            <asp:Button ID="CmdProsseguir" class="btn btn-primary px-5 py-4 float-md-right" runat="server" Text="Prosseguir" />
                        </div>
                    </div>

                </asp:Panel>
                <asp:Panel ID="PanFinaliza" runat="server" Visible="false">
                    <div class="form-row">
                        <h2>Confira os dados cadastrados e faça o upload do arquivo no <a href="https://wetransfer.com" target="_blank">WeTransfer*</a></h2>
                        <asp:Literal ID="LitDadosInscricao" runat="server"></asp:Literal>
                         <h6>Baixe o arquivo <a class="text-info" href="https://negociostvriosul.com.br/imagens/talentos/ArquivoAutorizacao.docx" target="_blank" >CESSÃO DE DIREITOS DE USO DE IMAGEM E VOZ</a>, anexe o arquivo preenchido e assinado junto com o material no WeTransfer</h6>
                        * Faça o upload do arquivo no WeTransfer. O nome do arquivo deve ser renomeado com o código de Inscrição exibido acima. Ex: INSCRICAO-<%= HidInsCodigo.Value %>
                        <div class="form-group col-12 mt-2">
                            <label for="TxtLinkArquivo"><b>Link do material no WeTransfer</b></label>
                            <asp:TextBox ID="TxtLinkArquivo" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="500" minlength="10" required type="url"></asp:TextBox>
                        </div>
                    </div>
                    <%--<div class=" d-flex justify-content-between">--%>
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:LinkButton ID="CmdVoltar" class="btn d-block d-sm-inline-block btn-outline-primary px-sm-3 py-2" runat="server">Editar</asp:LinkButton>
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="CmdFinalizar" class="btn btn-block btn-success px-5 py-4 mt-2 mt-sm-0 float-sm-right" runat="server" Text="Finalizar Inscrição" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="HidInsCodigo" runat="server" />
    <asp:HiddenField ID="HidTalCodigo" runat="server" />
    <asp:HiddenField ID="HidTalTitulo" runat="server" />
    <asp:HiddenField ID="HidTalLogo" runat="server" />
    <asp:HiddenField ID="HidAgeCodigo" runat="server" />
    <asp:HiddenField ID="HidFinalizado" runat="server" />
    <asp:HiddenField ID="HidAgeNome" runat="server" />
    <asp:HiddenField ID="HidAgeEmail" runat="server" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="../js/validate/jquery.validate.min.js"></script>
    <script src="../js/validate/localization/messages_pt_BR.min.js"></script>
    <script>
        $().ready(function () {
            $.validator.addMethod("valueNotEquals", function (value, element, arg) {
                return arg != element.value;
            }, "Value must not equal arg.");

            $("#form1").validate({
                ignore: [],
                rules: {
                    CboDuracao: { valueNotEquals: "0" }
                },
                messages: {
                    CboDuracao: { valueNotEquals: "Selecione a Duração." }
                }
            });
        });
    </script>
</asp:Content>

