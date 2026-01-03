
Partial Class SiteTalentos_TalentosInscricaoEstudante
    Inherits System.Web.UI.Page


    Private Sub VerificaPermissao()
        If Not IsNothing(Session("AgeResponsavel")) And Not IsDBNull(Session("AgeResponsavel")) Then
            If Session("AgeResponsavel") <> "" Then
                HidAgeCodigo.Value = Session("AgeCodigo")
            Else
                Response.Redirect("TalentosLogin?Edicao=Estudante")
            End If
        Else
            Response.Redirect("TalentosLogin?Edicao=Estudante")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            HidAgeCodigo.Value = 0
            HidTalCodigo.Value = 0
            HidFinalizado.Value = 0
            HidInsCodigo.Value = 0

            VerificaPermissao()

            If Not InscricaoAberta() Then
                'nao achou nada, bloqueia os botões de cadastro
                LitTitulo.Text = "<h4 class='text-center text-danger'>Inscrições encerradas</h4>"
                CmdVoltar.Visible = False
                CmdProsseguir.Visible = False
                CmdFinalizar.Visible = False
            End If

            If Not IsNothing(Request.QueryString("Codigo")) Then
                If Not IsDBNull(Request.QueryString("Codigo")) Then
                    If Request.QueryString("Codigo").ToString.Length > 0 Then
                        HidInsCodigo.Value = Request.QueryString("Codigo")
                        BuscarInscricao()
                    End If
                End If
            End If
            'Dim Arquivo As New System.IO.FileInfo((Server.MapPath("") & "TalentosInscricaoNovo\TalentosMapa\" & HidMapaMidia.Value))

            '  LitResposta.Text = MapPath("TalentosMapa\") & "teste.pdf"
        End If
    End Sub

    Private Function InscricaoAberta() As Boolean
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        'Busca Talento, verifica se tem talento ativo e dentro da data de inscrição
        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalLogoEstudante from TalentosNovo where TalAtivo=1 and TalEstudanteAtivo and now() between TalDataInscricaoIni and TalDataInscricaoFim")
        HidTalLogo.Value = "LogoTalentos.png"
        If oDt.Rows.Count > 0 Then
            HidTalCodigo.Value = oDt.Rows(0).Item("TalCodigo")
            'HidTalLogo.Value = oDt.Rows(0).Item("TalLogoEstudante").ToString
            Return True
        Else
            Return False
        End If
    End Function

