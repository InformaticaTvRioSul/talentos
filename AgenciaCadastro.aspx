<%@ Page Title="" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="AgenciaCadastro.aspx.vb" Inherits="SiteTalentos_AgenciaCadastro" %>

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
    <div class="bg-white text-black">
        <div class="container py-3">
            <asp:Literal ID="LitResposta" runat="server"></asp:Literal>
            <h1 class="titulo-dark mb-5"><a href="TalentosInscricoes" class="text-reset" style="font-size: 1.8rem">Novo Cadastro</a></h1>

            <div class="border p-3 my-2 rounded">
                <h3 class="border border-top-0 border-left-0 border-right-0 pb-2"><i class="fa fa-address-card-o fa-1x" aria-hidden="true"></i> Dados de Cadastro</h3>
                <div class="form-row">
                    <div class="form-group col-md-6 col-lg-6">
                        <label for="TxtNome">Nome Fantasia</label>
                        <asp:TextBox ID="TxtNomeFantasia" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="5" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6 col-lg-6">
                        <label for="TxtNome">Razão Social</label>
                        <asp:TextBox ID="TxtRazaoSocial" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="5" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4 col-lg-2">
                        <label for="TxtCNPJ">CNPJ</label>
                        <asp:TextBox ID="TxtCNPJ" CssClass="form-control form-control-sm" runat="server" onkeyup="mascara(this, mcnpj);" AutoCompleteType="Disabled" MaxLength="18" minlength="13" required type="tel"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4 col-lg-5">
                        <label for="TxtEmail">E-mail</label>
                        <asp:TextBox ID="TxtEmail" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="10" required placeholder="seuemail@seuprovedor.com.br" type="email"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4 col-lg-5">
                        <label for="TxtEmail">E-mail 2</label>
                        <asp:TextBox ID="TxtEmail2" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="10" placeholder="seuemail@seuprovedor.com.br" type="email"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4 col-lg-6">
                        <label for="TxtResponsavel">Responsável</label>
                        <asp:TextBox ID="TxtResponsavel" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="5" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4 col-lg-3">
                        <label for="TxtTelefone">Telefone</label>
                        <asp:TextBox ID="TxtTelefone" CssClass="form-control form-control-sm" runat="server" onkeyup="mascara(this, mtel);" AutoCompleteType="Disabled" MaxLength="14" minlength="13" required type="tel" placeholder="(00)0000-0000" title="(00)0000-0000"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4 col-lg-3">
                        <label for="TxtTelefone2">Telefone 2</label>
                        <asp:TextBox ID="TxtTelefone2" CssClass="form-control form-control-sm" runat="server" onkeyup="mascara(this, mtel);" AutoCompleteType="Disabled" MaxLength="14" minlength="13" type="tel" placeholder="(00)0000-0000" title="(00)0000-0000"></asp:TextBox>
                    </div>


                </div>
            </div>

            <div class="border p-3 mb-2 rounded">
                <h3 class="border border-top-0 border-left-0 border-right-0 pb-2"><i class="fa fa-map-marker fa-1x" aria-hidden="true"></i> Endereço</h3>
                <div class="form-row">
                    <div class="form-group col-md-4 col-lg-2">
                        <asp:Label ID="LblCEP" runat="server" Text="CEP"></asp:Label>
                        <asp:TextBox ID="TxtCEP" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" onkeyup="mascara(this, mcep);" MaxLength="9" minlength="9" required type="tel" placeholder="00000-000" title="00000-000"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <br />
                        <a href="javascript:void(0);" class="btn btn-sm btn-dark" id="BtnBuscaCEP">Buscar Endereço</a>
                    </div>
                    <div class="col-md-2">
                        <div id="divCarregando"></div>
                    </div>
                    <div class="w-100"></div>
                    <div class="form-group col-md-6 col-lg-9">
                        <asp:Label ID="LblEndereco" runat="server" Text="Endereço"></asp:Label>
                        <asp:TextBox ID="TxtEndereco" Style="text-transform: uppercase" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="100" minlength="1" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2 col-lg-1">
                        <asp:Label ID="LblNumero" runat="server" Text="Número"></asp:Label>
                        <asp:TextBox ID="TxtNumero" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="10" minlength="1" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label ID="LblComplemento" runat="server" Text="Complemento"></asp:Label>
                        <asp:TextBox ID="TxtComplemento" Style="text-transform: uppercase" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="30" type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label ID="LblBairro" runat="server" Text="Bairro"></asp:Label>
                        <asp:TextBox ID="TxtBairro" Style="text-transform: uppercase" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="50" minlength="2" required type="text"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <asp:Label ID="LblCidade" runat="server" Text="Cidade"></asp:Label>
                        <asp:TextBox ID="TxtCidade" Style="text-transform: uppercase" CssClass="form-control form-control-sm" runat="server" AutoCompleteType="Disabled" MaxLength="50" minlength="2" required type="text"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Label ID="LblEstado" runat="server" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="CboEstado" CssClass="form-control form-control-sm" runat="server">
                            <asp:ListItem Value="0">Selecione</asp:ListItem>
                            <asp:ListItem Value="AC">Acre</asp:ListItem>
                            <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                            <asp:ListItem Value="AP">Amapá</asp:ListItem>
                            <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                            <asp:ListItem Value="BA">Bahia</asp:ListItem>
                            <asp:ListItem Value="CE">Ceará</asp:ListItem>
                            <asp:ListItem Value="DF">Distrito Federal</asp:ListItem>
                            <asp:ListItem Value="ES">Espirito Santo</asp:ListItem>
                            <asp:ListItem Value="GO">Goiás</asp:ListItem>
                            <asp:ListItem Value="MA">Maranhão</asp:ListItem>
                            <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                            <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                            <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                            <asp:ListItem Value="PA">Pará</asp:ListItem>
                            <asp:ListItem Value="PB">Paraiba</asp:ListItem>
                            <asp:ListItem Value="PR">Paraná</asp:ListItem>
                            <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                            <asp:ListItem Value="PI">Piauí</asp:ListItem>
                            <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                            <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                            <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                            <asp:ListItem Value="RO">Rondônia</asp:ListItem>
                            <asp:ListItem Value="RR">Roraima</asp:ListItem>
                            <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                            <asp:ListItem Value="SP">São Paulo</asp:ListItem>
                            <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                            <asp:ListItem Value="TO">Tocantis</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="border p-3 mb-2">
                <h3 class="border border-top-0 border-left-0 border-right-0 pb-2"><i class="fa fa-lock fa-1x" aria-hidden="true"></i> Senha de Acesso</h3>
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label>Senha</label>
                        <asp:TextBox ID="TxtSenha" CssClass="form-control" TextMode="Password" MaxLength="10" minlength="6" required placeholder="6 a 10 caracteres" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Confirme a Senha</label>
                        <asp:TextBox ID="TxtSenha2" CssClass="form-control" TextMode="Password" MaxLength="10" minlength="6" required placeholder="6 a 10 caracteres" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>-</label>
                        <asp:Button ID="CmdSalvar" CssClass="btn btn-dark btn-block" runat="server" Text="Registrar" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="HidAgeCodigo" runat="server" />
    <asp:HiddenField ID="HidTalTitulo" runat="server" />
    <asp:HiddenField ID="HidTalDataFim" runat="server" />
    <asp:HiddenField ID="HidTalLogo" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    <script src="js/jsAgenda.js"></script>  
    <script src="js/validate/jquery.validate.min.js"></script>
    <script src="js/validate/localization/messages_pt_BR.min.js"></script>
    <script>
        $().ready(function () {
            $.validator.addMethod("valueNotEquals", function (value, element, arg) {
                return arg != element.value;
            }, "Value must not equal arg.");

            $("#form1").validate({
                ignore: [],
                rules: {
                    CboEstado: { valueNotEquals: "0" },
                    TxtSenha2: {
                        equalTo: "#ContentPlaceHolder1_TxtSenha"
                    }
                },
                messages: {
                    CboEstado: { valueNotEquals: "Selecione o Estado." },
                    TxtSenha2: { equalTo: "Senhas não conferem." }
                }
            });
        });


        $(document).ready(function () {
            $('#ContentPlaceHolder1_TxtCEP').keyup(function () {
                var cep = this.value;
                cep = cep.replace(/-/g, "");
                cep = cep.replace(/\./g, "");
                if (cep.length == 8) {
                    var htmlText2 = '<i class="fa fa-spinner float-left fa-pulse fa-3x fa-fw"></i><span>Carregando...</span>';
                    $("#divCarregando").append(htmlText2);
                    $.ajax({
                        type: "POST",
                        url: "SiteTalentos/Metodos.aspx/CEPList",
                        contentType: "application/json; charset=utf-8",
                        data: "{cep:'" + cep + "'}",
                        dataType: "json",
                        success: OnSuccess2,
                        failure: function (response) {
                            alert(response.d + ' erro');
                        }
                    });
                }
            });

        });


        $(document).ready(function () {
            $('#BtnBuscaCEP').click(function () {
                var cep = document.getElementById('ContentPlaceHolder1_TxtCEP').value;
                cep = cep.replace(/-/g, "");
                cep = cep.replace(/\./g, "");
                if (cep.length == 8) {
                    var htmlText2 = '<i class="fa fa-spinner float-left fa-pulse fa-3x fa-fw"></i><span>Carregando...</span>';
                    $("#divCarregando").append(htmlText2);
                    $.ajax({
                        type: "POST",
                        url: "SiteTalentos/Metodos.aspx/CEPList",
                        contentType: "application/json; charset=utf-8",
                        data: "{cep:'" + cep + "'}",
                        dataType: "json",
                        success: OnSuccess2,
                        failure: function (response) {
                            alert(response.d + ' erro');
                        }
                    });
                }
                else {
                    //nao tem plano entao nao precisa verificar                    
                    document.getElementById('divCarregando').innerHTML = "";
                    alert('Digite um CEP válido');
                }
            });

        });
        function OnSuccess2(jsonResult) {
            $(jsonResult.d).each(function () {
                if (this.Retorno == 1) {
                    document.getElementById('divCarregando').innerHTML = "";
                    try {
                        $("#CboEstado").val(this.UF);
                    }
                    catch (e2) { }
                    document.getElementById('ContentPlaceHolder1_TxtEndereco').value = this.Logradouro;
                    document.getElementById('ContentPlaceHolder1_TxtBairro').value = this.Bairro;
                    document.getElementById('ContentPlaceHolder1_TxtCidade').value = this.Cidade;
                    document.getElementById("ContentPlaceHolder1_TxtNumero").focus();
                }
                else {
                    document.getElementById('divCarregando').innerHTML = "";
                    alert('Não foi possível buscar o endereço');
                }
            });
        }
    </script>
</asp:Content>

