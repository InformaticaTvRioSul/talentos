<%@ Page Title="Edições Anteriores" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="Edicoes.aspx.vb" Inherits="SiteTalentos_Edicoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
    <div class="container">
        <h1 class="titulo-light mb-5">Edições anteriores</h1>

        <div id="Retorno" class="row">
                   
         </div>
               
         <div class="d-block text-center">
             <span id="carregando" class="d-block text-cinza-medio"></span>
             <a id="cmdMostrarMais" class="btn-mostrar-mais">    
                 Carregar mais                      
            </a> 
         </div>        
    </div>
    <asp:HiddenField ID="HidTipo" Value="1" runat="server" />
     <asp:HiddenField ID="HidPagina" Value="0" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    <script type="text/javascript">
        function buscaDados() {
            var tipo = document.getElementById('ContentPlaceHolder1_HidTipo').value;
            var pagina = document.getElementById('ContentPlaceHolder1_HidPagina').value;
            var htmlText2 = '<i class="fa fa-spinner fa-pulse fa-2x fa-fw"></i><span>Carregando...</span>';
            document.getElementById('carregando').innerHTML = htmlText2;
            var e = this.value;
            $.ajax({
                type: "POST",
                url: "SiteTalentos/Metodos.aspx/edicoes",
                contentType: "application/json; charset=utf-8",
                data: "{pagina:" + pagina + ",tipo:" + tipo + "}",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d + ' erro');
                }
            });
        }

        buscaDados();

        $('#cmdMostrarMais').click(function () {
            buscaDados();
        });

        function OnSuccess(jsonResult) {
            document.getElementById('carregando').innerHTML = "";
            document.getElementById('ContentPlaceHolder1_HidPagina').value = Number(document.getElementById('ContentPlaceHolder1_HidPagina').value) + 1;
            var htmlText = $(jsonResult.d);
            if (jsonResult.d === "") { document.getElementById('cmdMostrarMais').className = "d-none"; }
            $("#Retorno").append(htmlText);
        }
    </script>
</asp:Content>