#Region "Salvar"

    Protected Sub CmdProsseguir_Click(sender As Object, e As EventArgs) Handles CmdProsseguir.Click
        'Verifica se ja tem mais de 2 inscrições
        If Not PodeCadastrar() Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Já existem 3 inscrições para a instituição.")
            Exit Sub
        End If

        If Not ValidaCampos() Then
            Exit Sub
        End If

        If Salvar() Then
            LitResposta.Text = ""
            Response.Redirect("TalentosInscricaoEstudante?Codigo=" & HidInsCodigo.Value)
            'BuscarInscricao()
            'PanCliente.Visible = False
            'PanFinaliza.Visible = True
        End If
    End Sub

    Private Function ValidaCampos() As Boolean
        Dim Validado As Boolean = True
        Dim Mensagem As String = ""
        If Not TxtTitulo.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem = "Informe o Título"
        End If
        If Not TxtNomeEquipe.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Informe o Nome da Equipe"
        End If
        If Not TxtSloganEquipe.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Informe o Slogan da Equipe"
        End If
        If Not TxtIntregrantes.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Preencha as informações dos integrantes"
        End If
        If Not TxtCoordenador.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Informe o Coordenador"
        End If
        If Not TxtResponsavel.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Informe o Responsável"
        End If
        If Validado = True Then
            Return True
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap(Mensagem)
            Return False
        End If
    End Function

    Private Function Salvar() As Boolean
        Dim Erro As String = ""
        Dim comita As Boolean = True
        Try
            Dim NomeArquivo As String = ""
            Dim Ins As New ClsTalentosIsncricao
            Ins.InsTalCodigo = HidTalCodigo.Value
            Ins.InsAgeCodigo = HidAgeCodigo.Value

            Ins.InsTitulo = TxtTitulo.Text.ToUpper
            Ins.InsDuracao = CboDuracao.SelectedValue
            Ins.InsNomeEquipe = TxtNomeEquipe.Text.ToUpper
            Ins.InsSloganEquipe = TxtSloganEquipe.Text.ToUpper
            Ins.InsIntegrante = TxtIntregrantes.Text.ToUpper
            Ins.InsCoordenador = TxtCoordenador.Text.ToUpper
            Ins.InsResponsavel = TxtResponsavel.Text.ToUpper

            Dim oCn As MySql.Data.MySqlClient.MySqlConnection = ClsConexao.GetConnection
            Dim tranS As MySql.Data.MySqlClient.MySqlTransaction = oCn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)
            Dim oCommand As New MySql.Data.MySqlClient.MySqlCommand

            oCommand.Connection = oCn
            oCommand.Transaction = tranS

            If HidInsCodigo.Value <> "0" Then
                Ins.Updat = True
                Ins.InsCodigo = HidInsCodigo.Value
            End If

            If Not Ins.SalvarInscricaoEstudante(oCommand, Erro) Or Erro <> "" Then
                comita = False
            Else
                If Not Ins.Updat Then HidInsCodigo.Value = Ins.InsCodigo
            End If

            If comita Then
                tranS.Commit()
                oCommand.Dispose()
                tranS.Dispose()
                oCn.Close()
                oCn.Dispose()
            Else
                tranS.Rollback()
                oCommand.Dispose()
                tranS.Dispose()
                oCn.Close()
                oCn.Dispose()
                HidInsCodigo.Value = 0
            End If
        Catch ex As Exception
            Erro = Erro & " - " & ex.Message
            HidInsCodigo.Value = 0
            comita = False
        End Try
        If comita Then
            Return True
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao salvar os dados, por favor tente novamente. EX: " & Erro)
            Return False
        End If
    End Function

    Protected Sub CmdFinalizar_Click(sender As Object, e As EventArgs) Handles CmdFinalizar.Click
        If TxtLinkArquivo.Text.ToString.Length < 10 Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Insira o link do vídeo.")
            Exit Sub
        End If

        If Not InscricaoAberta() Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("As inscrições foram encerradas.")
            Exit Sub
        End If

        'Verifica se ja tem mais de 2 inscrições
        If Not PodeCadastrar() Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Já existem 2 inscrições para a instituição. <br> select count(*) as Total from TalentosInscricaoEstudante where InsTalCodigo = " & HidTalCodigo.Value & " and InsAgeCodigo=" & HidAgeCodigo.Value & IIf(HidInsCodigo.Value <> 0, " and InsCodigo<>" & HidInsCodigo.Value, ""))
            Exit Sub
        End If

        If Finalizar() Then
            'Evia Email
            ' CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Talentos - Inscrição Realizada", "Sua inscrição para o " & HidTalTitulo.Value & " foi realizada com sucesso", HidAgeEmail.Value)
            CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Talentos Estudante - Inscrição Realizada", "A Instituição " & HidAgeNome.Value & " realizou Inscrição Nº " & HidInsCodigo.Value & "  com sucesso. <a href='https://negociostvriosul.com.br/Admin/TalentosInscricaoEstudante.aspx?codigo=" & HidInsCodigo.Value & "'>Clique aqui para ver</a>.", "talentosdapublicidade@tvriosul.com.br")
            'Redireciona para pagina agradece
            Response.Redirect("TalentosConfirma?Edicao=Estudante")
        End If

    End Sub

    Private Function Finalizar() As Boolean
        Try
            Dim NomeArquivo As String = ""
            Dim Ins As New ClsTalentosIsncricao
            Ins.InsCodigo = HidInsCodigo.Value
            Ins.InsLinkArquivo = TxtLinkArquivo.Text.Trim
            Dim Erro As String = ""
            If Ins.FinalizarInscricaoEstudante(Erro) And Erro = "" Then
                Return True
            Else
                LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao finalizar a inscrição, por favor tente novamente. EX: F1")
                Return False
            End If
        Catch ex As Exception
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao finalizar a inscrição, por favor tente novamente. EX: F2 - " & ex.Message)
            Return False
        End Try
    End Function

    Private Function PodeCadastrar() As Boolean
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("select count(*) as Total from TalentosInscricaoEstudante where InsFinalizado=1 and InsTalCodigo = " & HidTalCodigo.Value & " and InsAgeCodigo=" & HidAgeCodigo.Value & IIf(HidInsCodigo.Value <> 0, " and InsCodigo<>" & HidInsCodigo.Value, ""))
        If oDt.Rows.Count > 0 Then
            If oDt.Rows(0).Item("Total") < 3 Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Public Sub CriaEmailCurriculo(ByRef ser As System.Web.HttpServerUtility, ByVal remetente As String, ByVal Assunto As String, ByVal Conteudo As String, ByVal destinatario As String)

        Dim cabecalho, rodape As String
        cabecalho = "  <div style='FONT-SIZE: small; TEXT-DECORATION: none; FONT-FAMILY: Calibri; FONT-WEIGHT: normal; COLOR: #000000; FONT-STYLE: normal; DISPLAY: inline;width:600px;'>" & _
                   "<table align='center' border='0' cellpadding='0' cellspacing='5'  style='COLOR: #000000' width='575'> <tr><td width='575'>" & _
                   "<table align='center' border='0' cellpadding='0' cellspacing='0' style='COLOR: #000000'>" & _
                   "<tr><td><img height='80' src='https://www.negociostvriosul.com.br/Imagens/TopoMailingSoLogo.jpg' /> </td> <td><img height='65' src='http://www.negociostvriosul.com.br/TalentosInscricaoNovo/imagens/" & HidTalLogo.Value & "' /> </td></tr> </table> </td>" & _
                   "</tr> <tr><td style='FONT-SIZE: 12px; FONT-FAMILY: arial, helvetica, sans-serif; PADDING-BOTTOM: 15px; PADDING-TOP: 15px; PADDING-LEFT: 15px; PADDING-RIGHT: 15px'>"

        rodape = " </td> </tr> </table> </div>"

        Dim resp As String = ""
        Utils2.EnviarEmail(remetente, destinatario, Assunto, cabecalho & Conteudo & rodape)
        ' resp = EnviarEmail(remetente, destinatario, "TV Rio Sul - " & Assunto, cabecalho & Conteudo & rodape)
        Response.Write(resp)
    End Sub

