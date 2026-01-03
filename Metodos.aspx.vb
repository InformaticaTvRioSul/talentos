
Imports System.Web.Services
Imports System.Xml
Partial Class RelatoriosNEW_Metodos
    Inherits System.Web.UI.Page

    <WebMethod> _
    Public Shared Function CEPList(ByVal cep As String) As ArrayList
        Dim retorno1 As New ArrayList
        cep = cep.Replace("-", "").Replace(".", "")
        Dim TipoLogradouro, Logradouro, Bairro, Cidade, UF As String
        TipoLogradouro = ""
        Logradouro = ""
        Bairro = ""
        Cidade = ""
        UF = ""
        Dim RetornoBusca As New XmlDocument
        Dim ret As Boolean = True
        RetornoBusca = New XmlDocument
        Try
            RetornoBusca.Load("http://webservice.uni5.net/web_cep.php?auth=1fb5ccca80d93235052186429dd21a94&formato=xml&cep=" & cep)
        Catch ex As Exception
            ret = False
        End Try
        Dim no As XmlNode
        Dim no4 As XmlNode
        For Each no In RetornoBusca.ChildNodes
            If no.Name = "webservicecep" Then
                For Each no4 In no.ChildNodes
                    If no4.Name = "uf" Then
                        UF = no4.InnerText().ToString.Replace(" - Distrito", "").Trim
                    ElseIf no4.Name = "cidade" Then
                        Cidade = no4.InnerText()
                    ElseIf no4.Name = "bairro" Then
                        Bairro = no4.InnerText()
                    ElseIf no4.Name = "tipo_logradouro" Then
                        TipoLogradouro = no4.InnerText()
                    ElseIf no4.Name = "logradouro" Then
                        Logradouro = no4.InnerText()
                    ElseIf no4.Name = "resultado" Then
                        If no4.InnerText() = "-1" Or no4.InnerText() = "0" Then
                            ret = False
                        End If
                    End If
                Next
            End If
        Next
        Dim Ende As EnderecoL
        Ende = New EnderecoL
        If ret Then
            Ende.Retorno = 1
            Ende.TipoLogradouro = TipoLogradouro
            Ende.Logradouro = Logradouro
            Ende.Bairro = Bairro
            Ende.Cidade = Cidade
            Ende.UF = UF
        Else
            Ende.Retorno = 0
            Ende.TipoLogradouro = ""
            Ende.Logradouro = ""
            Ende.Bairro = ""
            Ende.Cidade = ""
            Ende.UF = ""
        End If
        retorno1.Add(Ende)
        Return retorno1
    End Function


    <WebMethod> _
    Public Shared Function edicoes(ByVal pagina As Integer, ByVal tipo As Integer) As String
        Dim Link As String = ConfigurationManager.AppSettings("urlabsoluta").ToString
        Dim LinkImagens As String = ConfigurationManager.AppSettings("urlimagens").ToString & "Imagens/Talentos/"
        Dim oDt As Data.DataTable
        Dim Consulta As New ClsConsulta
        Dim buffer As String = ""
        Dim limite As String = 12
        pagina = pagina * limite


        oDt = Consulta.ConsultaSQL("Select TalCodigo, TalTitulo from TalentosNovo where TalAtivo=1 order by TalDataEvento desc limit " & pagina & "," & limite)
        If oDt.Rows.Count > 0 Then
            Dim imagem, TituloFormatado As String
            Dim i As Integer
            For i = 0 To oDt.Rows.Count - 1
                imagem = ""
                imagem = ClsConsulta.BuscarImagens("Talentos", oDt.Rows(i).Item("TalCodigo"), "thumb")

                TituloFormatado = Utils2.SubstituiCaracteresEspeciais(oDt.Rows(i).Item("TalTitulo")).Trim.Replace(" ", "-")

                buffer += "<div class='col col-sm-2 col-md-4 col-lg-3'>" & vbCrLf & "<div class='edicao'>" & vbCrLf & "<a href='" & Link & "talentos/edicao/" & oDt.Rows(i).Item("TalCodigo") & "'>" & _
                        "<h6>" & oDt.Rows(i).Item("TalTitulo") & "</h6> <div style='background-image: url(" & LinkImagens & imagem & ")'></div>" & vbCrLf & _
                        "</a>" & vbCrLf & "</div>" & vbCrLf & "</div>"
            Next
        Else
            buffer = ""
        End If
        Return buffer
    End Function


    '<WebMethod> _
    'Public Shared Function VerificaConfirmacao(ByVal CNPJ As String) As Boolean
    '    Dim confirma As New ClsConfirmacao
    '    confirma.ConCNPJ = CNPJ
    '    Return confirma.VerificaConfirmacao
    'End Function

    Public Class EnderecoL

        Private _retorno As Integer
        Private _TipoLogradouro, _Logradouro, _Bairro, _Cidade, _UF As String

        Property Retorno As Integer
            Get
                Return _retorno
            End Get
            Set(value As Integer)
                _retorno = value
            End Set
        End Property

        Property TipoLogradouro As String
            Get
                Return _TipoLogradouro
            End Get
            Set(value As String)
                _TipoLogradouro = value
            End Set
        End Property

        Property Logradouro As String
            Get
                Return _Logradouro
            End Get
            Set(value As String)
                _Logradouro = value
            End Set
        End Property
        Property Bairro As String
            Get
                Return _Bairro
            End Get
            Set(value As String)
                _Bairro = value
            End Set
        End Property
        Property Cidade As String
            Get
                Return _Cidade
            End Get
            Set(value As String)
                _Cidade = value
            End Set
        End Property

        Property UF As String
            Get
                Return _UF
            End Get
            Set(value As String)
                _UF = value
            End Set
        End Property
    End Class
End Class
