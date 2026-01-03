
Partial Class SiteTalentos_AgenciaCadastro
    Inherits System.Web.UI.Page
    Public Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not IsNothing(Session("AgeResponsavel")) And Not IsDBNull(Session("AgeResponsavel")) Then
                If Session("AgeResponsavel") <> "" Then
                    Response.Redirect("TalentosInscricoes")
                End If
            End If

            Dim oDt As Data.DataTable
            Dim Consulta As New ClsConsulta
            'Busca Talento, verifica se tem talento ativo e dentro da data de inscrição
            oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo, TalDataExibi, TalDataInscricaoIni, TalDataInscricaoFim from TalentosNovo where TalAtivo=1 and now() >= TalDataExibi and TalDataEvento > now() limit 0,1")
            If oDt.Rows.Count = 1 Then
                Dim DataExibi As Date = oDt.Rows(0).Item("TalDataExibi")
                Dim DataInscricaoIni As Date = oDt.Rows(0).Item("TalDataInscricaoIni")
                Dim DataInscricaoFim As Date = oDt.Rows(0).Item("TalDataInscricaoFim")
                HidTalDataFim.Value = DataInscricaoFim

                HidTalLogo.Value = Link & "imagens/Talentos/" & ClsConsulta.BuscarImagens("Talentos", oDt.Rows(0).Item("TalCodigo"), "logo")

                If Not (Date.Now() >= DataInscricaoIni And Date.Now() <= DataInscricaoFim) Then
                    Response.Redirect("talentoslogin")
                End If
            Else
                Response.Redirect("talentoslogin")
            End If
        End If
    End Sub


    Public Function Salvar() As Boolean
        Dim Erro As String = ""
        Dim retorno As Boolean = True
        Try
            Dim Age As New ClsTalentosContatos
            'Age.AgeCodigo = HidUsuCodigo.Value
            Age.AgeNomeFantasia = TxtNomeFantasia.Text.ToUpper
            Age.AgeRazaoSocial = TxtRazaoSocial.Text.ToUpper
            Age.AgeCNPJ = TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "")
            Age.AgeResponsavel = TxtResponsavel.Text.ToUpper
            Age.AgeSenha = TxtSenha.Text
            Age.AgeTelefone = TxtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Age.AgeTelefone2 = TxtTelefone2.Text.Replace("(", "").Replace(")", "").Replace("-", "")
            Age.AgeEmail = TxtEmail.Text.ToUpper
            Age.AgeEmail2 = TxtEmail2.Text.ToUpper
            'Endereco
            Age.AgeEndereco = TxtEndereco.Text.ToUpper
            Age.AgeNumero = TxtNumero.Text.ToUpper
            Age.AgeBairro = TxtBairro.Text.ToUpper
            Age.AgeCidade = TxtCidade.Text.ToUpper
            Age.AgeCEP = TxtCEP.Text.Replace(".", "").Replace("-", "")
            If CboEstado.SelectedValue <> "" Then Age.AgeUF = CboEstado.SelectedValue

            If Not Age.SalvarAgencia(Erro) Or Erro <> "" Then
                retorno = False
                Erro += "<br> Erro ao salvar dados: "
                Exit Try
            End If
            HidAgeCodigo.Value = Age.AgeCodigo
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
        LitResposta.Text = ""
        If TxtSenha.Text <> TxtSenha2.Text Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Senha e confirmação de senha devem ser iguais.")
            Exit Sub
        End If
        If TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "").Length < 14 Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("Digite um CNPJ válido.")
            Exit Sub
        End If

        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        oDt = Consulta.ConsultaSQL("Select AgeCodigo from TalentosAgencia where AgeAtivo=1 and AgeCNPJ = '" & TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "") & "'")
        If oDt.Rows.Count > 0 Then
            LitResposta.Text = Utils2.MensagemAvisoBootstrap("CNPJ já cadastrado.")
            Exit Sub
        End If

        If Salvar() Then
            Dim Data As Date = Date.Parse(HidTalDataFim.Value)
            CriaEmailCurriculo(Server, "sistema@negocios.tvriosul.com.br", "Talentos - Novo Cadastro", "Cadastro realizado com sucesso. O prazo final para a inscrição é até às " & Format(Data, "HH") & " horas do dia " & Format(Data, "dd/MM/yyyy") & "", TxtEmail.Text)
            CriaEmailCurriculo(Server, "sistema@negocios.tvriosul.com.br", "Talentos - Novo Cadastro", "A Agência " & TxtRazaoSocial.Text.ToUpper & ", CNPJ: " & TxtCNPJ.Text & " realizou cadastro", "talentosdapublicidade@tvriosul.com.br")
            Session.Add("AgeCodigo", HidAgeCodigo.Value)
            Session.Add("AgeResponsavel", TxtResponsavel.Text)
            Session.Add("AgeCNPJ", TxtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", ""))
            Session.Add("AgeNomeFantasia", TxtNomeFantasia.Text)
            Response.Redirect(Link & "TalentosInscricoes")
        End If
    End Sub

    Public Sub CriaEmailCurriculo(ByRef ser As System.Web.HttpServerUtility, ByVal remetente As String, ByVal Assunto As String, ByVal Conteudo As String, ByVal destinatario As String)

        Dim cabecalho, rodape As String
        cabecalho = "  <div style='FONT-SIZE: small; TEXT-DECORATION: none; FONT-FAMILY: Calibri; FONT-WEIGHT: normal; COLOR: #000000; FONT-STYLE: normal; DISPLAY: inline;width:600px;'>" & _
                    "<table align='center' border='0' cellpadding='0' cellspacing='5'  style='COLOR: #000000' width='575'> <tr><td width='575'>" & _
                    "<table align='center' border='0' cellpadding='0' cellspacing='0' style='COLOR: #000000'>" & _
                    "<tr><td><img height='80' src='" & Link & "Imagens/TopoMailingSoLogo.jpg' /> </td> <td><img height='65' src='" & HidTalLogo.Value & "' /> </td></tr> </table> </td>" & _
                    "</tr> <tr><td style='FONT-SIZE: 12px; FONT-FAMILY: arial, helvetica, sans-serif; PADDING-BOTTOM: 15px; PADDING-TOP: 15px; PADDING-LEFT: 15px; PADDING-RIGHT: 15px'>"


        rodape = " </td> </tr> </table> </div>"

        Dim resp As String = ""
        Utils2.EnviarEmail(remetente, destinatario, Assunto, cabecalho & Conteudo & rodape)
        ' resp = EnviarEmail(remetente, destinatario, "TV Rio Sul - " & Assunto, cabecalho & Conteudo & rodape)
        Response.Write(resp)
    End Sub


End Class
