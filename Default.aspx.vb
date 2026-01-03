
Partial Class SiteTalentos_Default
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            BuscaUltimoCadastrado()
        End If
    End Sub

    Private Sub BuscaUltimoCadastrado()
        Dim oDt, oDtVencedores As Data.DataTable
        Dim Consulta As New ClsConsulta

        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalTexto, TalPremio, TalDataEvento, TalEstudanteAtivo, TalAgenciaAtivo From TalentosNovo where TalAtivo=1 order by TalDataExibi Desc limit 0,1")

        If oDt.Rows.Count > 0 Then
            Page.Title = oDt.Rows(0).Item("TalTitulo")
            LitSobre.Text = oDt.Rows(0).Item("TalTexto")
            LitPremio.Text = oDt.Rows(0).Item("TalPremio")

            LitLogo.Text = "<img src='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "logo") & "' alt='Talentos da Publicidade' />"

            'If oDt.Rows(0).Item("TalEstudanteAtivo").ToString = "1" Then
            '    LitInscricao.Text = "<div class='col col-md-6 borda-right-branco inscricao'><h2>Talentos da Publicidade<b>Estudantes</b></h2>" &
            '                   "<div class='d-flex flex-column  align-items-center'>" &
            '                   "<a  href='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "estudanteRegulamento") & "' target='_blank'>LEIA O REGULAMENTO</a>" &
            '                   "<a href='" & Link & "talentosinscricoesestudante'>INSCRIÇÕES ESTUDANTES</a>" &
            '                   "</div></div>" &
            '                   "<div class='col col-md-6 inscricao'><h2>Talentos da Publicidade<b>Agências</b></h2>" &
            '                   "<div class='d-flex flex-column align-items-center'>" &
            '                   "<a href='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "regulamento") & "' target='_blank'>LEIA O REGULAMENTO</a>" &
            '                   "<a href='" & Link & "talentosinscricoes'>INSCRIÇÕES AGÊNCIAS</a>" &
            '                   "</div></div>"
            'Else
            '    LitInscricao.Text = "<div class='col col-md-12 inscricao'><h2>Talentos da Publicidade<b>Agências</b></h2>" &
            '                   "<div class='d-flex  flex-column align-items-center'>" &
            '                   "<a href='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "regulamento") & "' target='_blank'>LEIA O REGULAMENTO</a>" &
            '                   "<a href='" & Link & "talentosinscricoes'>INSCRIÇÕES AGÊNCIAS</a>" &
            '                   "</div></div>"
            'End If

            Dim estudanteContent As String = "<div class='col col-md-6 borda-right-branco inscricao'><h2>Talentos da Publicidade<b>Estudantes</b></h2>" &
                                             "<div class='d-flex flex-column  align-items-center'>" &
                                             "<a  href='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "estudanteRegulamento") & "' target='_blank'>LEIA O REGULAMENTO</a>" &
                                             "<a href='" & Link & "talentosinscricoesestudante'>INSCRIÇÕES ESTUDANTES</a>" &
                                             "</div></div>"

            Dim agenciaContent As String = "<div class='col col-md-6 inscricao'><h2>Talentos da Publicidade<b>Agências</b></h2>" &
                                           "<div class='d-flex flex-column align-items-center'>" &
                                           "<a href='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "regulamento") & "' target='_blank'>LEIA O REGULAMENTO</a>" &
                                           "<a href='" & Link & "talentosinscricoes'>INSCRIÇÕES AGÊNCIAS</a>" &
                                           "</div></div>"

            If oDt.Rows(0).Item("TalEstudanteAtivo").ToString = "1" Then
                LitInscricao.Text = estudanteContent

                If oDt.Rows(0).Item("TalAgenciaAtivo").ToString = "1" Then
                    LitInscricao.Text &= agenciaContent
                End If
            Else
                LitInscricao.Text = agenciaContent
            End If



        Else
            LitSobre.Text = "<div class='my-5 py-5'><h1 class='text-center'>Informação não disponível</h1></div>"
            LitPremio.Text = "<div class='my-5 py-5'><h1 class='text-center'>Informação não disponível</h1></div>"
        End If

        oDtVencedores = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalDataEvento From TalentosNovo inner join TalentosVencedores on TalCodigo = VenTalCodigo where TalAtivo=1 order by TalDataExibi Desc limit 0,1")
        If oDtVencedores.Rows.Count > 0 Then
            LitVencedores.Text = "<a href='" & Link & "TalentosVencedores/" & oDtVencedores.Rows(0).Item("TalCodigo") & "/campanha'>Vencedores " & Format(oDtVencedores.Rows(0).Item("TalDataEvento"), "yyyy") & "</a>"
        End If
    End Sub
End Class
