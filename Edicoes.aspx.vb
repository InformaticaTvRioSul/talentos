
Partial Class SiteTalentos_Edicoes
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HidTipo.Value = 1
            If Not IsNothing(Request.QueryString("tipo")) Then
                If Not IsDBNull(Request.QueryString("tipo")) Then
                    If Request.QueryString("tipo") <> "" And Request.QueryString("tipo") = "estudante" Then HidTipo.Value = 0
                End If
            End If
        End If
    End Sub
End Class
