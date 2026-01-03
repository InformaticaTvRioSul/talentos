
Partial Class SiteTalentos_MasterPage
    Inherits System.Web.UI.MasterPage
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            BuscaUltimoCadastrado()
            LitLogin.Text = "<a class='dropdown-item' href='" & Link & "TalentosLogin'>Login</a>"
            If Not IsNothing(Session("AgeResponsavel")) And Not IsDBNull(Session("AgeResponsavel")) Then
                If Session("AgeResponsavel") <> "" Then
                    LitLogin.Text = ""
                    CmdSair.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub BuscaUltimoCadastrado()
        Dim oDt, oDtVencedores As Data.DataTable
        Dim Consulta As New ClsConsulta

        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalDataEvento From TalentosNovo where TalAtivo=1 order by TalDataExibi Desc limit 0,1")
        If oDt.Rows.Count > 0 Then
            Page.Title = oDt.Rows(0).Item("TalTitulo")
            HidUrlLogo.Value = Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "logo")
        End If

        oDtVencedores = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalDataEvento From TalentosNovo inner join TalentosVencedores on TalCodigo = VenTalCodigo where TalAtivo=1 order by TalDataExibi Desc limit 0,1")
        If oDtVencedores.Rows.Count > 0 Then
            LitVencedores.Text = "<a class='dropdown-item' href='" & Link & "TalentosVencedores/" & oDtVencedores.Rows(0).Item("TalCodigo") & "/campanha'>Vencedores " & Format(oDtVencedores.Rows(0).Item("TalDataEvento"), "yyyy") & "</a>"
        End If
    End Sub

    Protected Sub CmdSair_Click(sender As Object, e As EventArgs) Handles CmdSair.Click
        Session.RemoveAll()
        Response.Redirect(Link & "Talentos")
    End Sub
End Class

