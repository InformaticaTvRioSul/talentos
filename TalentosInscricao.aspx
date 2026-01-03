<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="TalentosInscricao.aspx.vb" Inherits="SiteTalentos_TalentosInscricao" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="modal fade" id="ClienteCadastro" tabindex="-1" role="dialog" aria-labelledby="ClienteCadastroLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-black">Novo Cliente</h5>
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
                    <h5 class="modal-title text-black">Nova Produtora</h5>
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
            <h1 class="titulo-dark mb-5"><a href="TalentosInscricoes" class="text-reset" style="font-size: 1.8rem">Minhas inscrições</a></h1>

            <asp:Literal ID="LitTitulo" runat="server"></asp:Literal>
            <asp:Literal ID="LitDados" runat="server"></asp:Literal>
            <asp:Panel ID="PanCliente" runat="server">
                <div class="form-row">
                    <div class="form-group col-sm-12 col-md-6">
                        <label for="CboCategoria">Categoria</label>
                        <asp:DropDownList ID="CboCategoria" CssClass="form-control form-control-sm" required Style="max-width: 300px;" runat="server">
                            <asp:ListItem Value="CAMPANHA">CAMPANHA</asp:ListItem>
                            <%--<asp:ListItem Value="MERCADO">MERCADO</asp:ListItem>--%>
                            <asp:ListItem Value="INSTITUCIONAL">INSTITUCIONAL</asp:ListItem>
                            <asp:ListItem Value="FILMES 30+">FILMES 30+</asp:ListItem>
                            <asp:ListItem Value="VAREJO">VAREJO</asp:ListItem>                            
                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-sm-12 col-md-6">
                        <label for="CboDuracao">Duracao</label>
                        <asp:DropDownList ID="CboDuracao" CssClass="form-control form-control-sm" required Style="max-width: 300px;" runat="server">
                            <asp:ListItem Value="15">15"</asp:ListItem>
                            <asp:ListItem Value="30">30"</asp:ListItem>
                            <asp:ListItem Value="45">45"</asp:ListItem>
                            <asp:ListItem Value="60">60"</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-12">
                        <label for="TxtTitulo">Título do Comercial</label>
                        <asp:TextBox ID="TxtTitulo" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="5" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-12">
                        <label for="TxtPeriodo">Período de Veiculação</label>
                        <asp:TextBox ID="TxtPeriodo" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="5" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-sm-12 col-md-6">
                        <div class="form-row">
                            <div class="form-group col-sm-12 col-md-10">
                                <label for="CboCliente">Cliente</label>
                                <asp:DropDownList ID="CboCliente" onchange="ClienteProdutora(this.value,0);" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="LblClienteSelecionado" runat="server" Text=""></asp:Label>
                                <%--<div id="Cliente-Selecionado"></div>--%>
                            </div>
                            <div class="form-group col-sm-12 col-md-2">
                                <label>-</label><br />
                                <a href="javascript:void(0);" onclick="novoCliente();" class="btn btn-sm btn-dark">Novo</a>
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-sm-12 col-md-6">
                        <div class="form-row">
                            <div class="form-group col-sm-12 col-md-10">
                                <label for="CboProdutora">Produtora</label>
                                <asp:DropDownList ID="CboProdutora" onchange="ClienteProdutora(this.value,1);" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="LblProdutoraSelecionado" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="form-group col-sm-12 col-md-2">
                                <label>-</label><br />
                                <a href="javascript:void(0);" onclick="novoProdutora();" class="btn btn-sm btn-dark">Novo</a>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="PanUploadMapa" CssClass="form-group col-12" runat="server">
                        <label for="FilMapaMidia">Mapa de Mídia *Somente arquivos PDF</label>
                        <asp:FileUpload ID="FilMapaMidia" CssClass="form-control form-control-sm" required runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="PanVisualizaMapa" CssClass="form-group col-12" Visible="false" runat="server">
                        <label for="FilMapaMidia">Mapa de Mídia *Somente arquivos PDF</label><br />
                        <asp:Literal ID="LitMapa" runat="server"></asp:Literal>
                        <asp:LinkButton ID="CmdExcluirMapa" CssClass="btn-sm btn-danger" runat="server">Excluir Mapa</asp:LinkButton>
                    </asp:Panel>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:LinkButton ID="CmdRecarregaCliente" class="d-none float-left" runat="server">Cliente</asp:LinkButton>
                        <asp:LinkButton ID="CmdRecarregaProdutora" class="d-none float-left" runat="server">Produtora</asp:LinkButton>
                        <asp:Button ID="CmdProsseguir" class="btn btn-dark px-5 py-4 float-md-right" runat="server" Text="Prosseguir" />
                    </div>
                </div>

            </asp:Panel>
            <asp:Panel ID="PanFinaliza" runat="server" Visible="false">
                <div class="form-row">
                    <h2>Confira os dados cadastrados e faça o upload do arquivo no <a href="https://wetransfer.com" class="text-info" style="font-size: 2rem;" target="_blank">WeTransfer*</a></h2>
                    <asp:Literal ID="LitDadosInscricao" runat="server"></asp:Literal>
                    <h6>Baixe o arquivo <a class="text-info" href="https://negociostvriosul.com.br/imagens/talentos/ArquivoAutorizacao.docx" target="_blank" >CESSÃO DE DIREITOS DE USO DE IMAGEM E VOZ</a>, anexe o arquivo preenchido e assinado junto com o material no WeTransfer</h6>
                    * Faça o upload do arquivo no WeTransfer. O arquivo deve ser renomeado com o código de Inscrição exibido acima. Ex: INSCRICAO-<%= HidInsCodigo.Value %> 
                    <div class="form-group col-12">
                        <label for="TxtLinkArquivo">Link do WeTransfer</label>
                        <asp:TextBox ID="TxtLinkArquivo" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="500" minlength="10" required type="url"></asp:TextBox>
                    </div>
                </div>
                <%--<div class=" d-flex justify-content-between">--%>
                <div class="row">
                    <div class="col-sm-6">
                        <asp:LinkButton ID="CmdVoltar" class="btn d-block d-sm-inline-block btn-outline-dark px-sm-3 py-2" runat="server">Editar</asp:LinkButton>
                    </div>
                    <div class="col-sm-6">
                        <asp:Button ID="CmdFinalizar" class="btn btn-block btn-dark px-5 py-4 mt-2 mt-sm-0 float-sm-right" runat="server" Text="Finalizar Inscrição" />
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

    <asp:HiddenField ID="HidInsCodigo" runat="server" />
    <asp:HiddenField ID="HidTalCodigo" runat="server" />
    <asp:HiddenField ID="HidTalTitulo" runat="server" />
    <asp:HiddenField ID="HidTalLogo" runat="server" />
    <asp:HiddenField ID="HidAgeCodigo" runat="server" />
    <asp:HiddenField ID="HidCliCodigo" runat="server" />
    <asp:HiddenField ID="HidProCodigo" runat="server" />
    <asp:HiddenField ID="HidFinalizado" runat="server" />
    <asp:HiddenField ID="HidMapaMidia" runat="server" />
    <asp:HiddenField ID="HidAgeNome" runat="server" />
    <asp:HiddenField ID="HidAgeEmail" runat="server" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    <script>
        $().ready(function () {
            $.validator.addMethod("valueNotEquals", function (value, element, arg) {
                return arg != element.value;
            }, "Value must not equal arg.");

            $("#form1").validate({
                ignore: [],
                rules: {
                    CboCategoria: { valueNotEquals: "0" },
                    CboDuracao: { valueNotEquals: "0" },
                    CboProdutora: { valueNotEquals: "0" },
                    CboCliente: { valueNotEquals: "0" }
                },
                messages: {
                    CboCategoria: { valueNotEquals: "Selecione a Categoria." },
                    CboDuracao: { valueNotEquals: "Selecione a Duração." },
                    CboProdutora: { valueNotEquals: "Selecione a Produtora." },
                    CboCliente: { valueNotEquals: "Selecione o CLiente." }
                }
            });
        });

        function novoCliente() {
            var f = document.getElementById('FrameCadCliente');
            f.src = 'SiteTalentos/ClienteCadastro.aspx?CliCodigo=0';
            $('#ClienteCadastro').modal('show');
            // document.getElementById('NovoAgendamento').style.display = 'block';
        }

        function novoProdutora() {
            var f = document.getElementById('FrameCadProdutora');
            f.src = 'SiteTalentos/ProdutoraCadastro.aspx?ProCodigo=0';
            $('#ProdutoraCadastro').modal('show');
            // document.getElementById('NovoAgendamento').style.display = 'block';
        }

        //Quando seleciona um Cliente, mostra os dados do mesmo
        function ClienteProdutora(valor, tipo) {
            var e = document.getElementById("ContentPlaceHolder1_CboCliente");
            if (tipo == 1) { e = document.getElementById("ContentPlaceHolder1_CboProdutora"); }
            var buffer = '';
            var cnpj = '';
            var telefone = '';
            var codigo = 0;
            if (e.options[e.selectedIndex].text != "Selecione") {
                var res = valor.split("*", 4);
                codigo = res[0];
                cnpj = res[2];
                if (cnpj.length == 14) {
                    cnpj = cnpj.replace(/\D/g, "");                     //Remove tudo o que não é dígito
                    cnpj = cnpj.replace(/(\d{2})(\d)/, "$1.$2");
                    cnpj = cnpj.replace(/(\d{3})(\d)/, "$1.$2");
                    cnpj = cnpj.replace(/(\d{3})(\d)/, "$1/$2");
                    cnpj = cnpj.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
                }
                telefone = res[3];
                if (telefone.length == 10) {
                    telefone = telefone.replace(/\D/g, "");                     //Remove tudo o que não é dígito
                    telefone = telefone.replace(/(\d{0})(\d)/, "$1($2");
                    telefone = telefone.replace(/(\d{2})(\d)/, "$1)$2");
                    telefone = telefone.replace(/(\d{4})(\d)/, "$1-$2");
                }
                if (telefone.length == 10) {
                    telefone = telefone.replace(/\D/g, "");                     //Remove tudo o que não é dígito
                    telefone = telefone.replace(/(\d{0})(\d)/, "$1($2");
                    telefone = telefone.replace(/(\d{2})(\d)/, "$1)$2");
                    telefone = telefone.replace(/(\d{5})(\d)/, "$1-$2");
                }

                buffer = '<b>DETALHES DO CLIENTE SELCIONADO</b><br /> <b>NOME FANTASIA:</b> ' + res[1] + '<br /><b>CNPJ:</b> ' + cnpj + '<br /><b>TELEFONE:</b> ' + telefone + '<br />';
            }
            else {
                buffer = '';
            }
            if (tipo == 0) { document.getElementById('ContentPlaceHolder1_HidCliCodigo').value = codigo; document.getElementById('ContentPlaceHolder1_LblClienteSelecionado').innerHTML = buffer; }
            else { document.getElementById('ContentPlaceHolder1_HidProCodigo').value = codigo; document.getElementById('ContentPlaceHolder1_LblProdutoraSelecionado').innerHTML = buffer; }

        }

        function atualizaComboCliente() {
            $('#ClienteCadastro').modal('hide');
            document.getElementById('ContentPlaceHolder1_CmdRecarregaCliente').click();
        }

        function atualizaComboProdutora() {
            $('#ProdutoraCadastro').modal('hide');
            document.getElementById('ContentPlaceHolder1_CmdRecarregaProdutora').click();
        }

    </script>
</asp:Content>

