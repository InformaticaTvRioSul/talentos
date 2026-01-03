
Partial Class SiteTalentos_TalentosInscricoes
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
        If Not IsPostBack Then
            VerificaPermissao()
            BuscarInscricao()
            If Not IsNothing(Request.QueryString("Ret")) Then
                If Not IsDBNull(Request.QueryString("Ret")) Then
                    If Request.QueryString("Ret").ToString.Length > 0 Then
                        Select Case Request.QueryString("Ret")
                            Case 1
                                LitResposta.Text = Utils2.MensagemAvisoBootstrap("Cadastro realizado com sucesso.", "success", "top", 3)
                        End Select
                    End If
                End If
            End If

            Dim oDt As Data.DataTable
            Dim Consulta As New ClsConsulta
            'Busca Talento, verifica se tem talento ativo e dentro da data de inscrição
            oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalLogo from TalentosNovo where TalAtivo=1 and now() between TalDataInscricaoIni and TalDataInscricaoFim")
            If oDt.Rows.Count > 0 Then
                LitInscricao.Text = "<h3 class='text-center text-black'>Inscrições abertas para o " & oDt.Rows(0).Item("TalTitulo") & "</h3> <a href='TalentosInscricao' class='btn btn-dark float-right'>Nova Inscrição</a>"
            Else
                'nao achou nada, bloqueia os botões de cadastro
                LitInscricao.Text = "<h5 class='text-center text-black'>Nenhuma edição disponível para inscrição</h5>"
            End If

        End If
    End Sub

#Region "Busca"

    Private Sub BuscarInscricao()
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("select InsCodigo, CliCodigo, CliNomeFantasia, CliRazaoSocial, CliCNPJ, CliResponsavel, CliTelefone, ProCodigo, ProNomeFantasia, ProRazaoSocial, ProCNPJ, ProResponsavel, ProTelefone, InsTalCodigo, TalTitulo, InsDataCadastro, InsDataAlteracao, InsDataFinalizacao, InsLinkArquivo, InsCategoria, InsTitulo, InsPeriodoVeicula, InsDuracao, InsFinalizado from TalentosInscricao inner join TalentosCliente on InsCliCodigo = CliCodigo inner join TalentosProdutora on InsProCodigo = ProCodigo inner join TalentosNovo on InsTalCodigo = TalCodigo where InsAgeCodigo=" & HidAgeCodigo.Value & " order by InsDataCadastro desc")
        GrdInscricoes.DataSource = oDt
        GrdInscricoes.DataBind()
        If oDt.Rows.Count = 0 Then
            LblGrdInscricoes.Text = "<h3 class='text-center'>Nenhuma inscrição cadastrada</h3>"
        End If
    End Sub

    Protected Sub GrdInscricoes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrdInscricoes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(7).Text = 1 Then
                e.Row.Cells(7).Text = "<span class='text-success'>Concluída</span>"
            Else
                e.Row.Cells(7).Text = "<span class='text-danger'>Não enviada</span>"
            End If
        End If
    End Sub

#End Region
End Class
