
Partial Class SiteTalentos_TalentosConfirma
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Estudante As Boolean = False
            If Not IsNothing(Request.QueryString("Edicao")) Then
                If Not IsDBNull(Request.QueryString("Edicao")) Then
                    If Request.QueryString("Edicao").ToString = "Estudante" Then
                        Estudante = True
                    End If
                End If
            End If

            Dim oDt As Data.DataTable
            Dim Consulta As New ClsConsulta
            'Busca Talento, verifica se tem talento ativo e dentro da data de inscrição
            oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalLogo, TalLogoEstudante from TalentosNovo where TalAtivo=1 and now() between TalDataInscricaoIni and TalDataInscricaoFim")
             If oDt.Rows.Count > 0 Then
                If Not Estudante Then
                       LitImagem.Text = "<img style='width: 100%; max-width: 400px;' class='pt-4' src='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "logo") & "' />"
                    LitLink.Text = "<p class='text-center'>Para ver os detalhes da sua inscrição ou enviar uma nova inscrição, <a href='TalentosInscricoes' class='text-info'>clique aqui</a></p>"
                Else
                       LitImagem.Text = "<img style='width: 100%; max-width: 400px;' class='pt-4' src='" & Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "logo") & "' />"
                    LitLink.Text = "<p class='text-center'>Para ver os detalhes da sua inscrição ou enviar uma nova inscrição, <a href='TalentosInscricoesEstudante' class='text-info'>clique aqui</a></p>"
                End If
            End If
        End If
    End Sub
End Class
