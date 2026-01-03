
Partial Class SiteTalentos_Vencedores
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim TalCodigo As String = ""
            If Not IsNothing(RouteData.Values("edicao")) Then
                If Not IsDBNull(RouteData.Values("edicao")) Then
                    If RouteData.Values("edicao") <> "" Then
                        TalCodigo = RouteData.Values("edicao")
                    End If
                End If
            End If

            If TalCodigo <> "" Then
                Dim categoria As String = RouteData.Values("categoria").ToString
                BuscaDados(TalCodigo, categoria)
            Else
                Response.Redirect(Link & "talentos")
            End If
        End If
    End Sub

    Private Sub BuscaDados(ByVal TalCodigo As Integer, ByVal Categoria As String)
        Dim LinkImagens As String = ConfigurationManager.AppSettings("urlimagens").ToString & "Imagens/Talentos/"
        Dim oDt, oDtVencedores As Data.DataTable
        Dim Consulta As New ClsConsulta

        oDt = Consulta.ConsultaSQL("Select Distinct TalCodigo, TalTitulo, VenCategoria From TalentosNovo inner join TalentosVencedores on TalCodigo = VenTalCodigo where TalCodigo=" & TalCodigo & " and TalAtivo=1")
        If oDt.Rows.Count > 0 Then
            Page.Title = oDt.Rows(0).Item("TalTitulo")
            LitTitulo.Text = oDt.Rows(0).Item("TalTitulo")

            For i = 0 To oDt.Rows.Count - 1
                LitMenuCategoria.Text += "<div class='col col-md-3'> " & vbCrLf & "<a href='" & Link & "TalentosVencedores/" & TalCodigo & "/" & oDt.Rows(i).Item("VenCategoria") & "' class='btn-dark'>CATEGORIA<br />" & oDt.Rows(i).Item("VenCategoria") & "</a></div>" & vbCrLf
            Next

            oDtVencedores = Consulta.ConsultaSQL("Select VenCodigo, VenTitulo, VenUrlVideo, VenTexto from TalentosVencedores where VenTalCodigo = " & TalCodigo & " and VenCategoria = '" & Categoria & " ' order by VenPosicao")

            If oDtVencedores.Rows.Count > 0 Then
                Dim buffer As String = ""
                For i = 0 To oDtVencedores.Rows.Count - 1
                    buffer += "  <div class='row no-gutters mt-4'>" & vbCrLf & _
                            "<div class='col col-md-8'><h4>" & oDtVencedores.Rows(i).Item("VenTitulo") & "</h4><div class='video-container'>" & vbCrLf & _
                            "<iframe width='560' height='315' src='https://www.youtube.com/embed/" & oDtVencedores.Rows(i).Item("VenUrlVideo") & "' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>" & vbCrLf & _
                            "</div> </div>" & vbCrLf & _
                            "<div class='col col-md-4 vencedores-container'>  <div class='vencedores-texto'>" & vbCrLf & _
                            oDtVencedores.Rows(i).Item("VenTexto") & vbCrLf & _
                            "</div> </div> </div>"
                Next
                LitVencedores.Text = buffer
            Else
                LitVencedores.Text = "<h5 class='text-center' style='margin: 100px 0'>Nenhum registro para a categoria selecionada</h5>"
            End If
        Else
            LitVencedores.Text = "<div class='my-5 py-5'><h1 class='text-center'>Vencedores não disponíveis</h1></div>"
        End If

    End Sub
End Class
