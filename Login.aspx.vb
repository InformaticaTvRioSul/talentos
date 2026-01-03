
Partial Class SiteTalentos_Login
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim oDt As Data.DataTable
            Dim Consulta As New ClsConsulta
            'Busca Talento, verifica se tem talento ativo e dentro da data de inscrição
            oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalDataExibi, TalDataInscricaoIni, TalDataInscricaoFim from TalentosNovo where TalAtivo=1 and now() >= TalDataExibi and TalDataEvento > now() limit 0,1")
            If oDt.Rows.Count = 1 Then
                Dim DataExibi As Date = oDt.Rows(0).Item("TalDataExibi")
                Dim DataInscricaoIni As Date = oDt.Rows(0).Item("TalDataInscricaoIni")
                Dim DataInscricaoFim As Date = oDt.Rows(0).Item("TalDataInscricaoFim")

                If Date.Now() >= DataInscricaoIni And Date.Now() <= DataInscricaoFim Then
                    PanLogin.Visible = True
                    PanMensagemInscricao.Visible = False
                ElseIf Date.Now() <= DataInscricaoIni Then
                    LitMensagemInscricao.Text = "<p>As inscrições serão abertas no dia</p>" &
                    "<h4>" & Format(DataInscricaoIni, "dd 'de' MMMM 'de' yyyy") & "</h4>" &
                    "<p>Fique ligado!</p>"

                Else
                    LitMensagemInscricao.Text = "<p>Inscrições encerradas</p>"
                End If
            Else
                LitMensagemInscricao.Text = "<h3 class='py-4'>Nenhuma edição aberta para inscrição, volte mais tarde!</h3>"
            End If
        End If
    End Sub

    Protected Sub CmdLogin_Click(sender As Object, e As EventArgs) Handles CmdLogin.Click
        Session.Clear()
        LitResposta.Text = ""
        Dim Autenticado As Boolean = False
        Autenticado = ClsLogin.AutenticarTalentos(TxtLogin.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("'", "").Replace("’", "").Replace("´", "").Trim, TxtSenha.Text.Trim)
        If Autenticado Then
            Try
                Dim oDt As Data.DataTable
                Dim Consulta As New ClsConsulta
                oDt = Consulta.ConsultaSQL("Select AgeCodigo, AgeResponsavel, AgeCNPJ, AgeNomeFantasia from TalentosAgencia where AgeAtivo=1 and AgeCNPJ='" & TxtLogin.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("'", "").Replace("’", "").Replace("´", "").Trim & "'")
                If oDt.Rows.Count = 1 Then
                    Session.Add("AgeCodigo", oDt.Rows(0).Item("AgeCodigo"))
                    Session.Add("AgeResponsavel", oDt.Rows(0).Item("AgeResponsavel"))
                    Session.Add("AgeCNPJ", oDt.Rows(0).Item("AgeCNPJ"))
                    Session.Add("AgeNomeFantasia", oDt.Rows(0).Item("AgeNomeFantasia"))
                Else
                    LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um problema ao realizar o login. Tente novamente.")
                End If
            Catch ex As Exception
                Session.Clear()
                LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um problema ao realizar o login. Tente novamente." & ex.Message, True)
            End Try

            If Not IsNothing(Request.QueryString("Pag")) Then
                If Not IsDBNull(Request.QueryString("Pag")) Then
                    If Request.QueryString("Pag") <> "" Then Response.Redirect(Request.QueryString("Pag"))
                End If
            End If

            If Not IsNothing(Request.QueryString("Edicao")) Then
                If Not IsDBNull(Request.QueryString("Edicao")) Then
                    If Request.QueryString("Edicao").ToString = "Estudante" Then
                        Response.Redirect("TalentosInscricoesEstudante")
                    End If
                End If
            End If
            Response.Redirect(Link & "TalentosInscricoes")
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Login ou senha incorretos. Tente novamente.", "danger")
        End If

    End Sub

    Public Sub CriaEmailCurriculo(ByRef ser As System.Web.HttpServerUtility, ByVal remetente As String, ByVal Assunto As String, ByVal Conteudo As String, ByVal destinatario As String)

        Dim cabecalho, rodape As String
        cabecalho = "  <div style='FONT-SIZE: small; TEXT-DECORATION: none; FONT-FAMILY: Calibri; FONT-WEIGHT: normal; COLOR: #000000; FONT-STYLE: normal; DISPLAY: inline;width:600px;'>" & _
                    "<table align='center' border='0' cellpadding='0' cellspacing='5'  style='COLOR: #000000' width='575'> <tr><td width='575'>" & _
                    "<table align='center' border='0' cellpadding='0' cellspacing='0' style='COLOR: #000000' width='100'><tr><td>" & _
                    "<img height='80' src='http://www.comercialonline.tv.br/Imagens/TopoMailingSoLogo.jpg' /></td></tr> </table> </td>" & _
                    "</tr> <tr><td style='FONT-SIZE: 12px; FONT-FAMILY: arial, helvetica, sans-serif; PADDING-BOTTOM: 15px; PADDING-TOP: 15px; PADDING-LEFT: 15px; PADDING-RIGHT: 15px'>"


        rodape = " </td> </tr> </table> </div>"

        Dim resp As String = ""
        resp = Utils2.EnviarEmail(remetente, destinatario, Assunto, cabecalho & Conteudo & rodape)
        If resp = "True" Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Senha enviada para o e-mail cadastrado.", "success")
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Não conseguimos enviar a senha, por favor tente novamente.")
        End If
    End Sub

    Protected Sub CmdReenviaSenha_Click(sender As Object, e As EventArgs) Handles CmdReenviaSenha.Click
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("Select AgeSenha, AgeEmail from TalentosAgencia where AgeAtivo=1 and AgeCNPJ='" & TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "").Replace("'", "").Replace("’", "").Replace("´", "").Trim & "'")
        If oDt.Rows.Count > 0 Then
            CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Talentos - Reenvio de Senha", "Como solicitado, segue abaixo sua senha para acesso as inscrições do Talentos da Publicidade<br><br><b>Senha: </b> " & oDt.Rows(0).Item("AgeSenha"), oDt.Rows(0).Item("AgeEmail"))
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Não existe cadastro para o CNPJ " & TxtCNPJ.Text)
        End If
    End Sub

   
End Class
