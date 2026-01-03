
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
            Dim Cli As New ClsTalentosContatos
            Cli.CliAgeCodigo = HidAgeCodigo.Value
            Cli.CliNomeFantasia = TxtNomeFantasia.Text.ToUpper
            Cli.CliRazaoSocial = TxtRazaoSocial.Text.ToUpper
            Cli.CliCNPJ = TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "")
            Cli.CliResponsavel = TxtResponsavel.Text
            Cli.CliTelefone = TxtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Cli.CliTelefone2 = TxtTelefone2.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Cli.CliEmail = TxtEmail.Text.ToUpper
            Cli.CliEmail2 = TxtEmail2.Text.ToUpper
            'Endereco
            Cli.CliEndereco = TxtEndereco.Text.ToUpper
            Cli.CliNumero = TxtNumero.Text.ToUpper
            Cli.CliBairro = TxtBairro.Text.ToUpper
            Cli.CliCidade = TxtCidade.Text.ToUpper
            Cli.CliCEP = TxtCEP.Text.Replace(".", "").Replace("-", "")
            If CboEstado.SelectedValue <> "" Then Cli.CliUF = CboEstado.SelectedValue

            If Not Cli.SalvarCliente(Erro) Or Erro <> "" Then
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
