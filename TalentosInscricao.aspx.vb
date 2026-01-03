Imports System.IO
Partial Class SiteTalentos_TalentosInscricao
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Private Sub VerificaPermissao()
        If Not IsNothing(Session("AgeResponsavel")) And Not IsDBNull(Session("AgeResponsavel")) Then
            If Session("AgeResponsavel") <> "" Then
                HidAgeCodigo.Value = Session("AgeCodigo")
            Else
                Response.Redirect("TalentosLogin")
            End If
        Else
            Response.Redirect("TalentosLogin")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            HidAgeCodigo.Value = 0
            HidCliCodigo.Value = 0
            HidTalCodigo.Value = 0
            HidProCodigo.Value = 0
            HidFinalizado.Value = 0
            HidInsCodigo.Value = 0
            HidMapaMidia.Value = 0

            VerificaPermissao()

            If Not InscricaoAberta() Then
                'nao achou nada, bloqueia os botões de cadastro
                LitTitulo.Text = "<h4 class='text-center text-danger'>Inscrições encerradas</h4>"
                CmdVoltar.Visible = False
                CmdProsseguir.Visible = False
                CmdFinalizar.Visible = False
            End If

            Dim oDt As Data.DataTable
            Dim Consulta As New ClsConsulta
            'Cliente
            oDt = Consulta.ConsultaSQL("Select CONCAT(CliCodigo, '*', CliNomeFantasia, '*', CliCNPJ, '*', CliTelefone) as Codigo, CliNomeFantasia as Nome from TalentosCliente where CliAtivo=1 and CliAgeCodigo = " & HidAgeCodigo.Value & " order by CliNomeFantasia")
            Utils2.CarregaCombo(oDt, CboCliente)
            'Produtora
            oDt = Consulta.ConsultaSQL("Select CONCAT(ProCodigo, '*', ProNomeFantasia, '*', ProCNPJ, '*', ProTelefone) as Codigo, ProNomeFantasia as Nome from TalentosProdutora where ProAtivo=1 order by ProNomeFantasia")
            Utils2.CarregaCombo(oDt, CboProdutora)

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
        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalLogo from TalentosNovo where TalAtivo=1 and now() between TalDataInscricaoIni and TalDataInscricaoFim")
        HidTalLogo.Value = "LogoTalentos.png"
        If oDt.Rows.Count > 0 Then
            HidTalCodigo.Value = oDt.Rows(0).Item("TalCodigo")
            Return True
        Else
            Return False
        End If
    End Function

