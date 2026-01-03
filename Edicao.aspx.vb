
Partial Class SiteTalentos_Edicoes
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not IsNothing(RouteData.Values("edicao")) Then
                If Not IsDBNull(RouteData.Values("edicao")) Then
                    If RouteData.Values("edicao") <> "" Then
                        BuscaDados(RouteData.Values("edicao"))
                    Else
                        Response.Redirect(Link & "talentos")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BuscaDados(ByVal codigo As Integer)
        Dim LinkImagens As String = ConfigurationManager.AppSettings("urlimagens").ToString & "Imagens/Talentos/"
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta

        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo From TalentosNovo where TalCodigo=" & codigo & " and TalAtivo=1")
        If oDt.Rows.Count > 0 Then
            Page.Title = oDt.Rows(0).Item("TalTitulo")
            LitTitulo.Text = oDt.Rows(0).Item("TalTitulo")
            'LitTexto.Text = oDt.Rows(0).Item("TalTexto")
            LitVencedores.Text = "<a class='btn btn-sm btn-outline-light border-0' href='" & Link & "TalentosVencedores/" & codigo & "/Campanha'>Ver Vencedores</a>"

            Dim possuiGaleria As Boolean = False
            oDt = ClsPrincipal.BuscarImagensGaleria("Talentos", oDt.Rows(0).Item("TalCodigo"), "")
            If oDt.Rows.Count > 0 Then
                For i = 0 To oDt.Rows.Count - 1
                    If oDt.Rows(i).Item("ImaNome").ToString.Contains(".jpg") And Not oDt.Rows(i).Item("ImaNome").ToString.Contains("thumb") Then
                        possuiGaleria = True
                        If i = 0 Then
                            LitGaleriaBadge.Text += "<li data-target='#carouselExampleIndicators' data-slide-to='0' class='active'></li>"
                            LitGaleriaImagens.Text += "<div class='carousel-item active slideHome'> <div class='slideImagem' style='background-image: url(" & LinkImagens & oDt.Rows(i).Item("ImaNome") & ");'>  </div> </div>"
                        Else
                            LitGaleriaBadge.Text += "<li data-target='#carouselExampleIndicators' data-slide-to='" & i & "'></li>"
                            LitGaleriaImagens.Text += "<div class='carousel-item slideHome'> <div class='slideImagem' style='background-image: url(" & LinkImagens & oDt.Rows(i).Item("ImaNome") & ");'> </div> </div>"

                        End If
                        ' LitFotos.Text += " <img class='mySlides' src='" & LinkAntigo & "Imagens/Planejamidia/" & oDt.Rows(i).Item("ImaNome") & "' style='width:100%'>" & vbCrLf

                    End If
                Next
            End If

            If Not possuiGaleria Then PanGaleria.Visible = False

        Else
            Response.Redirect(Link & "talentos")
        End If

    End Sub
End Class
