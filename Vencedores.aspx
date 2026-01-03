<%@ Page Title="Vencedores" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="Vencedores.aspx.vb" Inherits="SiteTalentos_Vencedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">  
    <style>     
            .btn-dark  {
                width: 100%;
                display: inline-block;
                padding: 10px;
                 text-align: center;
                color: #fff;
                background-color: #292929;
                border: 1px solid #fff;
                margin-bottom: 10px;
            }

                .btn-dark:hover {
                    background-color: #444444;
                }

                .vencedores-container{
                    align-items: center;
                        display: flex;
                }

                .vencedores-texto{
                    font-size: .9rem;
                    background-color: #444444;
                    color: #fff;
                    padding: 20px;
                    width:100%;
                }
                .vencedores-texto b{
                    margin-top:5px;
                    display:block;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h1 class="titulo-light mb-5"><asp:Literal ID="LitTitulo" runat="server"></asp:Literal></h1>
                
        <div class="row">
            <asp:Literal ID="LitMenuCategoria" runat="server"></asp:Literal>      
        </div>

        <asp:Literal ID="LitVencedores" runat="server"></asp:Literal>            
    </div>
    <asp:HiddenField ID="HidTipo" Value="1" runat="server" />
     <asp:HiddenField ID="HidPagina" Value="0" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
    
</asp:Content>

