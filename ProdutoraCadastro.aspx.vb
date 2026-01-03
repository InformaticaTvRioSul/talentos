
Partial Class Talentos_ClienteCadastro
    Inherits System.Web.UI.Page

    Private Sub VerificaPermissao()
        If Not IsNothing(Session("AgeResponsavel")) And Not IsDBNull(Session("AgeResponsavel")) Then
            If Session("AgeResponsavel") <> "" Then
                HidAgeCodigo.Value = Session("AgeCodigo")
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "atualiza2", "fechaFrame();", True)
            End If
        Else
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "atualiza2", "fechaFrame();", True)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            HidAgeCodigo.Value = 0
            VerificaPermissao()
        End If
    End Sub

    Public Function Salvar() As Boolean
        Dim Erro As String = ""
        Dim retorno As Boolean = True
        Try
            Dim Pro As New ClsTalentosContatos
            Pro.ProAgeCodigo = HidAgeCodigo.Value
            Pro.ProNomeFantasia = TxtNomeFantasia.Text.ToUpper
            Pro.ProRazaoSocial = TxtRazaoSocial.Text.ToUpper
            Pro.ProCNPJ = TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "")
            Pro.ProResponsavel = TxtResponsavel.Text
            Pro.ProTelefone = TxtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Pro.ProTelefone2 = TxtTelefone2.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Pro.ProEmail = TxtEmail.Text.ToUpper
            Pro.ProEmail2 = TxtEmail2.Text.ToUpper
            'Endereco
            Pro.ProEndereco = TxtEndereco.Text.ToUpper
            Pro.ProNumero = TxtNumero.Text.ToUpper
            Pro.ProBairro = TxtBairro.Text.ToUpper
            Pro.ProCidade = TxtCidade.Text.ToUpper
            Pro.ProCEP = TxtCEP.Text.Replace(".", "").Replace("-", "")
            If CboEstado.SelectedValue <> "" Then Pro.ProUF = CboEstado.SelectedValue

            If Not Pro.SalvarProdutora(Erro) Or Erro <> "" Then
                retorno = False
                Erro += "<br> Erro ao salvar dados: "
                Exit Try
            End If
        Catch ex As Exception
            Erro += ex.Message
            retorno = False
        End Try

        If Not retorno Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Ocorreu um erro ao salvar os dados, por favor tente novamente. ex" & Erro)
        End If

        Return retorno
    End Function

    Protected Sub CmdSalvar_Click(sender As Object, e As EventArgs) Handles CmdSalvar.Click
        If TxtCNPJ.Text.Length < 14 Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Digite um CNPJ válido.")
            Exit Sub
        End If
        If Salvar() Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "atualiza", "atualizaCombo();", True)
        End If
    End Sub
End Class
