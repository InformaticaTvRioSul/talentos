<%@ Page Title="Edições Anteriores" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="Edicao.aspx.vb" Inherits="SiteTalentos_Edicoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">  
    <style>
        .slideHome {
            height: 600px;
        }

        .slideImagem {
            width: 100%;
            height: 100%;
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            overflow: hidden;
        }

        .carousel-control-next, .carousel-control-prev {
            opacity: .8;
        }

        .carousel-control-prev {
            left: -105px;
        }

        .carousel-control-next {
            right: -105px;
        }

        .carousel-control-next-icon, .carousel-control-prev-icon {
            width: 40px;
            height: 40px;
        }


        @media (max-width: 767px) {
            .slideHome {
                height: 410px;
                width: 100%;
            }

            .slideImagem {
                width: 100%;
                height: 70%;
            }

            .carousel-control-prev {
                left: 0;
            }

            .carousel-control-next {
                right: 0;
            }

            .carousel-control-next-icon, .carousel-control-prev-icon {
                width: 20px;
                height: 20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
     <div style="width: 100%; text-align: right;">
                <a style="font-size: 0.7rem" href="<%=Link%>TalentosEdicoes">< voltar</a>
            </div>
            </div>
    <div class="container">
        <div class="d-flex justify-content-between">
<h1 class="titulo-light mb-5"><asp:Literal ID="LitTitulo" runat="server"></asp:Literal></h1>
            <h1 class="mb-5"><asp:Literal ID="LitVencedores" runat="server"></asp:Literal></h1>
        </div>
        

        <asp:Panel ID="PanDados" runat="server">       
        <section>
            <div class="container py-5" style="min-height: 200px;">                      
            <asp:Panel ID="PanGaleria" runat="server">           
                <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <asp:Literal ID="LitGaleriaBadge" runat="server"></asp:Literal>                       
                    </ol>
                    <div class="carousel-inner">
                        <asp:Literal ID="LitGaleriaImagens" runat="server"></asp:Literal>                        
                    </div>
                    <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                 </div>
                 </asp:Panel>
                <div style="color:#fff!important">
                    <asp:Literal ID="LitTexto" runat="server"></asp:Literal>    
                </div>
                 
             </div>
        </section>
    </asp:Panel>    
    </div>
    <asp:HiddenField ID="HidTipo" Value="1" runat="server" />
     <asp:HiddenField ID="HidPagina" Value="0" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    
</asp:Content>