#End Region

#Region "Buscar"

    Private Function BuscarInscricao() As Boolean
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("select AgeCodigo, AgeNomeFantasia, AgeEmail, InsTalCodigo, TalTitulo, InsDataCadastro, InsDataAlteracao, InsLinkArquivo, InsTitulo, InsDuracao, InsNomeEquipe, InsSloganEquipe, InsIntegrantes, InsCoordenador, InsResponsavel, InsFinalizado from TalentosInscricaoEstudante inner join TalentosAgencia on InsAgeCodigo = AgeCodigo inner join TalentosNovo on InsTalCodigo = TalCodigo where InsCodigo=" & HidInsCodigo.Value)
        If oDt.Rows.Count > 0 Then
            HidFinalizado.Value = oDt.Rows(0).Item("InsFinalizado")
            If HidTalCodigo.Value = 0 And HidFinalizado.Value = 1 Then LitTitulo.Text = "<br>"
            HidTalCodigo.Value = oDt.Rows(0).Item("InsTalCodigo")
            HidAgeNome.Value = oDt.Rows(0).Item("AgeNomeFantasia")
            HidAgeEmail.Value = oDt.Rows(0).Item("AgeEmail")
            HidTalTitulo.Value = oDt.Rows(0).Item("InsTitulo")

            TxtTitulo.Text = oDt.Rows(0).Item("InsTitulo")
            TxtNomeEquipe.Text = oDt.Rows(0).Item("InsNomeEquipe")
            TxtSloganEquipe.Text = oDt.Rows(0).Item("InsSloganEquipe")
            TxtIntregrantes.Text = oDt.Rows(0).Item("InsIntegrantes")
            TxtCoordenador.Text = oDt.Rows(0).Item("InsCoordenador")
            TxtResponsavel.Text = oDt.Rows(0).Item("InsResponsavel")
            CboDuracao.SelectedValue = oDt.Rows(0).Item("InsDuracao")
            TxtLinkArquivo.Text = oDt.Rows(0).Item("InsLinkArquivo").ToString

            LitDados.Text = "<h4>Título: " & oDt.Rows(0).Item("InsTitulo") & "</h4><b>Duração: </b> " & oDt.Rows(0).Item("InsDuracao") & "<br><b>Nome da Equipe: </b> " & oDt.Rows(0).Item("InsNomeEquipe") & "<br><b>Slogan da Equipe: </b> " & oDt.Rows(0).Item("InsSloganEquipe") & "<br><br><b>Integrantes da Equipe: </b> " & oDt.Rows(0).Item("InsIntegrantes") & "<br><b>Coordenador da Equipe: </b> " & oDt.Rows(0).Item("InsCoordenador") & "<br><b>Responsável pela Inscrição: </b> " & oDt.Rows(0).Item("InsResponsavel") & "<br><b>Data de Cadastro: </b> " & oDt.Rows(0).Item("InsDataCadastro") & "<br><b>Data Modificação: </b> " & oDt.Rows(0).Item("InsDataAlteracao") & "<br>" & _
                            "<div class='card mx-auto bg-light' style='width: 18rem;'> <div class='card-body text-center'> <h5 class='card-title'>CÓDIGO DA INSCRIÇÃO</h5> <h2 class='card-subtitle mb-2'>" & HidInsCodigo.Value & "</h2> <p class='card-text'> </p> </div> </div><br>"

            PanCliente.Visible = False
            If HidFinalizado.Value = 0 Then
                PanFinaliza.Visible = True
            Else
                LitDados.Text += "<br><br> <label><b>Arquivo enviado</b></label><br> <a href='" & oDt.Rows(0).Item("InsLinkArquivo") & "' class='text-info' target='_blank'>Visualizar no WeTransfer</a><br>"
                PanFinaliza.Visible = False
            End If
            Return True
        Else
            Return False
        End If
    End Function

#End Region

    Protected Sub CmdVoltar_Click(sender As Object, e As EventArgs) Handles CmdVoltar.Click
        If HidFinalizado.Value = 0 Then
            LitDados.Text = ""
            PanFinaliza.Visible = False
            PanCliente.Visible = True
        End If
    End Sub
End Class