#Region "Salvar"

    Protected Sub CmdProsseguir_Click(sender As Object, e As EventArgs) Handles CmdProsseguir.Click
        'Verifica se ja tem mais de 2 inscricoes por cliente e por categoria, salva e exibe cadsatro wetransfer
        If CboCliente.SelectedValue = "0" Or CboProdutora.SelectedValue = "0" Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Selecione o CLiente e a Produtora.")
            Exit Sub
        End If

        If Not ValidaCampos() Then
            Exit Sub
        End If

        If HidMapaMidia.Value = 0 Then
            'verifica se o arquivo é pdf e se esta tudo certo
            Dim ext As String = ""
            If FilMapaMidia.HasFile Then
                If FilMapaMidia.PostedFile.ContentType.ToString = "application/pdf" Then
                    ext = ".pdf"
                    If FilMapaMidia.FileBytes.Length > 24000000 Then
                        LitResposta.Text = Utils2.MensagemAvisoBootstrap("O arquivo deve ser menor que 3MB. Tamanho atual: " & FilMapaMidia.FileBytes.Length)
                        Exit Sub
                    End If
                Else
                    LitResposta.Text = Utils2.MensagemAvisoBootstrap("Somente arquivos PDF são aceitos.")
                    Exit Sub
                End If
            Else
                LitResposta.Text = Utils2.MensagemAvisoBootstrap("Anexe o Mapa de Mídia. Somente arquivos PDF são aceitos.")
                Exit Sub
            End If
        End If

        'Verifica se ja tem mais de 2 inscrições
        If Not PodeCadastrar() Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Já existem 2 inscrições para o cliente na categoria selecionada.")
            Exit Sub
        End If

        If Salvar() Then
            BuscarInscricao()
            PanCliente.Visible = False
            PanFinaliza.Visible = True
        End If
    End Sub

    Private Function ValidaCampos() As Boolean
        Dim Validado As Boolean = True
        Dim Mensagem As String = ""
        If Not TxtTitulo.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem = "Informe o Título"
        End If
        If CboCategoria.SelectedValue = "0" Then
            Validado = False
            Mensagem += "<br>Informe a Categoria"
        End If
        If CboDuracao.SelectedValue = "0" Then
            Validado = False
            Mensagem += "<br>Informe a Duração"
        End If
        If Not TxtPeriodo.Text.Trim.Length >= 3 Then
            Validado = False
            Mensagem += "<br>Informe o Período de Veiculação"
        End If
        If CboCliente.SelectedValue = "0" Then
            Validado = False
            Mensagem += "<br>Informe o Cliente"
        End If
        If CboProdutora.SelectedValue = "0" Then
            Validado = False
            Mensagem += "<br>Informe a Produtora"
        End If
        If Validado Then
            Return True
        Else
            LitResposta.Text = Utils2.MensagemAvisoBootstrap(Mensagem)
            Return False
        End If
    End Function

    Private Function Salvar() As Boolean
        Try
            Dim NomeArquivo As String = ""
            Dim Ins As New ClsTalentosIsncricao
            Ins.InsTalCodigo = HidTalCodigo.Value
            Ins.InsCliCodigo = HidCliCodigo.Value
            Ins.InsProCodigo = HidProCodigo.Value
            Ins.InsAgeCodigo = HidAgeCodigo.Value

            Ins.InsCategoria = CboCategoria.SelectedValue
            Ins.InsTitulo = TxtTitulo.Text.ToUpper
            Ins.InsPeriodoVeicula = TxtPeriodo.Text.ToUpper
            Ins.InsDuracao = CboDuracao.SelectedValue
            Dim Erro As String = ""

            Dim oCn As MySql.Data.MySqlClient.MySqlConnection = ClsConexao.GetConnection
            Dim tranS As MySql.Data.MySqlClient.MySqlTransaction = oCn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted)
            Dim oCommand As New MySql.Data.MySqlClient.MySqlCommand

            oCommand.Connection = oCn
            oCommand.Transaction = tranS
            Dim comita As Boolean = True
            If HidInsCodigo.Value <> "0" Then
                Ins.Updat = True
                Ins.InsCodigo = HidInsCodigo.Value
            End If

            If Ins.SalvarInscricao(oCommand, Erro) Then
                If HidMapaMidia.Value = 0 Then
                    If Not Ins.Updat Then HidInsCodigo.Value = Ins.InsCodigo
                    Try
                        NomeArquivo = "MapaMidia_" & Ins.InsCodigo & "_" & Format(Date.Now, "yyyyMMddHHmmss") & ".pdf"
                        FilMapaMidia.SaveAs(Server.MapPath("") & "\imagens\Talentos\Mapas\" & NomeArquivo)
                        Ins.InsMapaMidia = NomeArquivo
                        HidMapaMidia.Value = NomeArquivo
                        If Not Ins.UpdateMapaMidia(oCommand, Erro) Or Erro <> "" Then
                            Erro = Erro & "<br>Erro Atualiza dados Upload"
                            comita = False
                        End If
                    Catch ex As Exception
                        Erro = Erro & "<br>Erro Upload: " & ex.Message
                        comita = False
                    End Try
                End If
            Else
                comita = False
            End If

            If comita Then
                tranS.Commit()
                oCommand.Dispose()
                tranS.Dispose()
                oCn.Close()
                oCn.Dispose()
                '  Response.Redirect("Agradecimento", False)
                LitResposta.Text = ""
                Return True
            Else
                tranS.Rollback()
                oCommand.Dispose()
                tranS.Dispose()
                oCn.Close()
                oCn.Dispose()
                '  Utils2.CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Erro no Talentos", Erro, "denisr@tvriosul.com.br")
                LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao salvar os dados, por favor tente novamente. EX: 1 - " & Erro)
                HidInsCodigo.Value = 0
                Return False
            End If
        Catch ex As Exception
            ' Utils2.CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Erro no Talentos", ex.Message, "denisr@tvriosul.com.br")
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao salvar os dados, por favor tente novamente. EX: " & ex.Message)
            HidInsCodigo.Value = 0
            Return False
        End Try
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
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Já existem 2 inscrições para o cliente na categoria selecionada.")
            Exit Sub
        End If

        If Finalizar() Then
            'Evia Email
            ' CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Talentos - Inscrição Realizada", "Sua inscrição para o " & HidTalTitulo.Value & " foi realizada com sucesso", HidAgeEmail.Value)
            CriaEmailCurriculo(Server, "sistema@negociostvriosul.com.br", "Talentos - Inscrição Realizada", "A Agência " & HidAgeNome.Value & " realizou Inscrição Nº " & HidInsCodigo.Value & "  com sucesso. <a href='http://negociostvriosul.com.br/Admin/TalentosInscricao.aspx?codigo=" & HidInsCodigo.Value & "'>Clique aqui para ver</a>.", "talentosdapublicidade@tvriosul.com.br")
            'Redireciona para pagina agradece
            Response.Redirect("TalentosConfirma")
        End If

    End Sub

    Private Function Finalizar() As Boolean
        Try
            Dim NomeArquivo As String = ""
            Dim Ins As New ClsTalentosIsncricao
            Ins.InsCodigo = HidInsCodigo.Value
            Ins.InsLinkArquivo = TxtLinkArquivo.Text.Trim
            Dim Erro As String = ""
            If Ins.FinalizarInscricao(Erro) And Erro = "" Then
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
        oDt = Consulta.ConsultaSQL("select count(*) as Total from TalentosInscricao where InsTalCodigo = " & HidTalCodigo.Value & " and InsCategoria = '" & CboCategoria.SelectedValue & "' and InsCliCodigo=" & HidCliCodigo.Value & " and InsAgeCodigo=" & HidAgeCodigo.Value & IIf(HidInsCodigo.Value <> 0, " and InsCodigo<>" & HidInsCodigo.Value, ""))
        If oDt.Rows.Count > 0 Then
            If oDt.Rows(0).Item("Total") < 2 Then
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
                   "<tr><td><img height='80' src='https://negociostvriosul.com.br/Imagens/TopoMailingSoLogo.jpg' /> </td> <td><img height='65' src='https://negociostvriosul.com.br/TalentosInscricaoNovo/imagens/" & HidTalLogo.Value & "' /> </td></tr> </table> </td>" & _
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
        oDt = Consulta.ConsultaSQL("select AgeCodigo, AgeNomeFantasia, AgeEmail, CliCodigo, CliNomeFantasia, CliRazaoSocial, CliCNPJ, CliResponsavel, CliTelefone, ProCodigo, ProNomeFantasia, ProRazaoSocial, ProCNPJ, ProResponsavel, ProTelefone, InsTalCodigo, TalTitulo, InsDataCadastro, InsDataAlteracao, InsLinkArquivo, InsCategoria, InsTitulo, InsPeriodoVeicula, InsDuracao, InsLinkArquivo, InsMapaMidia, InsFinalizado from TalentosInscricao inner join TalentosAgencia on InsAgeCodigo = AgeCodigo inner join TalentosCliente on InsCliCodigo = CliCodigo inner join TalentosProdutora on InsProCodigo = ProCodigo inner join TalentosNovo on InsTalCodigo = TalCodigo where InsCodigo=" & HidInsCodigo.Value)
        If oDt.Rows.Count > 0 Then
            HidFinalizado.Value = oDt.Rows(0).Item("InsFinalizado")
            If HidTalCodigo.Value = 0 And HidFinalizado.Value = 1 Then LitTitulo.Text = "<br>"
            HidCliCodigo.Value = oDt.Rows(0).Item("CliCodigo")
            HidProCodigo.Value = oDt.Rows(0).Item("ProCodigo")
            HidTalCodigo.Value = oDt.Rows(0).Item("InsTalCodigo")
            HidAgeNome.Value = oDt.Rows(0).Item("AgeNomeFantasia")
            HidAgeEmail.Value = oDt.Rows(0).Item("AgeEmail")
            HidTalTitulo.Value = oDt.Rows(0).Item("InsTitulo")

            TxtTitulo.Text = oDt.Rows(0).Item("InsTitulo")
            CboCategoria.SelectedValue = oDt.Rows(0).Item("InsCategoria")
            CboDuracao.SelectedValue = oDt.Rows(0).Item("InsDuracao")
            CboCliente.SelectedValue = oDt.Rows(0).Item("CliCodigo") & "*" & oDt.Rows(0).Item("CliNomeFantasia") & "*" & oDt.Rows(0).Item("CliCNPJ") & "*" & oDt.Rows(0).Item("CliTelefone")
            'CONCAT(CliCodigo, '*', CliNomeFantasia, '*', CliCNPJ, '*', CliTelefone)
            LblClienteSelecionado.Text = "<b>DETALHES DO CLIENTE SELCIONADO</b><br /> <b>NOME FANTASIA:</b> " & oDt.Rows(0).Item("CliNomeFantasia") & "<br /><b>CNPJ:</b> " & Utils2.FormataCNPJ(oDt.Rows(0).Item("CliCNPJ")) & "<br /><b>TELEFONE:</b> " & Utils2.FormataTelefone(oDt.Rows(0).Item("CliTelefone")) & "<br />"
            CboProdutora.SelectedValue = oDt.Rows(0).Item("ProCodigo") & "*" & oDt.Rows(0).Item("ProNomeFantasia") & "*" & oDt.Rows(0).Item("ProCNPJ") & "*" & oDt.Rows(0).Item("ProTelefone")
            LblProdutoraSelecionado.Text = "<b>DETALHES DA PRODUTORA SELCIONADA</b><br /> <b>NOME FANTASIA:</b> " & oDt.Rows(0).Item("ProNomeFantasia") & "<br /><b>CNPJ:</b> " & Utils2.FormataCNPJ(oDt.Rows(0).Item("ProCNPJ")) & "<br /><b>TELEFONE:</b> " & Utils2.FormataTelefone(oDt.Rows(0).Item("ProTelefone")) & "<br />"
            TxtPeriodo.Text = oDt.Rows(0).Item("InsPeriodoVeicula")
            TxtLinkArquivo.Text = oDt.Rows(0).Item("InsLinkArquivo")
            LitMapa.Text = ""
            If oDt.Rows(0).Item("InsMapaMidia").ToString.Length > 5 Then
                HidMapaMidia.Value = oDt.Rows(0).Item("InsMapaMidia")
                LitMapa.Text = "<label><b>Mapa de Mídia</b></label><br> <a href='" & Link & "imagens/Talentos/Mapas/" & HidMapaMidia.Value & "' class='btn btn-outline-info border-0 mb-1' target='_blank'>Visualizar Mapa salvo</a>"
                PanVisualizaMapa.Visible = True
                PanUploadMapa.Visible = False
            End If

            LitDados.Text = "<h4>Título: " & oDt.Rows(0).Item("InsTitulo") & "</h4><b>Duração: </b> " & oDt.Rows(0).Item("InsDuracao") & "<br><b>Período Veiculação: </b> " & oDt.Rows(0).Item("InsPeriodoVeicula") & "<br><b>Data de Cadastro: </b> " & oDt.Rows(0).Item("InsDataCadastro") & "<br><b>Data Modificação: </b> " & oDt.Rows(0).Item("InsDataAlteracao") & "<br>" & _
                            "<div class='row my-3'><div class='col-sm-12 col-md-6'><h4>Cliente</h4> <b>Nome Fantasia: </b> " & oDt.Rows(0).Item("CliNomeFantasia") & "<br><b>Razão Social: </b> " & oDt.Rows(0).Item("CliRazaoSocial") & "<br><b>Responsável: </b> " & oDt.Rows(0).Item("CliResponsavel") & "<br><b>CNPJ: </b> " & Utils2.FormataCNPJ(oDt.Rows(0).Item("CliCNPJ")) & "<br><b>Telefone: </b> " & Utils2.FormataTelefone(oDt.Rows(0).Item("CliTelefone")) & "</div>" & _
                            "<div class='col-sm-12 col-md-6'><h4>Produtora</h4> <b>Nome Fantasia: </b> " & oDt.Rows(0).Item("ProNomeFantasia") & "<br><b>Razão Social: </b> " & oDt.Rows(0).Item("ProRazaoSocial") & "<br><b>Responsável: </b> " & oDt.Rows(0).Item("ProResponsavel") & "<br><b>CNPJ: </b> " & Utils2.FormataCNPJ(oDt.Rows(0).Item("ProCNPJ")) & "<br><b>Telefone: </b> " & Utils2.FormataTelefone(oDt.Rows(0).Item("ProTelefone")) & "</div></div>" & _
                            "<div class='card mx-auto bg-light' style='width: 18rem;'> <div class='card-body text-center'> <h5 class='card-title'>CÓDIGO DA INSCRIÇÃO</h5> <h2 class='card-subtitle mb-2'>" & HidInsCodigo.Value & "</h2> <p class='card-text'> </p> </div> </div><br>" & LitMapa.Text


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

    Protected Sub CmdRecarregaCliente_Click(sender As Object, e As EventArgs) Handles CmdRecarregaCliente.Click
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("Select CONCAT(CliCodigo, '*', CliNomeFantasia, '*', CliCNPJ, '*', CliTelefone) as Codigo, CliNomeFantasia as Nome from TalentosCliente where CliAtivo=1 and CliAgeCodigo = " & HidAgeCodigo.Value & " order by CliNomeFantasia")
        Utils2.CarregaCombo(oDt, CboCliente)
    End Sub

    Protected Sub CmdRecarregaProdutora_Click(sender As Object, e As EventArgs) Handles CmdRecarregaProdutora.Click
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("Select CONCAT(ProCodigo, '*', ProNomeFantasia, '*', ProCNPJ, '*', ProTelefone) as Codigo, ProNomeFantasia as Nome from TalentosProdutora where ProAtivo=1 order by ProNomeFantasia")
        Utils2.CarregaCombo(oDt, CboProdutora)
    End Sub

#End Region



    Protected Sub CmdVoltar_Click(sender As Object, e As EventArgs) Handles CmdVoltar.Click
        If HidFinalizado.Value = 0 Then
            LitDados.Text = ""
            PanFinaliza.Visible = False
            PanCliente.Visible = True
        End If
    End Sub

    Protected Sub CmdExcluirMapa_Click(sender As Object, e As EventArgs) Handles CmdExcluirMapa.Click
        Try
            Dim Arquivo As New System.IO.FileInfo((Server.MapPath("") & "..\imagens\Talentos\Mapas\" & HidMapaMidia.Value))
            If Arquivo.Exists Then
                Arquivo.Delete()
                Dim consulta As New ClsConsulta
                consulta.ExecutaQuery("update TalentosInscricao set InsMapaMidia='' where InsCodigo=" & HidInsCodigo.Value)
            End If
            HidMapaMidia.Value = 0
            PanUploadMapa.Visible = True
            PanVisualizaMapa.Visible = False
        Catch ex As Exception
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Não foi possível excluir o arquivo, tente novamente ou contacte nosso setor de TI.")
        End Try

    End Sub
End Class
