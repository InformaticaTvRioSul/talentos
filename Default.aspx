<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="SiteTalentos_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://www.googletagmanager.com/gtag/js?id=UA-140183664-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-140183664-1');
    </script>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="Página da área comercial da TV Rio Sul" />
    <meta name="keywords" content="TV Rio Sul, tvriosul, Comercial, Televisão, anuncio de TV, mídia, televisiva, Contato, whatshapp" />
    <meta content="Negocios TV Rio Sul" name="Vendas" />
    <meta content="(24) 99313-9599, (24) 3355-9800" name="Contato" />
    <meta content="NegociosTVRioSul" name="title" />
    <meta content="Telefone é o (24) 99313-9599. (24) 3355-9800
É possível enviar fotos, vídeos, sugestões de reportagens e flagrantes.
Possíveis contato comercial.
Anuncie na TV Rio Sul, área comercial"
        name="description" />
    <meta content="negócios, contato comercial, Resende, Sul Fluminense, Sul do Rio e Costa Verde" name="keywords" />
    <meta property="og:title" content="Negocios TV Rio Sul" />
    <meta property="og:locale" content="pt_BR" />
    <meta property="og:url" content="https://negociostvriosul.com.br" />
    <meta property="og:site_name" content="Negocios TV Rio Sul" />
    <title>Talentos TV Rio Sul</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Heebo:wght@400;700&display=swap" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/d8dc78be34.js" crossorigin="anonymous"></script>

    <style>
        .borda-right-branco {
            border-right: 2px solid rgba(204, 159, 60, 0.5);
        }

        .logoPrincipal {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .logoPatrocinio {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-top:50px;    
        }

        .logoPatrocinio > p {
            margin:10px 10px 0 0;
            font-size:bold;
        }

        .logoPatrocinio > img {    
                max-width: 180px;
                /*background: #fff;*/
                /*border-radius: 30px;*/
                padding:15px 20px;
                margin:0 5px;
            }

            .logoPrincipal > img {
                padding: 0 20px;
                width: 100%;
                max-width: 550px;
            }

        .menu-home {
        }

            .menu-home a {
                position: relative;
                padding-top: 5px;
                display: block;
                margin-bottom: 8px;
                color: #CC9F3C;
            }

                /*.menu-home a:before {
                    content: "";
                    position: absolute;
                    left: 0;
                    top: 0;
                    height: 4px;
                    width: 20px;*/ /* or 100px */
                    /*border-bottom: 4px solid #fff;
                    border-radius: 4px;
                    margin-bottom: 4px;
                }*/


        .inscricao, .inscricao-h {
            text-align: center;
        }


            .inscricao h2 {
                color:#fff;
                font-size: 20px;
                margin-bottom: 28px;
                line-height: 0.9;
                font-weight:bold;
                text-transform:uppercase;
            }

                .inscricao h2 b {
                    font-size:20px;
                    display: block;
                }

            .inscricao a {
                width: 300px;
                display: inline-block;
                padding: 20px 10px;
                /*height: 60px;
                line-height: 60px;*/
                color: #fff;
                font-weight:bold;
                background-color: rgba(255, 255, 255, 0.0);
                border: 2px solid #fff;
                margin-bottom: 10px;
                border-radius:10px;
            }

            .inscricao-h {
                padding:30px ;
                margin-bottom:100px;
                position:relative;
                top:-28px;
                margin-left:20px;
            }

            .inscricao-h a {
                font-size:12px;
                width: 150px;
                display: inline-block;
                padding:5px 0;
                color: #ed82b9;
                font-weight:bold;
                border:  3px solid #ed82b9;
                margin-bottom: 20px;
                border-radius:10px;

            }


            @media (max-width: 700px) {
                .inscricao-h {
                padding:30px ;
                position:relative;
                top:-40px;
                margin-left:200px
            }

            .inscricao-h a {
                font-size:12px;
                width: 150px;
                display: inline-block;
                padding:5px 0;
                color: #ed82b9;
                font-weight:bold;
                border:  3px solid #ed82b9;
                margin-bottom: 20px;
                border-radius:10px;

            }

            .logoPatrocinio > img {    
                max-width: 80px;
                background: #fff;
                border-radius: 30px;
                padding:15px 20px;
                margin:0 5px;
            }
            .logoPatrocinio > p {
            margin:5px 10px 0 0;
            font-size:10px;
        }

            }

            
/*
                .inscricao a:hover {
                    border: #000;
                    transition:0.3s;
                   
                }*/
    </style>
</head>
<body>
    <section class="bg-azul2">
    <form id="form1" runat="server">
        <div class="container my-5">
            <div class="row">


                <div class="col col-md-8  borda-right-branco ">

                <div class="logoPrincipal">
                    <asp:Literal ID="LitLogo" runat="server"></asp:Literal>
                    
                </div>

                <div class="logoPatrocinio">
                    <p>Apoio:</p>
                    <img src="<%=Link%>SiteTalentos/imagens/zamix-logo.svg" alt="Zamix" />
                    <img src="<%=Link%>SiteTalentos/imagens/patro.svg" alt="Zamix" />
                    <img src="<%=Link%>SiteTalentos/imagens/Cibal.svg" alt="Zamix" />
                </div>




                </div>
                


                <div class="col col-md-4 px-md-3 px-lg-5">
                    <div class="menu-home">
                        <a href="#sobre">Sobre</a>
                        <a href="#inscricao">Regulamento</a>
                        <a href="#inscricao">Inscrições</a>
                        <a href="#premio">Prêmio</a>
                        <asp:Literal ID="LitVencedores" runat="server"></asp:Literal>                        
                        <a href="<%=Link%>TalentosEdicoes">Edições Anteriores</a>
                        <a href="<%=Link%>TalentosContato">Contato</a>
                    </div>
                    <br />
                    <a href="#inscricao" style="color:#CC9F3C">
                                            
<%--                        <img src="<%=Link%>SiteTalentos/imagens/iconeLogin.svg" style="width: 16px; margin-right:6px;" alt="Login" />--%>Login
                    </a>
                </div>
            </div>
        </div>

        </section>

    

        <section id="sobre" class="bg-purpe py-5 text-dark">


     <div class="inscricao-home"> 
        <div class="info-home">

            <div class="item1">
                <p>O que te espera em 2025</p>
            </div>
                  <div class="item">
                    <p>Localização </p>
                    <p>Volta Redonda, RJ</p>
                </div>

                <div class="item" style="display:none">
                    <p>Data </p>
                    <p>14 de março</p>
                </div>
                <div class="item">
                    <p>Inscrição</p>
                    <p>08 a 24 de Janeiro</p>
                </div>

            <div class="inscricao-h" style="display:none">
                <a href="https://materiais.negociostvriosul.com.br/confirmacao-22-talentos-da-publicidade" target="_blank">Confirmar Presença</a>
            </div>

<%--                <div class=" align-items-center inscricao-home ">
                     <a href="https://materiais.negociostvriosul.com.br/confirmacao-21-talentos-da-publicidade" target="_blank" >CONFIRMAR PRESENÇA</a>
                </div>--%>



            </div>
    </div>


            <div class="categorias">

                <h1>Categorias</h1>
                <div class="sub">
                <div class="varejo">
                    <img src="<%=Link%>SiteTalentos/imagens/Categorias/CatVarejo.png" alt="Alternate Text" />
                    <h4>Varejo</h4>
                </div>
                <div class="varejo">
                    <img src="<%=Link%>SiteTalentos/imagens/Categorias/CatCampanha.png" alt="Alternate Text" />
                    <h4>Campanha</h4>
                </div>

                <div class="varejo">
                    <img src="<%=Link%>SiteTalentos/imagens/Categorias/CatInstitucional.png" alt="Alternate Text" />
                    <h4>Institucional</h4>
                </div>

                <div class="varejo">
                    <img src="<%=Link%>SiteTalentos/imagens/Categorias/CatValorSocial.png" alt="Alternate Text" />
                    <h4>Valor Social</h4>
                </div>
                <div class="varejo">
                    <img src="<%=Link%>SiteTalentos/imagens/Categorias/CatEstudante.png" alt="Alternate Text" />
                    <h4>Estudantes</h4>
                </div>
                    </div>

            </div>



            <div class="container">
                <div class="categorias">
                <h2 >Sobre o Talentos da Publicidade</h2>
                <asp:Literal ID="LitSobre" runat="server"></asp:Literal>
               </div>

                <div class="img-home">
                    <img src="<%=Link%>SiteTalentos/imagens/bg-home.png" alt="Alternate Text" />
               </div>
        </section>

    <div class="bg-azul2">
        <section id="inscricao" class="container my-5">            




               
            <div class="row">
                <asp:Literal ID="LitInscricao" runat="server"></asp:Literal>
               <%-- <div class="col col-md-6 borda-right-branco inscricao">
                    <h2>Talentos da Publicidade<b>Estudantes</b></h2>

                    <div class="d-flex flex-column align-items-center">
                        <a href="#">LEIA O REGULAMENTO</a>

                        <a href="#">INSCRIÇÕES ESTUDANTES</a>
                    </div>

                </div>
                <div class="col col-md-6 inscricao">
                    <h2>Talentos da Publicidade<b>Agências</b></h2>
                    <div class="d-flex flex-column align-items-center">
                        <a href="#">LEIA O REGULAMENTO</a>
                        <a href="#">INSCRIÇÕES AGÊNCIAS</a>
                    </div>
                </div>--%>
            </div>
        </section>
        </div>
        <section id="premio" class="bg-purpe">
            <div class="container py-5">
                <div class="categorias">
                <h3 class="titulo-light">Prêmio</h3>
                <asp:Literal ID="LitPremio" runat="server"></asp:Literal>
                    </div>
            </div>
        </section>

    <div class="bg-azul2R">

        <section id="rodape" class="text-center">

            <img class="my-3" src="<%=Link %>Imagens/logoTVAfiliada.png" style="height: 40px;" />
        </section>       
    </form>
   </div>
    <script src="<%=Link %>js/jquery-3.2.1.min.js"></script>
    <script src="<%=Link %>js/bootstrap.min.js"></script>
    <script src="<%=Link %>js/bootstrap.bundle.min.js"></script>
</body>
</html>
