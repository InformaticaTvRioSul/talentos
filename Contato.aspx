<%@ Page Title="Contato" Language="VB" MasterPageFile="~/SiteTalentos/MasterPage.master" AutoEventWireup="false" CodeFile="Contato.aspx.vb" Inherits="SiteTalentos_Contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .container-contato {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 75px;
            background-color: #292929;
            padding: 0 20px;
            max-width: 420px;
        }

            .container-contato i {
                padding: 4px 5px;
                margin-right: 12px;
                border: 1px solid #fff;
            }
            .container-contato > div {
                display: flex;
                align-items: center;
            }
            .container-contato > div span {
                    font-size: .8rem;
    margin-top: 2px;
    margin-right: 5px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h1 class="titulo-light mb-5">Contato</h1>

        <p>Para tirar dúvidas ou receber mais informações,<br />
            envie um e-mail para a gente:</p>
        <div class="container-contato">
            <i class="far fa-envelope"></i>talentosdapublicidade@tvriosul.com.br
        </div>
        <br />
        <p>Ou ligue (horário comercial | 9h às 18h):</p>
        <div class="container-contato">
            <div><i class="fas fa-phone-alt"></i><span>24</span> 3355-9800</div>
            <div style="width: 1px; background-color: #fff; height: 50px; margin: 0 16px;"></div>
            <div><i class="fas fa-phone-alt"></i><span>24</span> 2102-9800</div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderEnd" runat="Server">
</asp:Content>

